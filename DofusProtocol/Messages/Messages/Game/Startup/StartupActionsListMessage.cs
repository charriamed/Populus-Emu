namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using System.Collections.Generic;
    using Stump.Core.IO;

    [Serializable]
    public class StartupActionsListMessage : Message
    {
        public const uint Id = 1301;
        public override uint MessageId
        {
            get { return Id; }
        }
        public StartupActionAddObject[] Actions { get; set; }

        public StartupActionsListMessage(StartupActionAddObject[] actions)
        {
            this.Actions = actions;
        }

        public StartupActionsListMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort((short)Actions.Count());
            for (var actionsIndex = 0; actionsIndex < Actions.Count(); actionsIndex++)
            {
                var objectToSend = Actions[actionsIndex];
                objectToSend.Serialize(writer);
            }
        }

        public override void Deserialize(IDataReader reader)
        {
            var actionsCount = reader.ReadUShort();
            Actions = new StartupActionAddObject[actionsCount];
            for (var actionsIndex = 0; actionsIndex < actionsCount; actionsIndex++)
            {
                var objectToAdd = new StartupActionAddObject();
                objectToAdd.Deserialize(reader);
                Actions[actionsIndex] = objectToAdd;
            }
        }

    }
}
