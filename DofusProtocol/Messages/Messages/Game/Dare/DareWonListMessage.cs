namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using System.Collections.Generic;
    using Stump.Core.IO;

    [Serializable]
    public class DareWonListMessage : Message
    {
        public const uint Id = 6682;
        public override uint MessageId
        {
            get { return Id; }
        }
        public double[] DareId { get; set; }

        public DareWonListMessage(double[] dareId)
        {
            this.DareId = dareId;
        }

        public DareWonListMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort((short)DareId.Count());
            for (var dareIdIndex = 0; dareIdIndex < DareId.Count(); dareIdIndex++)
            {
                writer.WriteDouble(DareId[dareIdIndex]);
            }
        }

        public override void Deserialize(IDataReader reader)
        {
            var dareIdCount = reader.ReadUShort();
            DareId = new double[dareIdCount];
            for (var dareIdIndex = 0; dareIdIndex < dareIdCount; dareIdIndex++)
            {
                DareId[dareIdIndex] = reader.ReadDouble();
            }
        }

    }
}
