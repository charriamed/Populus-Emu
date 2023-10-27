namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class TaxCollectorAttackedResultMessage : Message
    {
        public const uint Id = 5635;
        public override uint MessageId
        {
            get { return Id; }
        }
        public bool DeadOrAlive { get; set; }
        public TaxCollectorBasicInformations BasicInfos { get; set; }
        public BasicGuildInformations Guild { get; set; }

        public TaxCollectorAttackedResultMessage(bool deadOrAlive, TaxCollectorBasicInformations basicInfos, BasicGuildInformations guild)
        {
            this.DeadOrAlive = deadOrAlive;
            this.BasicInfos = basicInfos;
            this.Guild = guild;
        }

        public TaxCollectorAttackedResultMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteBoolean(DeadOrAlive);
            BasicInfos.Serialize(writer);
            Guild.Serialize(writer);
        }

        public override void Deserialize(IDataReader reader)
        {
            DeadOrAlive = reader.ReadBoolean();
            BasicInfos = new TaxCollectorBasicInformations();
            BasicInfos.Deserialize(reader);
            Guild = new BasicGuildInformations();
            Guild.Deserialize(reader);
        }

    }
}
