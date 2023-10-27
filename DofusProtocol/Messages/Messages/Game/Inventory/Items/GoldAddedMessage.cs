namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class GoldAddedMessage : Message
    {
        public const uint Id = 6030;
        public override uint MessageId
        {
            get { return Id; }
        }
        public GoldItem Gold { get; set; }

        public GoldAddedMessage(GoldItem gold)
        {
            this.Gold = gold;
        }

        public GoldAddedMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            Gold.Serialize(writer);
        }

        public override void Deserialize(IDataReader reader)
        {
            Gold = new GoldItem();
            Gold.Deserialize(reader);
        }

    }
}
