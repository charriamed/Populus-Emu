using MongoDB.Bson;
using Stump.DofusProtocol.Enums;
using Stump.Server.BaseServer.Logging;
using Stump.Server.WorldServer.Database.Items.Templates;
using Stump.Server.WorldServer.Game.Actors.Fight;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Characters;
using Stump.Server.WorldServer.Game.Effects.Instances;
using Stump.Server.WorldServer.Game.Fights.Results;
using Stump.Server.WorldServer.Game.Fights.Teams;
using Stump.Server.WorldServer.Game.Idols;
using Stump.Server.WorldServer.Game.Items;
using Stump.Server.WorldServer.Game.Maps;
using Stump.Server.WorldServer.Game.PvP;
using Stump.Server.WorldServer.Handlers.Context;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;

namespace Stump.Server.WorldServer.Game.Fights
{
    public class FightAgression : Fight<FightPlayerTeam, FightPlayerTeam>
    {
        public bool battleField;
        public FightAgression(int id, Map fightMap, FightPlayerTeam defendersTeam, FightPlayerTeam challengersTeam, bool isBattlefield = false)
            : base(id, fightMap, defendersTeam, challengersTeam)
        {
            m_placementTimer = Map.Area.CallDelayed(FightConfiguration.PlacementPhaseTime, StartFighting);
            this.battleField = isBattlefield;
        }

        public override void StartPlacement()
        {
            base.StartPlacement();
            m_placementTimer = Map.Area.CallDelayed(FightConfiguration.PlacementPhaseTime, StartFighting);
        }

        public override void StartFighting()
        {
            m_placementTimer.Dispose();

            base.StartFighting();
        }

        public override FightTypeEnum FightType
        {
            get { return FightTypeEnum.FIGHT_TYPE_AGRESSION; }
        }

        public override bool IsPvP
        {
            get { return true; }
        }

        public override bool IsMultiAccountRestricted
        {
            get { return true; }
        }

        protected override void ApplyResults()
        {
            foreach (var fightResult in Results)
            {
                fightResult.Apply();
            }
        }

        protected override List<IFightResult> GetResults()
        {
            var results = GetFightersAndLeavers().Where(entry => entry.HasResult).
                Select(fighter => fighter.GetFightResult()).ToList();

            var resultLeavers = GethDcFighters()?.Where(entry => entry.HasResult).
                Select(fighter => fighter.GetFightResult()).ToList();

            GetInformations();

            if (GetFightDuration().TotalMinutes < 2)
                SendFightInformationsOnFinish(false);
            else
                SendFightInformationsOnFinish(true);
            if (resultLeavers != null && resultLeavers.Count() >= 1)
                results.Add(resultLeavers.FirstOrDefault());

            foreach (var playerResult in results.OfType<FightPlayerResult>())
            {
                short m_honor = CalculateEarnedHonor(playerResult.Fighter);
                if (m_honor > 0 && m_honor < 120) { m_honor = 120; }

                if (GetFightDuration().TotalMinutes < 2)
                {

                    if (m_honor > 0)
                    {
                        playerResult.SetEarnedHonor((short)(m_honor / 2),
                                CalculateEarnedDishonor(playerResult.Fighter));
                    }
                    else
                    {
                        playerResult.SetEarnedHonor(m_honor,
                                CalculateEarnedDishonor(playerResult.Fighter));
                    }
                }

                else
                {
                    playerResult.SetEarnedHonor(m_honor,
                        CalculateEarnedDishonor(playerResult.Fighter));
                }

                if (this.battleField)
                {
                    CalculateEarnedRank(playerResult);
                  
                    
                    /*CalculateEarnedKolizetons(playerResult);
                    CalculateEarnedExp(playerResult);*/
                }
                else
                    CalculateEarnedKamasPevetons(playerResult);

                /*string log_insertAgression = "INSERT INTO logs_agress (FightId, Duration, Team, isWinner, " +
                                                                        "AccountId, AccountName, CharacterId, " +
                                                                        "CharacterName, IPAddress, ClientKey, Date) " +
                                                                        "VALUES" + $"('{UniqueId.ToString()}', '{GetFightDuration().TotalSeconds}', '{Enum.GetName(typeof(TeamEnum), playerResult.Fighter.Team.Id)}'," +
                                                                                   $"  {Winners.Id == playerResult.Fighter.Team.Id}, {playerResult.Character.Account.Id}, '{playerResult.Character.Account.Login}', {playerResult.Character.Id}," +
                                                                                   $" '{playerResult.Character.Name}', '{playerResult.Character.Client.IP}', '{playerResult.Character.Account.LastHardwareId}', '{DateTime.Now.ToString(CultureInfo.InvariantCulture)}');";

                Looger.Instance.Insert(log_insertAgression);*/
            }

            return results;
        }

