namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using System.Collections.Generic;
    using Stump.Core.IO;

    [Serializable]
    public class BreachCharactersMessage : Message
    {
        public const uint Id = 6811;
        public override uint MessageId
        {
            get { return Id; }
        }
        public ulong[] Characters { get; set; }

        public BreachCharactersMessage(ulong[] characters)
        {
            this.Characters = characters;
        }

        public BreachCharactersMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort((short)Characters.Count());
            for (var charactersIndex = 0; charactersIndex < Characters.Count(); charactersIndex++)
            {
                writer.WriteVarULong(Characters[charactersIndex]);
            }
        }

        public override void Deserialize(IDataReader reader)
        {
            var charactersCount = reader.ReadUShort();
            Characters = new ulong[charactersCount];
            for (var charactersIndex = 0; charactersIndex < charactersCount; charactersIndex++)
            {
                Characters[charactersIndex] = reader.ReadVarULong();
            }
        }

    }
}
