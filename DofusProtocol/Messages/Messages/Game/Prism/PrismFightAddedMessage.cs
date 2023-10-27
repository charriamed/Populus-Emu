namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class PrismFightAddedMessage : Message
    {
        public const uint Id = 6452;
        public override uint MessageId
        {
            get { return Id; }
        }
        public PrismFightersInformation Fight { get; set; }

        public PrismFightAddedMessage(PrismFightersInformation fight)
        {
            this.Fight = fight;
        }

        public PrismFightAddedMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            Fight.Serialize(writer);
        }

        public override void Deserialize(IDataReader reader)
        {
            Fight = new PrismFightersInformation();
            Fight.Deserialize(reader);
        }

    }
}
