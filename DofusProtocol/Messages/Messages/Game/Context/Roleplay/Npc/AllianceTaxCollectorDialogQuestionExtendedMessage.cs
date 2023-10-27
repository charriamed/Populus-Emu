namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class AllianceTaxCollectorDialogQuestionExtendedMessage : TaxCollectorDialogQuestionExtendedMessage
    {
        public new const uint Id = 6445;
        public override uint MessageId
        {
            get { return Id; }
        }
        public BasicNamedAllianceInformations Alliance { get; set; }

        public AllianceTaxCollectorDialogQuestionExtendedMessage(BasicGuildInformations guildInfo, ushort maxPods, ushort prospecting, ushort wisdom, sbyte taxCollectorsCount, int taxCollectorAttack, ulong kamas, ulong experience, uint pods, ulong itemsValue, BasicNamedAllianceInformations alliance)
        {
            this.GuildInfo = guildInfo;
            this.MaxPods = maxPods;
            this.Prospecting = prospecting;
            this.Wisdom = wisdom;
            this.TaxCollectorsCount = taxCollectorsCount;
            this.TaxCollectorAttack = taxCollectorAttack;
            this.Kamas = kamas;
            this.Experience = experience;
            this.Pods = pods;
            this.ItemsValue = itemsValue;
            this.Alliance = alliance;
        }

        public AllianceTaxCollectorDialogQuestionExtendedMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            Alliance.Serialize(writer);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            Alliance = new BasicNamedAllianceInformations();
            Alliance.Deserialize(reader);
        }

    }
}
