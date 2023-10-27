namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using System.Collections.Generic;
    using Stump.Core.IO;

    [Serializable]
    public class BreachStateMessage : Message
    {
        public const uint Id = 6799;
        public override uint MessageId
        {
            get { return Id; }
        }
        public CharacterMinimalInformations Owner { get; set; }
        public ObjectEffectInteger[] Bonuses { get; set; }
        public uint Bugdet { get; set; }
        public bool Saved { get; set; }

        public BreachStateMessage(CharacterMinimalInformations owner, ObjectEffectInteger[] bonuses, uint bugdet, bool saved)
        {
            this.Owner = owner;
            this.Bonuses = bonuses;
            this.Bugdet = bugdet;
            this.Saved = saved;
        }

        public BreachStateMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            Owner.Serialize(writer);
            writer.WriteShort((short)Bonuses.Count());
            for (var bonusesIndex = 0; bonusesIndex < Bonuses.Count(); bonusesIndex++)
            {
                var objectToSend = Bonuses[bonusesIndex];
                objectToSend.Serialize(writer);
            }
            writer.WriteVarUInt(Bugdet);
            writer.WriteBoolean(Saved);
        }

        public override void Deserialize(IDataReader reader)
        {
            Owner = new CharacterMinimalInformations();
            Owner.Deserialize(reader);
            var bonusesCount = reader.ReadUShort();
            Bonuses = new ObjectEffectInteger[bonusesCount];
            for (var bonusesIndex = 0; bonusesIndex < bonusesCount; bonusesIndex++)
            {
                var objectToAdd = new ObjectEffectInteger();
                objectToAdd.Deserialize(reader);
                Bonuses[bonusesIndex] = objectToAdd;
            }
            Bugdet = reader.ReadVarUInt();
            Saved = reader.ReadBoolean();
        }

    }
}
