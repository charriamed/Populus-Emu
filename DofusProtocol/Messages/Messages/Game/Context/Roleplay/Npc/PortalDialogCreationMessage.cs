namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class PortalDialogCreationMessage : NpcDialogCreationMessage
    {
        public new const uint Id = 6737;
        public override uint MessageId
        {
            get { return Id; }
        }
        public int Type { get; set; }

        public PortalDialogCreationMessage(double mapId, int npcId, int type)
        {
            this.MapId = mapId;
            this.NpcId = npcId;
            this.Type = type;
        }

        public PortalDialogCreationMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteInt(Type);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            Type = reader.ReadInt();
        }

    }
}
