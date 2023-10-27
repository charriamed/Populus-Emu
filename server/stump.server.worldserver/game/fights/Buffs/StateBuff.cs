using Stump.DofusProtocol.Enums;
using Stump.DofusProtocol.Types;
using Stump.Server.WorldServer.Database.Spells;
using Stump.Server.WorldServer.Game.Actors.Fight;
using Stump.Server.WorldServer.Game.Effects.Instances;
using Stump.Server.WorldServer.Game.Spells;
using System;
using Stump.Server.WorldServer.Game.Effects.Handlers.Spells;

namespace Stump.Server.WorldServer.Game.Fights.Buffs
{
    public class StateBuff : Buff
    {
        public StateBuff(int id, FightActor target, FightActor caster, SpellEffectHandler effect, Spell spell, FightDispellableEnum dispelable, SpellState state, FightActor triggerer = null)
            : base(id, target, caster, effect, spell, false, dispelable, triggerer: triggerer)
        {
            State = state;
        }

        public SpellState State
        {
            get;
        }

        public bool IsDisabled
        {
            get;
            set;
        }

        public override void Apply()
        {
            base.Apply();

            Target.TriggerBuffs(Target, BuffTriggerType.OnStateAdded);
            Target.TriggerBuffs(Target, BuffTriggerType.OnSpecificStateAdded, State.Id);
        }

        public override void Dispell()
        {
            base.Dispell();

            Target.TriggerBuffs(Target, BuffTriggerType.OnStateRemoved);
            Target.TriggerBuffs(Target, BuffTriggerType.OnSpecificStateRemoved, State.Id);
        }

        public override AbstractFightDispellableEffect GetAbstractFightDispellableEffect()
        {
            if (Delay == 0)
                return new FightTemporaryBoostStateEffect((uint)Id, Target.Id, Duration, (sbyte)Dispellable, (ushort)Spell.Id, (uint)(EffectFix?.ClientEffectId ?? (ushort)Effect.Id), 0, 1, (short)State.Id);

            var values = Effect.GetValues();

            return new FightTriggeredEffect((uint)Id, Target.Id, Delay,
                (sbyte)Dispellable,
                (ushort)Spell.Id, (uint)(EffectFix?.ClientEffectId ?? (ushort)Effect.Id), 0,
                (values.Length > 0 ? Convert.ToInt32(values[0]) : 0),
                (values.Length > 1 ? Convert.ToInt32(values[1]) : 0),
                (values.Length > 2 ? Convert.ToInt32(values[2]) : 0),
                Delay);
        }
    }
}