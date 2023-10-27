namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class StatsUpgradeResultMessage : Message
    {
        public const uint Id = 5609;
        public override uint MessageId
        {
            get { return Id; }
        }
        public sbyte Result { get; set; }
        public ushort NbCharacBoost { get; set; }

        public StatsUpgradeResultMessage(sbyte result, ushort nbCharacBoost)
        {
            this.Result = result;
            this.NbCharacBoost = nbCharacBoost;
        }

        public StatsUpgradeResultMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteSByte(Result);
            writer.WriteVarUShort(NbCharacBoost);
        }

        public override void Deserialize(IDataReader reader)
        {
            Result = reader.ReadSByte();
            NbCharacBoost = reader.ReadVarUShort();
        }

    }
}