        private int[,] earnedEXPMatrix = new int[,] {
                { 350000, 550000, 950000 },
                { 950000, 1300000, 1650000 },
                { 1650000, 2000000, 2900000 },
            };

        private void CalculateEarnedRank(FightPlayerResult result)
        {


            if (Winners == result.Fighter.Team)

            {
                double fightTime = this.GetFightDuration().TotalSeconds;
                bool leaver = false;
                CharacterFighter loser = null;
                if (Losers.Fighters.Count > 0)
                    loser = Losers.Fighters[0] as CharacterFighter;
                else if (Leavers.Count > 0)
                {
                    loser = Leavers[0] as CharacterFighter;
                    leaver = true;
                }

                int bonusFight = 0;
                int winPoints = 0;

                if (loser != null)
                    winPoints += loser.Character.GetCharacterRankBonus();

                if (fightTime > 200)
                    bonusFight += 2;
                if (fightTime > 300) // 5 minutes
                    bonusFight += 5;
                else if (fightTime > 600) // 10 minutes
                    bonusFight += 10;
                else if (fightTime > 900) // 15 minutes
                    bonusFight += 15;

                winPoints += bonusFight;
                if (GetFightDuration().TotalMinutes < 2)
                    winPoints = 10;

                if (leaver && GetFightDuration().TotalMinutes < 2)
                {
                    winPoints = 5;
                    result.Character.SendServerMessage("Votre adversaire a abandonné trop vite. Vous ne gagnerez donc malheureusement que <b>" + winPoints + "</b> CP durant ce combat.", Color.Chartreuse);
                }
                else
                {
                    {

                        foreach (var player in Losers.Fighters)

                        {
                            if ((player is CharacterFighter))

                            {
                                var playe = (player as CharacterFighter).Character;

                                if (playe.Inventory.GetEquipedItems().Where(x => x.Position != CharacterInventoryPositionEnum.INVENTORY_POSITION_MOUNT).Count() == 1)

                                { }
                                var los = playe.Inventory.GetEquipedItems().Where(x => x.Position != CharacterInventoryPositionEnum.INVENTORY_POSITION_MOUNT).ToArray()[new Random().Next(0, playe.Inventory.GetEquipedItems().Count())];

                                if (los != null)
                                {

                                    (FighterPlaying as CharacterFighter).Character.Fighter.Loot.AddItem(los.Template.Id);
                                    (FighterPlaying as CharacterFighter).Character.RefreshActor();
                                    playe.Inventory.RemoveItem(los);
                                }
                            }
                        }
                    }

                    if (result.Character.CharacterRankId == 1)
                    {

                        result.Loot.AddItem(16891, 1); // Calyston
                    }
                    else if (result.Character.CharacterRankId == 2)
                    {
                        result.Loot.AddItem(16891, 3);
                    }
                    else if (result.Character.CharacterRankId == 3)
                    {
                        result.Loot.AddItem(16891, 6);
                    }
                    else if (result.Character.CharacterRankId == 4)
                    {
                        result.Loot.AddItem(16891, 9);
                    }
                    else if (result.Character.CharacterRankId == 5)
                    {
                        result.Loot.AddItem(16891, 12);
                    }
                    else if (result.Character.CharacterRankId == 6)
                    {
                        result.Loot.AddItem(16891, 15);
                    }

                    result.Character.SendServerMessage("Vous avez gagné <b>" + winPoints + "</b> CP durant ce combat.", Color.Chartreuse);
                }
                result.Character.CharacterRankWin += 1;
                result.Character.CharacterRankExp += winPoints;
            }
            else
            {
                CharacterFighter winner = Winners.Fighters[0] as CharacterFighter;
                double fightTime = this.GetFightDuration().TotalSeconds;
                if (winner != null)
                {
                    int lostPoint = result.Character.GetCharacterRankBonus();
                    if (winner.Character.CharacterRankId < result.Character.CharacterRankId)
                        lostPoint += (int)(winner.Character.GetCharacterRankBonus());

                    if (fightTime > 200)
                        lostPoint += 2;
                    if (fightTime > 300) // 5 minutes
                        lostPoint += 5;
                    else if (fightTime > 600) // 10 minutes
                        lostPoint += 10;
                    else if (fightTime > 900) // 15 minutes
                        lostPoint += 15;
                    result.Character.CharacterRankLose += 1;
                    result.Character.SendServerMessage("Vous avez perdu <b>" + lostPoint + "</b> CP durant ce combat.", Color.Chartreuse);
                    result.Character.CharacterRankExp -= lostPoint;
                }
            }
        }

