namespace Stump.DofusProtocol.Types
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using System.Collections.Generic;
    using Stump.Core.IO;

    [Serializable]
    public class StatsPreset : Preset
    {
        public new const short Id = 521;
        public override short TypeId
        {
            get { return Id; }
        }
        public SimpleCharacterCharacteristicForPreset[] Stats { get; set; }

        public StatsPreset(short objectId, SimpleCharacterCharacteristicForPreset[] stats)
        {
            this.ObjectId = objectId;
            this.Stats = stats;
        }

        public StatsPreset() { }

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
            Stats = new SimpleCharacterCharacteristicForPreset[statsCount];
            for (var statsIndex = 0; statsIndex < statsCount; statsIndex++)
            {
                var objectToAdd = new SimpleCharacterCharacteristicForPreset();
                objectToAdd.Deserialize(reader);
                Stats[statsIndex] = objectToAdd;
            }
        }

    }
}
