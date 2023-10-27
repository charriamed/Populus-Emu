namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class SpouseInformationsMessage : Message
    {
        public const uint Id = 6356;
        public override uint MessageId
        {
            get { return Id; }
        }
        public FriendSpouseInformations Spouse { get; set; }

        public SpouseInformationsMessage(FriendSpouseInformations spouse)
        {
            this.Spouse = spouse;
        }

        public SpouseInformationsMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort(Spouse.TypeId);
            Spouse.Serialize(writer);
        }

        public override void Deserialize(IDataReader reader)
        {
            Spouse = ProtocolTypeManager.GetInstance<FriendSpouseInformations>(reader.ReadShort());
            Spouse.Deserialize(reader);
        }

    }
}
