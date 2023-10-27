using System;
using System.Collections.Generic;
using System.Linq;
using Stump.DofusProtocol.Enums;
using Stump.DofusProtocol.Types;
using Stump.Server.WorldServer.Game.Actors.Fight;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Characters;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Monsters;
using Stump.Server.WorldServer.Game.Fights.Results.Data;
using Stump.Server.WorldServer.Game.Fights.Teams;
using Stump.Server.WorldServer.Game.Formulas;
using Stump.Server.WorldServer.Game.Guilds;
using Stump.Server.WorldServer.Game.Items;
using Stump.Server.WorldServer.Game.Items.Player;
using Stump.Server.WorldServer.Handlers.Characters;
using Stump.Server.WorldServer.Handlers.Inventory;

namespace Stump.Server.WorldServer.Game.Fights.Results
{
    public class FightPlayerResult : FightResult<CharacterFighter>, IExperienceResult, IPvpResult
    {
        public FightPlayerResult(CharacterFighter fighter, FightOutcomeEnum outcome, FightLoot loot)
            : base(fighter, outcome, loot)
        {
        }

        public Character Character => Fighter.Character;
        protected FightTeam[] m_teams;
        public new ushort Level => Character.Level;

        public override bool CanLoot(FightTeam team) => Fighter.Team == team && (!Fighter.HasLeft() || Fighter.IsDisconnected);

        public FightExperienceData ExperienceData
        {
            get;
            private set;
        }

        public FightExperienceDataLos ExperienceDatalos
        {
            get;
            private set;
        }

        public FightPvpData PvpData
        {
            get;
            private set;
        }


        public override FightResultListEntry GetFightResultListEntry()
        {
            var additionalDatas = new List<DofusProtocol.Types.FightResultAdditionalData>();

            if (ExperienceData != null)
                additionalDatas.Add(ExperienceData.GetFightResultAdditionalData());



            if (ExperienceDatalos != null)
                additionalDatas.Remove(ExperienceDatalos.GetFightResultAdditionalData());

            if (PvpData != null)
                additionalDatas.Add(PvpData.GetFightResultAdditionalData());

            return new FightResultPlayerListEntry((ushort)Outcome, 0, Loot.GetFightLoot(), Id, Alive, (ushort)Level,
                additionalDatas.ToArray());
        }
        #region loot kamas
        public override void Apply()
        {
            Character.Inventory.AddKamas((ulong)Loot.Kamas);

            foreach (var drop in Loot.Items.Values)
            {
                // just diplay purpose
                if (drop.IgnoreGeneration)
                    continue;

                var template = ItemManager.Instance.TryGetTemplate(drop.ItemId);

                if (template.Effects.Count > 0)
                    for (var i = 0; i < drop.Amount; i++)
                    {
                        var item = ItemManager.Instance.CreatePlayerItem(Character, drop.ItemId, 1);
                        Character.Inventory.AddItem(item, false);
                    }
                else
                {
                    var item = ItemManager.Instance.CreatePlayerItem(Character, drop.ItemId, (int)drop.Amount);
                    Character.Inventory.AddItem(item, false);

                }
            }

            #endregion

            if (ExperienceData != null)
                ExperienceData.Apply();


            if (ExperienceDatalos != null)
                ExperienceDatalos.Apply();

            if (PvpData != null)
            {
                PvpData.Apply();
                Character.SaveLater();
            }

            CharacterHandler.SendCharacterStatsListMessage(Character.Client);
            InventoryHandler.SendInventoryContentMessage(Character.Client);
        }




