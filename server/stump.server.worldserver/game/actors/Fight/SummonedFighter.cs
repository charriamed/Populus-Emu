using Stump.DofusProtocol.Enums;
using Stump.DofusProtocol.Types;
using Stump.Server.WorldServer.Database.Monsters;
using Stump.Server.WorldServer.Database.World;
using Stump.Server.WorldServer.Game.Fights;
using Stump.Server.WorldServer.Game.Fights.Teams;
using Stump.Server.WorldServer.Handlers.Context;
using System.Collections.Generic;
using Spell = Stump.Server.WorldServer.Game.Spells.Spell;

namespace Stump.Server.WorldServer.Game.Actors.Fight
{
    public abstract class SummonedFighter : AIFighter
    {
        protected SummonedFighter(int id, FightTeam team, IEnumerable<Spell> spells, FightActor summoner, Cell cell)
            : base(team, spells)
        {
            Id = id;
            Position = summoner.Position.Clone();
            Cell = cell;
            Summoner = summoner;

            FightStartPosition = Position.Clone();
            MovementHistory.RegisterEntry(FightStartPosition.Cell);
        }

        protected SummonedFighter(int id, FightTeam team, IEnumerable<Spell> spells, FightActor summoner, Cell cell, int identifier, MonsterGrade template)
            : base(team, spells, identifier, template)
        {
            Id = id;
            Position = summoner.Position.Clone();
            Cell = cell;
            Summoner = summoner;

            FightStartPosition = Position.Clone();
            MovementHistory.RegisterEntry(FightStartPosition.Cell);
        }

        public override sealed int Id
        {
            get;
            protected set;
        }

        private CharacterFighter m_controller;
        public CharacterFighter Controller
        {
            get { return m_controller; }
            protected set
            {
                m_controller = value;

                if (value == null)
                {
                    Fight.TurnStopped -= OnTurnStopped;
                }
                else
                {
                    Fight.TurnStopped += OnTurnStopped;
                }
            }
        }

        public void SetController(CharacterFighter controller)
        {
            Controller = controller;
        }

        public bool IsControlled() => Controller != null;

        public override void OnTurnStarted(IFight fight, FightActor currentfighter)
        {
            if (IsControlled())
            {
                return;
            }

            base.OnTurnStarted(fight, currentfighter);
        }

        public override bool HasResult => false;

        public override int GetTackledAP(int mp, Cell cell) => 0;

        public override int GetTackledMP(int mp, Cell cell) => 0;

        public CharacterCharacteristicsInformations GetSlaveCharacteristicsInformations()
        {
            var characterFighter = Summoner as CharacterFighter;
            if (characterFighter == null)
            {
                return new CharacterCharacteristicsInformations();
            }

            var slaveStats = characterFighter.Character.GetCharacterCharacteristicsInformations();

            slaveStats.ActionPoints = Stats.AP;
            slaveStats.ActionPointsCurrent = (short)Stats.AP.Total;
            slaveStats.MovementPoints = Stats.MP;
            slaveStats.MovementPointsCurrent = (short)Stats.MP.Total;
            slaveStats.LifePoints = (uint)Stats.Health.Total;
            slaveStats.MaxLifePoints = (uint)Stats.Health.TotalMax;

            slaveStats.TackleEvade = Stats[PlayerFields.TackleEvade];
            slaveStats.Intelligence = Stats[PlayerFields.Intelligence];
            slaveStats.Strength = Stats[PlayerFields.Strength];
            slaveStats.Chance = Stats[PlayerFields.Chance];
            slaveStats.Wisdom = Stats[PlayerFields.Wisdom];
            slaveStats.Agility = Stats[PlayerFields.Agility];

            return slaveStats;
        }

        protected override void OnDead(FightActor killedBy, bool passTurn = true, bool isKillEffect = false)
        {
            Controller = null;
            base.OnDead(killedBy, passTurn);
            Summoner.RemoveSummon(this);
        }

        void OnTurnStopped(IFight fight, FightActor player)
        {
            if (fight.TimeLine.Next != this)
            {
                return;
            }

            var characterFighter = Summoner as CharacterFighter;
            if (characterFighter == null)
            {
                return;
            }

            ContextHandler.SendSlaveSwitchContextMessage(characterFighter.Character.Client, this);
        }
    }
}