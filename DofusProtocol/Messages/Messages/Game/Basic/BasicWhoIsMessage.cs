namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using System.Collections.Generic;
    using Stump.Core.IO;

    [Serializable]
    public class BasicWhoIsMessage : Message
    {
        public const uint Id = 180;
        public override uint MessageId
        {
            get { return Id; }
        }
        public bool Self { get; set; }
        public bool Verbose { get; set; }
        public sbyte Position { get; set; }
        public string AccountNickname { get; set; }
        public int AccountId { get; set; }
        public string PlayerName { get; set; }
        public ulong PlayerId { get; set; }
        public short AreaId { get; set; }
        public short ServerId { get; set; }
        public short OriginServerId { get; set; }
        public AbstractSocialGroupInfos[] SocialGroups { get; set; }
        public sbyte PlayerState { get; set; }

        public BasicWhoIsMessage(bool self, bool verbose, sbyte position, string accountNickname, int accountId, string playerName, ulong playerId, short areaId, short serverId, short originServerId, AbstractSocialGroupInfos[] socialGroups, sbyte playerState)
        {
            this.Self = self;
            this.Verbose = verbose;
            this.Position = position;
            this.AccountNickname = accountNickname;
            this.AccountId = accountId;
            this.PlayerName = playerName;
            this.PlayerId = playerId;
            this.AreaId = areaId;
            this.ServerId = serverId;
            this.OriginServerId = originServerId;
            this.SocialGroups = socialGroups;
            this.PlayerState = playerState;
        }

        public BasicWhoIsMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            var flag = new byte();
            flag = BooleanByteWrapper.SetFlag(flag, 0, Self);
            flag = BooleanByteWrapper.SetFlag(flag, 1, Verbose);
            writer.WriteByte(flag);
            writer.WriteSByte(Position);
            writer.WriteUTF(AccountNickname);
            writer.WriteInt(AccountId);
            writer.WriteUTF(PlayerName);
            writer.WriteVarULong(PlayerId);
            writer.WriteShort(AreaId);
            writer.WriteShort(ServerId);
            writer.WriteShort(OriginServerId);
            writer.WriteShort((short)SocialGroups.Count());
            for (var socialGroupsIndex = 0; socialGroupsIndex < SocialGroups.Count(); socialGroupsIndex++)
            {
                var objectToSend = SocialGroups[socialGroupsIndex];
                writer.WriteShort(objectToSend.TypeId);
                objectToSend.Serialize(writer);
            }
            writer.WriteSByte(PlayerState);
        }

        public override void Deserialize(IDataReader reader)
        {
            var flag = reader.ReadByte();
            Self = BooleanByteWrapper.GetFlag(flag, 0);
            Verbose = BooleanByteWrapper.GetFlag(flag, 1);
            Position = reader.ReadSByte();
            AccountNickname = reader.ReadUTF();
            AccountId = reader.ReadInt();
            PlayerName = reader.ReadUTF();
            PlayerId = reader.ReadVarULong();
            AreaId = reader.ReadShort();
            ServerId = reader.ReadShort();
            OriginServerId = reader.ReadShort();
            var socialGroupsCount = reader.ReadUShort();
            SocialGroups = new AbstractSocialGroupInfos[socialGroupsCount];
            for (var socialGroupsIndex = 0; socialGroupsIndex < socialGroupsCount; socialGroupsIndex++)
            {
                var objectToAdd = ProtocolTypeManager.GetInstance<AbstractSocialGroupInfos>(reader.ReadShort());
                objectToAdd.Deserialize(reader);
                SocialGroups[socialGroupsIndex] = objectToAdd;
            }
            PlayerState = reader.ReadSByte();
        }

    }
}
