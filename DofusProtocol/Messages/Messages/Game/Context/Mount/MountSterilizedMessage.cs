namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class MountSterilizedMessage : Message
    {
        public const uint Id = 5977;
        public override uint MessageId
        {
            get { return Id; }
        }
        public int MountId { get; set; }

        public MountSterilizedMessage(int mountId)
        {
            this.MountId = mountId;
        }

        public MountSterilizedMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteVarInt(MountId);
        }

        public override void Deserialize(IDataReader reader)
        {
            MountId = reader.ReadVarInt();
        }

    }
}
