namespace Stump.DofusProtocol.Types
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using System.Collections.Generic;
    using Stump.Core.IO;

    [Serializable]
    public class ObjectItemToSellInBid : ObjectItemToSell
    {
        public new const short Id = 164;
        public override short TypeId
        {
            get { return Id; }
        }
        public int UnsoldDelay { get; set; }

        public ObjectItemToSellInBid(ushort objectGID, ObjectEffect[] effects, uint objectUID, uint quantity, ulong objectPrice, int unsoldDelay)
        {
            this.ObjectGID = objectGID;
            this.Effects = effects;
            this.ObjectUID = objectUID;
            this.Quantity = quantity;
            this.ObjectPrice = objectPrice;
            this.UnsoldDelay = unsoldDelay;
        }

        public ObjectItemToSellInBid() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteInt(UnsoldDelay);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            UnsoldDelay = reader.ReadInt();
        }

    }
}
