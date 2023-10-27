using Stump.DofusProtocol.Enums;
using Stump.Server.WorldServer.Database.Monsters;
using Stump.Server.WorldServer.Game.Accounts;
using Stump.Server.WorldServer.Game.Actors.Fight;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Characters;
using Stump.Server.WorldServer.Game.Diverse;
using Stump.Server.WorldServer.Game.Fights.Challenges;
using Stump.Server.WorldServer.Game.Fights.Results;
using Stump.Server.WorldServer.Game.Fights.Results.Data;
using Stump.Server.WorldServer.Game.Fights.Teams;
using Stump.Server.WorldServer.Game.Formulas;
using Stump.Server.WorldServer.Game.Items;
using Stump.Server.WorldServer.Game.Items.Player.Custom;
using Stump.Server.WorldServer.Game.Maps;
using Stump.Server.WorldServer.Handlers.Context;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace Stump.Server.WorldServer.Game.Fights
{
    public class FightPvM : Fight<FightMonsterTeam, FightPlayerTeam>
    {
        private bool m_ageBonusDefined;

        public FightPvM(int id, Map fightMap, FightMonsterTeam defendersTeam, FightPlayerTeam challengersTeam)
            : base(id, fightMap, defendersTeam, challengersTeam)
        {
        }

        public override void StartPlacement()
        {
            base.StartPlacement();

            m_placementTimer = Map.Area.CallDelayed(FightConfiguration.PlacementPhaseTime, StartFighting);
        }

        public override void StartFighting()
        {
            m_placementTimer.Dispose();
            if (PlayerTeam.Leader.Character.IsPartyLeader())
                ActiveIdols = PlayerTeam.Leader.Character.Party.IdolInventory.ComputeIdols(this).ToList();
            else
                ActiveIdols = PlayerTeam.Leader.Character.IdolInventory.ComputeIdols(this).ToList();

            base.StartFighting();
        }

        protected override void OnFightStarted()
        {
            base.OnFightStarted();

            if (!Map.AllowFightChallenges)
                return;

            initChallenge();

            if (Map.IsDungeon() || IsPvMArenaFight)
                initChallenge();

            void initChallenge()
            {
                var challenge = ChallengeManager.Instance.GetRandomChallenge(this);

                // no challenge found
                if (challenge == null)
                    return;

                challenge.Initialize();
                AddChallenge(challenge);
            }

            // VOTE
            if(DateTime.Now < ChallengersTeam.Leader.Character.Account.SubscriptionEndDate.AddHours(3d))
            {
                var challenge = ChallengeManager.Instance.GetChallenge(51, this);

                if (challenge == null)
                    return;

                challenge.Initialize();
                AddChallenge(challenge);
            }
        }

        protected override void OnFighterAdded(FightTeam team, FightActor actor)
        {
            base.OnFighterAdded(team, actor);

            if (!(team is FightMonsterTeam) || m_ageBonusDefined)
                return;

            if (team.Leader is MonsterFighter monsterFighter)
                AgeBonus = monsterFighter.Monster.Group.AgeBonus;

            m_ageBonusDefined = true;
        }

        public FightPlayerTeam PlayerTeam => Teams.FirstOrDefault(x => x.TeamType == TeamTypeEnum.TEAM_TYPE_PLAYER) as FightPlayerTeam;

        public FightMonsterTeam MonsterTeam => Teams.FirstOrDefault(x => x.TeamType == TeamTypeEnum.TEAM_TYPE_MONSTER) as FightMonsterTeam;

        public override FightTypeEnum FightType => FightTypeEnum.FIGHT_TYPE_PvM;

        public override bool IsPvP => false;

        public bool IsPvMArenaFight
        {
            get;
            set;
        }

        private int LootOrbs(int prospec)
        {
            var listMonster = Fighters.Where(x => x is MonsterFighter);
            if (listMonster.Count() > 8)
            {
                for (int i = 0; i < listMonster.Count() - 8; i++)
                {
                    listMonster.ToList().RemoveAt(listMonster.Count() - i - 1);
                    i++;
                }
            }
            int levelTotal = listMonster.Select(x => (int)x.Level).Sum();

            var calc = (uint)(levelTotal / 2.5 * prospec / 120f);

            if (levelTotal > 900)
                calc = (uint)(levelTotal / 4 * prospec / 120f);

            if(levelTotal > 450)
                calc = (uint)(levelTotal / 3.5 * prospec / 120f);

            if (calc <= 0)
                calc = 1;

            return (int)calc * 100;

        }

        protected override List<IFightResult> GetResults()
        {
            var results = new List<IFightResult>();
            results.AddRange(GetFightersAndLeavers().Where(entry => entry.HasResult).Select(entry => entry.GetFightResult()));

            var taxCollectors = Map.SubArea.Maps.Select(x => x.TaxCollector).Where(x => x != null && x.CanGatherLoots());
            results.AddRange(taxCollectors.Select(x => new TaxCollectorProspectingResult(x, this)));

            foreach (var team in m_teams)
            {
                IEnumerable<FightActor> droppers = team.OpposedTeam.GetAllFighters(entry => entry.IsDead() && entry.CanDrop()).ToList();
                var looters = results.Where(x => x.CanLoot(team) && !(x is TaxCollectorProspectingResult)).OrderByDescending(entry => entry.Prospecting).
                    Concat(results.OfType<TaxCollectorProspectingResult>().Where(x => x.CanLoot(team)).OrderByDescending(x => x.Prospecting)); // tax collector loots at the end
                var teamPP = team.GetAllFighters<CharacterFighter>().Sum(entry => (entry.Stats[PlayerFields.Prospecting].Total >= 100) ? 100 : entry.Stats[PlayerFields.Prospecting].Total);
                var looterx = looters.ToList();
                var kamas = Winners == team ? droppers.Sum(entry => entry.GetDroppedKamas()) * team.GetAllFighters<CharacterFighter>().Count() : 0;
                foreach (var looter in looters)
                {
                    //VIP
                    var character = World.Instance.GetCharacter(looter.Id);
                    var multiplicator = 1.0f;
                    if (character != null && World.Instance.GetCharacters(x => x.Client.IP == character.Client.IP).ToList().Exists(x => x.WorldAccount.Vip))
                    {
                        multiplicator = 1.15f;
                    }

                    //looter.Loot.Kamas = teamPP > 0 ? (int)(FightFormulas.AdjustDroppedKamas(looter, teamPP, kamas) * multiplicator) : 0;
                    if (team == Winners)
                    {
						looter.Loot.Kamas = (long)(LootOrbs(looter.Prospecting) * multiplicator);
                        foreach (var item in droppers.SelectMany(dropper => dropper.RollLoot(looter)))
                        {
                            Character charId = null;
                            if(character != null)
                            {
                                charId = World.Instance.GetCharacters(x => x.Client.IP == character.Client.IP && x.IsIpDrop).FirstOrDefault();
                            }
                            if(charId != null)
                            {
                                var newLooter = looters.FirstOrDefault(y => y.Id == charId.Id);

                                if (newLooter != null)
                                    newLooter.Loot.AddItem(item);
                                else
                                    looter.Loot.AddItem(item);
                            }
                            else
                                looter.Loot.AddItem(item);
                        }
                        //looter.Loot.AddItem(LootOrbs(looter.Prospecting));

                        #region ecailles
                        foreach (var monster in this.DefendersTeam.GetAllFighters())
                        {
                            if (monster is MonsterFighter)
                            {
                                if ((monster as MonsterFighter).Monster.Template.Id == 377 && !SoulStoneFilled.FIGHT_MAPS.Contains(PlayerTeam.Fight.Map.Id))
                                {
                                    looter.Loot.AddItem(new DroppedItem(14995, 1));
                                }
                                if ((monster as MonsterFighter).Monster.Template.Id == 375 && !SoulStoneFilled.FIGHT_MAPS.Contains(PlayerTeam.Fight.Map.Id))
                                {
                                    looter.Loot.AddItem(new DroppedItem(14994, 1));
                                }
                                if ((monster as MonsterFighter).Monster.Template.Id == 374 && !SoulStoneFilled.FIGHT_MAPS.Contains(PlayerTeam.Fight.Map.Id))
                                {
                                    looter.Loot.AddItem(new DroppedItem(14993, 1));
                                }
                            }
                        }
                        #endregion
                    }



                    if (looter is IExperienceResult)
                    {
                        var winXP = FightFormulas.CalculateWinExp(looter, team.GetAllFighters<CharacterFighter>(), droppers);
                        var biggestwave = DefendersTeam.m_wavesFighters.OrderByDescending(x => x.WaveNumber).FirstOrDefault();
                        if (biggestwave != null)
                            winXP = FightFormulas.CalculateWinExp(looter, team.GetAllFighters<CharacterFighter>(), droppers, (biggestwave.WaveNumber + 1));

                        (looter as IExperienceResult).AddEarnedExperience(team == Winners ? winXP : (long)Math.Round(winXP * 0.10));

                        if (FighterPlaying.Fight.DefendersTeam.Fighters.Any(x => x.Level >= 120))
                        {
                            if (looter is FightPlayerResult)
                            {
                                (looter as FightPlayerResult).Character.Record.WinPvm++;
                            }
                        }
                    }
                }
            }
            if (Winners == null || Draw)
            {
                return results;
            }

            else if (DefendersTeam.Fighters.Any(x => x is MonsterFighter && (x as MonsterFighter).Monster.Nani))
            {
                var NaniMonster = Map.NaniMonster;
                if (NaniMonster == null) return results;

                MonsterNaniManager.Instance.ResetSpawn(Map.NaniMonster);
                Map.NaniMonster = null;

                var characters = Winners.Fighters.OfType<CharacterFighter>();
                if (characters.Count() < 1) return results;

                if (Winners.TeamType == TeamTypeEnum.TEAM_TYPE_PLAYER) World.Instance.SendAnnounce("<b>" + string.Join(",", characters.Select(x => x.Name)) + "</b> ont vaincu : <b>" + NaniMonster.Template.Name + "</b>.");
                
            }
            return results;
        }
        #region drop perso

        /*#region bowlton
        //if (team == Winners && looter is FightPlayerResult && this.DefendersTeam.GetAllFighters().Where(x => x is MonsterFighter).ToList().Exists(x => (x as MonsterFighter).Monster.Grade.Level > 20))
        //{
        //    Actors.RolePlay.Characters.Character character = (looter as FightPlayerResult).Character;

        //    MonsterFighter boss6 = this.DefendersTeam.GetAllFighters().Where(x => x is MonsterFighter).ToList().FirstOrDefault(x => (x as MonsterFighter).Monster.Grade.Level > 20) as MonsterFighter;

        //    if (boss6 != null)
        //    {
        //        uint bp6 = ((uint)new Stump.Core.Mathematics.CryptoRandom().Next((int)1, (int)Math.Ceiling(boss6.Level / 20 * 1.61)));

        //        looter.Loot.AddItem(new Items.DroppedItem(13026, bp6));
        //    }
        //}

        //if (team == Winners && looter is FightPlayerResult && this.DefendersTeam.GetAllFighters().Where(x => x is MonsterFighter).ToList().Exists(x => (x as MonsterFighter).Monster.Grade.Level > 20))
        //{
        //    Actors.RolePlay.Characters.Character character = (looter as FightPlayerResult).Character;

        //    MonsterFighter boss9 = this.DefendersTeam.GetAllFighters().Where(x => x is MonsterFighter).ToList().FirstOrDefault(x => (x as MonsterFighter).Monster.Grade.Level > 20) as MonsterFighter;

        //    if (boss9 != null)
        //    {
        //        uint bp9 = ((uint)new Stump.Core.Mathematics.CryptoRandom().Next((int)1, (int)Math.Ceiling(boss9.Level / 20 * 1.61)));

        //        looter.Loot.AddItem(new Items.DroppedItem(13023, bp9));
        //    }
        //}

        #endregion

        #region drop jeton perfection
        if (team == Winners && looter is FightPlayerResult && this.DefendersTeam.GetAllFighters().Where(x => x is MonsterFighter).ToList().Exists(x => (x as MonsterFighter).Monster.Template.IsBoss))
        {
            Character character = (looter as FightPlayerResult).Character;
            MonsterFighter boss2 = this.DefendersTeam.GetAllFighters().Where(x => x is MonsterFighter).ToList().FirstOrDefault(x => (x as MonsterFighter).Monster.Template.IsBoss) as MonsterFighter;
            MonsterFighter boss4 = this.DefendersTeam.GetAllFighters().Where(x => x is MonsterFighter).ToList().FirstOrDefault(x => (x as MonsterFighter).Monster.Grade.Level > 99) as MonsterFighter;

            if (boss2 != null && boss4 != null)
            {
                uint bp2 = ((uint)new Stump.Core.Mathematics.CryptoRandom().Next((int)1, (int)Math.Ceiling(boss2.Level / 30 * 1.61)));
                looter.Loot.AddItem(new Items.DroppedItem(16892, bp2));
            }
        }
        #endregion

        #region capturedemuldo
        if (team == Winners && looter is FightPlayerResult && this.DefendersTeam.GetAllFighters().Where(x => x is MonsterFighter).ToList().Exists(x => (x as MonsterFighter).Monster.Grade.Level > 175))
        {
            Actors.RolePlay.Characters.Character character = (looter as FightPlayerResult).Character;
            Items.Player.BasePlayerItem Filet = character.Inventory.TryGetItem(CharacterInventoryPositionEnum.ACCESSORY_POSITION_WEAPON);


            if (Filet != null && Filet.Template.Id == 17953 && character.Fighter.HasState((int)SpellStatesEnum.APPRIVOISEMENT_10) && character.Area.Id == 5)

            {
                MonsterFighter muldo1 = this.DefendersTeam.GetAllFighters().Where(x => x is MonsterFighter).ToList().FirstOrDefault(x => (x as MonsterFighter).Monster.Template.Id == 4434) as MonsterFighter;
                MonsterFighter muldo2 = this.DefendersTeam.GetAllFighters().Where(x => x is MonsterFighter).ToList().FirstOrDefault(x => (x as MonsterFighter).Monster.Template.Id == 4435) as MonsterFighter;
                MonsterFighter muldo3 = this.DefendersTeam.GetAllFighters().Where(x => x is MonsterFighter).ToList().FirstOrDefault(x => (x as MonsterFighter).Monster.Template.Id == 4436) as MonsterFighter;
                MonsterFighter muldo4 = this.DefendersTeam.GetAllFighters().Where(x => x is MonsterFighter).ToList().FirstOrDefault(x => (x as MonsterFighter).Monster.Template.Id == 4437) as MonsterFighter;
                MonsterFighter muldo5 = this.DefendersTeam.GetAllFighters().Where(x => x is MonsterFighter).ToList().FirstOrDefault(x => (x as MonsterFighter).Monster.Template.Id == 4438) as MonsterFighter;
                character.Inventory.RemoveItem(Filet, 1);
                if (muldo1 != null)

                {
                    looter.Loot.AddItem(new Items.DroppedItem(17957, 1));
                }

                if (muldo2 != null)

                {
                    looter.Loot.AddItem(new Items.DroppedItem(17956, 1));
                }

                if (muldo3 != null)

                {
                    looter.Loot.AddItem(new Items.DroppedItem(17958, 1));
                }

                if (muldo4 != null)

                {
                    looter.Loot.AddItem(new Items.DroppedItem(17959, 1));
                }

                if (muldo5 != null)

                {
                    looter.Loot.AddItem(new Items.DroppedItem(17960, 1));
                }

            }
        }
        #endregion

        #region drop rune
        if (team == Winners && looter is FightPlayerResult && this.DefendersTeam.GetAllFighters().Where(x => x is MonsterFighter).ToList().Exists(x => (x as MonsterFighter).Monster.Grade.Level > 20))
        {
            Character character = (looter as FightPlayerResult).Character;
            MonsterFighter boss4 = this.DefendersTeam.GetAllFighters().Where(x => x is MonsterFighter).ToList().FirstOrDefault(x => (x as MonsterFighter).Monster.Grade.Level > 20) as MonsterFighter;

            if (boss4 != null)
            {
                uint bp2 = ((uint)new Stump.Core.Mathematics.CryptoRandom().Next((int)1, (int)Math.Ceiling(boss4.Level / 9 * 8.61)));
                looter.Loot.AddItem(new Items.DroppedItem(276, bp2));
            }
        }
        #endregion

        #region DROP PB
        if (team == Winners && looter is FightPlayerResult && this.DefendersTeam.GetAllFighters().Where(x => x is MonsterFighter).ToList().Exists(x => (x as MonsterFighter).Monster.Grade.Level > 175))
        {
            Actors.RolePlay.Characters.Character character = (looter as FightPlayerResult).Character;
            Items.Player.BasePlayerItem BPsearcher = character.Inventory.TryGetItem(CharacterInventoryPositionEnum.ACCESSORY_POSITION_PETS);


            if (BPsearcher != null && BPsearcher.Template.Id == 10657)

            {
                MonsterFighter boss = this.DefendersTeam.GetAllFighters().Where(x => x is MonsterFighter).ToList().FirstOrDefault(x => (x as MonsterFighter).Monster.Grade.Level > 165) as MonsterFighter;
                MonsterFighter boss3 = this.DefendersTeam.GetAllFighters().Where(x => x is MonsterFighter).ToList().FirstOrDefault(x => (x as MonsterFighter).Monster.Template.IsBoss) as MonsterFighter;


                if (boss != null && boss3 != null)

                {
                    uint bp = ((uint)new Stump.Core.Mathematics.CryptoRandom().Next((int)1, (int)Math.Ceiling(boss.Level / 20 * 1.11)));
                    looter.Loot.AddItem(new Items.DroppedItem(7919, bp));

                }
            }
        }
        #endregion

        #region DROP TUTU
        if (team == Winners && looter is FightPlayerResult && this.DefendersTeam.GetAllFighters().Where(x => x is MonsterFighter).ToList().Exists(x => (x as MonsterFighter).Monster.Template.Id == 113))
        {
            Actors.RolePlay.Characters.Character character = (looter as FightPlayerResult).Character;
            Items.Player.BasePlayerItem Tutusearcher = character.Inventory.TryGetItem(CharacterInventoryPositionEnum.ACCESSORY_POSITION_PETS);


            if (Tutusearcher != null && Tutusearcher.Template.Id == 8153)

            {
                MonsterFighter boss10 = this.DefendersTeam.GetAllFighters().Where(x => x is MonsterFighter).ToList().FirstOrDefault(x => (x as MonsterFighter).Monster.Template.Id == 113) as MonsterFighter;

                if (boss10 != null)

                {
                    looter.Loot.AddItem(new Items.DroppedItem(2267, 1));
                }
            }
        }
        #endregion

        #region DROP dofus
        if (team == Winners && looter is FightPlayerResult && this.DefendersTeam.GetAllFighters().Where(x => x is MonsterFighter).ToList().Exists(x => (x as MonsterFighter).Monster.Template.Id == 2431))
        {

            looter.Loot.AddItem(new Items.DroppedItem(6980, 1));
        }

        if (team == Winners && looter is FightPlayerResult && this.DefendersTeam.GetAllFighters().Where(x => x is MonsterFighter).ToList().Exists(x => (x as MonsterFighter).Monster.Template.Id == 854))
        {

            looter.Loot.AddItem(new Items.DroppedItem(7044, 1));
        }
    }
    #endregion*/

        #endregion drop perso

        protected override void SendGameFightJoinMessage(CharacterFighter fighter)
        {
            ContextHandler.SendGameFightJoinMessage(fighter.Character.Client, true, true, IsStarted, IsStarted ? 0 : (int)GetPlacementTimeLeft().TotalMilliseconds / 100, FightType);
        }

        protected override bool CanCancelFight() => false;

        public override TimeSpan GetPlacementTimeLeft()
        {
            var timeleft = FightConfiguration.PlacementPhaseTime - (DateTime.Now - CreationTime).TotalMilliseconds;

            if (timeleft < 0)
                timeleft = 0;

            return TimeSpan.FromMilliseconds(timeleft);
        }

        protected override void OnDisposed()
        {
            if (m_placementTimer != null)
                m_placementTimer.Dispose();

            base.OnDisposed();
        }
    }
}