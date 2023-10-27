namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using System.Collections.Generic;
    using Stump.Core.IO;

    [Serializable]
    public class TreasureHuntShowLegendaryUIMessage : Message
    {
        public const uint Id = 6498;
        public override uint MessageId
        {
            get { return Id; }
        }
        public ushort[] AvailableLegendaryIds { get; set; }

        public TreasureHuntShowLegendaryUIMessage(ushort[] availableLegendaryIds)
        {
            this.AvailableLegendaryIds = availableLegendaryIds;
        }

        public TreasureHuntShowLegendaryUIMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort((short)AvailableLegendaryIds.Count());
            for (var availableLegendaryIdsIndex = 0; availableLegendaryIdsIndex < AvailableLegendaryIds.Count(); availableLegendaryIdsIndex++)
            {
                writer.WriteVarUShort(AvailableLegendaryIds[availableLegendaryIdsIndex]);
            }
        }

        public override void Deserialize(IDataReader reader)
        {
            var availableLegendaryIdsCount = reader.ReadUShort();
            AvailableLegendaryIds = new ushort[availableLegendaryIdsCount];
            for (var availableLegendaryIdsIndex = 0; availableLegendaryIdsIndex < availableLegendaryIdsCount; availableLegendaryIdsIndex++)
            {
                AvailableLegendaryIds[availableLegendaryIdsIndex] = reader.ReadVarUShort();
            }
        }

    }
}
