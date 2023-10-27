namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class ObjectUseMessage : Message
    {
        public const uint Id = 3019;
        public override uint MessageId
        {
            get { return Id; }
        }
        public uint ObjectUID { get; set; }

        public ObjectUseMessage(uint objectUID)
        {
            this.ObjectUID = objectUID;
        }

        public ObjectUseMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteVarUInt(ObjectUID);
        }

        public override void Deserialize(IDataReader reader)
        {
            ObjectUID = reader.ReadVarUInt();
        }

    }
}
