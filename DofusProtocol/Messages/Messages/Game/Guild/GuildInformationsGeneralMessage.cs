namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class GuildInformationsGeneralMessage : Message
    {
        public const uint Id = 5557;
        public override uint MessageId
        {
            get { return Id; }
        }
        public bool AbandonnedPaddock { get; set; }
        public byte Level { get; set; }
        public ulong ExpLevelFloor { get; set; }
        public ulong Experience { get; set; }
        public ulong ExpNextLevelFloor { get; set; }
        public int CreationDate { get; set; }
        public ushort NbTotalMembers { get; set; }
        public ushort NbConnectedMembers { get; set; }

        public GuildInformationsGeneralMessage(bool abandonnedPaddock, byte level, ulong expLevelFloor, ulong experience, ulong expNextLevelFloor, int creationDate, ushort nbTotalMembers, ushort nbConnectedMembers)
        {
            this.AbandonnedPaddock = abandonnedPaddock;
            this.Level = level;
            this.ExpLevelFloor = expLevelFloor;
            this.Experience = experience;
            this.ExpNextLevelFloor = expNextLevelFloor;
            this.CreationDate = creationDate;
            this.NbTotalMembers = nbTotalMembers;
            this.NbConnectedMembers = nbConnectedMembers;
        }

        public GuildInformationsGeneralMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteBoolean(AbandonnedPaddock);
            writer.WriteByte(Level);
            writer.WriteVarULong(ExpLevelFloor);
            writer.WriteVarULong(Experience);
            writer.WriteVarULong(ExpNextLevelFloor);
            writer.WriteInt(CreationDate);
            writer.WriteVarUShort(NbTotalMembers);
            writer.WriteVarUShort(NbConnectedMembers);
        }

        public override void Deserialize(IDataReader reader)
        {
            AbandonnedPaddock = reader.ReadBoolean();
            Level = reader.ReadByte();
            ExpLevelFloor = reader.ReadVarULong();
            Experience = reader.ReadVarULong();
            ExpNextLevelFloor = reader.ReadVarULong();
            CreationDate = reader.ReadInt();
            NbTotalMembers = reader.ReadVarUShort();
            NbConnectedMembers = reader.ReadVarUShort();
        }

    }
}
