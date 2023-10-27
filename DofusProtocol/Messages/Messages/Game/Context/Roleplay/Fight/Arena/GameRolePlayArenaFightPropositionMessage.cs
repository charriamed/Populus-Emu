namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using System.Collections.Generic;
    using Stump.Core.IO;

    [Serializable]
    public class GameRolePlayArenaFightPropositionMessage : Message
    {
        public const uint Id = 6276;
        public override uint MessageId
        {
            get { return Id; }
        }
        public ushort FightId { get; set; }
        public double[] AlliesId { get; set; }
        public ushort Duration { get; set; }

        public GameRolePlayArenaFightPropositionMessage(ushort fightId, double[] alliesId, ushort duration)
        {
            this.FightId = fightId;
            this.AlliesId = alliesId;
            this.Duration = duration;
        }

        public GameRolePlayArenaFightPropositionMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteVarUShort(FightId);
            writer.WriteShort((short)AlliesId.Count());
            for (var alliesIdIndex = 0; alliesIdIndex < AlliesId.Count(); alliesIdIndex++)
            {
                writer.WriteDouble(AlliesId[alliesIdIndex]);
            }
            writer.WriteVarUShort(Duration);
        }

        public override void Deserialize(IDataReader reader)
        {
            FightId = reader.ReadVarUShort();
            var alliesIdCount = reader.ReadUShort();
            AlliesId = new double[alliesIdCount];
            for (var alliesIdIndex = 0; alliesIdIndex < alliesIdCount; alliesIdIndex++)
            {
                AlliesId[alliesIdIndex] = reader.ReadDouble();
            }
            Duration = reader.ReadVarUShort();
        }

    }
}
