namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class MountDataErrorMessage : Message
    {
        public const uint Id = 6172;
        public override uint MessageId
        {
            get { return Id; }
        }
        public sbyte Reason { get; set; }

        public MountDataErrorMessage(sbyte reason)
        {
            this.Reason = reason;
        }

        public MountDataErrorMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteSByte(Reason);
        }

        public override void Deserialize(IDataReader reader)
        {
            Reason = reader.ReadSByte();
        }

    }
}
