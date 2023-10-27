namespace Stump.DofusProtocol.Types
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class GameFightSpellCooldown
    {
        public const short Id  = 205;
        public virtual short TypeId
        {
            get { return Id; }
        }
        public int SpellId { get; set; }
        public sbyte Cooldown { get; set; }

        public GameFightSpellCooldown(int spellId, sbyte cooldown)
        {
            this.SpellId = spellId;
            this.Cooldown = cooldown;
        }

        public GameFightSpellCooldown() { }

        public virtual void Serialize(IDataWriter writer)
        {
            writer.WriteInt(SpellId);
            writer.WriteSByte(Cooldown);
        }

        public virtual void Deserialize(IDataReader reader)
        {
            SpellId = reader.ReadInt();
            Cooldown = reader.ReadSByte();
        }

    }
}
