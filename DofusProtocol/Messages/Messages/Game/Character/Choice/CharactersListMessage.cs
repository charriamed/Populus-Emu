namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using System.Collections.Generic;
    using Stump.Core.IO;

    [Serializable]
    public class CharactersListMessage : BasicCharactersListMessage
    {
        public new const uint Id = 151;
        public override uint MessageId
        {
            get { return Id; }
        }
        public bool HasStartupActions { get; set; }

        public CharactersListMessage(CharacterBaseInformations[] characters, bool hasStartupActions)
        {
            this.Characters = characters;
            this.HasStartupActions = hasStartupActions;
        }

        public CharactersListMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteBoolean(HasStartupActions);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            HasStartupActions = reader.ReadBoolean();
        }

    }
}
