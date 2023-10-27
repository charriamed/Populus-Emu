namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class ContactLookMessage : Message
    {
        public const uint Id = 5934;
        public override uint MessageId
        {
            get { return Id; }
        }
        public uint RequestId { get; set; }
        public string PlayerName { get; set; }
        public ulong PlayerId { get; set; }
        public EntityLook Look { get; set; }

        public ContactLookMessage(uint requestId, string playerName, ulong playerId, EntityLook look)
        {
            this.RequestId = requestId;
            this.PlayerName = playerName;
            this.PlayerId = playerId;
            this.Look = look;
        }

        public ContactLookMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteVarUInt(RequestId);
            writer.WriteUTF(PlayerName);
            writer.WriteVarULong(PlayerId);
            Look.Serialize(writer);
        }

        public override void Deserialize(IDataReader reader)
        {
            RequestId = reader.ReadVarUInt();
            PlayerName = reader.ReadUTF();
            PlayerId = reader.ReadVarULong();
            Look = new EntityLook();
            Look.Deserialize(reader);
        }

    }
}
