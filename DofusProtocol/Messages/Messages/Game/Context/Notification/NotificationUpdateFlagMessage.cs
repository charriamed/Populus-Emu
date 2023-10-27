namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class NotificationUpdateFlagMessage : Message
    {
        public const uint Id = 6090;
        public override uint MessageId
        {
            get { return Id; }
        }
        public ushort Index { get; set; }

        public NotificationUpdateFlagMessage(ushort index)
        {
            this.Index = index;
        }

        public NotificationUpdateFlagMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteVarUShort(Index);
        }

        public override void Deserialize(IDataReader reader)
        {
            Index = reader.ReadVarUShort();
        }

    }
}
