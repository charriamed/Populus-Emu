namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class InviteInHavenBagOfferMessage : Message
    {
        public const uint Id = 6643;
        public override uint MessageId
        {
            get { return Id; }
        }
        public CharacterMinimalInformations HostInformations { get; set; }
        public int TimeLeftBeforeCancel { get; set; }

        public InviteInHavenBagOfferMessage(CharacterMinimalInformations hostInformations, int timeLeftBeforeCancel)
        {
            this.HostInformations = hostInformations;
            this.TimeLeftBeforeCancel = timeLeftBeforeCancel;
        }

        public InviteInHavenBagOfferMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            HostInformations.Serialize(writer);
            writer.WriteVarInt(TimeLeftBeforeCancel);
        }

        public override void Deserialize(IDataReader reader)
        {
            HostInformations = new CharacterMinimalInformations();
            HostInformations.Deserialize(reader);
            TimeLeftBeforeCancel = reader.ReadVarInt();
        }

    }
}
