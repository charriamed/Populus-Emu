namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class GuildPaddockBoughtMessage : Message
    {
        public const uint Id = 5952;
        public override uint MessageId
        {
            get { return Id; }
        }
        public PaddockContentInformations PaddockInfo { get; set; }

        public GuildPaddockBoughtMessage(PaddockContentInformations paddockInfo)
        {
            this.PaddockInfo = paddockInfo;
        }

        public GuildPaddockBoughtMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            PaddockInfo.Serialize(writer);
        }

        public override void Deserialize(IDataReader reader)
        {
            PaddockInfo = new PaddockContentInformations();
            PaddockInfo.Deserialize(reader);
        }

    }
}
