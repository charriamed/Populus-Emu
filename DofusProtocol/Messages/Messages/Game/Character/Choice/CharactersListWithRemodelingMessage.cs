namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using System.Collections.Generic;
    using Stump.Core.IO;

    [Serializable]
    public class CharactersListWithRemodelingMessage : CharactersListMessage
    {
        public new const uint Id = 6550;
        public override uint MessageId
        {
            get { return Id; }
        }
        public CharacterToRemodelInformations[] CharactersToRemodel { get; set; }

        public CharactersListWithRemodelingMessage(CharacterBaseInformations[] characters, bool hasStartupActions, CharacterToRemodelInformations[] charactersToRemodel)
        {
            this.Characters = characters;
            this.HasStartupActions = hasStartupActions;
            this.CharactersToRemodel = charactersToRemodel;
        }

        public CharactersListWithRemodelingMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteShort((short)CharactersToRemodel.Count());
            for (var charactersToRemodelIndex = 0; charactersToRemodelIndex < CharactersToRemodel.Count(); charactersToRemodelIndex++)
            {
                var objectToSend = CharactersToRemodel[charactersToRemodelIndex];
                objectToSend.Serialize(writer);
            }
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            var charactersToRemodelCount = reader.ReadUShort();
            CharactersToRemodel = new CharacterToRemodelInformations[charactersToRemodelCount];
            for (var charactersToRemodelIndex = 0; charactersToRemodelIndex < charactersToRemodelCount; charactersToRemodelIndex++)
            {
                var objectToAdd = new CharacterToRemodelInformations();
                objectToAdd.Deserialize(reader);
                CharactersToRemodel[charactersToRemodelIndex] = objectToAdd;
            }
        }

    }
}
