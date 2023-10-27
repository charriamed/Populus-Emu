﻿using Stump.DofusProtocol.Enums;
using Stump.Server.WorldServer.Database.World;
using Stump.Server.WorldServer.Game.Actors.Fight;
using Stump.Server.WorldServer.Game.Effects.Instances;
using Stump.Server.WorldServer.Game.Spells;
using Stump.Server.WorldServer.Handlers.Actions;
using Stump.Server.WorldServer.Handlers.Context;
using Stump.Server.WorldServer.Game.Spells.Casts;
using Stump.Server.WorldServer.Game.Fights.Buffs;
using Stump.Server.WorldServer.Game.Effects.Handlers.Spells.States;
using Stump.Server.WorldServer.Game.Fights;
using System.Linq;
using System;

namespace Stump.Server.WorldServer.Game.Effects.Handlers.Spells.Others
{
    [EffectHandler(EffectsEnum.Effect_ReviveAndGiveHPToLastDiedAlly)]
    public class ReviveAndGiveHP : SpellEffectHandler
    {
        public ReviveAndGiveHP(EffectDice effect, FightActor caster, SpellCastHandler castHandler, Cell targetedCell, bool critical)
            : base(effect, caster, castHandler, targetedCell, critical)
        {
        }

        public FightActor LastDeadFighter
        {
            get;
            private set;
        }

        protected override bool InternalApply()
        {
            var integerEffect = GenerateEffect();

            if (integerEffect == null)
                return false;

            LastDeadFighter = Caster.Team.GetLastDeadFighter();

            if (LastDeadFighter == null)
                return false;

            ReviveActor(LastDeadFighter, integerEffect.Value);

            if (LastDeadFighter is CharacterFighter) {
                foreach (var effect in (LastDeadFighter as CharacterFighter).Character.Inventory.GetEquipedItems().SelectMany(x => x.Effects).Where(y => y.EffectId == EffectsEnum.Effect_CastSpell_1175))
                {
                    EffectManager.Instance.GetSpellEffectHandler((EffectDice)effect, LastDeadFighter,
                        new DefaultSpellCastHandler(new SpellCastInformations(LastDeadFighter, null, LastDeadFighter.Cell)), LastDeadFighter.Cell, false).Apply();
                }
            }

            return true;
        }

        void ReviveActor(FightActor actor, int heal)
        {
            var cell = TargetedCell;
            if (!Fight.IsCellFree(cell))
                cell = Map.GetRandomAdjacentFreeCell(TargetedPoint, true);

            actor.Revive(heal, Caster);
            actor.SummoningEffect = this;
            actor.Position.Cell = cell;
            actor.BuffRemoved += OnBuffRemoved;

            if (Spell.Id == (int)SpellIdEnum.SPIRITUAL_LEASH_420)
            {
                try
                {
                    var actorBuffId = actor.PopNextBuffId();

                    var addStateHandler = new AddState(new EffectDice(EffectsEnum.Effect_AddState, (short)SpellStatesEnum.ZOMBI_74, 0, 0), actor, null, actor.Cell, false);
                    var actorBuff = new StateBuff(actorBuffId, actor, Caster, addStateHandler,
                        Spell, FightDispellableEnum.DISPELLABLE_BY_DEATH, SpellManager.Instance.GetSpellState((uint)SpellStatesEnum.ZOMBI_74))
                    {
                        Duration = -1000
                    };

                    actor.AddBuff(actorBuff, true);
                }
                catch (Exception e) { }
                
            }

            ActionsHandler.SendGameActionFightReviveMessage(Fight.Clients, Caster, actor);
            ContextHandler.SendGameFightTurnListMessage(Fight.Clients, Fight);
        }

        static void OnBuffRemoved(FightActor actor, Buff buff)
        {
            if ((buff as StateBuff)?.State.Id == (uint)SpellStatesEnum.ZOMBI_74)
                actor.Die();
        }
    }
}
