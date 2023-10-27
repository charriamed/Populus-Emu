namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class ChallengeResultMessage : Message
    {
        public const uint Id = 6019;
        public override uint MessageId
        {
            get { return Id; }
        }
        public ushort ChallengeId { get; set; }
        public bool Success { get; set; }

        public ChallengeResultMessage(ushort challengeId, bool success)
        {
            this.ChallengeId = challengeId;
            this.Success = success;
        }

        public ChallengeResultMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteVarUShort(ChallengeId);
            writer.WriteBoolean(Success);
        }

        public override void Deserialize(IDataReader reader)
        {
            ChallengeId = reader.ReadVarUShort();
            Success = reader.ReadBoolean();
        }

    }
}
