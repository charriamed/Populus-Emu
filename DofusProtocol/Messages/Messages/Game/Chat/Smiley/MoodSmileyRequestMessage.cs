namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class MoodSmileyRequestMessage : Message
    {
        public const uint Id = 6192;
        public override uint MessageId
        {
            get { return Id; }
        }
        public ushort SmileyId { get; set; }

        public MoodSmileyRequestMessage(ushort smileyId)
        {
            this.SmileyId = smileyId;
        }

        public MoodSmileyRequestMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteVarUShort(SmileyId);
        }

        public override void Deserialize(IDataReader reader)
        {
            SmileyId = reader.ReadVarUShort();
        }

    }
}
