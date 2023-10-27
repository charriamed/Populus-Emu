namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class IgnoredDeleteResultMessage : Message
    {
        public const uint Id = 5677;
        public override uint MessageId
        {
            get { return Id; }
        }
        public bool Success { get; set; }
        public bool Session { get; set; }
        public string Name { get; set; }

        public IgnoredDeleteResultMessage(bool success, bool session, string name)
        {
            this.Success = success;
            this.Session = session;
            this.Name = name;
        }

        public IgnoredDeleteResultMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            var flag = new byte();
            flag = BooleanByteWrapper.SetFlag(flag, 0, Success);
            flag = BooleanByteWrapper.SetFlag(flag, 1, Session);
            writer.WriteByte(flag);
            writer.WriteUTF(Name);
        }

        public override void Deserialize(IDataReader reader)
        {
            var flag = reader.ReadByte();
            Success = BooleanByteWrapper.GetFlag(flag, 0);
            Session = BooleanByteWrapper.GetFlag(flag, 1);
            Name = reader.ReadUTF();
        }

    }
}
