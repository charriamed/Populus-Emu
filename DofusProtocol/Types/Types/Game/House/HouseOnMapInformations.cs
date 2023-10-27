namespace Stump.DofusProtocol.Types
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using System.Collections.Generic;
    using Stump.Core.IO;

    [Serializable]
    public class HouseOnMapInformations : HouseInformations
    {
        public new const short Id = 510;
        public override short TypeId
        {
            get { return Id; }
        }
        public int[] DoorsOnMap { get; set; }
        public HouseInstanceInformations[] HouseInstances { get; set; }

        public HouseOnMapInformations(uint houseId, ushort modelId, int[] doorsOnMap, HouseInstanceInformations[] houseInstances)
        {
            this.HouseId = houseId;
            this.ModelId = modelId;
            this.DoorsOnMap = doorsOnMap;
            this.HouseInstances = houseInstances;
        }

        public HouseOnMapInformations() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteShort((short)DoorsOnMap.Count());
            for (var doorsOnMapIndex = 0; doorsOnMapIndex < DoorsOnMap.Count(); doorsOnMapIndex++)
            {
                writer.WriteInt(DoorsOnMap[doorsOnMapIndex]);
            }
            writer.WriteShort((short)HouseInstances.Count());
            for (var houseInstancesIndex = 0; houseInstancesIndex < HouseInstances.Count(); houseInstancesIndex++)
            {
                var objectToSend = HouseInstances[houseInstancesIndex];
                objectToSend.Serialize(writer);
            }
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            var doorsOnMapCount = reader.ReadUShort();
            DoorsOnMap = new int[doorsOnMapCount];
            for (var doorsOnMapIndex = 0; doorsOnMapIndex < doorsOnMapCount; doorsOnMapIndex++)
            {
                DoorsOnMap[doorsOnMapIndex] = reader.ReadInt();
            }
            var houseInstancesCount = reader.ReadUShort();
            HouseInstances = new HouseInstanceInformations[houseInstancesCount];
            for (var houseInstancesIndex = 0; houseInstancesIndex < houseInstancesCount; houseInstancesIndex++)
            {
                var objectToAdd = new HouseInstanceInformations();
                objectToAdd.Deserialize(reader);
                HouseInstances[houseInstancesIndex] = objectToAdd;
            }
        }

    }
}
