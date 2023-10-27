namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class CompassUpdatePartyMemberMessage : CompassUpdateMessage
    {
        public new const uint Id = 5589;
        public override uint MessageId
        {
            get { return Id; }
        }
        public ulong MemberId { get; set; }
        public bool Active { get; set; }

        public CompassUpdatePartyMemberMessage(sbyte type, MapCoordinates coords, ulong memberId, bool active)
        {
            this.Type = type;
            this.Coords = coords;
            this.MemberId = memberId;
            this.Active = active;
        }

        public CompassUpdatePartyMemberMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteVarULong(MemberId);
            writer.WriteBoolean(Active);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            MemberId = reader.ReadVarULong();
            Active = reader.ReadBoolean();
        }

    }
}
