namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using System.Collections.Generic;
    using Stump.Core.IO;

    [Serializable]
    public class GameFightPlacementPossiblePositionsMessage : Message
    {
        public const uint Id = 703;
        public override uint MessageId
        {
            get { return Id; }
        }
        public ushort[] PositionsForChallengers { get; set; }
        public ushort[] PositionsForDefenders { get; set; }
        public sbyte TeamNumber { get; set; }

        public GameFightPlacementPossiblePositionsMessage(ushort[] positionsForChallengers, ushort[] positionsForDefenders, sbyte teamNumber)
        {
            this.PositionsForChallengers = positionsForChallengers;
            this.PositionsForDefenders = positionsForDefenders;
            this.TeamNumber = teamNumber;
        }

        public GameFightPlacementPossiblePositionsMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort((short)PositionsForChallengers.Count());
            for (var positionsForChallengersIndex = 0; positionsForChallengersIndex < PositionsForChallengers.Count(); positionsForChallengersIndex++)
            {
                writer.WriteVarUShort(PositionsForChallengers[positionsForChallengersIndex]);
            }
            writer.WriteShort((short)PositionsForDefenders.Count());
            for (var positionsForDefendersIndex = 0; positionsForDefendersIndex < PositionsForDefenders.Count(); positionsForDefendersIndex++)
            {
                writer.WriteVarUShort(PositionsForDefenders[positionsForDefendersIndex]);
            }
            writer.WriteSByte(TeamNumber);
        }

        public override void Deserialize(IDataReader reader)
        {
            var positionsForChallengersCount = reader.ReadUShort();
            PositionsForChallengers = new ushort[positionsForChallengersCount];
            for (var positionsForChallengersIndex = 0; positionsForChallengersIndex < positionsForChallengersCount; positionsForChallengersIndex++)
            {
                PositionsForChallengers[positionsForChallengersIndex] = reader.ReadVarUShort();
            }
            var positionsForDefendersCount = reader.ReadUShort();
            PositionsForDefenders = new ushort[positionsForDefendersCount];
            for (var positionsForDefendersIndex = 0; positionsForDefendersIndex < positionsForDefendersCount; positionsForDefendersIndex++)
            {
                PositionsForDefenders[positionsForDefendersIndex] = reader.ReadVarUShort();
            }
            TeamNumber = reader.ReadSByte();
        }

    }
}
