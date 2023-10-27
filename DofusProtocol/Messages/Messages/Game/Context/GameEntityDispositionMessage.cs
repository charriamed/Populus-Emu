namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class GameEntityDispositionMessage : Message
    {
        public const uint Id = 5693;
        public override uint MessageId
        {
            get { return Id; }
        }
        public IdentifiedEntityDispositionInformations Disposition { get; set; }

        public GameEntityDispositionMessage(IdentifiedEntityDispositionInformations disposition)
        {
            this.Disposition = disposition;
        }

        public GameEntityDispositionMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            Disposition.Serialize(writer);
        }

        public override void Deserialize(IDataReader reader)
        {
            Disposition = new IdentifiedEntityDispositionInformations();
            Disposition.Deserialize(reader);
        }

    }
}
