namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class BreachInvitationOfferMessage : Message
    {
        public const uint Id = 6805;
        public override uint MessageId
        {
            get { return Id; }
        }
        public CharacterMinimalInformations Host { get; set; }
        public uint TimeLeftBeforeCancel { get; set; }

        public BreachInvitationOfferMessage(CharacterMinimalInformations host, uint timeLeftBeforeCancel)
        {
            this.Host = host;
            this.TimeLeftBeforeCancel = timeLeftBeforeCancel;
        }

        public BreachInvitationOfferMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            Host.Serialize(writer);
            writer.WriteVarUInt(TimeLeftBeforeCancel);
        }

        public override void Deserialize(IDataReader reader)
        {
            Host = new CharacterMinimalInformations();
            Host.Deserialize(reader);
            TimeLeftBeforeCancel = reader.ReadVarUInt();
        }

    }
}