        private void CalculateEarnedExp(FightPlayerResult result)
        {
            if (Winners == result.Fighter.Team)
            {
                double fightTime = this.GetFightDuration().TotalSeconds;
                int characterLevel = result.Character.Level;

                #region Level 51 ~ 100
                /* Level 51 ~ 100 */
                if (characterLevel >= 51 && characterLevel <= 100)
                {
                    if (fightTime <= 300)
                    {
                        result.AddEarnedExperience(earnedEXPMatrix[0, 0]);
                    }

                    else if (fightTime > 301 && fightTime <= 600)
                    {
                        result.AddEarnedExperience(earnedEXPMatrix[0, 1]);
                    }

                    else if (fightTime >= 601)
                    {
                        result.AddEarnedExperience(earnedEXPMatrix[0, 2]);
                    }

                }
                #endregion

                #region Level 101 ~ 150
                /* Level 51 ~ 100 */
                else if (characterLevel >= 101 && characterLevel <= 150)
                {
                    if (fightTime <= 300)
                    {
                        result.AddEarnedExperience(earnedEXPMatrix[1, 0]);
                    }

                    else if (fightTime > 301 && fightTime <= 600)
                    {
                        result.AddEarnedExperience(earnedEXPMatrix[1, 1]);
                    }
                    else if (fightTime >= 601)
                    {
                        result.AddEarnedExperience(earnedEXPMatrix[1, 2]);
                    }

                }
                #endregion

                #region Level 151 ~ 200
                /* Level 151 ~ 200 */
                else if (characterLevel >= 151 && characterLevel <= 200)
                {
                    if (fightTime <= 300)
                    {
                        result.AddEarnedExperience(earnedEXPMatrix[2, 0]);
                    }

                    else if (fightTime > 301 && fightTime <= 600)
                    {
                        result.AddEarnedExperience(earnedEXPMatrix[2, 1]);
                    }

                    else if (fightTime >= 601)
                    {
                        result.AddEarnedExperience(earnedEXPMatrix[2, 2]);
                    }

                }
                #endregion

            }
        }

