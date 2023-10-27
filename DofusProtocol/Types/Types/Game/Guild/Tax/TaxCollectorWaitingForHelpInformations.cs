namespace Stump.DofusProtocol.Types
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class TaxCollectorWaitingForHelpInformations : TaxCollectorComplementaryInformations
    {
        public new const short Id = 447;
        public override short TypeId
        {
            get { return Id; }
        }
        public ProtectedEntityWaitingForHelpInfo WaitingForHelpInfo { get; set; }

        public TaxCollectorWaitingForHelpInformations(ProtectedEntityWaitingForHelpInfo waitingForHelpInfo)
        {
            this.WaitingForHelpInfo = waitingForHelpInfo;
        }

        public TaxCollectorWaitingForHelpInformations() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            WaitingForHelpInfo.Serialize(writer);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            WaitingForHelpInfo = new ProtectedEntityWaitingForHelpInfo();
            WaitingForHelpInfo.Deserialize(reader);
        }

    }
}
