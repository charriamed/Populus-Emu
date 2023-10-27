namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class MountEmoteIconUsedOkMessage : Message
    {
        public const uint Id = 5978;
        public override uint MessageId
        {
            get { return Id; }
        }
        public int MountId { get; set; }
        public sbyte ReactionType { get; set; }

        public MountEmoteIconUsedOkMessage(int mountId, sbyte reactionType)
        {
            this.MountId = mountId;
            this.ReactionType = reactionType;
        }

        public MountEmoteIconUsedOkMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteVarInt(MountId);
            writer.WriteSByte(ReactionType);
        }

        public override void Deserialize(IDataReader reader)
        {
            MountId = reader.ReadVarInt();
            ReactionType = reader.ReadSByte();
        }

    }
}
