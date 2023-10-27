namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class ChallengeTargetsListRequestMessage : Message
    {
        public const uint Id = 5614;
        public override uint MessageId
        {
            get { return Id; }
        }
        public ushort ChallengeId { get; set; }

        public ChallengeTargetsListRequestMessage(ushort challengeId)
        {
            this.ChallengeId = challengeId;
        }

        public ChallengeTargetsListRequestMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteVarUShort(ChallengeId);
        }

        public override void Deserialize(IDataReader reader)
        {
            ChallengeId = reader.ReadVarUShort();
        }

    }
}
