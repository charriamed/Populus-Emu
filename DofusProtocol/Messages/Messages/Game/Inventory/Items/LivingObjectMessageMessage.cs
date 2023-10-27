namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class LivingObjectMessageMessage : Message
    {
        public const uint Id = 6065;
        public override uint MessageId
        {
            get { return Id; }
        }
        public ushort MsgId { get; set; }
        public int TimeStamp { get; set; }
        public string Owner { get; set; }
        public ushort ObjectGenericId { get; set; }

        public LivingObjectMessageMessage(ushort msgId, int timeStamp, string owner, ushort objectGenericId)
        {
            this.MsgId = msgId;
            this.TimeStamp = timeStamp;
            this.Owner = owner;
            this.ObjectGenericId = objectGenericId;
        }

        public LivingObjectMessageMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteVarUShort(MsgId);
            writer.WriteInt(TimeStamp);
            writer.WriteUTF(Owner);
            writer.WriteVarUShort(ObjectGenericId);
        }

        public override void Deserialize(IDataReader reader)
        {
            MsgId = reader.ReadVarUShort();
            TimeStamp = reader.ReadInt();
            Owner = reader.ReadUTF();
            ObjectGenericId = reader.ReadVarUShort();
        }

    }
}
