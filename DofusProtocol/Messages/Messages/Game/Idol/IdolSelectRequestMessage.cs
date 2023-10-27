namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class IdolSelectRequestMessage : Message
    {
        public const uint Id = 6587;
        public override uint MessageId
        {
            get { return Id; }
        }
        public bool Activate { get; set; }
        public bool Party { get; set; }
        public ushort IdolId { get; set; }

        public IdolSelectRequestMessage(bool activate, bool party, ushort idolId)
        {
            this.Activate = activate;
            this.Party = party;
            this.IdolId = idolId;
        }

        public IdolSelectRequestMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            var flag = new byte();
            flag = BooleanByteWrapper.SetFlag(flag, 0, Activate);
            flag = BooleanByteWrapper.SetFlag(flag, 1, Party);
            writer.WriteByte(flag);
            writer.WriteVarUShort(IdolId);
        }

        public override void Deserialize(IDataReader reader)
        {
            var flag = reader.ReadByte();
            Activate = BooleanByteWrapper.GetFlag(flag, 0);
            Party = BooleanByteWrapper.GetFlag(flag, 1);
            IdolId = reader.ReadVarUShort();
        }

    }
}
