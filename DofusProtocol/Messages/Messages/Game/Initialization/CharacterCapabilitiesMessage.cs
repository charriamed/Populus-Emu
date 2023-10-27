namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class CharacterCapabilitiesMessage : Message
    {
        public const uint Id = 6339;
        public override uint MessageId
        {
            get { return Id; }
        }
        public uint GuildEmblemSymbolCategories { get; set; }

        public CharacterCapabilitiesMessage(uint guildEmblemSymbolCategories)
        {
            this.GuildEmblemSymbolCategories = guildEmblemSymbolCategories;
        }

        public CharacterCapabilitiesMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteVarUInt(GuildEmblemSymbolCategories);
        }

        public override void Deserialize(IDataReader reader)
        {
            GuildEmblemSymbolCategories = reader.ReadVarUInt();
        }

    }
}
