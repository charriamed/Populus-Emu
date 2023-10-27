using System;
using System.Collections.Generic;
using System.Linq;
using Stump.Core.Reflection;
using Stump.DofusProtocol.Enums;
using Stump.DofusProtocol.Types;
using Stump.Server.WorldServer.Database.Achievements;
using Stump.Server.WorldServer.Game.Achievements.Criterions;
using Stump.Server.WorldServer.Game.Actors.Fight;
using Stump.Server.WorldServer.Game.Actors.RolePlay;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Characters;
using Stump.Server.WorldServer.Game.Guilds;
using Stump.Server.WorldServer.Game.Items;
using Stump.Server.WorldServer.Game.Maps;
using Stump.Server.WorldServer.Handlers.Achievements;
using Stump.Server.WorldServer.Handlers.Characters;
using Stump.DofusProtocol.Enums.Custom;
using Stump.DofusProtocol.Messages;
using Stump.Server.WorldServer.Game.Items.Player;
using Stump.Server.WorldServer.Game.Fights;
using Stump.Server.WorldServer.Game.Fights.Challenges;
using Stump.Server.WorldServer.Game.Fights.Challenges.Custom;
using Stump.Server.WorldServer.Game.Quests;
using Stump.Server.WorldServer.Database.Items.Templates;

namespace Stump.Server.WorldServer.Game.Achievements
{
    public class PlayerAchievement
    {
        // FIELDS
        public delegate void AchievementCompleted(Character character, AchievementTemplate achievement);

        private readonly object m_lock = new object();
        private AchievementPointsCriterion m_achievementPointsCriterion;
        private CraftCountCriterion m_craftCountCriterion;
        private DecraftCountCriterion m_decraftCountCriterion;
        private ChallengeCountCriterion m_challengesCriterion;
        private ChallengeInDungeonCountCriterion m_challengesInDungeonCriterion;
        private QuestFinishedCountCriterion m_questFinishedCountCriterion;

        private List<AchievementTemplate> m_finishedAchievements;
        private Dictionary<string, AbstractCriterion> m_finishedCriterions;

        private LevelCriterion m_levelCriterion;
        private JobLevelCriterion m_jobLevelCriterion;
        private Dictionary<AbstractCriterion, int> m_runningCriterions;

        private List<DefaultChallenge> m_challs = new List<DefaultChallenge>();

        // CONSTRUCTORS
        public PlayerAchievement(Character character)
        {
            Owner = character;

            InitializeEvents();
        }

        // PROPERTIES
        public Character Owner { get; }

        public IReadOnlyList<AchievementTemplate> FinishedAchievements => m_finishedAchievements.AsReadOnly();

        public IReadOnlyList<PlayerAchievementReward> RewardAchievements => Owner.Record.AchievementRewards;

        // METHODS
        public void LoadAchievements()
        {
            m_finishedAchievements = new List<AchievementTemplate>();
            m_finishedCriterions = new Dictionary<string, AbstractCriterion>();
            m_runningCriterions = new Dictionary<AbstractCriterion, int>();

            foreach (var achievementId in Owner.Record.FinishedAchievements)
            {
                var achievement = Singleton<AchievementManager>.Instance.TryGetAchievement(achievementId);
                if (achievement != null)
                {
                    m_finishedAchievements.Add(achievement);
                }
            }
            foreach (var finishedAchievementObjective in Owner.Record.FinishedAchievementObjectives)
            {
                var achievementObjective =
                    Singleton<AchievementManager>.Instance.TryGetAchievementObjective(finishedAchievementObjective);
                if (achievementObjective != null && !m_finishedCriterions.ContainsValue(achievementObjective.AbstractCriterion))
                {
                    m_finishedCriterions.Add(achievementObjective.Criterion, achievementObjective.AbstractCriterion);
                }
            }
            foreach (var runningAchievementObjective in Owner.Record.RunningAchievementObjectives)
            {
                var achievementObjective =
                    Singleton<AchievementManager>.Instance.TryGetAchievementObjective(
                        (uint)runningAchievementObjective.Key);
                if (achievementObjective != null)
                {
                    m_runningCriterions.Add(achievementObjective.AbstractCriterion, runningAchievementObjective.Value);
                }
            }

            foreach (var rewardableAchievement in Owner.Record.AchievementRewards)
            {
                rewardableAchievement.Initialize(Owner);
            }

            ManageCriterions();
        }

