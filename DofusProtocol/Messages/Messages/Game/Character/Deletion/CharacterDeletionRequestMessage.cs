namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class CharacterDeletionRequestMessage : Message
    {
        public const uint Id = 165;
        public override uint MessageId
        {
            get { return Id; }
        }
        public ulong CharacterId { get; set; }
        public string SecretAnswerHash { get; set; }

        public CharacterDeletionRequestMessage(ulong characterId, string secretAnswerHash)
        {
            this.CharacterId = characterId;
            this.SecretAnswerHash = secretAnswerHash;
        }

        public CharacterDeletionRequestMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteVarULong(CharacterId);
            writer.WriteUTF(SecretAnswerHash);
        }

        public override void Deserialize(IDataReader reader)
        {
            CharacterId = reader.ReadVarULong();
            SecretAnswerHash = reader.ReadUTF();
        }

    }
}
