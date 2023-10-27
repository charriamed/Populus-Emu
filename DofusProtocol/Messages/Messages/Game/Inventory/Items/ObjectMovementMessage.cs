namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class ObjectMovementMessage : Message
    {
        public const uint Id = 3010;
        public override uint MessageId
        {
            get { return Id; }
        }
        public uint ObjectUID { get; set; }
        public short Position { get; set; }

        public ObjectMovementMessage(uint objectUID, short position)
        {
            this.ObjectUID = objectUID;
            this.Position = position;
        }

        public ObjectMovementMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteVarUInt(ObjectUID);
            writer.WriteShort(Position);
        }

        public override void Deserialize(IDataReader reader)
        {
            ObjectUID = reader.ReadVarUInt();
            Position = reader.ReadShort();
        }

    }
}
