using Stump.DofusProtocol.Enums;
using Stump.DofusProtocol.Types;
using Stump.Server.WorldServer.Game.Actors.Fight;
using Stump.Server.WorldServer.Game.Spells;
using System;
using Stump.Server.WorldServer.Game.Effects.Handlers.Spells;
using System.Collections.Generic;

namespace Stump.Server.WorldServer.Game.Fights.Buffs
{
    public class SpellBuff : Buff
    {
        public SpellBuff(int id, FightActor target, FightActor caster, SpellEffectHandler effect, Spell spell, Spell boostedSpell, short boost, bool critical, FightDispellableEnum dispelable, List<CharacterSpell> spellos = null)
            : base(id, target, caster, effect, spell, critical, dispelable)
        {
            BoostedSpell = boostedSpell;
            Boost = boost;
            Spellos = spellos;
        }

        public Spell BoostedSpell
        {
            get;
        }

        public List<CharacterSpell> Spellos
        {
            get;
        }

        public short Boost
        {
            get;
        }

        public override void Apply()
        {
            base.Apply();
            if (Effect.EffectId == EffectsEnum.Effect_294)
            {
                try { (Target as CharacterFighter).Character.SpellRangeHandler((short)BoostedSpell.Id, (short)-Boost); } catch { }
            }
            else if (Effect.EffectId == EffectsEnum.Effect_SpellRangeIncrease)
            {
                try { (Target as CharacterFighter).Character.SpellRangeHandler((short)BoostedSpell.Id, Boost); } catch { }
            }
            else if (Effect.EffectId == EffectsEnum.Effect_SpellObstaclesDisable)
            {
                var chara = Target as CharacterFighter;
                if (chara == null) return;
                if (Spellos == null)
                {
                    chara.Character.SpellObstaclesDisable(BoostedSpell);
                }
                else
                {
                    Spellos.ForEach(x => chara.Character.SpellObstaclesDisable(x));
                }
            }
            else if (Effect.EffectId == EffectsEnum.Effect_ApCostReduce)
            {
                try { (Target as CharacterFighter).Character.ReduceSpellCost((short)BoostedSpell.Id, (uint)Boost); } catch { }
            }
            else
            {
                try { (Target as CharacterFighter).Character.SpellAddDamage((short)BoostedSpell.Id, (uint)Boost); } catch { }
                Target.BuffSpell(BoostedSpell, Boost);
            }
        }

        public override void Dispell()
        {
            base.Dispell();
            if (Effect.EffectId == EffectsEnum.Effect_294)
            {
                try { (Target as CharacterFighter).Character.IncreaseRangeDisable((short)BoostedSpell.Id, (short)-Boost); } catch { }
            }
            else if (Effect.EffectId == EffectsEnum.Effect_SpellRangeIncrease)
            {
                try { (Target as CharacterFighter).Character.IncreaseRangeDisable((short)BoostedSpell.Id, Boost); } catch { }
            }
            else if (Effect.EffectId == EffectsEnum.Effect_SpellObstaclesDisable)
            {
                var chara = Target as CharacterFighter;
                if (chara == null) return;
                if (Spellos == null)
                {
                    chara.Character.SpellObstaclesEnable(BoostedSpell);
                }
                else
                {
                    Spellos.ForEach(x => chara.Character.SpellObstaclesEnable(x));
                }
            }
            else if (Effect.EffectId == EffectsEnum.Effect_ApCostReduce)
            {
                try { (Target as CharacterFighter).Character.SpellCostDisable((short)BoostedSpell.Id, Boost); } catch { }
            }
            else
            {
                Target.UnBuffSpell(BoostedSpell, Boost);
                try { (Target as CharacterFighter).Character.SpellAddDamageDisable((short)BoostedSpell.Id); } catch { }
            }
        }

        public override AbstractFightDispellableEffect GetAbstractFightDispellableEffect()
        {
            if (Delay == 0)
            {
                if (Effect.EffectId == EffectsEnum.Effect_294 || Effect.EffectId == EffectsEnum.Effect_SpellRangeIncrease || Effect.EffectId == EffectsEnum.Effect_SpellObstaclesDisable || Effect.EffectId == EffectsEnum.Effect_ApCostReduce) return new AbstractFightDispellableEffect();
                var x = new FightTemporarySpellBoostEffect((ushort)Id, Target.Id, Duration, (sbyte)Dispellable, (ushort)Spell.Id, (uint)Effect.Id, 0, Boost, (ushort)BoostedSpell.Id);
                return x;

            }


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