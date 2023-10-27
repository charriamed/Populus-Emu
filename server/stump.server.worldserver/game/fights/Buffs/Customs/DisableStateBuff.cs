﻿using Stump.DofusProtocol.Enums;
using Stump.DofusProtocol.Types;
using Stump.Server.WorldServer.Game.Actors.Fight;
using Stump.Server.WorldServer.Game.Spells;
using System;
using Stump.Server.WorldServer.Game.Effects.Handlers.Spells;

namespace Stump.Server.WorldServer.Game.Fights.Buffs.Customs
{
    public class DisableStateBuff : Buff
    {
        public DisableStateBuff(int id, FightActor target, FightActor caster, SpellEffectHandler effect, Spell spell, FightDispellableEnum dispelable, StateBuff stateBuff, FightActor triggerer = null)
            : base(id, target, caster, effect, spell, false, dispelable, triggerer)
        {
            StateBuff = stateBuff;
        }

        public StateBuff StateBuff
        {
            get;
        }

        public override void Apply()
        {
            base.Apply();

            StateBuff.IsDisabled = true;
        }

        public override void Dispell()
        {
            base.Dispell();

            StateBuff.IsDisabled = false;
        }

        public override AbstractFightDispellableEffect GetAbstractFightDispellableEffect()
        {
            if (Delay == 0)
                return new FightTemporaryBoostStateEffect((uint)Id, Target.Id, Duration, (sbyte)Dispellable, (ushort)Spell.Id, (uint)(EffectFix?.ClientEffectId ?? Effect.Id), 0, -100, (short)StateBuff.State.Id);

            var values = Effect.GetValues();

            return new FightTriggeredEffect((uint)Id, Target.Id, Delay,
                (sbyte)Dispellable,
                (ushort)Spell.Id, (uint)(EffectFix?.ClientEffectId ?? Effect.Id), 0,
                (values.Length > 0 ? Convert.ToInt32(values[0]) : 0),
                (values.Length > 1 ? Convert.ToInt32(values[1]) : 0),
                (values.Length > 2 ? Convert.ToInt32(values[2]) : 0),
                Delay);
        }
    }
}
