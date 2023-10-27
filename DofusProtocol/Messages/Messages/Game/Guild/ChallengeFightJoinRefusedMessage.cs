namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class ChallengeFightJoinRefusedMessage : Message
    {
        public const uint Id = 5908;
        public override uint MessageId
        {
            get { return Id; }
        }
        public ulong PlayerId { get; set; }
        public sbyte Reason { get; set; }

        public ChallengeFightJoinRefusedMessage(ulong playerId, sbyte reason)
        {
            this.PlayerId = playerId;
            this.Reason = reason;
        }

        public ChallengeFightJoinRefusedMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteVarULong(PlayerId);
            writer.WriteSByte(Reason);
        }

        public override void Deserialize(IDataReader reader)
        {
            PlayerId = reader.ReadVarULong();
            Reason = reader.ReadSByte();
        }

    }
}
