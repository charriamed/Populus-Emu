namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class MountRenameRequestMessage : Message
    {
        public const uint Id = 5987;
        public override uint MessageId
        {
            get { return Id; }
        }
        public string Name { get; set; }
        public int MountId { get; set; }

        public MountRenameRequestMessage(string name, int mountId)
        {
            this.Name = name;
            this.MountId = mountId;
        }

        public MountRenameRequestMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteUTF(Name);
            writer.WriteVarInt(MountId);
        }

        public override void Deserialize(IDataReader reader)
        {
            Name = reader.ReadUTF();
            MountId = reader.ReadVarInt();
        }

    }
}
