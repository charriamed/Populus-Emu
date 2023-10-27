namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class LockableStateUpdateStorageMessage : LockableStateUpdateAbstractMessage
    {
        public new const uint Id = 5669;
        public override uint MessageId
        {
            get { return Id; }
        }
        public double MapId { get; set; }
        public uint ElementId { get; set; }

        public LockableStateUpdateStorageMessage(bool locked, double mapId, uint elementId)
        {
            this.Locked = locked;
            this.MapId = mapId;
            this.ElementId = elementId;
        }

        public LockableStateUpdateStorageMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteDouble(MapId);
            writer.WriteVarUInt(ElementId);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            MapId = reader.ReadDouble();
            ElementId = reader.ReadVarUInt();
        }

    }
}
