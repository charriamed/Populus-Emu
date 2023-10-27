namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using System.Collections.Generic;
    using Stump.Core.IO;

    [Serializable]
    public class BasicCharactersListMessage : Message
    {
        public const uint Id = 6475;
        public override uint MessageId
        {
            get { return Id; }
        }
        public CharacterBaseInformations[] Characters { get; set; }

        public BasicCharactersListMessage(CharacterBaseInformations[] characters)
        {
            this.Characters = characters;
        }

        public BasicCharactersListMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort((short)Characters.Count());
            for (var charactersIndex = 0; charactersIndex < Characters.Count(); charactersIndex++)
            {
                var objectToSend = Characters[charactersIndex];
                writer.WriteShort(objectToSend.TypeId);
                objectToSend.Serialize(writer);
            }
        }

        public override void Deserialize(IDataReader reader)
        {
            var charactersCount = reader.ReadUShort();
            Characters = new CharacterBaseInformations[charactersCount];
            for (var charactersIndex = 0; charactersIndex < charactersCount; charactersIndex++)
            {
                var objectToAdd = ProtocolTypeManager.GetInstance<CharacterBaseInformations>(reader.ReadShort());
                objectToAdd.Deserialize(reader);
                Characters[charactersIndex] = objectToAdd;
            }
        }

    }
}
