namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using System.Collections.Generic;
    using Stump.Core.IO;

    [Serializable]
    public class GuildInAllianceFactsMessage : GuildFactsMessage
    {
        public new const uint Id = 6422;
        public override uint MessageId
        {
            get { return Id; }
        }
        public BasicNamedAllianceInformations AllianceInfos { get; set; }

        public GuildInAllianceFactsMessage(GuildFactSheetInformations infos, int creationDate, ushort nbTaxCollectors, CharacterMinimalGuildPublicInformations[] members, BasicNamedAllianceInformations allianceInfos)
        {
            this.Infos = infos;
            this.CreationDate = creationDate;
            this.NbTaxCollectors = nbTaxCollectors;
            this.Members = members;
            this.AllianceInfos = allianceInfos;
        }

        public GuildInAllianceFactsMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            AllianceInfos.Serialize(writer);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            AllianceInfos = new BasicNamedAllianceInformations();
            AllianceInfos.Deserialize(reader);
        }

    }
}
