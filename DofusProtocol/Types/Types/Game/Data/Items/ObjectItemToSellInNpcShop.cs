namespace Stump.DofusProtocol.Types
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using System.Collections.Generic;
    using Stump.Core.IO;

    [Serializable]
    public class ObjectItemToSellInNpcShop : ObjectItemMinimalInformation
    {
        public new const short Id = 352;
        public override short TypeId
        {
            get { return Id; }
        }
        public ulong ObjectPrice { get; set; }
        public string BuyCriterion { get; set; }

        public ObjectItemToSellInNpcShop(ushort objectGID, ObjectEffect[] effects, ulong objectPrice, string buyCriterion)
        {
            this.ObjectGID = objectGID;
            this.Effects = effects;
            this.ObjectPrice = objectPrice;
            this.BuyCriterion = buyCriterion;
        }

        public ObjectItemToSellInNpcShop() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteVarULong(ObjectPrice);
            writer.WriteUTF(BuyCriterion);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            ObjectPrice = reader.ReadVarULong();
            BuyCriterion = reader.ReadUTF();
        }

    }
}