        private void CalculateEarnedKolizetons(FightPlayerResult result)
        {

            var target = result.Fighter.OpposedTeam.GetAllFightersWithLeavers<CharacterFighter>().First();
            if (target != null)
            {
                double fightTime = this.GetFightDuration().TotalSeconds;

                /* Character Winner */
                if (Winners == result.Fighter.Team)
                {
                    int characterLevel = result.Character.Level;
                    if (fightTime <= 300)
                    {
                       result.Loot.Kamas += (long)earnedKamasMatrix[0, 0];
                       result.Loot.AddItem((int)ItemIdEnum.KOLIZETON_12736, 1);
                    }

                    result.Loot.AddItem((int)ItemIdEnum.PARCHEMIN_DES_FLAQUEUX_10670, 1);

                    result.Character.SendServerMessage("Vous avez gagné le combat, félicitations !");
                    result.Character.Teleport(result.Character.MapBattleField, result.Character.CellBattleField);
                }
            }
        }

        protected override void SendGameFightJoinMessage(CharacterFighter fighter)
        {
            ContextHandler.SendGameFightJoinMessage(fighter.Character.Client, CanCancelFight(), true, IsStarted, (int)GetPlacementTimeLeft().TotalMilliseconds / 100, FightType);
        }

        private void SendFightInformationsOnFinish(bool real)
        {
            var m_winnerscount = Winners.GetAllFighters().OfType<CharacterFighter>().Count();
            var m_loserscount = Losers.GetAllFighters().OfType<CharacterFighter>().Count();
            var m_leaverscount = Leavers.Count;

            if (!real)
            {
                foreach (var fighter in this.GetAllCharacters())
                    fighter.SendServerMessage("En raison de la durée du combat trop court, certaines statistiques ont changé.", System.Drawing.Color.DarkOrange);
            }

            if (m_winnerscount == 1 && m_loserscount == 1)
            {
                if (battleField)
                {
                    World.Instance.SendAnnounce($"Le joueur <b>{(Winners.Fighters[0] as CharacterFighter).Character.Name}</b> a battu le joueur <b>{(Losers.Fighters[0] as CharacterFighter).Character.Name}</b> en DeadMatch." +
                        $" La bataille a duré <b>{this.GetFightDuration().Minutes}</b> minutes et <b>{this.GetFightDuration().Seconds}</b> secondes.", Color.YellowGreen);
                }
                else
                {
                    World.Instance.SendAnnounce($"Le joueur <b>{(Winners.Fighters[0] as CharacterFighter).Character.Name}</b> a battu le joueur <b>{(Losers.Fighters[0] as CharacterFighter).Character.Name}</b> en combat PvP." +
                        $" L'agression a duré <b>{this.GetFightDuration().Minutes}</b> minutes et <b>{this.GetFightDuration().Seconds}</b> secondes.", Color.YellowGreen);
                }
            }

            else if ((m_winnerscount == 1 && m_leaverscount == 1) || (m_loserscount == 1 && m_leaverscount == 1))
            {
                if (battleField)
                {
                    World.Instance.SendAnnounce($"Le joueur <b>{(Winners.Fighters[0] as CharacterFighter).Character.Name}</b> a battu le joueur <b>{(Leavers[0] as CharacterFighter).Character.Name}</b> en DeadMatch." +
                        $" La bataille a duré <b>{this.GetFightDuration().Minutes}</b> minutes et <b>{this.GetFightDuration().Seconds}</b> secondes.", Color.YellowGreen);
                }
                else
                {
                    World.Instance.SendAnnounce($"Le joueur <b>{(Winners.Fighters[0] as CharacterFighter).Character.Name}</b> a battu le joueur <b>{(Leavers[0] as CharacterFighter).Character.Name}</b> en combat PvP." +
                        $" L'agression a duré <b>{this.GetFightDuration().Minutes}</b> minutes et <b>{this.GetFightDuration().Seconds}</b> secondes.", Color.YellowGreen);
                }
            }
        }

        public override TimeSpan GetPlacementTimeLeft()
        {
            var timeleft = FightConfiguration.PlacementPhaseTime - (DateTime.Now - CreationTime).TotalMilliseconds;

            if (timeleft < 0)
                timeleft = 0;

            return TimeSpan.FromMilliseconds(timeleft);
        }

        protected override bool CanCancelFight() => false;

