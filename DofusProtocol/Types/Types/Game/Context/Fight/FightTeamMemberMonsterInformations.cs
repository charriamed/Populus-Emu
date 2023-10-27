namespace Stump.DofusProtocol.Types
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class FightTeamMemberMonsterInformations : FightTeamMemberInformations
    {
        public new const short Id = 6;
        public override short TypeId
        {
            get { return Id; }
        }
        public int MonsterId { get; set; }
        public sbyte Grade { get; set; }

        public FightTeamMemberMonsterInformations(double objectId, int monsterId, sbyte grade)
        {
            this.ObjectId = objectId;
            this.MonsterId = monsterId;
            this.Grade = grade;
        }

        public FightTeamMemberMonsterInformations() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteInt(MonsterId);
            writer.WriteSByte(Grade);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            MonsterId = reader.ReadInt();
            Grade = reader.ReadSByte();
        }

    }
}
