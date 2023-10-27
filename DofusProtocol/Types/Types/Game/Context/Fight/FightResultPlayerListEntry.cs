namespace Stump.DofusProtocol.Types
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using System.Collections.Generic;
    using Stump.Core.IO;

    [Serializable]
    public class FightResultPlayerListEntry : FightResultFighterListEntry
    {
        public new const short Id = 24;
        public override short TypeId
        {
            get { return Id; }
        }
        public ushort Level { get; set; }
        public FightResultAdditionalData[] Additional { get; set; }

        public FightResultPlayerListEntry(ushort outcome, sbyte wave, FightLoot rewards, double objectId, bool alive, ushort level, FightResultAdditionalData[] additional)
        {
            this.Outcome = outcome;
            this.Wave = wave;
            this.Rewards = rewards;
            this.ObjectId = objectId;
            this.Alive = alive;
            this.Level = level;
            this.Additional = additional;
        }

        public FightResultPlayerListEntry() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteVarUShort(Level);
            writer.WriteShort((short)Additional.Count());
            for (var additionalIndex = 0; additionalIndex < Additional.Count(); additionalIndex++)
            {
                var objectToSend = Additional[additionalIndex];
                writer.WriteShort(objectToSend.TypeId);
                objectToSend.Serialize(writer);
            }
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            Level = reader.ReadVarUShort();
            var additionalCount = reader.ReadUShort();
            Additional = new FightResultAdditionalData[additionalCount];
            for (var additionalIndex = 0; additionalIndex < additionalCount; additionalIndex++)
            {
                var objectToAdd = ProtocolTypeManager.GetInstance<FightResultAdditionalData>(reader.ReadShort());
                objectToAdd.Deserialize(reader);
                Additional[additionalIndex] = objectToAdd;
            }
        }

    }
}
