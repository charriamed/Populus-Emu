namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using System.Collections.Generic;
    using Stump.Core.IO;

    [Serializable]
    public class ExchangeMountsStableBornAddMessage : ExchangeMountsStableAddMessage
    {
        public new const uint Id = 6557;
        public override uint MessageId
        {
            get { return Id; }
        }

        public ExchangeMountsStableBornAddMessage(MountClientData[] mountDescription)
        {
            this.MountDescription = mountDescription;
        }

        public ExchangeMountsStableBornAddMessage() { }

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
