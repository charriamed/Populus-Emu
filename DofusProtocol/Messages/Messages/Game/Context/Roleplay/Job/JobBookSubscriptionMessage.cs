namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using System.Collections.Generic;
    using Stump.Core.IO;

    [Serializable]
    public class JobBookSubscriptionMessage : Message
    {
        public const uint Id = 6593;
        public override uint MessageId
        {
            get { return Id; }
        }
        public JobBookSubscription[] Subscriptions { get; set; }

        public JobBookSubscriptionMessage(JobBookSubscription[] subscriptions)
        {
            this.Subscriptions = subscriptions;
        }

        public JobBookSubscriptionMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort((short)Subscriptions.Count());
            for (var subscriptionsIndex = 0; subscriptionsIndex < Subscriptions.Count(); subscriptionsIndex++)
            {
                var objectToSend = Subscriptions[subscriptionsIndex];
                objectToSend.Serialize(writer);
            }
        }

        public override void Deserialize(IDataReader reader)
        {
            var subscriptionsCount = reader.ReadUShort();
            Subscriptions = new JobBookSubscription[subscriptionsCount];
            for (var subscriptionsIndex = 0; subscriptionsIndex < subscriptionsCount; subscriptionsIndex++)
            {
                var objectToAdd = new JobBookSubscription();
                objectToAdd.Deserialize(reader);
                Subscriptions[subscriptionsIndex] = objectToAdd;
            }
        }

    }
}