        public void AddEarnedExperience(long experience)
        {
            if (Fighter.HasLeft() && !Fighter.IsDisconnected)
                return;
            #region vip
            if (ExperienceData == null)
                ExperienceData = new FightExperienceData(Character);

            if (Character.WorldAccount.Vip2 == 1)
            {
                var bonus = (long)(experience * 0.25);

                experience += bonus;
            }
            if (Character.WorldAccount.Vip2 == 2)
            {
                var bonus = (long)(experience * 0.50);

                experience += bonus;
            }
            if (Character.WorldAccount.Vip2 == 3)
            {
                var bonus = (long)(experience * 0.65);

                experience += bonus;
            }
            if (Character.WorldAccount.Vip2 == 4)
            {
                var bonus = (long)(experience * 0.85);

                experience += bonus;
            }
            #endregion
            #region xp dd
            if (Character.IsRiding && Character.EquippedMount.GivenExperience > 0)
            {
                var xp = (long)(experience * (Character.EquippedMount.GivenExperience * 0.01));
                var mountXp = (int)Character.EquippedMount.AdjustGivenExperience(Character, xp);

                experience -= xp;

                if (mountXp > 0)
                {
                    ExperienceData.ShowExperienceForMount = true;
                    ExperienceData.ExperienceForMount += mountXp;
                }
            }
            #endregion
            //#region perte xp défaite mob
            //if (Fight.Losers == Fighter.Team && Fighter.Fight is FightPvM && Character.Level < 200 && Character.Level > 1)
            //{

            //    Character.LevelDown(1);
            //    Character.Stats.Agility.Base = 0;
            //    Character.Stats.Strength.Base = 0;
            //    Character.Stats.Vitality.Base = 0;
            //    Character.Stats.Wisdom.Base = 0;
            //    Character.Stats.Intelligence.Base = 0;
            //    Character.Stats.Chance.Base = 0;
            //    Character.StatsPoints = (ushort)(Character.Level * 5);
            //    Character.RefreshStats();
            //    Character.Inventory.CheckItemsCriterias();
            //    Character.RefreshActor();
            //    Character.SendServerMessage("Vous venez de perdre 1 niveau suite à la perte du combat, vos caractéristiques et stats ont été remis à 0 !");
            //    Character.Spells.ForgetAllSpells();
            //    Character.Record.LosPvm++;
            //}

            //if (Fight.Losers == Fighter.Team && Fighter.Fight is FightPvM && Character.Level == 200 && Character.Record.safe200 == 0)
            //{

            //    Character.LosExperience(462952000);
            //    Character.Inventory.CheckItemsCriterias();
            //    Character.RefreshActor();
            //    Character.SendServerMessage("Vous venez de perdre la moitié de l'expérience de votre niveau 200 suite à la perte du combat!");
            //    if (Character.Experience < 7407232000)
            //    {
            //        Character.Stats.Agility.Base = 0;
            //        Character.Stats.Strength.Base = 0;
            //        Character.Stats.Vitality.Base = 0;
            //        Character.Stats.Wisdom.Base = 0;
            //        Character.Stats.Intelligence.Base = 0;
            //        Character.Stats.Chance.Base = 0;
            //        Character.StatsPoints = (ushort)(Character.Level * 5);
            //        Character.RefreshStats();
            //        Character.Spells.ForgetAllSpells();
            //    }
            //   else if (Character.Record.safe200 == 1)
            //    {
            //        Character.SendServerMessage("La gorgée de votre potion safe 200 vous permet de ne pas perdre d'expérience");
            //    }

            //}

            //#endregion
            //#region zone epique

            //if (Fight.Winners == Fighter.Team && Fighter.Fight is FightPvM && Character.SubArea.Id == 813 || Character.SubArea.Id == 814)
            //{ 
            //    Character.Record.Xpepique++;

            //Character.SendServerMessage("Vous venez de gagner un PE suite à votre victoire en zone épique"); }

            //if (Fight.Losers == Fighter.Team && Fighter.Fight is FightPvM && Character.SubArea.Id == 813 || Character.SubArea.Id == 814)

            //{
            //         var leveldown = (Character.Level);

            //        Character.LevelDown(leveldown);
            //        Character.Stats.Agility.Base = 0;
            //        Character.Stats.Strength.Base = 0;
            //        Character.Stats.Vitality.Base = 0;
            //        Character.Stats.Wisdom.Base = 0;
            //        Character.Stats.Intelligence.Base = 0;
            //        Character.Stats.Chance.Base = 0;
            //        Character.StatsPoints = (ushort)(Character.Level * 5);
            //        Character.Spells.ForgetAllSpells();
            //        Character.SpellsPoints = 0;
            //        Character.RefreshStats();
            //        Character.Inventory.CheckItemsCriterias();
            //        Character.RefreshActor();
            //        Character.Energy = 0;

            //    if (Character.Record.safeitem == 0)
            //    {
            //        foreach (var equippedItem in Character.Inventory.GetEquipedItems())
            //            Character.Inventory.RemoveItem(equippedItem);
            //    }

            // if (Character.Record.safeitem == 1)
            //    {
            //        foreach (var equippedItem in Character.Inventory.GetEquipedItems())
            //            Character.Inventory.RemoveItem(equippedItem, 3);
            //        foreach (var equippedItem in Character.Inventory.GetEquipedItems())
            //            Character.Inventory.MoveItem(equippedItem, CharacterInventoryPositionEnum.INVENTORY_POSITION_NOT_EQUIPED);
            //        Character.RefreshActor();
            //    }



            //            /* foreach (var equippedItem in Character.Inventory.GetEquipedItems())
            //                 Character.Inventory.MoveItem(equippedItem, CharacterInventoryPositionEnum.INVENTORY_POSITION_NOT_EQUIPED);
            //             Character.RefreshActor(); */


            //        }


            //#endregion

            //#region dj otoustam
            //if (Fight.Losers == Fighter.Team && Fighter.Fight is FightPvM && Character.SubArea.Id == 447 )

            //{
            //    var honordown = (Character.Honor);

            //    Character.SubHonor(honordown);              
            //    Character.RefreshActor();
            //    Character.Energy = 0;
            //    Character.OpenPopup("Vous venez de retomber grade 1 suite à votre défaite dans le donjon d'Oto ustam et Amayro");

            //       /* foreach (var equippedItem in Character.Inventory.GetEquipedItems())
            //            Character.Inventory.MoveItem(equippedItem, CharacterInventoryPositionEnum.INVENTORY_POSITION_NOT_EQUIPED);
            //        Character.RefreshActor(); */
            //    }





            //#endregion


            #region mob honneur


            if (Character.SubArea.IsAgressibleMonsters && Fighter.Fight is FightPvM)
            {
                var monstersSpawn = MonsterManager.Instance.GetMonsterIdAgressableBySubArea(Character.SubArea.Id);
                List<int> monstersIdsContains = new List<int>();
                for (int i = 0; i < Fighter.OpposedTeam.Fighters.Count; i++)
                {
                    foreach (var test in monstersSpawn)
                    {
                        /*if ((Fighter.OpposedTeam.Fighters[i] as MonsterFighter).Monster.Template.Id == test) */
                        monstersIdsContains.Add(test);
                    }
                }
                if (monstersIdsContains.Count > 0)
                {
                    var record = MonsterManager.Instance.GetMonsterAgressionByMonsterId(monstersIdsContains.FirstOrDefault());
                    var minhonor = record.HonorId * monstersIdsContains.Count / 2;
                    if (record != null)
                    {
                        if (Fight.Winners == Fighter.Team && Character.AlignmentSide != 0 && Character.PvPEnabled)
                        {
                            Character.AddHonor((ushort)(record.HonorId * monstersIdsContains.Count / 3));
                            Character.SendServerMessage("Vous avez gagné : " + (record.HonorId * monstersIdsContains.Count / 3) + " points d'honneurs !");
                        }


                        else if (Fight.Losers == Fighter.Team && Character.AlignmentSide != 0 && Character.Honor >= minhonor && Character.PvPEnabled)
                        {
                            Character.SubHonor((ushort)(record.HonorId * monstersIdsContains.Count * 2));
                            Character.SendServerMessage("Vous avez perdu : " + (record.HonorId * monstersIdsContains.Count / 2) + " points d'honneurs !");
                        }
                    }

                    ExperienceData.ShowExperienceFightDelta = true;
                    ExperienceData.ShowExperience = true;
                    ExperienceData.ShowExperienceLevelFloor = Character.Level != 200;
                    ExperienceData.ShowExperienceNextLevelFloor = Character.Level != 200;
                    ExperienceData.ExperienceFightDelta += experience;
                }
            }
            #endregion
            #region xp guilde
            if (Character.GuildMember != null && Character.GuildMember.GivenPercent > 0)
            {
                var xp = (int)(experience * (Character.GuildMember.GivenPercent * 0.01));
                var guildXp = (int)Character.Guild.AdjustGivenExperience(Character, xp);

                experience -= xp;
                guildXp = guildXp > Guild.MaxGuildXP ? Guild.MaxGuildXP : guildXp;

                if (guildXp > 0)
                {
                    ExperienceData.ShowExperienceForGuild = true;
                    ExperienceData.ExperienceForGuild += guildXp;
                }
            }
            #endregion

            //VIP
            var multiplicator = 1.0f;
            if (World.Instance.GetCharacters(x => x.Client.IP == Character.Client.IP).ToList().Exists(x => x.WorldAccount.Vip))
            {
                multiplicator = 1.3f;
            }

            ExperienceData.ShowExperienceFightDelta = true;
            ExperienceData.ShowExperience = true;
            ExperienceData.ShowExperienceLevelFloor = true;
            ExperienceData.ShowExperienceNextLevelFloor = true;
            ExperienceData.ExperienceFightDelta += (long)(experience * multiplicator);
        }



        public void SetEarnedHonor(short honor, short dishonor)
        {
            if (PvpData == null)
                PvpData = new FightPvpData(Character);

            PvpData.HonorDelta = honor;
            PvpData.DishonorDelta = dishonor;
            PvpData.Honor = Character.Honor;
            PvpData.Dishonor = Character.Dishonor;
            PvpData.Grade = (byte)Character.AlignmentGrade;
            PvpData.MinHonorForGrade = Character.LowerBoundHonor;
            PvpData.MaxHonorForGrade = Character.UpperBoundHonor;
        }
    }
}
