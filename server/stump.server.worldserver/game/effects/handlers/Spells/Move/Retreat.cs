﻿using Stump.DofusProtocol.Enums;
using Stump.Server.WorldServer.Database.World;
using Stump.Server.WorldServer.Game.Actors.Fight;
using Stump.Server.WorldServer.Game.Effects.Instances;
using Stump.Server.WorldServer.Game.Spells.Casts;
using System.Linq;

namespace Stump.Server.WorldServer.Game.Effects.Handlers.Spells.Move
{
    [EffectHandler(EffectsEnum.Effect_Retreat)]
    public class Retreat : Push
    {
        public Retreat(EffectDice effect, FightActor caster, SpellCastHandler castHandler, Cell targetedCell, bool critical)
            : base(effect, caster, castHandler, targetedCell, critical)
        {
            DamagesDisabled = true;
        }

        protected override bool InternalApply()
        {
            var target = GetAffectedActors();

            if (!target.Any())
                return false;

            SetAffectedActors(new[] { Caster });

            return base.InternalApply();
        }
    }
}
