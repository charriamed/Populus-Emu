namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using System.Collections.Generic;
    using Stump.Core.IO;

    [Serializable]
    public class AllianceInsiderInfoMessage : Message
    {
        public const uint Id = 6403;
        public override uint MessageId
        {
            get { return Id; }
        }
        public AllianceFactSheetInformations AllianceInfos { get; set; }
        public GuildInsiderFactSheetInformations[] Guilds { get; set; }
        public PrismSubareaEmptyInfo[] Prisms { get; set; }

        public AllianceInsiderInfoMessage(AllianceFactSheetInformations allianceInfos, GuildInsiderFactSheetInformations[] guilds, PrismSubareaEmptyInfo[] prisms)
        {
            this.AllianceInfos = allianceInfos;
            this.Guilds = guilds;
            this.Prisms = prisms;
        }

        public AllianceInsiderInfoMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            AllianceInfos.Serialize(writer);
            writer.WriteShort((short)Guilds.Count());
            for (var guildsIndex = 0; guildsIndex < Guilds.Count(); guildsIndex++)
            {
                var objectToSend = Guilds[guildsIndex];
                objectToSend.Serialize(writer);
            }
            writer.WriteShort((short)Prisms.Count());
            for (var prismsIndex = 0; prismsIndex < Prisms.Count(); prismsIndex++)
            {
                var objectToSend = Prisms[prismsIndex];
                writer.WriteShort(objectToSend.TypeId);
                objectToSend.Serialize(writer);
            }
        }

        public override void Deserialize(IDataReader reader)
        {
            AllianceInfos = new AllianceFactSheetInformations();
            AllianceInfos.Deserialize(reader);
            var guildsCount = reader.ReadUShort();
            Guilds = new GuildInsiderFactSheetInformations[guildsCount];
            for (var guildsIndex = 0; guildsIndex < guildsCount; guildsIndex++)
            {
                var objectToAdd = new GuildInsiderFactSheetInformations();
                objectToAdd.Deserialize(reader);
                Guilds[guildsIndex] = objectToAdd;
            }
            var prismsCount = reader.ReadUShort();
            Prisms = new PrismSubareaEmptyInfo[prismsCount];
            for (var prismsIndex = 0; prismsIndex < prismsCount; prismsIndex++)
            {
                var objectToAdd = ProtocolTypeManager.GetInstance<PrismSubareaEmptyInfo>(reader.ReadShort());
                objectToAdd.Deserialize(reader);
                Prisms[prismsIndex] = objectToAdd;
            }
        }

    }
}
