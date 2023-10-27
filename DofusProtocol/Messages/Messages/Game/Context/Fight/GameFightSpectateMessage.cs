namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using System.Collections.Generic;
    using Stump.Core.IO;

    [Serializable]
    public class GameFightSpectateMessage : Message
    {
        public const uint Id = 6069;
        public override uint MessageId
        {
            get { return Id; }
        }
        public FightDispellableEffectExtendedInformations[] Effects { get; set; }
        public GameActionMark[] Marks { get; set; }
        public ushort GameTurn { get; set; }
        public int FightStart { get; set; }
        public Idol[] Idols { get; set; }

        public GameFightSpectateMessage(FightDispellableEffectExtendedInformations[] effects, GameActionMark[] marks, ushort gameTurn, int fightStart, Idol[] idols)
        {
            this.Effects = effects;
            this.Marks = marks;
            this.GameTurn = gameTurn;
            this.FightStart = fightStart;
            this.Idols = idols;
        }

        public GameFightSpectateMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort((short)Effects.Count());
            for (var effectsIndex = 0; effectsIndex < Effects.Count(); effectsIndex++)
            {
                var objectToSend = Effects[effectsIndex];
                objectToSend.Serialize(writer);
            }
            writer.WriteShort((short)Marks.Count());
            for (var marksIndex = 0; marksIndex < Marks.Count(); marksIndex++)
            {
                var objectToSend = Marks[marksIndex];
                objectToSend.Serialize(writer);
            }
            writer.WriteVarUShort(GameTurn);
            writer.WriteInt(FightStart);
            writer.WriteShort((short)Idols.Count());
            for (var idolsIndex = 0; idolsIndex < Idols.Count(); idolsIndex++)
            {
                var objectToSend = Idols[idolsIndex];
                objectToSend.Serialize(writer);
            }
        }

        public override void Deserialize(IDataReader reader)
        {
            var effectsCount = reader.ReadUShort();
            Effects = new FightDispellableEffectExtendedInformations[effectsCount];
            for (var effectsIndex = 0; effectsIndex < effectsCount; effectsIndex++)
            {
                var objectToAdd = new FightDispellableEffectExtendedInformations();
                objectToAdd.Deserialize(reader);
                Effects[effectsIndex] = objectToAdd;
            }
            var marksCount = reader.ReadUShort();
            Marks = new GameActionMark[marksCount];
            for (var marksIndex = 0; marksIndex < marksCount; marksIndex++)
            {
                var objectToAdd = new GameActionMark();
                objectToAdd.Deserialize(reader);
                Marks[marksIndex] = objectToAdd;
            }
            GameTurn = reader.ReadVarUShort();
            FightStart = reader.ReadInt();
            var idolsCount = reader.ReadUShort();
            Idols = new Idol[idolsCount];
            for (var idolsIndex = 0; idolsIndex < idolsCount; idolsIndex++)
            {
                var objectToAdd = new Idol();
                objectToAdd.Deserialize(reader);
                Idols[idolsIndex] = objectToAdd;
            }
        }

    }
}
