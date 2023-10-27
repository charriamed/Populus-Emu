namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class LockableCodeResultMessage : Message
    {
        public const uint Id = 5672;
        public override uint MessageId
        {
            get { return Id; }
        }
        public sbyte Result { get; set; }

        public LockableCodeResultMessage(sbyte result)
        {
            this.Result = result;
        }

        public LockableCodeResultMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteSByte(Result);
        }

        public override void Deserialize(IDataReader reader)
        {
            Result = reader.ReadSByte();
        }

    }
}
