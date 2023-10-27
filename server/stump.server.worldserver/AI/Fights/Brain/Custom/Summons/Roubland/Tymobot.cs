//using Stump.DofusProtocol.Enums;
//using Stump.Server.WorldServer.AI.Fights.Actions;
//using Stump.Server.WorldServer.Game.Actors.Fight;
//using Stump.Server.WorldServer.Game.Fights;
//using Stump.Server.WorldServer.Game.Fights.Teams;
//using Stump.Server.WorldServer.Game.Spells;
//using TreeSharp;

//namespace Stump.Server.WorldServer.AI.Fights.Brain.Custom.Summons.Ani
//{
//    [BrainIdentifier((int)MonsterIdEnum.ROUBLABOT)]
//    class Tymobot : Brain
//    {
//        public Tymobot(AIFighter fighter)
//            : base(fighter)
//        {
//            fighter.TurnPassed += OnTurnPassed;
//        }

//        void OnTurnPassed(FightActor fighter)
//        {
//            if (fighter != Fighter)
//                return;
//            using (Fighter.Fight.StartSequence(SequenceTypeEnum.SEQUENCE_TURN_END))
//            {
//                Fighter.Die(fighter);
//            }
//        }
//    }
//}