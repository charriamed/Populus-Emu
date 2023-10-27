namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class HouseLockFromInsideRequestMessage : LockableChangeCodeMessage
    {
        public new const uint Id = 5885;
        public override uint MessageId
        {
            get { return Id; }
        }

        public HouseLockFromInsideRequestMessage(string code)
        {
            this.Code = code;
        }

        public HouseLockFromInsideRequestMessage() { }

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
