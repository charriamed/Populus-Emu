using Stump.Core.Mathematics;
using Stump.DofusProtocol.Enums;
using Stump.Server.BaseServer.IPC.Messages;
using Stump.Server.WorldServer.Core.IPC;
using Stump.Server.WorldServer.Game.Actors.Fight;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Characters;
using Stump.Server.WorldServer.Game.Fights.Challenges;
using Stump.Server.WorldServer.Game.Fights.Results;
using Stump.Server.WorldServer.Game.Fights.Teams;
using Stump.Server.WorldServer.Game.Items;
using Stump.Server.WorldServer.Game.Maps;
using Stump.Server.WorldServer.Handlers.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using Stump.Core.Reflection;
namespace Stump.Server.WorldServer.Game.Fights
{
    public class FightPvBoss : Fight<FightMonsterTeam, FightPlayerTeam>
    {
        private readonly Dictionary<byte, byte> _doplon = new Dictionary<byte, byte>
        {{20, 1}, {40, 1}, {60, 1}, {80, 1}, {100, 1}, {120, 1}, {140, 1}, {160, 1}, {180, 1}, {200, 1}, {220, 1}};

        private readonly Dictionary<byte, ushort> _doplonKamas = new Dictionary<byte, ushort>
        { {20, 5000},{40, 20000},{60, 45000},{80, 800},{100, 1250},{120, 1800},{140, 2450},{160, 3200},{180, 4050},{200, 5000}, {220, 10000}};


        public FightPvBoss(int id, Map fightMap, FightMonsterTeam defendersTeam, FightPlayerTeam challengersTeam)
            : base(id, fightMap, defendersTeam, challengersTeam)
        {
        }
        public override bool IsPvP => false;
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


        protected override void OnFighterAdded(FightTeam team, FightActor actor)
        {
            //if (BreedFighter != null && !(team is FightMonsterTeam)) return;
            base.OnFighterAdded(team, actor);
            if (!(team is FightMonsterTeam))
            {
                BreedFighter = actor;
            }
        }

        public FightActor BreedFighter { get; set; }

        public override FightTypeEnum FightType => FightTypeEnum.FIGHT_TYPE_PvM;
        protected override List<IFightResult> GetResults()
        {
            var list = new List<IFightResult>();
            list.AddRange(
                from entry in GetFightersAndLeavers()
                where !(entry is SummonedFighter)
                select entry.GetFightResult());
            if (Winners.Fighters.Contains(BreedFighter))
            {
                var actor = Losers.Fighters.FirstOrDefault();
                var dopple = actor as MonsterFighter;
                if (dopple != null)
                {
                    var doppleGrade = dopple.Monster.Grade;
                    foreach (
                        var fightPlayerResult in
                            list.OfType<FightPlayerResult>()
                                .Where(
                                    fightPlayerResult =>
                                        fightPlayerResult.Fighter.HasWin() &&
                                        !Leavers.Contains(fightPlayerResult.Fighter)))
                    {
                        Singleton<ItemManager>.Instance.CreateDopeul(((CharacterFighter)fightPlayerResult.Fighter).Character, dopple.Monster.Template.Id);
                        fightPlayerResult.AddEarnedExperience(doppleGrade.GradeXp);
                        fightPlayerResult.Loot.Kamas = _doplonKamas[(byte)doppleGrade.Level];
                        fightPlayerResult.Loot.AddItem(8425, _doplon[(byte)doppleGrade.Level]);
                    }
                }
            }
            return list;
        }

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