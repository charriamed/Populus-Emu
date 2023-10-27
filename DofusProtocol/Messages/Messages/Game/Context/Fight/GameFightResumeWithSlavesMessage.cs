namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using System.Collections.Generic;
    using Stump.Core.IO;

    [Serializable]
    public class GameFightResumeWithSlavesMessage : GameFightResumeMessage
    {
        public new const uint Id = 6215;
        public override uint MessageId
        {
            get { return Id; }
        }
        public GameFightResumeSlaveInfo[] SlavesInfo { get; set; }

        public GameFightResumeWithSlavesMessage(FightDispellableEffectExtendedInformations[] effects, GameActionMark[] marks, ushort gameTurn, int fightStart, Idol[] idols, GameFightSpellCooldown[] spellCooldowns, sbyte summonCount, sbyte bombCount, GameFightResumeSlaveInfo[] slavesInfo)
        {
            this.Effects = effects;
            this.Marks = marks;
            this.GameTurn = gameTurn;
            this.FightStart = fightStart;
            this.Idols = idols;
            this.SpellCooldowns = spellCooldowns;
            this.SummonCount = summonCount;
            this.BombCount = bombCount;
            this.SlavesInfo = slavesInfo;
        }

        public GameFightResumeWithSlavesMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteShort((short)SlavesInfo.Count());
            for (var slavesInfoIndex = 0; slavesInfoIndex < SlavesInfo.Count(); slavesInfoIndex++)
            {
                var objectToSend = SlavesInfo[slavesInfoIndex];
                objectToSend.Serialize(writer);
            }
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            var slavesInfoCount = reader.ReadUShort();
            SlavesInfo = new GameFightResumeSlaveInfo[slavesInfoCount];
            for (var slavesInfoIndex = 0; slavesInfoIndex < slavesInfoCount; slavesInfoIndex++)
            {
                var objectToAdd = new GameFightResumeSlaveInfo();
                objectToAdd.Deserialize(reader);
                SlavesInfo[slavesInfoIndex] = objectToAdd;
            }
        }

    }
}
