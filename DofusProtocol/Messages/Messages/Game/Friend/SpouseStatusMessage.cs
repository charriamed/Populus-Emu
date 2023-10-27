namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class SpouseStatusMessage : Message
    {
        public const uint Id = 6265;
        public override uint MessageId
        {
            get { return Id; }
        }
        public bool HasSpouse { get; set; }

        public SpouseStatusMessage(bool hasSpouse)
        {
            this.HasSpouse = hasSpouse;
        }

        public SpouseStatusMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteBoolean(HasSpouse);
        }

        public override void Deserialize(IDataReader reader)
        {
            HasSpouse = reader.ReadBoolean();
        }

    }
}
