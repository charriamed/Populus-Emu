using Stump.DofusProtocol.Enums;
using Stump.Server.WorldServer.Game.Actors.Fight;
using Stump.Server.WorldServer.Game.Fights.Teams;
using Stump.Server.WorldServer.Game.Spells;

namespace Stump.Server.WorldServer.AI.Fights.Brain.Custom.Summons
{
    [BrainIdentifier((int)MonsterIdEnum.SAC_ANIME)]
    public class LivingBagBrain : Brain
    {
        public LivingBagBrain(AIFighter fighter)
            : base(fighter)
        {
            fighter.Team.FighterAdded += OnFighterAdded;
        }

        void OnFighterAdded(FightTeam team, FightActor fighter)
        {
            if (fighter != Fighter)
                return;

            fighter.CastAutoSpell(new Spell((int)SpellIdEnum.BAGRIFICE_3252, 1), fighter.Cell);
        }
    }

    [BrainIdentifier((int)MonsterIdEnum.MUSETTE_ANIMEE)]
    public class MusetteBrain : Brain
    {
        public MusetteBrain(AIFighter fighter)
            : base(fighter)
        {
            fighter.Team.FighterAdded += OnFighterAdded;
        }

        void OnFighterAdded(FightTeam team, FightActor fighter)
        {
            if (fighter != Fighter)
                return;

            fighter.CastAutoSpell(new Spell((int)SpellIdEnum.SHARING_9549, 1), fighter.Cell);
        }
    }

    [BrainIdentifier((int)MonsterIdEnum.PELLE_ANIMEE)]
    [BrainIdentifier((int)MonsterIdEnum.BECHE_ANIMEE)]
    [BrainIdentifier((int)MonsterIdEnum.COFFRE_ANIME)]
    [BrainIdentifier((int)MonsterIdEnum.COFFRE_REGENERANT)]
    public class EnuBrain : Brain
    {
        public EnuBrain(AIFighter fighter)
            : base(fighter)
        {
            fighter.Team.FighterAdded += OnFighterAdded;
        }

        void OnFighterAdded(FightTeam team, FightActor fighter)
        {
            if (fighter != Fighter)
                return;

            fighter.CastAutoSpell(new Spell((int)SpellIdEnum.FRASCH_9556, 1), fighter.Cell);
        }
    }
}
