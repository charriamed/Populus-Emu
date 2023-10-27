namespace Stump.DofusProtocol.Types
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using System.Collections.Generic;
    using Stump.Core.IO;

    [Serializable]
    public class FightCommonInformations
    {
        public const short Id  = 43;
        public virtual short TypeId
        {
            get { return Id; }
        }
        public ushort FightId { get; set; }
        public sbyte FightType { get; set; }
        public FightTeamInformations[] FightTeams { get; set; }
        public ushort[] FightTeamsPositions { get; set; }
        public FightOptionsInformations[] FightTeamsOptions { get; set; }

        public FightCommonInformations(ushort fightId, sbyte fightType, FightTeamInformations[] fightTeams, ushort[] fightTeamsPositions, FightOptionsInformations[] fightTeamsOptions)
        {
            this.FightId = fightId;
            this.FightType = fightType;
            this.FightTeams = fightTeams;
            this.FightTeamsPositions = fightTeamsPositions;
            this.FightTeamsOptions = fightTeamsOptions;
        }

        public FightCommonInformations() { }

        public virtual void Serialize(IDataWriter writer)
        {
            writer.WriteVarUShort(FightId);
            writer.WriteSByte(FightType);
            writer.WriteShort((short)FightTeams.Count());
            for (var fightTeamsIndex = 0; fightTeamsIndex < FightTeams.Count(); fightTeamsIndex++)
            {
                var objectToSend = FightTeams[fightTeamsIndex];
                writer.WriteShort(objectToSend.TypeId);
                objectToSend.Serialize(writer);
            }
            writer.WriteShort((short)FightTeamsPositions.Count());
            for (var fightTeamsPositionsIndex = 0; fightTeamsPositionsIndex < FightTeamsPositions.Count(); fightTeamsPositionsIndex++)
            {
                writer.WriteVarUShort(FightTeamsPositions[fightTeamsPositionsIndex]);
            }
            writer.WriteShort((short)FightTeamsOptions.Count());
            for (var fightTeamsOptionsIndex = 0; fightTeamsOptionsIndex < FightTeamsOptions.Count(); fightTeamsOptionsIndex++)
            {
                var objectToSend = FightTeamsOptions[fightTeamsOptionsIndex];
                objectToSend.Serialize(writer);
            }
        }

        public virtual void Deserialize(IDataReader reader)
        {
            FightId = reader.ReadVarUShort();
            FightType = reader.ReadSByte();
            var fightTeamsCount = reader.ReadUShort();
            FightTeams = new FightTeamInformations[fightTeamsCount];
            for (var fightTeamsIndex = 0; fightTeamsIndex < fightTeamsCount; fightTeamsIndex++)
            {
                var objectToAdd = ProtocolTypeManager.GetInstance<FightTeamInformations>(reader.ReadShort());
                objectToAdd.Deserialize(reader);
                FightTeams[fightTeamsIndex] = objectToAdd;
            }
            var fightTeamsPositionsCount = reader.ReadUShort();
            FightTeamsPositions = new ushort[fightTeamsPositionsCount];
            for (var fightTeamsPositionsIndex = 0; fightTeamsPositionsIndex < fightTeamsPositionsCount; fightTeamsPositionsIndex++)
            {
                FightTeamsPositions[fightTeamsPositionsIndex] = reader.ReadVarUShort();
            }
            var fightTeamsOptionsCount = reader.ReadUShort();
            FightTeamsOptions = new FightOptionsInformations[fightTeamsOptionsCount];
            for (var fightTeamsOptionsIndex = 0; fightTeamsOptionsIndex < fightTeamsOptionsCount; fightTeamsOptionsIndex++)
            {
                var objectToAdd = new FightOptionsInformations();
                objectToAdd.Deserialize(reader);
                FightTeamsOptions[fightTeamsOptionsIndex] = objectToAdd;
            }
        }

    }
}
