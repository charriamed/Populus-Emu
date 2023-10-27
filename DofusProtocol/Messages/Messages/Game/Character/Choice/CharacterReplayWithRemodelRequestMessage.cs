namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class CharacterReplayWithRemodelRequestMessage : CharacterReplayRequestMessage
    {
        public new const uint Id = 6551;
        public override uint MessageId
        {
            get { return Id; }
        }
        public RemodelingInformation Remodel { get; set; }

        public CharacterReplayWithRemodelRequestMessage(ulong characterId, RemodelingInformation remodel)
        {
            this.CharacterId = characterId;
            this.Remodel = remodel;
        }

        public CharacterReplayWithRemodelRequestMessage() { }

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
