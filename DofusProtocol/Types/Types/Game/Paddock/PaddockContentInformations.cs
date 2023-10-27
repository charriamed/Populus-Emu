namespace Stump.DofusProtocol.Types
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using System.Collections.Generic;
    using Stump.Core.IO;

    [Serializable]
    public class PaddockContentInformations : PaddockInformations
    {
        public new const short Id = 183;
        public override short TypeId
        {
            get { return Id; }
        }
        public double PaddockId { get; set; }
        public short WorldX { get; set; }
        public short WorldY { get; set; }
        public double MapId { get; set; }
        public ushort SubAreaId { get; set; }
        public bool Abandonned { get; set; }
        public MountInformationsForPaddock[] MountsInformations { get; set; }

        public PaddockContentInformations(ushort maxOutdoorMount, ushort maxItems, double paddockId, short worldX, short worldY, double mapId, ushort subAreaId, bool abandonned, MountInformationsForPaddock[] mountsInformations)
        {
            this.MaxOutdoorMount = maxOutdoorMount;
            this.MaxItems = maxItems;
            this.PaddockId = paddockId;
            this.WorldX = worldX;
            this.WorldY = worldY;
            this.MapId = mapId;
            this.SubAreaId = subAreaId;
            this.Abandonned = abandonned;
            this.MountsInformations = mountsInformations;
        }

        public PaddockContentInformations() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteDouble(PaddockId);
            writer.WriteShort(WorldX);
            writer.WriteShort(WorldY);
            writer.WriteDouble(MapId);
            writer.WriteVarUShort(SubAreaId);
            writer.WriteBoolean(Abandonned);
            writer.WriteShort((short)MountsInformations.Count());
            for (var mountsInformationsIndex = 0; mountsInformationsIndex < MountsInformations.Count(); mountsInformationsIndex++)
            {
                var objectToSend = MountsInformations[mountsInformationsIndex];
                objectToSend.Serialize(writer);
            }
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            PaddockId = reader.ReadDouble();
            WorldX = reader.ReadShort();
            WorldY = reader.ReadShort();
            MapId = reader.ReadDouble();
            SubAreaId = reader.ReadVarUShort();
            Abandonned = reader.ReadBoolean();
            var mountsInformationsCount = reader.ReadUShort();
            MountsInformations = new MountInformationsForPaddock[mountsInformationsCount];
            for (var mountsInformationsIndex = 0; mountsInformationsIndex < mountsInformationsCount; mountsInformationsIndex++)
            {
                var objectToAdd = new MountInformationsForPaddock();
                objectToAdd.Deserialize(reader);
                MountsInformations[mountsInformationsIndex] = objectToAdd;
            }
        }

    }
}
