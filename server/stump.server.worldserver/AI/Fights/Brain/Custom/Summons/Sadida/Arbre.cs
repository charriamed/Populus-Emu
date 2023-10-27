using Stump.DofusProtocol.Enums;
using Stump.Server.WorldServer.Game.Actors.Fight;
using Stump.Server.WorldServer.Game.Fights.Teams;
using Stump.Server.WorldServer.Game.Spells;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stump.Server.WorldServer.AI.Fights.Brain.Custom.Summons.Sadida
{
    [BrainIdentifier((int)MonsterIdEnum.ARBRE)]
    public class Arbre : Brain
    {
        public Arbre(AIFighter fighter)
            : base(fighter)
        {
            fighter.Team.FighterAdded += OnFighterAdded;
        }

        void OnFighterAdded(FightTeam team, FightActor fighter)
        {
            if (fighter != Fighter)
                return;

            fighter.CastAutoSpell(new Spell(5567, 1), fighter.Cell);
        }
    }
}
