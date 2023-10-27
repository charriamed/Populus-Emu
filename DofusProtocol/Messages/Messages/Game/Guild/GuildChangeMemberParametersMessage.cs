namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class GuildChangeMemberParametersMessage : Message
    {
        public const uint Id = 5549;
        public override uint MessageId
        {
            get { return Id; }
        }
        public ulong MemberId { get; set; }
        public ushort Rank { get; set; }
        public sbyte ExperienceGivenPercent { get; set; }
        public uint Rights { get; set; }

        public GuildChangeMemberParametersMessage(ulong memberId, ushort rank, sbyte experienceGivenPercent, uint rights)
        {
            this.MemberId = memberId;
            this.Rank = rank;
            this.ExperienceGivenPercent = experienceGivenPercent;
            this.Rights = rights;
        }

        public GuildChangeMemberParametersMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteVarULong(MemberId);
            writer.WriteVarUShort(Rank);
            writer.WriteSByte(ExperienceGivenPercent);
            writer.WriteVarUInt(Rights);
        }

        public override void Deserialize(IDataReader reader)
        {
            MemberId = reader.ReadVarULong();
            Rank = reader.ReadVarUShort();
            ExperienceGivenPercent = reader.ReadSByte();
            Rights = reader.ReadVarUInt();
        }

    }
}
