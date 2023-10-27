namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using System.Collections.Generic;
    using Stump.Core.IO;

    [Serializable]
    public class GuildInformationsMembersMessage : Message
    {
        public const uint Id = 5558;
        public override uint MessageId
        {
            get { return Id; }
        }
        public GuildMember[] Members { get; set; }

        public GuildInformationsMembersMessage(GuildMember[] members)
        {
            this.Members = members;
        }

        public GuildInformationsMembersMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort((short)Members.Count());
            for (var membersIndex = 0; membersIndex < Members.Count(); membersIndex++)
            {
                var objectToSend = Members[membersIndex];
                objectToSend.Serialize(writer);
            }
        }

        public override void Deserialize(IDataReader reader)
        {
            var membersCount = reader.ReadUShort();
            Members = new GuildMember[membersCount];
            for (var membersIndex = 0; membersIndex < membersCount; membersIndex++)
            {
                var objectToAdd = new GuildMember();
                objectToAdd.Deserialize(reader);
                Members[membersIndex] = objectToAdd;
            }
        }

    }
}
