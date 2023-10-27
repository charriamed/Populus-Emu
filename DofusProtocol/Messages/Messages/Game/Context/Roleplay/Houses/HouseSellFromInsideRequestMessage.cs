namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class HouseSellFromInsideRequestMessage : HouseSellRequestMessage
    {
        public new const uint Id = 5884;
        public override uint MessageId
        {
            get { return Id; }
        }

        public HouseSellFromInsideRequestMessage(int instanceId, ulong amount, bool forSale)
        {
            this.InstanceId = instanceId;
            this.Amount = amount;
            this.ForSale = forSale;
        }

        public HouseSellFromInsideRequestMessage() { }

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
