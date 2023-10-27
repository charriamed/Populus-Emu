namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class ChangeMapMessage : Message
    {
        public const uint Id = 221;
        public override uint MessageId
        {
            get { return Id; }
        }
        public double MapId { get; set; }
        public bool Autopilot { get; set; }

        public ChangeMapMessage(double mapId, bool autopilot)
        {
            this.MapId = mapId;
            this.Autopilot = autopilot;
        }

        public ChangeMapMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteDouble(MapId);
            writer.WriteBoolean(Autopilot);
        }

        public override void Deserialize(IDataReader reader)
        {
            MapId = reader.ReadDouble();
            Autopilot = reader.ReadBoolean();
        }

    }
}
