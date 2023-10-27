namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using System.Collections.Generic;
    using Stump.Core.IO;

    [Serializable]
    public class ExchangeBidHouseInListUpdatedMessage : ExchangeBidHouseInListAddedMessage
    {
        public new const uint Id = 6337;
        public override uint MessageId
        {
            get { return Id; }
        }

        public ExchangeBidHouseInListUpdatedMessage(int itemUID, int objGenericId, ObjectEffect[] effects, ulong[] prices)
        {
            this.ItemUID = itemUID;
            this.ObjGenericId = objGenericId;
            this.Effects = effects;
            this.Prices = prices;
        }

        public ExchangeBidHouseInListUpdatedMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
        }

    }
}
