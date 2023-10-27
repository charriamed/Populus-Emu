namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class ClientUIOpenedByObjectMessage : ClientUIOpenedMessage
    {
        public new const uint Id = 6463;
        public override uint MessageId
        {
            get { return Id; }
        }
        public uint Uid { get; set; }

        public ClientUIOpenedByObjectMessage(sbyte type, uint uid)
        {
            this.Type = type;
            this.Uid = uid;
        }

        public ClientUIOpenedByObjectMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteVarUInt(Uid);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            Uid = reader.ReadVarUInt();
        }

    }
}
