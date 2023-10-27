namespace Stump.DofusProtocol.Types
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class StatedElement
    {
        public const short Id  = 108;
        public virtual short TypeId
        {
            get { return Id; }
        }
        public int ElementId { get; set; }
        public ushort ElementCellId { get; set; }
        public uint ElementState { get; set; }
        public bool OnCurrentMap { get; set; }

        public StatedElement(int elementId, ushort elementCellId, uint elementState, bool onCurrentMap)
        {
            this.ElementId = elementId;
            this.ElementCellId = elementCellId;
            this.ElementState = elementState;
            this.OnCurrentMap = onCurrentMap;
        }

        public StatedElement() { }

        public virtual void Serialize(IDataWriter writer)
        {
            writer.WriteInt(ElementId);
            writer.WriteVarUShort(ElementCellId);
            writer.WriteVarUInt(ElementState);
            writer.WriteBoolean(OnCurrentMap);
        }

        public virtual void Deserialize(IDataReader reader)
        {
            ElementId = reader.ReadInt();
            ElementCellId = reader.ReadVarUShort();
            ElementState = reader.ReadVarUInt();
            OnCurrentMap = reader.ReadBoolean();
        }

    }
}
