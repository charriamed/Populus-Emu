namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class AchievementDetailedListRequestMessage : Message
    {
        public const uint Id = 6357;
        public override uint MessageId
        {
            get { return Id; }
        }
        public ushort CategoryId { get; set; }

        public AchievementDetailedListRequestMessage(ushort categoryId)
        {
            this.CategoryId = categoryId;
        }

        public AchievementDetailedListRequestMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteVarUShort(CategoryId);
        }

        public override void Deserialize(IDataReader reader)
        {
            CategoryId = reader.ReadVarUShort();
        }

    }
}
