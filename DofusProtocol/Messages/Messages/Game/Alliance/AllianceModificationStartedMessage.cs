namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class AllianceModificationStartedMessage : Message
    {
        public const uint Id = 6444;
        public override uint MessageId
        {
            get { return Id; }
        }
        public bool CanChangeName { get; set; }
        public bool CanChangeTag { get; set; }
        public bool CanChangeEmblem { get; set; }

        public AllianceModificationStartedMessage(bool canChangeName, bool canChangeTag, bool canChangeEmblem)
        {
            this.CanChangeName = canChangeName;
            this.CanChangeTag = canChangeTag;
            this.CanChangeEmblem = canChangeEmblem;
        }

        public AllianceModificationStartedMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            var flag = new byte();
            flag = BooleanByteWrapper.SetFlag(flag, 0, CanChangeName);
            flag = BooleanByteWrapper.SetFlag(flag, 1, CanChangeTag);
            flag = BooleanByteWrapper.SetFlag(flag, 2, CanChangeEmblem);
            writer.WriteByte(flag);
        }

        public override void Deserialize(IDataReader reader)
        {
            var flag = reader.ReadByte();
            CanChangeName = BooleanByteWrapper.GetFlag(flag, 0);
            CanChangeTag = BooleanByteWrapper.GetFlag(flag, 1);
            CanChangeEmblem = BooleanByteWrapper.GetFlag(flag, 2);
        }

    }
}
