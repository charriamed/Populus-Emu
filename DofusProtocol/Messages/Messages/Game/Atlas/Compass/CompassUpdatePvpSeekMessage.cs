namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class CompassUpdatePvpSeekMessage : CompassUpdateMessage
    {
        public new const uint Id = 6013;
        public override uint MessageId
        {
            get { return Id; }
        }
        public ulong MemberId { get; set; }
        public string MemberName { get; set; }

        public CompassUpdatePvpSeekMessage(sbyte type, MapCoordinates coords, ulong memberId, string memberName)
        {
            this.Type = type;
            this.Coords = coords;
            this.MemberId = memberId;
            this.MemberName = memberName;
        }

        public CompassUpdatePvpSeekMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteVarULong(MemberId);
            writer.WriteUTF(MemberName);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            MemberId = reader.ReadVarULong();
            MemberName = reader.ReadUTF();
        }

    }
}
