namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class GuildModificationStartedMessage : Message
    {
        public const uint Id = 6324;
        public override uint MessageId
        {
            get { return Id; }
        }
        public bool CanChangeName { get; set; }
        public bool CanChangeEmblem { get; set; }

        public GuildModificationStartedMessage(bool canChangeName, bool canChangeEmblem)
        {
            this.CanChangeName = canChangeName;
            this.CanChangeEmblem = canChangeEmblem;
        }

        public GuildModificationStartedMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            var flag = new byte();
            flag = BooleanByteWrapper.SetFlag(flag, 0, CanChangeName);
            flag = BooleanByteWrapper.SetFlag(flag, 1, CanChangeEmblem);
            writer.WriteByte(flag);
        }

        public override void Deserialize(IDataReader reader)
        {
            var flag = reader.ReadByte();
            CanChangeName = BooleanByteWrapper.GetFlag(flag, 0);
            CanChangeEmblem = BooleanByteWrapper.GetFlag(flag, 1);
        }

    }
}
