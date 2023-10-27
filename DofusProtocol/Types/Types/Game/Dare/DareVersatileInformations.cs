namespace Stump.DofusProtocol.Types
{
    using System.Linq;
    using System.Text;
    using System;
    using Stump.Core.IO;
    using Stump.DofusProtocol.Types;

    [Serializable]
    public class DareVersatileInformations
    {
        public const short Id = 504;
        public virtual short TypeId
        {
            get { return Id; }
        }
        public double dareId;
        public int countEntrants;
        public int countWinners;

        public DareVersatileInformations(double dareId, int countEntrants, int countWinners)
        {
            this.dareId = dareId;
            this.countEntrants = countEntrants;
            this.countWinners = countWinners;
        }

        public DareVersatileInformations() { }

        public virtual void Serialize(IDataWriter writer)
        {
            writer.WriteDouble(dareId);
            writer.WriteInt(countEntrants);
            writer.WriteInt(countWinners);
        }

        public virtual void Deserialize(IDataReader reader)
        {
            dareId = reader.ReadDouble();
            countEntrants = reader.ReadInt();
            countWinners = reader.ReadInt();
        }

    }
}