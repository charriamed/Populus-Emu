﻿using System;
using Stump.DofusProtocol.Enums;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Characters;
using Stump.Server.WorldServer.Game.Effects.Instances;
using Stump.Server.WorldServer.Game.Items.Player;
using Stump.Server.WorldServer.Handlers.Basic;

namespace Stump.Server.WorldServer.Game.Effects.Handlers.Usables
{
    [EffectHandler(EffectsEnum.Effect_AddHealth)]
    public class RestoreHp : UsableEffectHandler
    {
        public RestoreHp(EffectBase effect, Character target, BasePlayerItem item)
            : base(effect, target, item)
        {
        }

        protected override bool InternalApply()
        {
            var integerEffect = Effect.GenerateEffect(EffectGenerationContext.Item) as EffectInteger;

            if (integerEffect == null)
                return false;

            if (Target.Stats.Health.DamageTaken == 0)
            {
                // health already to max
                BasicHandler.SendTextInformationMessage(Target.Client, TextInformationTypeEnum.TEXT_INFORMATION_MESSAGE, 225);
                return false;
            }

            var heal = (int) (integerEffect.Value * NumberOfUses);
            if (Target.Stats.Health.DamageTaken < heal)
            {
                heal = Target.Stats.Health.DamageTaken;
            }

            UsedItems = (uint)Math.Ceiling((double)heal / integerEffect.Value);
            Target.Stats.Health.DamageTaken -= heal;

            // x hp restored
            Target.RefreshStats();
            BasicHandler.SendTextInformationMessage(Target.Client, TextInformationTypeEnum.TEXT_INFORMATION_MESSAGE, 1, heal);
            Target.PlayEmote(EmotesEnum.EMOTE_MANGER, true);

            return true;
        }
    }
}