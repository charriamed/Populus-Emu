namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using System.Collections.Generic;
    using Stump.Core.IO;

    [Serializable]
    public class PresetUseResultWithMissingIdsMessage : PresetUseResultMessage
    {
        public new const uint Id = 6757;
        public override uint MessageId
        {
            get { return Id; }
        }
        public ushort[] MissingIds { get; set; }

        public PresetUseResultWithMissingIdsMessage(short presetId, sbyte code, ushort[] missingIds)
        {
            this.PresetId = presetId;
            this.Code = code;
            this.MissingIds = missingIds;
        }

        public PresetUseResultWithMissingIdsMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteShort((short)MissingIds.Count());
            for (var missingIdsIndex = 0; missingIdsIndex < MissingIds.Count(); missingIdsIndex++)
            {
                writer.WriteVarUShort(MissingIds[missingIdsIndex]);
            }
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            var missingIdsCount = reader.ReadUShort();
            MissingIds = new ushort[missingIdsCount];
            for (var missingIdsIndex = 0; missingIdsIndex < missingIdsCount; missingIdsIndex++)
            {
                MissingIds[missingIdsIndex] = reader.ReadVarUShort();
            }
        }

    }
}
