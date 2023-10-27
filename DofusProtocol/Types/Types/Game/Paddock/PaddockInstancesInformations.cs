namespace Stump.DofusProtocol.Types
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using System.Collections.Generic;
    using Stump.Core.IO;

    [Serializable]
    public class PaddockInstancesInformations : PaddockInformations
    {
        public new const short Id = 509;
        public override short TypeId
        {
            get { return Id; }
        }
        public PaddockBuyableInformations[] Paddocks { get; set; }

        public PaddockInstancesInformations(ushort maxOutdoorMount, ushort maxItems, PaddockBuyableInformations[] paddocks)
        {
            this.MaxOutdoorMount = maxOutdoorMount;
            this.MaxItems = maxItems;
            this.Paddocks = paddocks;
        }

        public PaddockInstancesInformations() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteShort((short)Paddocks.Count());
            for (var paddocksIndex = 0; paddocksIndex < Paddocks.Count(); paddocksIndex++)
            {
                var objectToSend = Paddocks[paddocksIndex];
                writer.WriteShort(objectToSend.TypeId);
                objectToSend.Serialize(writer);
            }
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            var paddocksCount = reader.ReadUShort();
            Paddocks = new PaddockBuyableInformations[paddocksCount];
            for (var paddocksIndex = 0; paddocksIndex < paddocksCount; paddocksIndex++)
            {
                var objectToAdd = ProtocolTypeManager.GetInstance<PaddockBuyableInformations>(reader.ReadShort());
                objectToAdd.Deserialize(reader);
                Paddocks[paddocksIndex] = objectToAdd;
            }
        }

    }
}
