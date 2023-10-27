namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using System.Collections.Generic;
    using Stump.Core.IO;

    [Serializable]
    public class GuildFactsMessage : Message
    {
        public const uint Id = 6415;
        public override uint MessageId
        {
            get { return Id; }
        }
        public GuildFactSheetInformations Infos { get; set; }
        public int CreationDate { get; set; }
        public ushort NbTaxCollectors { get; set; }
        public CharacterMinimalGuildPublicInformations[] Members { get; set; }

        public GuildFactsMessage(GuildFactSheetInformations infos, int creationDate, ushort nbTaxCollectors, CharacterMinimalGuildPublicInformations[] members)
        {
            this.Infos = infos;
            this.CreationDate = creationDate;
            this.NbTaxCollectors = nbTaxCollectors;
            this.Members = members;
        }

        public GuildFactsMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort(Infos.TypeId);
            Infos.Serialize(writer);
            writer.WriteInt(CreationDate);
            writer.WriteVarUShort(NbTaxCollectors);
            writer.WriteShort((short)Members.Count());
            for (var membersIndex = 0; membersIndex < Members.Count(); membersIndex++)
            {
                var objectToSend = Members[membersIndex];
                objectToSend.Serialize(writer);
            }
        }

        public override void Deserialize(IDataReader reader)
        {
            Infos = ProtocolTypeManager.GetInstance<GuildFactSheetInformations>(reader.ReadShort());
            Infos.Deserialize(reader);
            CreationDate = reader.ReadInt();
            NbTaxCollectors = reader.ReadVarUShort();
            var membersCount = reader.ReadUShort();
            Members = new CharacterMinimalGuildPublicInformations[membersCount];
            for (var membersIndex = 0; membersIndex < membersCount; membersIndex++)
            {
                var objectToAdd = new CharacterMinimalGuildPublicInformations();
                objectToAdd.Deserialize(reader);
                Members[membersIndex] = objectToAdd;
            }
        }

    }
}
