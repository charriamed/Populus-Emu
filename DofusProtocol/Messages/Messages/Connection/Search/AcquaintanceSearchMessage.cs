namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class AcquaintanceSearchMessage : Message
    {
        public const uint Id = 6144;
        public override uint MessageId
        {
            get { return Id; }
        }
        public string Nickname { get; set; }

        public AcquaintanceSearchMessage(string nickname)
        {
            this.Nickname = nickname;
        }

        public AcquaintanceSearchMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteUTF(Nickname);
        }

        public override void Deserialize(IDataReader reader)
        {
            Nickname = reader.ReadUTF();
        }

    }
}
