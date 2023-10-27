using Stump.DofusProtocol.Enums;
using Stump.Server.WorldServer.Database.Items.Templates;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Characters;
using Stump.Server.WorldServer.Game.Effects.Handlers.Usables;
using Stump.Server.WorldServer.Game.Effects.Instances;
using Stump.Server.WorldServer.Game.Items.Player;
using Stump.Server.WorldServer.Handlers.Context;
using Stump.Server.WorldServer.Handlers.Visual;

namespace Stump.Server.WorldServer.Game.Effects.Handlers.Usables
{
    [EffectHandler(EffectsEnum.Effect_Play_Animation)]
    public class PlayAnimation : UsableEffectHandler
    {
        public PlayAnimation(EffectBase effect, Character target, BasePlayerItem item) : base(effect, target, item)
        {
        }

        protected override bool InternalApply()
        {
            var integerEffect = Effect.GenerateEffect(EffectGenerationContext.Item) as EffectInteger;

            if (integerEffect == null)
                return false;

            UsedItems = NumberOfUses;

            if (TargetCell == null)
                TargetCell = Target.Cell;

            Target.Map.ForEach(x => VisualHandler.SendGameRolePlaySpellAnimMessage(x.Client, Target, TargetCell.Id, integerEffect.Value));

            return true;
        }
    }
}