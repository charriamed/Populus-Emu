﻿using System;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Characters;
using System.Linq;

namespace Stump.Server.WorldServer.Game.Conditions.Criterions
{
    public class QuestActiveCriterion : Criterion
    {
        public const string Identifier = "Qa";

        public int QuestId
        {
            get;
            set;
        }

        public override bool Eval(Character character)
        {
            return character.Quests.Where(x => !x.Finished && x.Id == QuestId).Count() > 0;
        }

        public override void Build()
        {
            int questId;

            if (!int.TryParse(Literal, out questId))
                throw new Exception(string.Format("Cannot build QuestActiveCriterion, {0} is not a valid quest id", Literal));

            QuestId = questId;
        }

        public override string ToString()
        {
            return FormatToString(Identifier);
        }
    }
}