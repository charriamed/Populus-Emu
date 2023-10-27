namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class ChallengeInfoMessage : Message
    {
        public const uint Id = 6022;
        public override uint MessageId
        {
            get { return Id; }
        }
        public ushort ChallengeId { get; set; }
        public double TargetId { get; set; }
        public uint XpBonus { get; set; }
        public uint DropBonus { get; set; }

        public ChallengeInfoMessage(ushort challengeId, double targetId, uint xpBonus, uint dropBonus)
        {
            this.ChallengeId = challengeId;
            this.TargetId = targetId;
            this.XpBonus = xpBonus;
            this.DropBonus = dropBonus;
        }

        public ChallengeInfoMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteVarUShort(ChallengeId);
            writer.WriteDouble(TargetId);
            writer.WriteVarUInt(XpBonus);
            writer.WriteVarUInt(DropBonus);
        }

        public override void Deserialize(IDataReader reader)
        {
            ChallengeId = reader.ReadVarUShort();
            TargetId = reader.ReadDouble();
            XpBonus = reader.ReadVarUInt();
            DropBonus = reader.ReadVarUInt();
        }

    }
}
