namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using System.Collections.Generic;
    using Stump.Core.IO;

    [Serializable]
    public class AccessoryPreviewRequestMessage : Message
    {
        public const uint Id = 6518;
        public override uint MessageId
        {
            get { return Id; }
        }
        public ushort[] GenericId { get; set; }

        public AccessoryPreviewRequestMessage(ushort[] genericId)
        {
            this.GenericId = genericId;
        }

        public AccessoryPreviewRequestMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort((short)GenericId.Count());
            for (var genericIdIndex = 0; genericIdIndex < GenericId.Count(); genericIdIndex++)
            {
                writer.WriteVarUShort(GenericId[genericIdIndex]);
            }
        }

        public override void Deserialize(IDataReader reader)
        {
            var genericIdCount = reader.ReadUShort();
            GenericId = new ushort[genericIdCount];
            for (var genericIdIndex = 0; genericIdIndex < genericIdCount; genericIdIndex++)
            {
                GenericId[genericIdIndex] = reader.ReadVarUShort();
            }
        }

    }
}
