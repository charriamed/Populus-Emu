namespace Stump.DofusProtocol.Types
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class QuestActiveInformations
    {
        public const short Id  = 381;
        public virtual short TypeId
        {
            get { return Id; }
        }
        public ushort QuestId { get; set; }

        public QuestActiveInformations(ushort questId)
        {
            this.QuestId = questId;
        }

        public QuestActiveInformations() { }

        public virtual void Serialize(IDataWriter writer)
        {
            writer.WriteVarUShort(QuestId);
        }

        public virtual void Deserialize(IDataReader reader)
        {
            QuestId = reader.ReadVarUShort();
        }

    }
}
