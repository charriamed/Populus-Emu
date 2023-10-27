﻿using System;
using Stump.DofusProtocol.Enums;
using Stump.DofusProtocol.Types;
using Stump.Server.WorldServer.Game.Actors.Fight;
using Stump.Server.WorldServer.Game.Effects.Handlers.Spells;
using Stump.Server.WorldServer.Game.Spells;

namespace Stump.Server.WorldServer.Game.Fights.Buffs.Customs
{
    public class RescaleSkinBuff : Buff
    {
        public RescaleSkinBuff(int id, FightActor target, FightActor caster, SpellEffectHandler effectHandler, Spell spell, bool critical, FightDispellableEnum dispelable, double rescaleFactor)
            : base(id, target, caster, effectHandler, spell, critical, dispelable)
        {
            RescaleFactor = rescaleFactor;
        }

        public double RescaleFactor
        {
            get;
        }

        public override void Apply()
        {
            base.Apply();
            Target.UpdateLook(Caster);
        }

        public override void Dispell()
        {
            base.Dispell();
            Target.UpdateLook(Caster);
        }

        public override AbstractFightDispellableEffect GetAbstractFightDispellableEffect()
        {
            var values = Effect.GetValues();

            if (Delay == 0)
                return new AbstractFightDispellableEffect();

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