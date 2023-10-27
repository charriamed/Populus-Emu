namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class GuestModeMessage : Message
    {
        public const uint Id = 6505;
        public override uint MessageId
        {
            get { return Id; }
        }
        public bool Active { get; set; }

        public GuestModeMessage(bool active)
        {
            this.Active = active;
        }

        public GuestModeMessage() { }

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
