namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class CharacterReplayRequestMessage : Message
    {
        public const uint Id = 167;
        public override uint MessageId
        {
            get { return Id; }
        }
        public ulong CharacterId { get; set; }

        public CharacterReplayRequestMessage(ulong characterId)
        {
            this.CharacterId = characterId;
        }

        public CharacterReplayRequestMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteVarULong(CharacterId);
        }

        public override void Deserialize(IDataReader reader)
        {
            CharacterId = reader.ReadVarULong();
        }

    }
}
