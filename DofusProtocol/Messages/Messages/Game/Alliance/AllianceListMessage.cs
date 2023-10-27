namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using System.Collections.Generic;
    using Stump.Core.IO;

    [Serializable]
    public class AllianceListMessage : Message
    {
        public const uint Id = 6408;
        public override uint MessageId
        {
            get { return Id; }
        }
        public AllianceFactSheetInformations[] Alliances { get; set; }

        public AllianceListMessage(AllianceFactSheetInformations[] alliances)
        {
            this.Alliances = alliances;
        }

        public AllianceListMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort((short)Alliances.Count());
            for (var alliancesIndex = 0; alliancesIndex < Alliances.Count(); alliancesIndex++)
            {
                var objectToSend = Alliances[alliancesIndex];
                objectToSend.Serialize(writer);
            }
        }

        public override void Deserialize(IDataReader reader)
        {
            var alliancesCount = reader.ReadUShort();
            Alliances = new AllianceFactSheetInformations[alliancesCount];
            for (var alliancesIndex = 0; alliancesIndex < alliancesCount; alliancesIndex++)
            {
                var objectToAdd = new AllianceFactSheetInformations();
                objectToAdd.Deserialize(reader);
                Alliances[alliancesIndex] = objectToAdd;
            }
        }

    }
}
