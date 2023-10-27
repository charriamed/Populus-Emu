namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using System.Collections.Generic;
    using Stump.Core.IO;

    [Serializable]
    public class GameFightResumeMessage : GameFightSpectateMessage
    {
        public new const uint Id = 6067;
        public override uint MessageId
        {
            get { return Id; }
        }
        public GameFightSpellCooldown[] SpellCooldowns { get; set; }
        public sbyte SummonCount { get; set; }
        public sbyte BombCount { get; set; }

        public GameFightResumeMessage(FightDispellableEffectExtendedInformations[] effects, GameActionMark[] marks, ushort gameTurn, int fightStart, Idol[] idols, GameFightSpellCooldown[] spellCooldowns, sbyte summonCount, sbyte bombCount)
        {
            this.Effects = effects;
            this.Marks = marks;
            this.GameTurn = gameTurn;
            this.FightStart = fightStart;
            this.Idols = idols;
            this.SpellCooldowns = spellCooldowns;
            this.SummonCount = summonCount;
            this.BombCount = bombCount;
        }

        public GameFightResumeMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteShort((short)SpellCooldowns.Count());
            for (var spellCooldownsIndex = 0; spellCooldownsIndex < SpellCooldowns.Count(); spellCooldownsIndex++)
            {
                var objectToSend = SpellCooldowns[spellCooldownsIndex];
                objectToSend.Serialize(writer);
            }
            writer.WriteSByte(SummonCount);
            writer.WriteSByte(BombCount);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            var spellCooldownsCount = reader.ReadUShort();
            SpellCooldowns = new GameFightSpellCooldown[spellCooldownsCount];
            for (var spellCooldownsIndex = 0; spellCooldownsIndex < spellCooldownsCount; spellCooldownsIndex++)
            {
                var objectToAdd = new GameFightSpellCooldown();
                objectToAdd.Deserialize(reader);
                SpellCooldowns[spellCooldownsIndex] = objectToAdd;
            }
            SummonCount = reader.ReadSByte();
            BombCount = reader.ReadSByte();
        }

    }
}
