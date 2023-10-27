namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class LockableStateUpdateAbstractMessage : Message
    {
        public const uint Id = 5671;
        public override uint MessageId
        {
            get { return Id; }
        }
        public bool Locked { get; set; }

        public LockableStateUpdateAbstractMessage(bool locked)
        {
            this.Locked = locked;
        }

        public LockableStateUpdateAbstractMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteBoolean(Locked);
        }

        public override void Deserialize(IDataReader reader)
        {
            Locked = reader.ReadBoolean();
        }

    }
}
