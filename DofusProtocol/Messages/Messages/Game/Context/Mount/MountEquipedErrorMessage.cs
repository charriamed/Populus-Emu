namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class MountEquipedErrorMessage : Message
    {
        public const uint Id = 5963;
        public override uint MessageId
        {
            get { return Id; }
        }
        public sbyte ErrorType { get; set; }

        public MountEquipedErrorMessage(sbyte errorType)
        {
            this.ErrorType = errorType;
        }

        public MountEquipedErrorMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteSByte(ErrorType);
        }

        public override void Deserialize(IDataReader reader)
        {
            ErrorType = reader.ReadSByte();
        }

    }
}
