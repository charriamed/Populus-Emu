namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class GuildSpellUpgradeRequestMessage : Message
    {
        public const uint Id = 5699;
        public override uint MessageId
        {
            get { return Id; }
        }
        public int SpellId { get; set; }

        public GuildSpellUpgradeRequestMessage(int spellId)
        {
            this.SpellId = spellId;
        }

        public GuildSpellUpgradeRequestMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteInt(SpellId);
        }

        public override void Deserialize(IDataReader reader)
        {
            SpellId = reader.ReadInt();
        }

    }
}
