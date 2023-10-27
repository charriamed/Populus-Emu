namespace Stump.DofusProtocol.Types
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System;
    using Stump.Core.IO;
    using Stump.DofusProtocol.Types;

    [Serializable]
    public class FightExternalInformations
    {
        public const short Id = 117;
        public virtual short TypeId
        {
            get { return Id; }
        }
        public ushort fightId;
        public sbyte fightType;
        public int fightStart;
        public bool fightSpectatorLocked;
        public FightTeamLightInformations[] fightTeams;
        public FightOptionsInformations[] fightTeamsOptions;

        public FightExternalInformations(ushort fightId, sbyte fightType, int fightStart, bool fightSpectatorLocked, FightTeamLightInformations[] fightTeams, FightOptionsInformations[] fightTeamsOptions)
        {
            this.fightId = fightId;
            this.fightType = fightType;
            this.fightStart = fightStart;
            this.fightSpectatorLocked = fightSpectatorLocked;
            this.fightTeams = fightTeams;
            this.fightTeamsOptions = fightTeamsOptions;
        }

        public FightExternalInformations() { }

        public virtual void Serialize(IDataWriter writer)
        {
            writer.WriteVarUShort(fightId);
            writer.WriteSByte(fightType);
            writer.WriteInt(fightStart);
            writer.WriteBoolean(fightSpectatorLocked);
            for (int i = 0; i < 2; i++)
            {
                fightTeams[i].Serialize(writer);
            }
            for (int i = 0; i < 2; i++)
            {
                fightTeamsOptions[i].Serialize(writer);
            }
        }

        public virtual void Deserialize(IDataReader reader)
        {
            fightId = reader.ReadVarUShort();
            fightType = reader.ReadSByte();
            fightStart = reader.ReadInt();
            fightSpectatorLocked = reader.ReadBoolean();
            fightTeams = new FightTeamLightInformations[2];
            for (int i = 0; i < 2; i++)
            {
                fightTeams[i] = new FightTeamLightInformations();
                fightTeams[i].Deserialize(reader);
            }
            fightTeamsOptions = new FightOptionsInformations[2];
            for (int i = 0; i < 2; i++)
            {
                fightTeamsOptions[i] = new FightOptionsInformations();
                fightTeamsOptions[i].Deserialize(reader);
            }
        }

    }
}