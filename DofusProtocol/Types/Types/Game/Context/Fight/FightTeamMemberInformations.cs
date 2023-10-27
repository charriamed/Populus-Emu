namespace Stump.DofusProtocol.Types
{
    using System.Linq;
    using System.Text;
    using System;
    using Stump.Core.IO;
    using Stump.DofusProtocol.Types;

    [Serializable]
    public class FightTeamMemberInformations
    {
        public const short Id = 44;
        public virtual short TypeId
        {
            get { return Id; }
        }
        public double ObjectId;

        public FightTeamMemberInformations(double objectId)
        {
            this.ObjectId = objectId;
        }

        public FightTeamMemberInformations() { }

        public virtual void Serialize(IDataWriter writer)
        {
            writer.WriteDouble(ObjectId);
        }

        public virtual void Deserialize(IDataReader reader)
        {
            ObjectId = reader.ReadDouble();
        }

    }
}