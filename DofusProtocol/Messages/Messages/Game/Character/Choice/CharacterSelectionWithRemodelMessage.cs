namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class CharacterSelectionWithRemodelMessage : CharacterSelectionMessage
    {
        public new const uint Id = 6549;
        public override uint MessageId
        {
            get { return Id; }
        }
        public RemodelingInformation Remodel { get; set; }

        public CharacterSelectionWithRemodelMessage(ulong objectId, RemodelingInformation remodel)
        {
            this.ObjectId = objectId;
            this.Remodel = remodel;
        }

        public CharacterSelectionWithRemodelMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            Remodel.Serialize(writer);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            Remodel = new RemodelingInformation();
            Remodel.Deserialize(reader);
        }

    }
}
