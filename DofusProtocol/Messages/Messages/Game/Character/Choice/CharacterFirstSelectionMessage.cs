namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class CharacterFirstSelectionMessage : CharacterSelectionMessage
    {
        public new const uint Id = 6084;
        public override uint MessageId
        {
            get { return Id; }
        }
        public bool DoTutorial { get; set; }

        public CharacterFirstSelectionMessage(ulong objectId, bool doTutorial)
        {
            this.ObjectId = objectId;
            this.DoTutorial = doTutorial;
        }

        public CharacterFirstSelectionMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteBoolean(DoTutorial);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            DoTutorial = reader.ReadBoolean();
        }

    }
}
