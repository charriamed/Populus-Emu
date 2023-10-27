namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class HouseGuildRightsMessage : Message
    {
        public const uint Id = 5703;
        public override uint MessageId
        {
            get { return Id; }
        }
        public uint HouseId { get; set; }
        public int InstanceId { get; set; }
        public bool SecondHand { get; set; }
        public GuildInformations GuildInfo { get; set; }
        public uint Rights { get; set; }

        public HouseGuildRightsMessage(uint houseId, int instanceId, bool secondHand, GuildInformations guildInfo, uint rights)
        {
            this.HouseId = houseId;
            this.InstanceId = instanceId;
            this.SecondHand = secondHand;
            this.GuildInfo = guildInfo;
            this.Rights = rights;
        }

        public HouseGuildRightsMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteVarUInt(HouseId);
            writer.WriteInt(InstanceId);
            writer.WriteBoolean(SecondHand);
            GuildInfo.Serialize(writer);
            writer.WriteVarUInt(Rights);
        }

        public override void Deserialize(IDataReader reader)
        {
            HouseId = reader.ReadVarUInt();
            InstanceId = reader.ReadInt();
            SecondHand = reader.ReadBoolean();
            GuildInfo = new GuildInformations();
            GuildInfo.Deserialize(reader);
            Rights = reader.ReadVarUInt();
        }

    }
}
