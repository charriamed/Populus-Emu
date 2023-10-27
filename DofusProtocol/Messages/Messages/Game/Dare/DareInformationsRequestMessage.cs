namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class DareInformationsRequestMessage : Message
    {
        public const uint Id = 6659;
        public override uint MessageId
        {
            get { return Id; }
        }
        public double DareId { get; set; }

        public DareInformationsRequestMessage(double dareId)
        {
            this.DareId = dareId;
        }

        public DareInformationsRequestMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteDouble(DareId);
        }

        public override void Deserialize(IDataReader reader)
        {
            DareId = reader.ReadDouble();
        }

    }
}
