namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using System.Collections.Generic;
    using Stump.Core.IO;

    [Serializable]
    public class ServerSessionConstantsMessage : Message
    {
        public const uint Id = 6434;
        public override uint MessageId
        {
            get { return Id; }
        }
        public ServerSessionConstant[] Variables { get; set; }

        public ServerSessionConstantsMessage(ServerSessionConstant[] variables)
        {
            this.Variables = variables;
        }

        public ServerSessionConstantsMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort((short)Variables.Count());
            for (var variablesIndex = 0; variablesIndex < Variables.Count(); variablesIndex++)
            {
                var objectToSend = Variables[variablesIndex];
                writer.WriteShort(objectToSend.TypeId);
                objectToSend.Serialize(writer);
            }
        }

        public override void Deserialize(IDataReader reader)
        {
            var variablesCount = reader.ReadUShort();
            Variables = new ServerSessionConstant[variablesCount];
            for (var variablesIndex = 0; variablesIndex < variablesCount; variablesIndex++)
            {
                var objectToAdd = ProtocolTypeManager.GetInstance<ServerSessionConstant>(reader.ReadShort());
                objectToAdd.Deserialize(reader);
                Variables[variablesIndex] = objectToAdd;
            }
        }

    }
}
