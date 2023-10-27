namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using System.Collections.Generic;
    using Stump.Core.IO;

    [Serializable]
    public class ExchangeObjectsRemovedMessage : ExchangeObjectMessage
    {
        public new const uint Id = 6532;
        public override uint MessageId
        {
            get { return Id; }
        }
        public uint[] ObjectUID { get; set; }

        public ExchangeObjectsRemovedMessage(bool remote, uint[] objectUID)
        {
            this.Remote = remote;
            this.ObjectUID = objectUID;
        }

        public ExchangeObjectsRemovedMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteShort((short)ObjectUID.Count());
            for (var objectUIDIndex = 0; objectUIDIndex < ObjectUID.Count(); objectUIDIndex++)
            {
                writer.WriteVarUInt(ObjectUID[objectUIDIndex]);
            }
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            var objectUIDCount = reader.ReadUShort();
            ObjectUID = new uint[objectUIDCount];
            for (var objectUIDIndex = 0; objectUIDIndex < objectUIDCount; objectUIDIndex++)
            {
                ObjectUID[objectUIDIndex] = reader.ReadVarUInt();
            }
        }

    }
}
