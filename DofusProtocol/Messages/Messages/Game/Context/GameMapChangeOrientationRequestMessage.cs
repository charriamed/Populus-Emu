namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class GameMapChangeOrientationRequestMessage : Message
    {
        public const uint Id = 945;
        public override uint MessageId
        {
            get { return Id; }
        }
        public sbyte Direction { get; set; }

        public GameMapChangeOrientationRequestMessage(sbyte direction)
        {
            this.Direction = direction;
        }

        public GameMapChangeOrientationRequestMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteSByte(Direction);
        }

        public override void Deserialize(IDataReader reader)
        {
            Direction = reader.ReadSByte();
        }

    }
}
