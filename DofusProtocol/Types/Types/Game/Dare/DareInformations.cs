namespace Stump.DofusProtocol.Types
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using System.Collections.Generic;
    using Stump.Core.IO;

    [Serializable]
    public class DareInformations
    {
        public const short Id  = 502;
        public virtual short TypeId
        {
            get { return Id; }
        }
        public double DareId { get; set; }
        public CharacterBasicMinimalInformations Creator { get; set; }
        public ulong SubscriptionFee { get; set; }
        public ulong Jackpot { get; set; }
        public ushort MaxCountWinners { get; set; }
        public double EndDate { get; set; }
        public bool IsPrivate { get; set; }
        public uint GuildId { get; set; }
        public uint AllianceId { get; set; }
        public DareCriteria[] Criterions { get; set; }
        public double StartDate { get; set; }

        public DareInformations(double dareId, CharacterBasicMinimalInformations creator, ulong subscriptionFee, ulong jackpot, ushort maxCountWinners, double endDate, bool isPrivate, uint guildId, uint allianceId, DareCriteria[] criterions, double startDate)
        {
            this.DareId = dareId;
            this.Creator = creator;
            this.SubscriptionFee = subscriptionFee;
            this.Jackpot = jackpot;
            this.MaxCountWinners = maxCountWinners;
            this.EndDate = endDate;
            this.IsPrivate = isPrivate;
            this.GuildId = guildId;
            this.AllianceId = allianceId;
            this.Criterions = criterions;
            this.StartDate = startDate;
        }

        public DareInformations() { }

        public virtual void Serialize(IDataWriter writer)
        {
            writer.WriteDouble(DareId);
            Creator.Serialize(writer);
            writer.WriteVarULong(SubscriptionFee);
            writer.WriteVarULong(Jackpot);
            writer.WriteUShort(MaxCountWinners);
            writer.WriteDouble(EndDate);
            writer.WriteBoolean(IsPrivate);
            writer.WriteVarUInt(GuildId);
            writer.WriteVarUInt(AllianceId);
            writer.WriteShort((short)Criterions.Count());
            for (var criterionsIndex = 0; criterionsIndex < Criterions.Count(); criterionsIndex++)
            {
                var objectToSend = Criterions[criterionsIndex];
                objectToSend.Serialize(writer);
            }
            writer.WriteDouble(StartDate);
        }

        public virtual void Deserialize(IDataReader reader)
        {
            DareId = reader.ReadDouble();
            Creator = new CharacterBasicMinimalInformations();
            Creator.Deserialize(reader);
            SubscriptionFee = reader.ReadVarULong();
            Jackpot = reader.ReadVarULong();
            MaxCountWinners = reader.ReadUShort();
            EndDate = reader.ReadDouble();
            IsPrivate = reader.ReadBoolean();
            GuildId = reader.ReadVarUInt();
            AllianceId = reader.ReadVarUInt();
            var criterionsCount = reader.ReadUShort();
            Criterions = new DareCriteria[criterionsCount];
            for (var criterionsIndex = 0; criterionsIndex < criterionsCount; criterionsIndex++)
            {
                var objectToAdd = new DareCriteria();
                objectToAdd.Deserialize(reader);
                Criterions[criterionsIndex] = objectToAdd;
            }
            StartDate = reader.ReadDouble();
        }

    }
}
