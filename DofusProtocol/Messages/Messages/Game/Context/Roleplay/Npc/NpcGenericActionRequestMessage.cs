namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class NpcGenericActionRequestMessage : Message
    {
        public const uint Id = 5898;
        public override uint MessageId
        {
            get { return Id; }
        }
        public int NpcId { get; set; }
        public sbyte NpcActionId { get; set; }
        public double NpcMapId { get; set; }

        public NpcGenericActionRequestMessage(int npcId, sbyte npcActionId, double npcMapId)
        {
            this.NpcId = npcId;
            this.NpcActionId = npcActionId;
            this.NpcMapId = npcMapId;
        }

        public NpcGenericActionRequestMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteInt(NpcId);
            writer.WriteSByte(NpcActionId);
            writer.WriteDouble(NpcMapId);
        }

        public override void Deserialize(IDataReader reader)
        {
            NpcId = reader.ReadInt();
            NpcActionId = reader.ReadSByte();
            NpcMapId = reader.ReadDouble();
        }

    }
}
