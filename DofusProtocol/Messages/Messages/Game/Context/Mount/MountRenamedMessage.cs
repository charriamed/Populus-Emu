namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class MountRenamedMessage : Message
    {
        public const uint Id = 5983;
        public override uint MessageId
        {
            get { return Id; }
        }
        public int MountId { get; set; }
        public string Name { get; set; }

        public MountRenamedMessage(int mountId, string name)
        {
            this.MountId = mountId;
            this.Name = name;
        }

        public MountRenamedMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteVarInt(MountId);
            writer.WriteUTF(Name);
        }

        public override void Deserialize(IDataReader reader)
        {
            MountId = reader.ReadVarInt();
            Name = reader.ReadUTF();
        }

    }
}
