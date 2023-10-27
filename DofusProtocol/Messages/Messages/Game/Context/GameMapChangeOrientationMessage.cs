namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class GameMapChangeOrientationMessage : Message
    {
        public const uint Id = 946;
        public override uint MessageId
        {
            get { return Id; }
        }
        public ActorOrientation Orientation { get; set; }

        public GameMapChangeOrientationMessage(ActorOrientation orientation)
        {
            this.Orientation = orientation;
        }

        public GameMapChangeOrientationMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            Orientation.Serialize(writer);
        }

        public override void Deserialize(IDataReader reader)
        {
            Orientation = new ActorOrientation();
            Orientation.Deserialize(reader);
        }

    }
}
