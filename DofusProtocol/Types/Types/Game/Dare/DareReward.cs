namespace Stump.DofusProtocol.Types
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class DareReward
    {
        public const short Id  = 505;
        public virtual short TypeId
        {
            get { return Id; }
        }
        public sbyte Type { get; set; }
        public ushort MonsterId { get; set; }
        public ulong Kamas { get; set; }
        public double DareId { get; set; }

        public DareReward(sbyte type, ushort monsterId, ulong kamas, double dareId)
        {
            this.Type = type;
            this.MonsterId = monsterId;
            this.Kamas = kamas;
            this.DareId = dareId;
        }

        public DareReward() { }

        public virtual void Serialize(IDataWriter writer)
        {
            writer.WriteSByte(Type);
            writer.WriteVarUShort(MonsterId);
            writer.WriteVarULong(Kamas);
            writer.WriteDouble(DareId);
        }

        public virtual void Deserialize(IDataReader reader)
        {
            Type = reader.ReadSByte();
            MonsterId = reader.ReadVarUShort();
            Kamas = reader.ReadVarULong();
            DareId = reader.ReadDouble();
        }

    }
}
