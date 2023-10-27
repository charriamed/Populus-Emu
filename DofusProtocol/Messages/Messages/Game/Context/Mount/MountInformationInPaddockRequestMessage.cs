namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class MountInformationInPaddockRequestMessage : Message
    {
        public const uint Id = 5975;
        public override uint MessageId
        {
            get { return Id; }
        }
        public int MapRideId { get; set; }

        public MountInformationInPaddockRequestMessage(int mapRideId)
        {
            this.MapRideId = mapRideId;
        }

        public MountInformationInPaddockRequestMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteVarInt(MapRideId);
        }

        public override void Deserialize(IDataReader reader)
        {
            MapRideId = reader.ReadVarInt();
        }

    }
}
