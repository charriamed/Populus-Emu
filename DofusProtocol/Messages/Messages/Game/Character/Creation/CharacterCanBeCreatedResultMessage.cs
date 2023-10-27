namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class CharacterCanBeCreatedResultMessage : Message
    {
        public const uint Id = 6733;
        public override uint MessageId
        {
            get { return Id; }
        }
        public bool YesYouCan { get; set; }

        public CharacterCanBeCreatedResultMessage(bool yesYouCan)
        {
            this.YesYouCan = yesYouCan;
        }

        public CharacterCanBeCreatedResultMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteBoolean(YesYouCan);
        }

        public override void Deserialize(IDataReader reader)
        {
            YesYouCan = reader.ReadBoolean();
        }

    }
}
