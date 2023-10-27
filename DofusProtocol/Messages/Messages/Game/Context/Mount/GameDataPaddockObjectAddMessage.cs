namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class GameDataPaddockObjectAddMessage : Message
    {
        public const uint Id = 5990;
        public override uint MessageId
        {
            get { return Id; }
        }
        public PaddockItem PaddockItemDescription { get; set; }

        public GameDataPaddockObjectAddMessage(PaddockItem paddockItemDescription)
        {
            this.PaddockItemDescription = paddockItemDescription;
        }

        public GameDataPaddockObjectAddMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            PaddockItemDescription.Serialize(writer);
        }

        public override void Deserialize(IDataReader reader)
        {
            PaddockItemDescription = new PaddockItem();
            PaddockItemDescription.Deserialize(reader);
        }

    }
}
