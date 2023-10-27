using Stump.DofusProtocol.Enums;
using Stump.Server.WorldServer.AI.Fights.Actions;
using Stump.Server.WorldServer.Database.World;
using Stump.Server.WorldServer.Game.Actors.Fight;
using Stump.Server.WorldServer.Game.Effects;
using Stump.Server.WorldServer.Game.Effects.Handlers.Spells;
using Stump.Server.WorldServer.Game.Effects.Instances;
using Stump.Server.WorldServer.Game.Fights;
using Stump.Server.WorldServer.Game.Fights.Buffs;
using Stump.Server.WorldServer.Game.Fights.Teams;
using Stump.Server.WorldServer.Game.Spells;
using System;
using System.Collections.Generic;
using System.Linq;
using TreeSharp;

namespace Stump.Server.WorldServer.AI.Fights.Brain.Custom.Summons
{
    [BrainIdentifier((int)MonsterIdEnum.ROULETTE_5108)]
    public class Ruleta1 : Brain
    {
        public Ruleta1(AIFighter fighter)
            : base(fighter)
        {
            fighter.Team.FighterAdded += OnFighterAdded;
        }

        void OnFighterAdded(FightTeam team, FightActor fighter)
        {
            if (fighter != Fighter)
                return;

            if (!(Fighter is SummonedMonster))
                return;

            var roulette = (SummonedFighter)fighter;

            roulette.CastAutoSpell(new Spell(9445, 1), roulette.Summoner.Cell);
        }
    }

    [BrainIdentifier((int)MonsterIdEnum.ROULETTE_5189)]
    public class Ruleta2 : Brain
    {
        public Ruleta2(AIFighter fighter)
            : base(fighter)
        {
            fighter.Fight.TurnStarted += OnTurnStarted;
        }

        void OnTurnStarted(IFight fight, FightActor player)
        {
            if (player != Fighter)
                return;

            if (!(Fighter is SummonedMonster))
                return;

            player.CastAutoSpell(new Spell(9435, 1), player.Cell);
        }
    }
}
