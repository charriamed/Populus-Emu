using System.Linq;
using Stump.DofusProtocol.Enums;
using Stump.Server.WorldServer.Game.Actors.Fight;
using Stump.Server.WorldServer.Game.Fights.Teams;
using Stump.Server.WorldServer.Game.Spells;
using Stump.Server.WorldServer.Game.Fights;

namespace Stump.Server.WorldServer.AI.Fights.Brain.Custom.Summons
{
    [BrainIdentifier((int)MonsterIdEnum.TOFU_NOIR_4561)]
    public class Tofu : Brain
    {
        public Tofu(AIFighter fighter)
            : base(fighter)
        {
            fighter.Team.FighterAdded += OnFighterAdded;
        }

        void OnFighterAdded(FightTeam team, FightActor fighter)
        {
            var spellHandler = SpellManager.Instance.GetSpellCastHandler((Fighter as SummonedMonster).Summoner, new Spell((int)SpellIdEnum.ANIMAL_LINK_7064, 1), Fighter.Cell, false);
            spellHandler.Initialize();

            var handlers = spellHandler.GetEffectHandlers().ToArray();

            using (Fighter.Fight.StartSequence(SequenceTypeEnum.SEQUENCE_SPELL))
            {
                spellHandler.Execute();
            }
        }
    }
}
