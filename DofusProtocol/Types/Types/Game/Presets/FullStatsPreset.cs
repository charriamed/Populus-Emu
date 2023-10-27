namespace Stump.DofusProtocol.Types
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using System.Collections.Generic;
    using Stump.Core.IO;

    [Serializable]
    public class FullStatsPreset : Preset
    {
        public new const short Id = 532;
        public override short TypeId
        {
            get { return Id; }
        }
        public CharacterCharacteristicForPreset[] Stats { get; set; }

        public FullStatsPreset(short objectId, CharacterCharacteristicForPreset[] stats)
        {
            this.ObjectId = objectId;
            this.Stats = stats;
        }

        public FullStatsPreset() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteShort((short)Stats.Count());
            for (var statsIndex = 0; statsIndex < Stats.Count(); statsIndex++)
            {
                var objectToSend = Stats[statsIndex];
                objectToSend.Serialize(writer);
            }
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            var statsCount = reader.ReadUShort();
            Stats = new CharacterCharacteristicForPreset[statsCount];
            for (var statsIndex = 0; statsIndex < statsCount; statsIndex++)
            {
                var objectToAdd = new CharacterCharacteristicForPreset();
                objectToAdd.Deserialize(reader);
                Stats[statsIndex] = objectToAdd;
            }
        }

    }
}