        private void InitializeEvents()
        {
            Owner.ChangeSubArea += OnChangeSubArea;
            Owner.FightEnded += OnFightEnded;
            Owner.FightStarted += OnFightStarted;
            Owner.LevelChanged += OnLevelChanged;
            Owner.CraftItem += OnCraftItem;
            Owner.DecraftItem += OnDecraftItem;
            Owner.OnQuestFinished += OnQuestFinished;
            Owner.Inventory.ItemAdded += OnItemAdded;
        }

        private void OnItemAdded(ItemsCollection<BasePlayerItem> sender, BasePlayerItem item)
        {
            var criterion = AchievementManager.Instance.TryGetHaveItemCriterionByItemId(item.Template.Id);
            if (criterion == null)
                return;

            if (ContainsCriterion(criterion.Criterion))
                return;

            if (criterion.Eval(Owner))
                AddCriterion(criterion);
        }

        private void OnDecraftItem(ItemTemplate item, int runeQuantity)
        {
            var criterion = Singleton<AchievementManager>.Instance.IncrementableCriterion[typeof(DecraftCountCriterion)] as DecraftCountCriterion;
            while (criterion != null)
            {
                if (criterion.ItemId != item.Id || ContainsCriterion(criterion.Criterion))
                {
                    criterion = (DecraftCountCriterion)criterion.Next;
                    continue;
                }

                AddCriterion(criterion);
                criterion = (DecraftCountCriterion)criterion.Next;
            }
            ManageIncrementalCriterions(ref m_decraftCountCriterion);
        }

        private void OnQuestFinished(Character character, Quest quest)
        {
            var questCriterion = AchievementManager.Instance.TryGetQuestFinishedCriterionByQuestId(quest.Id);

            if (questCriterion == null)
                return;

            if (ContainsCriterion(questCriterion.Criterion))
                return;

            if (questCriterion.Eval(Owner))
                AddCriterion(questCriterion);

            ManageIncrementalCriterions(ref m_questFinishedCountCriterion);
        }

        public AchievementTemplate TryGetFinishedAchievement(short id)
        {
            return m_finishedAchievements.FirstOrDefault(entry => entry.Id == id);
        }

