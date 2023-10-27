namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class GuildModificationNameValidMessage : Message
    {
        public const uint Id = 6327;
        public override uint MessageId
        {
            get { return Id; }
        }
        public string GuildName { get; set; }

        public GuildModificationNameValidMessage(string guildName)
        {
            this.GuildName = guildName;
        }

        public GuildModificationNameValidMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteUTF(GuildName);
        }

        public override void Deserialize(IDataReader reader)
        {
            GuildName = reader.ReadUTF();
        }

    }
}
