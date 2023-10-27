namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class StartupActionAddMessage : Message
    {
        public const uint Id = 6538;
        public override uint MessageId
        {
            get { return Id; }
        }
        public StartupActionAddObject NewAction { get; set; }

        public StartupActionAddMessage(StartupActionAddObject newAction)
        {
            this.NewAction = newAction;
        }

        public StartupActionAddMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            NewAction.Serialize(writer);
        }

        public override void Deserialize(IDataReader reader)
        {
            NewAction = new StartupActionAddObject();
            NewAction.Deserialize(reader);
        }

    }
}
