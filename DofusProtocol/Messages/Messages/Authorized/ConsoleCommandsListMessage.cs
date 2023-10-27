namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using System.Collections.Generic;
    using Stump.Core.IO;

    [Serializable]
    public class ConsoleCommandsListMessage : Message
    {
        public const uint Id = 6127;
        public override uint MessageId
        {
            get { return Id; }
        }
        public string[] Aliases { get; set; }
        public string[] Args { get; set; }
        public string[] Descriptions { get; set; }

        public ConsoleCommandsListMessage(string[] aliases, string[] args, string[] descriptions)
        {
            this.Aliases = aliases;
            this.Args = args;
            this.Descriptions = descriptions;
        }

        public ConsoleCommandsListMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort((short)Aliases.Count());
            for (var aliasesIndex = 0; aliasesIndex < Aliases.Count(); aliasesIndex++)
            {
                writer.WriteUTF(Aliases[aliasesIndex]);
            }
            writer.WriteShort((short)Args.Count());
            for (var argsIndex = 0; argsIndex < Args.Count(); argsIndex++)
            {
                writer.WriteUTF(Args[argsIndex]);
            }
            writer.WriteShort((short)Descriptions.Count());
            for (var descriptionsIndex = 0; descriptionsIndex < Descriptions.Count(); descriptionsIndex++)
            {
                writer.WriteUTF(Descriptions[descriptionsIndex]);
            }
        }

        public override void Deserialize(IDataReader reader)
        {
            var aliasesCount = reader.ReadUShort();
            Aliases = new string[aliasesCount];
            for (var aliasesIndex = 0; aliasesIndex < aliasesCount; aliasesIndex++)
            {
                Aliases[aliasesIndex] = reader.ReadUTF();
            }
            var argsCount = reader.ReadUShort();
            Args = new string[argsCount];
            for (var argsIndex = 0; argsIndex < argsCount; argsIndex++)
            {
                Args[argsIndex] = reader.ReadUTF();
            }
            var descriptionsCount = reader.ReadUShort();
            Descriptions = new string[descriptionsCount];
            for (var descriptionsIndex = 0; descriptionsIndex < descriptionsCount; descriptionsIndex++)
            {
                Descriptions[descriptionsIndex] = reader.ReadUTF();
            }
        }

    }
}
