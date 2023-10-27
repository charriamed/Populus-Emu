namespace Stump.DofusProtocol.Types
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class JobBookSubscription
    {
        public const short Id  = 500;
        public virtual short TypeId
        {
            get { return Id; }
        }
        public sbyte JobId { get; set; }
        public bool Subscribed { get; set; }

        public JobBookSubscription(sbyte jobId, bool subscribed)
        {
            this.JobId = jobId;
            this.Subscribed = subscribed;
        }

        public JobBookSubscription() { }

        public virtual void Serialize(IDataWriter writer)
        {
            writer.WriteSByte(JobId);
            writer.WriteBoolean(Subscribed);
        }

        public virtual void Deserialize(IDataReader reader)
        {
            JobId = reader.ReadSByte();
            Subscribed = reader.ReadBoolean();
        }

    }
}
