using Stump.DofusProtocol.Types;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Characters;

namespace Stump.Server.WorldServer.Game.Fights.Results.Data
{
    public class FightExperienceDataLos : FightResultAdditionalData
    {
        public FightExperienceDataLos(Character character)
            : base(character)
        {
        }

        public bool ShowExperience
        {
            get;
            set;
        }

        public bool ShowExperienceLevelFloor
        {
            get;
            set;
        }

        public bool ShowExperienceNextLevelFloor
        {
            get;
            set;
        }

        public bool ShowExperienceFightDelta
        {
            get;
            set;
        }

        public bool ShowExperienceForGuild
        {
            get;
            set;
        }

        public bool ShowExperienceForMount
        {
            get;
            set;
        }

        public bool IsIncarnationExperience
        {
            get;
            set;
        }

        public int ExperienceFightDelta
        {
            get;
            set;
        }

        public int ExperienceForGuild
        {
            get;
            set;
        }

        public int ExperienceForMount
        {
            get;
            set;
        }

        public override DofusProtocol.Types.FightResultAdditionalData GetFightResultAdditionalData()
        {
            return new FightResultExperienceData(ShowExperience, ShowExperienceLevelFloor, ShowExperienceNextLevelFloor, ShowExperienceFightDelta, ShowExperienceForGuild, ShowExperienceForMount,
                                                 IsIncarnationExperience, (ulong)Character.Experience, (ulong)Character.LowerBoundExperience, (ulong)Character.UpperBoundExperience, (ulong)ExperienceFightDelta, (ulong)ExperienceForGuild, (ulong)ExperienceForMount, 1);
        }

        public override void Apply()
        {
            Character.LosExperience(ExperienceFightDelta);

        }
    }
}