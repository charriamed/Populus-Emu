namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class LockableUseCodeMessage : Message
    {
        public const uint Id = 5667;
        public override uint MessageId
        {
            get { return Id; }
        }
        public string Code { get; set; }

        public LockableUseCodeMessage(string code)
        {
            this.Code = code;
        }

        public LockableUseCodeMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteUTF(Code);
        }

        public override void Deserialize(IDataReader reader)
        {
            Code = reader.ReadUTF();
        }

    }
}
