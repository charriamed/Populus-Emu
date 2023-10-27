namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class GuildLevelUpMessage : Message
    {
        public const uint Id = 6062;
        public override uint MessageId
        {
            get { return Id; }
        }
        public byte NewLevel { get; set; }

        public GuildLevelUpMessage(byte newLevel)
        {
            this.NewLevel = newLevel;
        }

        public GuildLevelUpMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteByte(NewLevel);
        }

        public override void Deserialize(IDataReader reader)
        {
            NewLevel = reader.ReadByte();
        }

    }
}
