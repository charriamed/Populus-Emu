namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class BasicStatMessage : Message
    {
        public const uint Id = 6530;
        public override uint MessageId
        {
            get { return Id; }
        }
        public double TimeSpent { get; set; }
        public ushort StatId { get; set; }

        public BasicStatMessage(double timeSpent, ushort statId)
        {
            this.TimeSpent = timeSpent;
            this.StatId = statId;
        }

        public BasicStatMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteDouble(TimeSpent);
            writer.WriteVarUShort(StatId);
        }

        public override void Deserialize(IDataReader reader)
        {
            TimeSpent = reader.ReadDouble();
            StatId = reader.ReadVarUShort();
        }

    }
}
