using System;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Characters;
using System.Linq;
using Stump.Server.WorldServer.Game.Quests;

namespace Stump.Server.WorldServer.Game.Conditions.Criterions
{
    public class QuestObjectiveCriterion : Criterion
    {
        public const string Identifier = "Qo";

        public int QuestObjectiveId
        {
            get;
            set;
        }

        public override bool Eval(Character character)
        {
            return character.Quests.Where(x => x.CurrentStep.Objectives.Any(y => y.ObjectiveRecord.ObjectiveId == QuestObjectiveId && y.Finished)).Count() > 0;
        }

        public override void Build()
        {
            int questObjectiveId;

            if (!int.TryParse(Literal, out questObjectiveId))
                throw new Exception(string.Format("Cannot build QuestActiveCriterion, {0} is not a valid quest objective id", Literal));

            QuestObjectiveId = questObjectiveId;
        }

        public override string ToString()
        {
            return FormatToString(Identifier);
        }
    }
}