        public Dictionary<Character, Map> m_playersMaps = new Dictionary<Character, Map>();

        #region Loot PvPSeek

        private int[,] earnedKamasMatrix = new int[,] {
                { 20000, 30000, 50000 },
                { 30000, 50000, 70000 },
                { 50000, 60000, 90000 },
                { 60000, 80000, 110000 },
            };

        private int[,] earnedPvMatrix = new int[,] {
                { 1, 1, 1 },
                { 1, 1, 1 },
                { 1, 1, 1 },
                { 1, 2, 2 },
            };


        private void CalculateEarnedKamasPevetons(FightPlayerResult result)
        {
            var pvpSeek = result.Character.Inventory.GetItems(x => x.Template.Id == (int)ItemIdEnum.ORDRE_DEXECUTION_10085).FirstOrDefault();

            if (pvpSeek != null)
            {
                if (pvpSeek.Effects.FirstOrDefault(x => x.EffectId == EffectsEnum.Effect_Seek) is EffectString seekEffect)
                {
                    var target = result.Fighter.OpposedTeam.GetAllFightersWithLeavers<CharacterFighter>().FirstOrDefault(x => x.Name == seekEffect.Text);
                    if (target != null)
                    {
                        result.Character.Inventory.RemoveItem(pvpSeek);

                        double fightTime = this.GetFightDuration().TotalSeconds;

                        /* Character Winner */
                        if (Winners == result.Fighter.Team)
                        {
                            int characterLevel = result.Character.Level;

                            #region Level ~ 50
                            /* Level ~ 50 */
                            if (characterLevel <= 50)
                            {

                                if (fightTime <= 300)
                                {
                                    result.Loot.Kamas += (long)earnedKamasMatrix[0, 0];
                                    result.Loot.AddItem((int)ItemIdEnum.PEVETON_10275, (uint)earnedPvMatrix[0, 0]);
                                }

                                else if (fightTime > 301 && fightTime <= 600)
                                {
                                    result.Loot.Kamas += (long)earnedKamasMatrix[0, 1];
                                    result.Loot.AddItem((int)ItemIdEnum.PEVETON_10275, (uint)earnedPvMatrix[0, 1]);
                                }

                                else if (fightTime >= 601)
                                {
                                    result.Loot.Kamas += (long)earnedKamasMatrix[0, 2];
                                    result.Loot.AddItem((int)ItemIdEnum.PEVETON_10275, (uint)earnedPvMatrix[0, 2]);
                                }
                            }
                            #endregion

                            #region Level 51 ~ 100
                            /* Level 51 ~ 100 */
                            else if (characterLevel >= 51 && characterLevel <= 100)
                            {
                                if (fightTime <= 300)
                                {
                                    result.Loot.Kamas += (long)earnedKamasMatrix[1, 0];
                                    result.Loot.AddItem((int)ItemIdEnum.PEVETON_10275, (uint)earnedPvMatrix[1, 0]);
                                }

                                else if (fightTime > 301 && fightTime <= 600)
                                {
                                    result.Loot.Kamas += (long)earnedKamasMatrix[1, 1];
                                    result.Loot.AddItem((int)ItemIdEnum.PEVETON_10275, (uint)earnedPvMatrix[1, 1]);
                                }

                                else if (fightTime >= 601)
                                {
                                    result.Loot.Kamas += (long)earnedKamasMatrix[1, 2];
                                    result.Loot.AddItem((int)ItemIdEnum.PEVETON_10275, (uint)earnedPvMatrix[1, 2]);
                                }

                            }
                            #endregion

                            #region Level 101 ~ 150
                            /* Level 51 ~ 100 */
                            else if (characterLevel >= 101 && characterLevel <= 150)
                            {
                                if (fightTime <= 300)
                                {
                                    result.Loot.Kamas += (long)earnedKamasMatrix[2, 0];
                                    result.Loot.AddItem((int)ItemIdEnum.PEVETON_10275, (uint)earnedPvMatrix[2, 0]);
                                }

                                else if (fightTime > 301 && fightTime <= 600)
                                {
                                    result.Loot.Kamas += (long)earnedKamasMatrix[2, 1];
                                    result.Loot.AddItem((int)ItemIdEnum.PEVETON_10275, (uint)earnedPvMatrix[2, 1]);
                                }

                                else if (fightTime >= 601)
                                {
                                    result.Loot.Kamas += (long)earnedKamasMatrix[2, 2];
                                    result.Loot.AddItem((int)ItemIdEnum.PEVETON_10275, (uint)earnedPvMatrix[2, 2]);
                                }

                            }
                            #endregion

                            #region Level 151 ~ 200
                            /* Level 151 ~ 200 */
                            else if (characterLevel >= 151 && characterLevel <= 200)
                            {
                                if (fightTime <= 300)
                                {
                                    result.Loot.Kamas += (long)earnedKamasMatrix[3, 0];
                                    result.Loot.AddItem((int)ItemIdEnum.PEVETON_10275, (uint)earnedPvMatrix[3, 0]);
                                }

                                else if (fightTime > 301 && fightTime <= 600)
                                {
                                    result.Loot.Kamas += (long)earnedKamasMatrix[3, 1];
                                    result.Loot.AddItem((int)ItemIdEnum.PEVETON_10275, (uint)earnedPvMatrix[3, 1]);
                                }

                                else if (fightTime >= 601)
                                {
                                    result.Loot.Kamas += (long)earnedKamasMatrix[3, 2];
                                    result.Loot.AddItem((int)ItemIdEnum.PEVETON_10275, (uint)earnedPvMatrix[3, 2]);
                                }

                            }
                            #endregion
                            

                            //Back to old map
                            if (m_playersMaps.Keys.Contains(result.Character))
                                result.Character.NextMap = m_playersMaps[result.Character];
                            else
                                result.Character.NextMap = World.Instance.GetMap(100270593);

                            result.Character.SendServerMessage("Vous avez gagné le combat, félicitations !");
                        }

                        /* Target Winner */
                        else
                        {
                            int targeLevel = target.Level;
                            #region Level ~ 50
                            /* Level ~ 50 */
                            if (targeLevel <= 50)
                            {
                                if (fightTime <= 300)
                                {
                                    target.Loot.Kamas += (long)earnedKamasMatrix[0, 0];
                                    target.Loot.AddItem((int)ItemIdEnum.PEVETON_10275, (uint)earnedPvMatrix[0, 0]);
                                }

                                else if (fightTime > 301 && fightTime <= 600)
                                {
                                    target.Loot.Kamas += (long)earnedKamasMatrix[0, 1];
                                    target.Loot.AddItem((int)ItemIdEnum.PEVETON_10275, (uint)earnedPvMatrix[0, 1]);
                                }

                                else if (fightTime >= 601)
                                {
                                    target.Loot.Kamas += (long)earnedKamasMatrix[0, 2];
                                    target.Loot.AddItem((int)ItemIdEnum.PEVETON_10275, (uint)earnedPvMatrix[0, 2]);
                                }
                            }
                            #endregion

                            #region Level 51 ~ 100
                            /* Level 51 ~ 100 */
                            else if (targeLevel >= 51 && targeLevel <= 100)
                            {
                                if (fightTime <= 300)
                                {
                                    target.Loot.Kamas += (long)earnedKamasMatrix[1, 0];
                                    target.Loot.AddItem((int)ItemIdEnum.PEVETON_10275, (uint)earnedPvMatrix[1, 0]);
                                }

                                else if (fightTime > 301 && fightTime <= 600)
                                {
                                    target.Loot.Kamas += (long)earnedKamasMatrix[1, 1];
                                    target.Loot.AddItem((int)ItemIdEnum.PEVETON_10275, (uint)earnedPvMatrix[1, 1]);
                                }

                                else if (fightTime >= 601)
                                {
                                    target.Loot.Kamas += (long)earnedKamasMatrix[1, 2];
                                    target.Loot.AddItem((int)ItemIdEnum.PEVETON_10275, (uint)earnedPvMatrix[1, 2]);
                                }

                            }
                            #endregion

                            #region Level 101 ~ 150
                            /* Level 51 ~ 100 */
                            else if (targeLevel >= 101 && targeLevel <= 150)
                            {
                                if (fightTime <= 300)
                                {
                                    target.Loot.Kamas += (long)earnedKamasMatrix[2, 0];
                                    target.Loot.AddItem((int)ItemIdEnum.PEVETON_10275, (uint)earnedPvMatrix[2, 0]);
                                }

                                else if (fightTime > 301 && fightTime <= 600)
                                {
                                    target.Loot.Kamas += (long)earnedKamasMatrix[2, 1];
                                    target.Loot.AddItem((int)ItemIdEnum.PEVETON_10275, (uint)earnedPvMatrix[2, 1]);
                                }

                                else if (fightTime >= 601)
                                {
                                    target.Loot.Kamas += (long)earnedKamasMatrix[2, 2];
                                    target.Loot.AddItem((int)ItemIdEnum.PEVETON_10275, (uint)earnedPvMatrix[2, 2]);
                                }

                            }
                            #endregion

                            #region Level 151 ~ 200
                            /* Level 151 ~ 200 */
                            else if (targeLevel >= 151 && targeLevel <= 200)
                            {
                                if (fightTime <= 300)
                                {
                                    target.Loot.Kamas += (long)earnedKamasMatrix[3, 0];
                                    target.Loot.AddItem((int)ItemIdEnum.PEVETON_10275, (uint)earnedPvMatrix[3, 0]);
                                }

                                else if (fightTime > 301 && fightTime <= 600)
                                {
                                    target.Loot.Kamas += (long)earnedKamasMatrix[3, 1];
                                    target.Loot.AddItem((int)ItemIdEnum.PEVETON_10275, (uint)earnedPvMatrix[3, 1]);
                                }

                                else if (fightTime >= 601)
                                {
                                    target.Loot.Kamas += (long)earnedKamasMatrix[3, 2];
                                    target.Loot.AddItem((int)ItemIdEnum.PEVETON_10275, (uint)earnedPvMatrix[3, 2]);
                                }
                            }

                            #endregion
                            

                            //Back to old map
                            if (m_playersMaps.Keys.Contains(target.Character))
                                target.Character.NextMap = m_playersMaps[target.Character];
                            else
                                target.Character.NextMap = World.Instance.GetMap(100270593);
                            target.Character.SendServerMessage("Vous avez gagné le combat, félicitations !");
                        }

                    }
                }
                else
                    result.Character.Inventory.RemoveItem(pvpSeek);
            }
        }
        #endregion

