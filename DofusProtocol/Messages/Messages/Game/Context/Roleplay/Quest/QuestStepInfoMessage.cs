namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class QuestStepInfoMessage : Message
    {
        public const uint Id = 5625;
        public override uint MessageId
        {
            get { return Id; }
        }
        public QuestActiveInformations Infos { get; set; }

        public QuestStepInfoMessage(QuestActiveInformations infos)
        {
            this.Infos = infos;
        }

        public QuestStepInfoMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort(Infos.TypeId);
            Infos.Serialize(writer);
        }

        public override void Deserialize(IDataReader reader)
        {
            Infos = ProtocolTypeManager.GetInstance<QuestActiveInformations>(reader.ReadShort());
            Infos.Deserialize(reader);
        }

    }
}
