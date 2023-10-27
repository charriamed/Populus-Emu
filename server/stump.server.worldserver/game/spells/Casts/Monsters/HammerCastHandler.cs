using System.Linq;
using Stump.DofusProtocol.Enums;
using Stump.Server.WorldServer.Game.Actors.Fight;
using Stump.Server.WorldServer.Game.Fights;
using Stump.Server.WorldServer.Game.Fights.Buffs;

namespace Stump.Server.WorldServer.Game.Spells.Casts.Monsters
{
    [SpellCastHandler(SpellIdEnum.TEERTS_HAMMER)]
    [SpellCastHandler(SpellIdEnum.BENANDGERRIZ_HAMMER)]
    public class HammerCastHandler : DefaultSpellCastHandler
    {
        public HammerCastHandler(SpellCastInformations cast)
            : base(cast)
        {
        }

        public override void Execute()
        {
            if (!m_initialized)
                Initialize();

            //Base Effects
            Handlers[0].Apply();
            Handlers[Spell.Id == (int)SpellIdEnum.BENANDGERRIZ_HAMMER ? 1 : (Critical ? 2 : 1)].Apply();

            if (!Critical)
                return;

            Handlers[3].Apply(); //Frozen State

            if (Spell.Id == (int) SpellIdEnum.BENANDGERRIZ_HAMMER)
                Handlers[4].Apply();
            else
            {
                Handlers[5].Apply(); //StealHP Water
                Handlers[6].Apply(); //Change Look   
            }

            var affectedActors = Handlers[3].GetAffectedActors().ToArray();

            foreach (var actor in affectedActors)
            {
                var buffId = actor.PopNextBuffId();
                var effect = Spell.CurrentSpellLevel.Effects[0];

                var buff = new TriggerBuff(buffId, actor, actor, Handlers[0], Spell, Spell, false, FightDispellableEnum.DISPELLABLE_BY_DEATH, 0, SpellBuffTrigger)
                {
                    Duration = (short)effect.Duration
                };
                buff.SetTrigger(BuffTriggerType.AfterDamaged);

                actor.AddBuff(buff); 
            }
        }

        private void SpellBuffTrigger(TriggerBuff buff, FightActor triggerrer, BuffTriggerType trigger, object token)
        {
            var damage = token as Damage;
            if (damage == null)
                return;

            if (damage.Source == null)
                return;

            var target = buff.Target;

            if (damage.Source == target)
                return;

            if (damage.Spell != null && damage.Spell.Id != (int)SpellIdEnum.PUNCH_0)
                return;

            Handlers[Spell.Id == (int)SpellIdEnum.BENANDGERRIZ_HAMMER ? 2 : 1].Apply(); //Remove Effects
            Handlers[Spell.Id == (int)SpellIdEnum.BENANDGERRIZ_HAMMER ? 5 : 4].Apply(); //Dispell State
        }
    }
}