        #region Sistema de Honra  

        /* - - - Variáveis - - - */
        private List<Character> _allWinners = new List<Character>();
        private List<CharacterFighter> _noHonor = new List<CharacterFighter>();
        private List<Character> _allLosers = new List<Character>();
        private int TotalNivelVencedores, TotalNivAsaVencedores, QtdVencedores;
        private int TotalNivelPerdedores, TotalNivAsaPerdedores, QtdPerdedores;
        List<string> _ipsWinners = new List<string>();
        List<string> _ipLosers = new List<string>();


        /* - - - Coletando informações sobre os jogadores - - - */
        public void GetInformations()
        {
            var m_losers = this.GetLosersAndLeaversWithDc().OfType<CharacterFighter>().ToList();

            if (m_losers != null && m_losers.Count() > 0)

            {
                //Armazenando informações sobre os perdedores
                foreach (var _perso in m_losers)
                {
                    if (_perso.Character.AlignmentSide == AlignmentSideEnum.ALIGNMENT_NEUTRAL)
                        _noHonor.Add(_perso);

                    try
                    {
                        _allLosers.Add(_perso.Character);
                        _ipLosers.Add(_perso.Character.Client.IP);
                    }

                    catch (Exception e) { }

                    TotalNivelPerdedores += _perso.Level;
                    TotalNivAsaPerdedores += _perso.Character.AlignmentGrade;
                    QtdPerdedores++;
                }
            }

            //Armazenando informações sobre os vencedores
            foreach (var _perso in Winners.GetAllFightersWithLeavers().OfType<CharacterFighter>())
            {
                if (_perso.Character.AlignmentSide == AlignmentSideEnum.ALIGNMENT_NEUTRAL || _ipLosers.Contains(_perso.Character.Client.IP))
                    _noHonor.Add(_perso);

                try
                {
                    _ipsWinners.Add((_perso as CharacterFighter).Character.Client.IP);
                    _allWinners.Add((_perso as CharacterFighter).Character);
                }

                catch (Exception e) { }

                TotalNivelVencedores += _perso.Level;
                TotalNivAsaVencedores += (_perso as CharacterFighter).Character.AlignmentGrade;
                QtdVencedores++;
            }


        }


