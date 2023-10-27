namespace Stump.DofusProtocol.Types
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class HumanOptionObjectUse : HumanOption
    {
        public new const short Id = 449;
        public override short TypeId
        {
            get { return Id; }
        }
        public sbyte DelayTypeId { get; set; }
        public double DelayEndTime { get; set; }
        public ushort ObjectGID { get; set; }

        public HumanOptionObjectUse(sbyte delayTypeId, double delayEndTime, ushort objectGID)
        {
            this.DelayTypeId = delayTypeId;
            this.DelayEndTime = delayEndTime;
            this.ObjectGID = objectGID;
        }

        public HumanOptionObjectUse() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteSByte(DelayTypeId);
            writer.WriteDouble(DelayEndTime);
            writer.WriteVarUShort(ObjectGID);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            DelayTypeId = reader.ReadSByte();
            DelayEndTime = reader.ReadDouble();
            ObjectGID = reader.ReadVarUShort();
        }

    }
}
