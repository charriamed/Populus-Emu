namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class AllianceKickRequestMessage : Message
    {
        public const uint Id = 6400;
        public override uint MessageId
        {
            get { return Id; }
        }
        public uint KickedId { get; set; }

        public AllianceKickRequestMessage(uint kickedId)
        {
            this.KickedId = kickedId;
        }

        public AllianceKickRequestMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteVarUInt(KickedId);
        }

        public override void Deserialize(IDataReader reader)
        {
            KickedId = reader.ReadVarUInt();
        }

    }
}
