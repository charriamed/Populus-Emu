namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using System.Collections.Generic;
    using Stump.Core.IO;

    [Serializable]
    public class AllianceFactsMessage : Message
    {
        public const uint Id = 6414;
        public override uint MessageId
        {
            get { return Id; }
        }
        public AllianceFactSheetInformations Infos { get; set; }
        public GuildInAllianceInformations[] Guilds { get; set; }
        public ushort[] ControlledSubareaIds { get; set; }
        public ulong LeaderCharacterId { get; set; }
        public string LeaderCharacterName { get; set; }

        public AllianceFactsMessage(AllianceFactSheetInformations infos, GuildInAllianceInformations[] guilds, ushort[] controlledSubareaIds, ulong leaderCharacterId, string leaderCharacterName)
        {
            this.Infos = infos;
            this.Guilds = guilds;
            this.ControlledSubareaIds = controlledSubareaIds;
            this.LeaderCharacterId = leaderCharacterId;
            this.LeaderCharacterName = leaderCharacterName;
        }

        public AllianceFactsMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort(Infos.TypeId);
            Infos.Serialize(writer);
            writer.WriteShort((short)Guilds.Count());
            for (var guildsIndex = 0; guildsIndex < Guilds.Count(); guildsIndex++)
            {
                var objectToSend = Guilds[guildsIndex];
                objectToSend.Serialize(writer);
            }
            writer.WriteShort((short)ControlledSubareaIds.Count());
            for (var controlledSubareaIdsIndex = 0; controlledSubareaIdsIndex < ControlledSubareaIds.Count(); controlledSubareaIdsIndex++)
            {
                writer.WriteVarUShort(ControlledSubareaIds[controlledSubareaIdsIndex]);
            }
            writer.WriteVarULong(LeaderCharacterId);
            writer.WriteUTF(LeaderCharacterName);
        }

        public override void Deserialize(IDataReader reader)
        {
            Infos = ProtocolTypeManager.GetInstance<AllianceFactSheetInformations>(reader.ReadShort());
            Infos.Deserialize(reader);
            var guildsCount = reader.ReadUShort();
            Guilds = new GuildInAllianceInformations[guildsCount];
            for (var guildsIndex = 0; guildsIndex < guildsCount; guildsIndex++)
            {
                var objectToAdd = new GuildInAllianceInformations();
                objectToAdd.Deserialize(reader);
                Guilds[guildsIndex] = objectToAdd;
            }
            var controlledSubareaIdsCount = reader.ReadUShort();
            ControlledSubareaIds = new ushort[controlledSubareaIdsCount];
            for (var controlledSubareaIdsIndex = 0; controlledSubareaIdsIndex < controlledSubareaIdsCount; controlledSubareaIdsIndex++)
            {
                ControlledSubareaIds[controlledSubareaIdsIndex] = reader.ReadVarUShort();
            }
            LeaderCharacterId = reader.ReadVarULong();
            LeaderCharacterName = reader.ReadUTF();
        }

    }
}
