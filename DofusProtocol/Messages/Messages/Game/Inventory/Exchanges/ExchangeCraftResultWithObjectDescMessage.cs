namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class ExchangeCraftResultWithObjectDescMessage : ExchangeCraftResultMessage
    {
        public new const uint Id = 5999;
        public override uint MessageId
        {
            get { return Id; }
        }
        public ObjectItemNotInContainer ObjectInfo { get; set; }

        public ExchangeCraftResultWithObjectDescMessage(sbyte craftResult, ObjectItemNotInContainer objectInfo)
        {
            this.CraftResult = craftResult;
            this.ObjectInfo = objectInfo;
        }

        public ExchangeCraftResultWithObjectDescMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            ObjectInfo.Serialize(writer);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            ObjectInfo = new ObjectItemNotInContainer();
            ObjectInfo.Deserialize(reader);
        }

    }
}
