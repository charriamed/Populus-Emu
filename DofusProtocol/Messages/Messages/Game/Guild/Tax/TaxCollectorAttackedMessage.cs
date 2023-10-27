namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class TaxCollectorAttackedMessage : Message
    {
        public const uint Id = 5918;
        public override uint MessageId
        {
            get { return Id; }
        }
        public ushort FirstNameId { get; set; }
        public ushort LastNameId { get; set; }
        public short WorldX { get; set; }
        public short WorldY { get; set; }
        public double MapId { get; set; }
        public ushort SubAreaId { get; set; }
        public BasicGuildInformations Guild { get; set; }

        public TaxCollectorAttackedMessage(ushort firstNameId, ushort lastNameId, short worldX, short worldY, double mapId, ushort subAreaId, BasicGuildInformations guild)
        {
            this.FirstNameId = firstNameId;
            this.LastNameId = lastNameId;
            this.WorldX = worldX;
            this.WorldY = worldY;
            this.MapId = mapId;
            this.SubAreaId = subAreaId;
            this.Guild = guild;
        }

        public TaxCollectorAttackedMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteVarUShort(FirstNameId);
            writer.WriteVarUShort(LastNameId);
            writer.WriteShort(WorldX);
            writer.WriteShort(WorldY);
            writer.WriteDouble(MapId);
            writer.WriteVarUShort(SubAreaId);
            Guild.Serialize(writer);
        }

        public override void Deserialize(IDataReader reader)
        {
            FirstNameId = reader.ReadVarUShort();
            LastNameId = reader.ReadVarUShort();
            WorldX = reader.ReadShort();
            WorldY = reader.ReadShort();
            MapId = reader.ReadDouble();
            SubAreaId = reader.ReadVarUShort();
            Guild = new BasicGuildInformations();
            Guild.Deserialize(reader);
        }

    }
}
