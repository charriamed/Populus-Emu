namespace Stump.DofusProtocol.Types
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class ProtectedEntityWaitingForHelpInfo
    {
        public const short Id  = 186;
        public virtual short TypeId
        {
            get { return Id; }
        }
        public int TimeLeftBeforeFight { get; set; }
        public int WaitTimeForPlacement { get; set; }
        public sbyte NbPositionForDefensors { get; set; }

        public ProtectedEntityWaitingForHelpInfo(int timeLeftBeforeFight, int waitTimeForPlacement, sbyte nbPositionForDefensors)
        {
            this.TimeLeftBeforeFight = timeLeftBeforeFight;
            this.WaitTimeForPlacement = waitTimeForPlacement;
            this.NbPositionForDefensors = nbPositionForDefensors;
        }

        public ProtectedEntityWaitingForHelpInfo() { }

        public virtual void Serialize(IDataWriter writer)
        {
            writer.WriteInt(TimeLeftBeforeFight);
            writer.WriteInt(WaitTimeForPlacement);
            writer.WriteSByte(NbPositionForDefensors);
        }

        public virtual void Deserialize(IDataReader reader)
        {
            TimeLeftBeforeFight = reader.ReadInt();
            WaitTimeForPlacement = reader.ReadInt();
            NbPositionForDefensors = reader.ReadSByte();
        }

    }
}
