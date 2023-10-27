namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class CharacterNameSuggestionSuccessMessage : Message
    {
        public const uint Id = 5544;
        public override uint MessageId
        {
            get { return Id; }
        }
        public string Suggestion { get; set; }

        public CharacterNameSuggestionSuccessMessage(string suggestion)
        {
            this.Suggestion = suggestion;
        }

        public CharacterNameSuggestionSuccessMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteUTF(Suggestion);
        }

        public override void Deserialize(IDataReader reader)
        {
            Suggestion = reader.ReadUTF();
        }

    }
}
