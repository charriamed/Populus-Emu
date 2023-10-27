namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class ChallengeTargetUpdateMessage : Message
    {
        public const uint Id = 6123;
        public override uint MessageId
        {
            get { return Id; }
        }
        public ushort ChallengeId { get; set; }
        public double TargetId { get; set; }

        public ChallengeTargetUpdateMessage(ushort challengeId, double targetId)
        {
            this.ChallengeId = challengeId;
            this.TargetId = targetId;
        }

        public ChallengeTargetUpdateMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteVarUShort(ChallengeId);
            writer.WriteDouble(TargetId);
        }

        public override void Deserialize(IDataReader reader)
        {
            ChallengeId = reader.ReadVarUShort();
            TargetId = reader.ReadDouble();
        }

    }
}
