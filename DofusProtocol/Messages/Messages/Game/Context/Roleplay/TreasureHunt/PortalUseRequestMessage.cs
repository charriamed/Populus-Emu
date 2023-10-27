namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class PortalUseRequestMessage : Message
    {
        public const uint Id = 6492;
        public override uint MessageId
        {
            get { return Id; }
        }
        public uint PortalId { get; set; }

        public PortalUseRequestMessage(uint portalId)
        {
            this.PortalId = portalId;
        }

        public PortalUseRequestMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteVarUInt(PortalId);
        }

        public override void Deserialize(IDataReader reader)
        {
            PortalId = reader.ReadVarUInt();
        }

    }
}
