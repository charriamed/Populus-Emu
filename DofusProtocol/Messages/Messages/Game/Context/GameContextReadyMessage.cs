namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class GameContextReadyMessage : Message
    {
        public const uint Id = 6071;
        public override uint MessageId
        {
            get { return Id; }
        }
        public double MapId { get; set; }

        public GameContextReadyMessage(double mapId)
        {
            this.MapId = mapId;
        }

        public GameContextReadyMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteDouble(MapId);
        }

        public override void Deserialize(IDataReader reader)
        {
            MapId = reader.ReadDouble();
        }

    }
}
