namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class CharacterSelectedSuccessMessage : Message
    {
        public const uint Id = 153;
        public override uint MessageId
        {
            get { return Id; }
        }
        public CharacterBaseInformations Infos { get; set; }
        public bool IsCollectingStats { get; set; }

        public CharacterSelectedSuccessMessage(CharacterBaseInformations infos, bool isCollectingStats)
        {
            this.Infos = infos;
            this.IsCollectingStats = isCollectingStats;
        }

        public CharacterSelectedSuccessMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            Infos.Serialize(writer);
            writer.WriteBoolean(IsCollectingStats);
        }

        public override void Deserialize(IDataReader reader)
        {
            Infos = new CharacterBaseInformations();
            Infos.Deserialize(reader);
            IsCollectingStats = reader.ReadBoolean();
        }

    }
}
