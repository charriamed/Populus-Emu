namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class NpcDialogCreationMessage : Message
    {
        public const uint Id = 5618;
        public override uint MessageId
        {
            get { return Id; }
        }
        public double MapId { get; set; }
        public int NpcId { get; set; }

        public NpcDialogCreationMessage(double mapId, int npcId)
        {
            this.MapId = mapId;
            this.NpcId = npcId;
        }

        public NpcDialogCreationMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteDouble(MapId);
            writer.WriteInt(NpcId);
        }

        public override void Deserialize(IDataReader reader)
        {
            MapId = reader.ReadDouble();
            NpcId = reader.ReadInt();
        }

    }
}
