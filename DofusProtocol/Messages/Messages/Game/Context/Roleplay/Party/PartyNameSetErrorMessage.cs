namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class PartyNameSetErrorMessage : AbstractPartyMessage
    {
        public new const uint Id = 6501;
        public override uint MessageId
        {
            get { return Id; }
        }
        public sbyte Result { get; set; }

        public PartyNameSetErrorMessage(uint partyId, sbyte result)
        {
            this.PartyId = partyId;
            this.Result = result;
        }

        public PartyNameSetErrorMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteSByte(Result);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            Result = reader.ReadSByte();
        }

    }
}
