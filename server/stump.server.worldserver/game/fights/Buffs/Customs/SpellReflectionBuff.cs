using Stump.DofusProtocol.Enums;
using Stump.DofusProtocol.Types;
using Stump.Server.WorldServer.Game.Actors.Fight;
using Stump.Server.WorldServer.Game.Effects.Instances;
using Stump.Server.WorldServer.Game.Spells;
using System;
using MongoDB.Bson.Serialization.Conventions;
using Stump.Core.Mathematics;
using Stump.Server.WorldServer.Game.Effects.Handlers.Spells;

namespace Stump.Server.WorldServer.Game.Fights.Buffs.Customs
{
    public class SpellReflectionBuff : Buff
    {
        public SpellReflectionBuff(int id, FightActor target, FightActor caster, SpellEffectHandler effect, Spell spell, bool critical, FightDispellableEnum dispelable)
            : base(id, target, caster, effect, spell, critical, dispelable)
        {
        }
        

        public int ReflectedLevel => Dice.DiceFace;
        public int ReflectionChance => Dice.Value;

        public bool RollReflection()
        {
            var rand = new CryptoRandom();

            return rand.Next(0, 101) <= ReflectionChance;
        }
        
        public override AbstractFightDispellableEffect GetAbstractFightDispellableEffect()
        {
            var turnDuration = Delay == 0 ? Duration : Delay;

            var values = Effect.GetValues();

            return new FightTriggeredEffect((uint)Id, Target.Id, turnDuration,
                (sbyte)Dispellable,
                (ushort)Spell.Id, (uint)(EffectFix?.ClientEffectId ?? Effect.Id), 0,
                (values.Length > 0 ? Convert.ToInt32(values[0]) : 0),
                (values.Length > 1 ? Convert.ToInt32(values[1]) : 0),
                (values.Length > 2 ? Convert.ToInt32(values[2]) : 0),
                Delay);
        }
    }
}