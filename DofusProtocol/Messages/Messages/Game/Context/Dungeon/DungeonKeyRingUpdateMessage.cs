namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class DungeonKeyRingUpdateMessage : Message
    {
        public const uint Id = 6296;
        public override uint MessageId
        {
            get { return Id; }
        }
        public ushort DungeonId { get; set; }
        public bool Available { get; set; }

        public DungeonKeyRingUpdateMessage(ushort dungeonId, bool available)
        {
            this.DungeonId = dungeonId;
            this.Available = available;
        }

        public DungeonKeyRingUpdateMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteVarUShort(DungeonId);
            writer.WriteBoolean(Available);
        }

        public override void Deserialize(IDataReader reader)
        {
            DungeonId = reader.ReadVarUShort();
            Available = reader.ReadBoolean();
        }

    }
}
