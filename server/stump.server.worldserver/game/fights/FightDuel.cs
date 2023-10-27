using System;
using System.Collections.Generic;
using System.Linq;
using Stump.DofusProtocol.Enums;
using Stump.Server.WorldServer.Game.Actors.Fight;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Characters;
using Stump.Server.WorldServer.Game.Fights.Results;
using Stump.Server.WorldServer.Game.Fights.Teams;
using Stump.Server.WorldServer.Game.Maps;
using Stump.Server.WorldServer.Handlers.Context;

namespace Stump.Server.WorldServer.Game.Fights
{
    public class FightDuel : Fight<FightPlayerTeam, FightPlayerTeam>
    {
        public FightDuel(int id, Map fightMap, FightPlayerTeam defendersTeam, FightPlayerTeam challengersTeam)
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

            base.StartFighting();
        }

        public override bool IsDeathTemporarily => true;

        public override FightTypeEnum FightType => FightTypeEnum.FIGHT_TYPE_CHALLENGE;

        public override bool IsPvP => true;

        public override bool IsMultiAccountRestricted => false;

        protected override List<IFightResult> GetResults()
        {
           /* if (Map.Id == 172757504 && FighterPlaying.Team == Losers)
            { 
            
                    (FighterPlaying as CharacterFighter).Character.OpenPopup("Vous venez de perdre votre combat classement, vous êtes donc retomber niveau 1");
                    (FighterPlaying as CharacterFighter).Character.LevelDown(199);
                    (FighterPlaying as CharacterFighter).Character.Inventory.MoveItem(item, CharacterInventoryPositionEnum.INVENTORY_POSITION_NOT_EQUIPED)); 
                  
                    (FighterPlaying as CharacterFighter).Character.ResetStats(false);
                    (FighterPlaying as CharacterFighter).Character.Spells.ForgetAllSpells();
                    (FighterPlaying as CharacterFighter).Character.RefreshActor();
                    (FighterPlaying as CharacterFighter).Character.RefreshStats();
                    (FighterPlaying as CharacterFighter).Character.StatsPoints = 0;
                    (FighterPlaying as CharacterFighter).Character.SpellsPoints = 0;
                    (FighterPlaying as CharacterFighter).Character.ResetStats(false);
                    ;

                } */
            
                   
                
                return GetFightersAndLeavers().Where(entry => entry.HasResult).Select(fighter => fighter.GetFightResult()).ToList();

            }
        

        protected override void SendGameFightJoinMessage(CharacterFighter fighter)
        {
            ContextHandler.SendGameFightJoinMessage(fighter.Character.Client, CanCancelFight(), true, IsStarted, IsStarted ? 0 : (int)GetPlacementTimeLeft().TotalMilliseconds / 100, FightType);
        }

        public override TimeSpan GetPlacementTimeLeft()
        {
            var timeleft = TimeSpan.FromMilliseconds(FightConfiguration.PlacementPhaseTime) - ( DateTime.Now - CreationTime );

            if (timeleft < TimeSpan.Zero)
                timeleft = TimeSpan.Zero;

            return timeleft;
        }

        protected override bool CanCancelFight() => State == FightState.Placement;
    }
}