namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class SubscriptionZoneMessage : Message
    {
        public const uint Id = 5573;
        public override uint MessageId
        {
            get { return Id; }
        }
        public bool Active { get; set; }

        public SubscriptionZoneMessage(bool active)
        {
            this.Active = active;
        }

        public SubscriptionZoneMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteBoolean(Active);
        }

        public override void Deserialize(IDataReader reader)
        {
            Active = reader.ReadBoolean();
        }

    }
}
