namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class TaxCollectorDialogQuestionBasicMessage : Message
    {
        public const uint Id = 5619;
        public override uint MessageId
        {
            get { return Id; }
        }
        public BasicGuildInformations GuildInfo { get; set; }

        public TaxCollectorDialogQuestionBasicMessage(BasicGuildInformations guildInfo)
        {
            this.GuildInfo = guildInfo;
        }

        public TaxCollectorDialogQuestionBasicMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            GuildInfo.Serialize(writer);
        }

        public override void Deserialize(IDataReader reader)
        {
            GuildInfo = new BasicGuildInformations();
            GuildInfo.Deserialize(reader);
        }

    }
}
