namespace Stump.DofusProtocol.Types
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using System.Collections.Generic;
    using Stump.Core.IO;

    [Serializable]
    public class FightStartingPositions
    {
        public const short Id  = 513;
        public virtual short TypeId
        {
            get { return Id; }
        }
        public ushort[] PositionsForChallengers { get; set; }
        public ushort[] PositionsForDefenders { get; set; }

        public FightStartingPositions(ushort[] positionsForChallengers, ushort[] positionsForDefenders)
        {
            this.PositionsForChallengers = positionsForChallengers;
            this.PositionsForDefenders = positionsForDefenders;
        }

        public FightStartingPositions() { }

        public virtual void Serialize(IDataWriter writer)
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
        }

        public virtual void Deserialize(IDataReader reader)
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
        }

    }
}
