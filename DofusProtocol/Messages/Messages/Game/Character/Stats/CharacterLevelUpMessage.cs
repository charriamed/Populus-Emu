namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class CharacterLevelUpMessage : Message
    {
        public const uint Id = 5670;
        public override uint MessageId
        {
            get { return Id; }
        }
        public ushort NewLevel { get; set; }

        public CharacterLevelUpMessage(ushort newLevel)
        {
            this.NewLevel = newLevel;
        }

        public CharacterLevelUpMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteVarUShort(NewLevel);
        }

        public override void Deserialize(IDataReader reader)
        {
            NewLevel = reader.ReadVarUShort();
        }

    }
}
