//using Stump.DofusProtocol.Enums;
//using Stump.Server.WorldServer.Database.World;
//using Stump.Server.WorldServer.Game.Actors.Fight;
//using Stump.Server.WorldServer.Game.Effects.Instances;
//using Stump.Server.WorldServer.Game.Fights.Buffs;
//using Stump.Server.WorldServer.Game.Spells.Casts;
//using Stump.Server.WorldServer.Handlers.Actions;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Stump.Server.WorldServer.Game.Effects.Handlers.Spells.Armor
//{
//    [EffectHandler(EffectsEnum.Effect_984)]
//    public class DofusIvoire : SpellEffectHandler
//    {
//        public DofusIvoire(EffectDice effect, FightActor caster, SpellCastHandler castHandler, Cell targetedCell, bool critical)
//            : base(effect, caster, castHandler, targetedCell, critical)
//        {
//        }

//        protected override bool InternalApply()
//        {
//            foreach (var actor in GetAffectedActors())
//            {
//                var integerEffect = GenerateEffect();

//                if (integerEffect == null)
//                    return false;

//                var shieldAmount = (short)(Caster.Stats.Health.TotalMaxWithoutPermanentDamages * (integerEffect.Value / 100d));

//                if (Effect.Duration != 0 || Effect.Delay != 0)
//                {
//                    if (Spell.Id == (int)SpellIdEnum.EMERALD_DOFUS)
//                    {
//                        var buff = Caster.GetBuffs(entry => entry.Spell.Id == (int)SpellIdEnum.EMERALD_DOFUS &&
//                                                   entry.Dice.Id == 1039).ToList();

//                        if (buff.Count() == 0)
//                            AddStatBuff(actor, shieldAmount, PlayerFields.Shield, (short)EffectsEnum.Effect_AddShield);
//                    }

//                    else
//                        AddStatBuff(actor, shieldAmount, PlayerFields.Shield, (short)EffectsEnum.Effect_AddShield);

//                }
//                else
//                {
//                    actor.Stats[PlayerFields.Shield].Context += shieldAmount;
//                }
//            }

//            return true;
//        }
//    }
//}
