namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class ObjectUseOnCharacterMessage : ObjectUseMessage
    {
        public new const uint Id = 3003;
        public override uint MessageId
        {
            get { return Id; }
        }
        public ulong CharacterId { get; set; }

        public ObjectUseOnCharacterMessage(uint objectUID, ulong characterId)
        {
            this.ObjectUID = objectUID;
            this.CharacterId = characterId;
        }

        public ObjectUseOnCharacterMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteVarULong(CharacterId);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            CharacterId = reader.ReadVarULong();
        }

    }
}
