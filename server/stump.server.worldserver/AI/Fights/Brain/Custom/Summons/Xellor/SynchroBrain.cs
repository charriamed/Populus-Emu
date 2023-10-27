﻿using System.Linq;
using Stump.DofusProtocol.Enums;
using Stump.Server.WorldServer.Game.Actors.Fight;
using Stump.Server.WorldServer.Game.Fights.Teams;
using Stump.Server.WorldServer.Game.Spells;

namespace Stump.Server.WorldServer.AI.Fights.Brain.Custom.Summons
{
    [BrainIdentifier((int)MonsterIdEnum.SYNCHRO)]
    public class SynchroBrain : Brain
    {
        public SynchroBrain(AIFighter fighter)
            : base(fighter)
        {
            fighter.Team.FighterAdded += OnFighterAdded;
        }

        void OnFighterAdded(FightTeam team, FightActor fighter)
        {
            if (fighter != Fighter)
                return;

            var spellHandler = SpellManager.Instance.GetSpellCastHandler(Fighter, new Spell((int)SpellIdEnum.SYNCHRONISATION, 0), Fighter.Cell, false);
            spellHandler.Initialize();

            var handlers = spellHandler.GetEffectHandlers().ToArray();

            using (Fighter.Fight.StartSequence(SequenceTypeEnum.SEQUENCE_SPELL))
            {

                handlers[1].Apply(); //SubAP Summoner
                handlers[2].Apply(); //BuffTrigger
                handlers[3].Apply(); //SpellImmunity
            }
        }
    }
}
