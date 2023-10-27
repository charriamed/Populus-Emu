namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class MapRewardRateMessage : Message
    {
        public const uint Id = 6827;
        public override uint MessageId
        {
            get { return Id; }
        }
        public short MapRate { get; set; }
        public short SubAreaRate { get; set; }
        public short TotalRate { get; set; }

        public MapRewardRateMessage(short mapRate, short subAreaRate, short totalRate)
        {
            this.MapRate = mapRate;
            this.SubAreaRate = subAreaRate;
            this.TotalRate = totalRate;
        }

        public MapRewardRateMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteVarShort(MapRate);
            writer.WriteVarShort(SubAreaRate);
            writer.WriteVarShort(TotalRate);
        }

        public override void Deserialize(IDataReader reader)
        {
            MapRate = reader.ReadVarShort();
            SubAreaRate = reader.ReadVarShort();
            TotalRate = reader.ReadVarShort();
        }

    }
}