        /* - - - Calculando a honra ganha/perdida - - - */

        public short CalculateEarnedHonor(CharacterFighter character)
        {
            short result = 0;

            if (Draw)
            {
                result = 0;
            }
            else
            {
                if (character.OpposedTeam.AlignmentSide == AlignmentSideEnum.ALIGNMENT_NEUTRAL || _noHonor.Contains(character))
                {
                    result = 0;
                }

                else if (QtdVencedores == 0 || QtdPerdedores == 0)
                {
                    result = 0;
                }

                else
                {
                    int somaGanhador = (TotalNivAsaVencedores * (QtdPerdedores - 1)) + TotalNivelVencedores;
                    int somaPerdedor = (TotalNivAsaPerdedores * (QtdVencedores - 1)) + TotalNivelPerdedores;
                    bool passar = false;

                    /* - - - Geração de honra aos vencedores - - - */
                    if (_allWinners.Contains(character.Character))
                    {
                        if (QtdVencedores == 1 && QtdPerdedores == 1
                            && Math.Abs(TotalNivelPerdedores - TotalNivelVencedores) < 20)
                        {
                            passar = true;
                        }

                        else if (somaPerdedor >= somaGanhador || somaGanhador - somaPerdedor < 6)
                        {
                            passar = true;
                        }

                        if (!passar)
                        {
                            return 0;
                        }
                        else if (QtdPerdedores == 1)
                        {
                            var _loser = _allLosers[0];

                            if (_loser != null)
                            {
                                if (_loser.Honor < 60)
                                {
                                    return 60;
                                }
                                else
                                {
                                    result = ((short)(_loser.Honor * 0.05));
                                }
                            }
                        }
                        else
                        {
                            double _sumHonor = 0;

                            foreach (var _actor in _allLosers)
                            {
                                _sumHonor += _actor.Honor;
                            }

                            result = ((short)((_sumHonor * 0.05) / QtdPerdedores));
                        }
                    } //Fim da checagem de vencedores


                    /* - - - Geração de honra aos perdedores - - - */
                    else
                    {
                        if (QtdVencedores == 1 && QtdPerdedores == 1
                            && Math.Abs(TotalNivelPerdedores - TotalNivelVencedores) < 20)
                        {
                            passar = true;
                        }

                        else if (somaPerdedor >= somaGanhador || somaGanhador - somaPerdedor < 6)
                        {
                            passar = true;
                        }

                        if (!passar)
                        {
                            return 0;
                        }

                        result = ((short)-((character.Character.Honor * 10) / 100));

                    }//Fim da checagem de perdedores

                }

            }
            return result;
        }

        #endregion 

        public short CalculateEarnedDishonor(CharacterFighter character)
        {
            if (Draw)
                return 0;

            return character.OpposedTeam.AlignmentSide != AlignmentSideEnum.ALIGNMENT_NEUTRAL ? (short)0 : (short)1;
        }
    }
}