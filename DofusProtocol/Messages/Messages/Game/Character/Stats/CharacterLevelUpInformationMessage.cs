namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class CharacterLevelUpInformationMessage : CharacterLevelUpMessage
    {
        public new const uint Id = 6076;
        public override uint MessageId
        {
            get { return Id; }
        }
        public string Name { get; set; }
        public ulong ObjectId { get; set; }

        public CharacterLevelUpInformationMessage(ushort newLevel, string name, ulong objectId)
        {
            this.NewLevel = newLevel;
            this.Name = name;
            this.ObjectId = objectId;
        }

        public CharacterLevelUpInformationMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteUTF(Name);
            writer.WriteVarULong(ObjectId);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            Name = reader.ReadUTF();
            ObjectId = reader.ReadVarULong();
        }

    }
}