        public bool AchievementIsCompleted(uint achievementId)
        {
            if (m_finishedAchievements.FirstOrDefault(i => i.Id == achievementId) != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public IEnumerable<Achievement> TryGetFinishedAchievements(AchievementCategoryRecord category)
        {
            var result = from template in category.Achievements
                         where m_finishedAchievements.Contains(template)
                         select template.GetAchievement(this);

            return result;
        }

        

        public void CompleteAchievement(AchievementTemplate achievement)
        {
            lock (m_lock)
            {
                var reward = Owner.Record.AchievementRewards.FirstOrDefault(entry => entry == (byte)Owner.Level);
                if (reward == null)
                {
                    reward = new PlayerAchievementReward(Owner, achievement);

                    if (!Owner.Record.AchievementRewards.Contains(reward))
                        Owner.Record.AchievementRewards.Add(reward);
                }
                else
                {
                    if (!reward.Contains(achievement))
                        reward.AddRewardableAchievement(achievement);
                }

                if (!Owner.Record.FinishedAchievements.Contains((ushort)achievement.Id))
                    Owner.Record.FinishedAchievements.Add((ushort)achievement.Id);

                Owner.Record.AchievementPoints += (int)achievement.Points;

                if (!Owner.Record.UnAcceptedAchievements.Contains((ushort)achievement.Id))
                    Owner.Record.UnAcceptedAchievements.Add((ushort)achievement.Id);

                if (!m_finishedAchievements.Contains(achievement))
                    m_finishedAchievements.Add(achievement);
            }

            AchievementHandler.SendAchievementFinishedMessage(Owner.Client, (ushort)achievement.Id, (ulong)Owner.Id, Owner.Level);

            OnAchievementCompleted(achievement);
        }

        public void UnLockUnCompletedAchievement(AchievementTemplate achievement)
        {
            lock (m_lock)
            {
                var reward = Owner.Record.AchievementRewards.FirstOrDefault(entry => entry == (byte)Owner.Level);
                if (reward == null)
                {
                    reward = new PlayerAchievementReward(Owner, achievement);

                    if(!Owner.Record.AchievementRewards.Contains(reward))
                        Owner.Record.AchievementRewards.Add(reward);
                }
                else
                {
                    if(!reward.Contains(achievement))
                        reward.AddRewardableAchievement(achievement);
                }

                if(!Owner.Record.FinishedAchievements.Contains((ushort)achievement.Id))
                    Owner.Record.FinishedAchievements.Add((ushort)achievement.Id);

                if(!Owner.Record.UnAcceptedAchievements.Contains((ushort)achievement.Id))
                    Owner.Record.UnAcceptedAchievements.Add((ushort)achievement.Id);

                if(!m_finishedAchievements.Contains(achievement))
                    m_finishedAchievements.Add(achievement);
            }

            AchievementHandler.SendAchievementFinishedMessage(Owner.Client, (ushort)achievement.Id, (ulong)Owner.Id, Owner.Level);
        }

        public List<AchievementAchievedRewardable> GetRewardableAchievements()
        {
            var achievements = new List<AchievementAchievedRewardable>();
            foreach (var item in RewardAchievements)
            {
                achievements.AddRange(item.GetRewardableAchievements());
            }

            return achievements;
        }

        public int GetRunningCriterion(AbstractCriterion criterion)
        {
            return m_runningCriterions.ContainsKey(criterion) ? m_runningCriterions[criterion] : 0;
        }

        public bool RewardAchievement(AchievementTemplate achievement, out int experience, out int guildExperience)
        {
            bool result;
            PlayerAchievementReward reward = null;
            experience = 0;
            guildExperience = 0;

            if (achievement != null)
            {
                lock (m_lock)
                {
                    foreach (var item in Owner.Record.AchievementRewards)
                    {
                        if (item.Contains(achievement))
                        {
                            reward = item;
                            break;
                        }
                    }
                }

                result = reward != null && RewardAchievement(achievement, reward, out experience, out guildExperience);
            }
            else
            {
                result = false;
            }

            return result;
        }

        public bool RewardAchievement(AchievementTemplate achievement, PlayerAchievementReward owner, out int experience,
            out int guildExperience)
        {
            experience = 0;
            guildExperience = 0;
            if (!owner.Remove(achievement))
            {
                return false;
            }

            experience = achievement.GetExperienceReward(Owner.Client);
            if (experience > 0)
            {
                if (Owner.GuildMember != null && Owner.GuildMember.GivenPercent > 0)
                {
                    var guildXP = (int)(experience * (Owner.GuildMember.GivenPercent * 0.01));
                    var adjustedGuildExperience = (int)Owner.Guild.AdjustGivenExperience(Owner, guildXP);
                    adjustedGuildExperience = Math.Min(Guild.MaxGuildXP, adjustedGuildExperience);

                    experience = (int)(experience * (100.0 - Owner.GuildMember.GivenPercent) * 0.01);
                    if (adjustedGuildExperience > 0)
                    {
                        guildExperience = adjustedGuildExperience;
                    }
                }
            }
            if (experience < 0)
            {
                experience = 0;
            }

            var kamas = (ulong)achievement.GetKamasReward(Owner.Client);
            if (kamas > 0)
            {
                Owner.Inventory.AddKamas(kamas);
                Owner.Client.Character.SendServerMessage("Vous avez gagné "+ kamas +" kamas.");
            }

            foreach (var item in achievement.Rewards)
            {
                for (var i = 0; i < item.ItemsReward.Length; i++)
                {
                    var id = item.ItemsReward[i];
                    var quantity = item.ItemsQuantityReward[i];

                    var itemTemplate = Singleton<ItemManager>.Instance.TryGetTemplate((int)id);
                    if (itemTemplate != null)
                    {
                        Owner.Inventory.AddItem(itemTemplate, (int)quantity);
                    }
                }

                if(item.EmotesReward != null)
                {
                    foreach (var emoteId in item.EmotesReward)
                    {
                        Owner.Record.EmotesCSV = Owner.Record.EmotesCSV + "," + emoteId; //TODO Test
                        Owner.Client.Send(new EmoteAddMessage((byte)emoteId));
                    }
                }

                if(item.SpellsReward != null)
                {
                    foreach (var spellId in item.SpellsReward.Where(spellId => !Owner.Spells.HasSpell((int)spellId)))
                    {
                        Owner.Spells.LearnSpell((int)spellId);
                    }
                }                

                if(item.TitlesReward!= null)
                {
                    foreach (var titleId in item.TitlesReward.Where(titleId => !Owner.HasTitle((short)titleId)))
                    {
                        Owner.AddTitle((short)titleId);
                    }
                }                

                if(item.OrnamentsReward != null)
                {
                    foreach (var ornamentId in item.OrnamentsReward.Where(ornamentId => !Owner.HasOrnament((short)ornamentId)))
                    {
                        Owner.AddOrnament((short)ornamentId);
                    }
                }
            }
            // TODO : items

            if (!owner.Any())
            {
                Owner.Record.AchievementRewards.Remove(owner);
            }

            if (Owner.Record.UnAcceptedAchievements.Contains((ushort)achievement.Id))
                Owner.Record.UnAcceptedAchievements.Remove((ushort)achievement.Id);

            Owner.RefreshStats();

            return true;
        }

        public void RewardAllAchievements(Action<AchievementTemplate, bool> action)
        {
            var totalExperience = 0;
            var totalGuildExperience = 0;
            lock (m_lock)
            {
                while (RewardAchievements.Count > 0)
                {
                    var achievementReward = RewardAchievements[0];
                    while (achievementReward.RewardAchievements.Count > 0)
                    {
                        var achievement = achievementReward.RewardAchievements[0];

                        int guildExperience;
                        int experience;
                        action(achievement,
                            RewardAchievement(achievement, achievementReward, out experience, out guildExperience));
                        totalExperience += experience;
                        totalGuildExperience += guildExperience;
                    }
                }
            }

            if (totalExperience > 0)
            {
                Owner.AddExperience(totalExperience);
            }
            else
            {
                totalExperience = 0;
            }

            if (Owner.GuildMember != null && totalGuildExperience > 0)
            {
                Owner.GuildMember.AddXP(totalGuildExperience);
            }
            else
            {
                totalGuildExperience = 0;
            }

            CharacterHandler.SendCharacterExperienceGainMessage(Owner.Client, (ulong)totalExperience, 0L,
                (ulong)totalGuildExperience, 0L);
        }

        #region Handlers

        private void OnFightEnded(Character character, CharacterFighter fighter)
        {
            if (fighter.Fight.FightType == FightTypeEnum.FIGHT_TYPE_PvM)
            {
                if (fighter.HasWin())
                {
                    if (fighter.Fight.Map.IsDungeon())
                    {
                        // KILL BOSS
                        foreach (var criterion in fighter.Fight.DefendersTeam.GetAllFighters<MonsterFighter>().Select(item => Singleton<AchievementManager>.Instance.TryGetCriterionByBoss(item.Monster.Template)).Where(criterion => criterion != null))
                        {
                            if (m_runningCriterions.ContainsKey(criterion))
                                m_runningCriterions[criterion]++;
                            else
                                m_runningCriterions.Add(criterion, 1);

                            if (ContainsCriterion(criterion.Criterion)) continue;
                            if (criterion.Eval(Owner))
                                AddCriterion(criterion);
                        }

                        // IDOLS
                        foreach (var criterion in fighter.Fight.DefendersTeam.GetAllFighters<MonsterFighter>().Select(item => Singleton<AchievementManager>.Instance.TryGetIdolsScoreCriterionByMonster(item.Monster.Template)).Where(criterion => criterion != null))
                        {
                            if (ContainsCriterion(criterion.Criterion)) continue;
                            if (criterion.Eval(Owner))
                                AddCriterion(criterion);
                        }

                        // KILL BOSS WITH CHALL
                        foreach (var challenges in fighter.Fight.DefendersTeam.GetAllFighters<MonsterFighter>(x => x.Monster.Template.IsBoss).Select(item => Singleton<AchievementManager>.Instance.TryGetCriterionsByBossWithChallenge(item.Monster.Template)).Where(criterion => criterion != null))
                        {
                            foreach (var challenge in challenges)
                            {
                                if (m_challs.FirstOrDefault(x => x.Id == challenge.ChallIdentifier) == null)
                                    continue;

                                if (m_challs.FirstOrDefault(x => x.Id == challenge.ChallIdentifier).Status == ChallengeStatusEnum.SUCCESS)
                                {
                                    if (m_runningCriterions.ContainsKey(challenge))
                                        m_runningCriterions[challenge]++;
                                    else
                                        m_runningCriterions.Add(challenge, 1);

                                    if (ContainsCriterion(challenge.Criterion)) continue;
                                    if (challenge.Eval(Owner))
                                        AddCriterion(challenge);
                                }
                            }
                        }
                    }

                    //KILL MONSTER WITH CHALL
                    if(fighter.Fight.Challenges.FirstOrDefault(x => x.Status == ChallengeStatusEnum.SUCCESS) != null)
                    {
                        foreach (var criterion in fighter.Fight.DefendersTeam.GetAllFighters<MonsterFighter>().Select(item => Singleton<AchievementManager>.Instance.TryGetCriterionByMonster(item.Monster.Template)).Where(criterion => criterion != null))
                        {
                            if (m_runningCriterions.ContainsKey(criterion))
                                m_runningCriterions[criterion]++;
                            else
                                m_runningCriterions.Add(criterion, 1);

                            if (ContainsCriterion(criterion.Criterion)) continue;
                            if (criterion.Eval(Owner))
                                AddCriterion(criterion);
                        }
                    }

                    //CHALL COUNT
                    var teamLevel = 0;
                    foreach (var fighterr in fighter.Team.Fighters)
                    {
                        if (fighterr.Level > 200)
                            teamLevel += 200;
                        else
                            teamLevel += fighterr.Level;
                    }

                    var opposedTeamLevel = 0;
                    foreach (var fighterr in fighter.OpposedTeam.Fighters)
                        opposedTeamLevel += fighterr.Level;

                    if (teamLevel <= opposedTeamLevel)
                    {
                        var challCount = fighter.Fight.Challenges.Where(x => x.Status == DofusProtocol.Enums.Custom.ChallengeStatusEnum.SUCCESS).Count();
                        if (fighter.Fight.Map.IsDungeon())
                        {
                            character.ChallengesInDungeonCount += challCount;

                            if (character.ChallengesInDungeonCount > 10000)
                                character.ChallengesInDungeonCount = 10000;
                        }
                        else
                        {
                            character.ChallengesCount += challCount;

                            if (character.ChallengesCount > 10000)
                                character.ChallengesCount = 10000;
                        }
                    }

                    ManageIncrementalCriterions(ref m_challengesCriterion);
                    ManageIncrementalCriterions(ref m_challengesInDungeonCriterion);
                }
            }

            m_challs.Clear();
        }

        private void OnFightStarted(Character character, CharacterFighter fighter)
        {
            //Challs
            if (fighter.Fight.Map.IsDungeon())
            {
                foreach (var challenges in fighter.Fight.DefendersTeam.GetAllFighters<MonsterFighter>(x => x.Monster.Template.IsBoss).Select(item => Singleton<AchievementManager>.Instance.TryGetCriterionsByBossWithChallenge(item.Monster.Template)).Where(criterion => criterion != null))
                {
                    foreach(var challenge in challenges)
                    {
                        var chall = ChallengeManager.Instance.GetChallenge(challenge.ChallIdentifier, fighter.Fight);

                        if (chall == null)
                            continue;

                        m_challs.Add(chall);

                        chall.Initialize();
                        
                        if (chall is VolunteerChallenge || chall is ReprieveChallenge)
                            chall.Target = fighter.Fight.DefendersTeam.GetAllFighters<MonsterFighter>(x => x.Monster.Template.Id == challenge.GetMonsterIdByChallId(challenge.ChallIdentifier)).FirstOrDefault();
                    }
                }
            }
        }

        private void OnChangeSubArea(RolePlayActor actor, SubArea subArea)
        {
            var achievement = subArea.Record.Achievement;
            lock (m_lock)
            {
                if (achievement != null)
                {
                    if (!m_finishedAchievements.Contains(achievement))
                    {
                        CompleteAchievement(achievement);
                    }
                }
            }
            //if (actor is Character && (actor as Character).LastMap != null && (actor as Character).LastMap.SubArea != subArea)
            //{
            //   if(World.Instance.GetCharacters(x => x.SubArea == actor.LastMap.SubArea).Where(x=> x.Guild.Alliance == (actor as Character).Guild.Alliance).Count() == 0)
            //   {
            //       subArea.AlliancesInZone.Remove((actor as Character).Guild.Alliance);
            //   }
            //}
            //if (actor is Character && !subArea.AlliancesInZone.Contains((actor as Character).Guild.Alliance))
            //{
            //   subArea.AlliancesInZone.Add((actor as Character).Guild.Alliance);
            //}
        }

        private void OnLevelChanged(Character character, ushort currentLevel, int difference)
        {
            if (difference > 0)
            {
                ManageIncrementalCriterions(ref m_levelCriterion);
            }
        }
        
        private void OnCraftItem(BasePlayerItem item, int quantity)
        {
            ManageIncrementalCriterions(ref m_craftCountCriterion);
            ManageIncrementalCriterions(ref m_jobLevelCriterion);
        }

        private void OnAchievementCompleted(AchievementTemplate achievement)
        {
            var achievementCriterion = Singleton<AchievementManager>.Instance.TryGetAchievementCriterion(achievement);
            if (achievementCriterion != null)
            {
                AddCriterion(achievementCriterion);
            }
        }

        private void ManageCriterions()
        {
            m_levelCriterion = Singleton<AchievementManager>.Instance.MinLevelCriterion;
            m_achievementPointsCriterion = Singleton<AchievementManager>.Instance.MinAchievementPointsCriterion;
            m_craftCountCriterion = Singleton<AchievementManager>.Instance.CraftCountCriterion;
            m_decraftCountCriterion = Singleton<AchievementManager>.Instance.DecraftCountCriterion;
            m_challengesCriterion = Singleton<AchievementManager>.Instance.ChallengeCountCriterion;
            m_challengesInDungeonCriterion = Singleton<AchievementManager>.Instance.ChallengeInDungeonCountCriterion;
            m_jobLevelCriterion = Singleton<AchievementManager>.Instance.MinJobLevelCriterion;
            m_questFinishedCountCriterion = Singleton<AchievementManager>.Instance.QuestFinishedCountCriterion;

            ManageIncrementalCriterions(ref m_levelCriterion);
            ManageIncrementalCriterions(ref m_challengesCriterion);
            ManageIncrementalCriterions(ref m_challengesInDungeonCriterion);
            ManageIncrementalCriterions(ref m_achievementPointsCriterion);
            ManageIncrementalCriterions(ref m_craftCountCriterion);
            ManageIncrementalCriterions(ref m_decraftCountCriterion);
            ManageIncrementalCriterions(ref m_jobLevelCriterion);
            ManageIncrementalCriterions(ref m_questFinishedCountCriterion);

            //QUEST
            foreach(var quest in Owner.Quests.Where(x => x.Finished))
            {
                OnQuestFinished(Owner, quest);
            }
        }

        private void ManageIncrementalCriterions<T>(ref T criterion)
            where T : AbstractCriterion
        {
            criterion = Singleton<AchievementManager>.Instance.IncrementableCriterion[typeof(T)] as T;
            while (criterion != null)
            {
                if (criterion.Eval(Owner))
                {
                    if (!ContainsCriterion(criterion.Criterion))
                    {
                        AddCriterion(criterion);
                    }
                }

                criterion = (T)criterion.Next;
            }
        }

        #endregion

        #region Criterions manager

        public void AddCriterion(AbstractCriterion criterion)
        {
            if (!m_finishedCriterions.ContainsKey(criterion.Criterion)) // esto es igual a decir no exite
            {
                m_finishedCriterions.Add(criterion.Criterion, criterion);

                foreach (var item in criterion.UsefullFor.Where(item => item.Objectives.All(entry => m_finishedCriterions.ContainsKey(entry.Criterion))))
                {
                    CompleteAchievement(item);
                }

                if(!string.IsNullOrEmpty(Convert.ToString(criterion.DefaultObjective.Id)) && !Owner.Record.FinishedAchievementObjectives.Contains(criterion.DefaultObjective.Id))
                    Owner.Record.FinishedAchievementObjectives.Add(criterion.DefaultObjective.Id);
            }
        }

        public bool ContainsCriterion(string criterion)
        {
            return m_finishedCriterions.ContainsKey(criterion);
        }

        #endregion
    }
}