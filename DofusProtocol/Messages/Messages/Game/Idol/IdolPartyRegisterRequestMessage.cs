namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class IdolPartyRegisterRequestMessage : Message
    {
        public const uint Id = 6582;
        public override uint MessageId
        {
            get { return Id; }
        }
        public bool Register { get; set; }

        public IdolPartyRegisterRequestMessage(bool register)
        {
            this.Register = register;
        }

        public IdolPartyRegisterRequestMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteBoolean(Register);
        }

        public override void Deserialize(IDataReader reader)
        {
            Register = reader.ReadBoolean();
        }

    }
}
