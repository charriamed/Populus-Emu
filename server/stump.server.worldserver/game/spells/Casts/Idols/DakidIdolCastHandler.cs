//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Stump.Server.WorldServer.Game.Fights;
//using Stump.DofusProtocol.Enums;
//using Stump.Server.WorldServer.Game.Fights.Buffs;
//using Stump.Server.WorldServer.Game.Actors.Fight;
//using Stump.Server.WorldServer.Game.Effects.Instances;
//using Stump.Server.WorldServer.Game.Effects.Handlers.Spells;
//using Stump.Server.WorldServer.Game.Effects.Handlers.Spells.Debuffs;
//using Stump.Server.WorldServer.Database.World;
//using Stump.Server.WorldServer.Game.Maps.Cells.Shapes;

//namespace Stump.Server.WorldServer.Game.Spells.Casts.Idols
//{
//    [SpellCastHandler(SpellIdEnum.DAKID)]
//    public class DakidIdolCastHandler : DefaultSpellCastHandler
//    {
//        public DakidIdolCastHandler(SpellCastInformations cast) : base(cast)
//        {
//        }
//        public override void Execute()
//        {
//            foreach (var spellEffectHandler in Handlers)
//            {
//                spellEffectHandler.DefaultDispellableStatus = FightDispellableEnum.NOT_DISPELLABLE;
//                spellEffectHandler.AddTriggerBuff(Caster, BuffTriggerType.AfterDamaged, Dakid);
//            }
//        }
//        public void Dakid(TriggerBuff buff, FightActor trigerrer, BuffTriggerType trigger, object token)
//        {
//            Damage damage = token as Damage;
//            if (damage == null)
//                return;

//            if (buff.EffectHandler.Effect.EffectId == EffectsEnum.Effect_PullForward)
//            {
//                (buff.EffectHandler.Effect as EffectDice).DiceNum = (short)(buff.Target.Position.Point.ManhattanDistanceTo(trigerrer.Position.Point) - 1);
//                buff.EffectHandler.SetAffectedActors(new FightActor[] { damage.Source });
//            }
//            else
//            {
//                buff.EffectHandler.Effect.Duration = 2;
//            }

//            buff.EffectHandler.Apply();
//        }
//    }
//    [SpellCastHandler(SpellIdEnum.DAGOB)]
//    public class DagobIdolCastHandler : DefaultSpellCastHandler
//    {
//        public DagobIdolCastHandler(SpellCastInformations cast) : base(cast)
//        {
//        }
//        public override void Execute()
//        {
//            foreach (var spellEffectHandler in Handlers)
//            {
//                spellEffectHandler.AddTriggerBuff(Caster, BuffTriggerType.OnTurnBegin, Dagob);
//            }
//        }

//        private void Dagob(TriggerBuff buff, FightActor trigerrer, BuffTriggerType trigger, object token)
//        {
//            buff.EffectHandler.Apply();
//        }
//    }
//    [SpellCastHandler(SpellIdEnum.NYAN)]
//    public class NyanIdolCastHandler : DefaultSpellCastHandler
//    {
//        public NyanIdolCastHandler(SpellCastInformations cast) : base(cast)
//        {
//        }
//        public override void Execute()
//        {
//            foreach (var spellEffectHandler in Handlers)
//            {
//                spellEffectHandler.Effect.Duration = 1;
//                spellEffectHandler.DefaultDispellableStatus = FightDispellableEnum.NOT_DISPELLABLE;
//                spellEffectHandler.AddTriggerBuff(Caster, BuffTriggerType.OnTurnBegin, Nyan);
//            }
//        }

//        private void Nyan(TriggerBuff buff, FightActor trigerrer, BuffTriggerType trigger, object token)
//        {
//            buff.EffectHandler.TargetedCell = buff.Target.Cell;
//            buff.EffectHandler.Apply();
//        }
//    }
//    [SpellCastHandler(SpellIdEnum.OUGAA)]
//    public class OugaaIdolCastHandler : DefaultSpellCastHandler
//    {
//        public OugaaIdolCastHandler(SpellCastInformations cast) : base(cast)
//        {
//        }
//        public override void Execute()
//        {
//            foreach (var spellEffectHandler in Handlers)
//            {
//                spellEffectHandler.DefaultDispellableStatus = FightDispellableEnum.NOT_DISPELLABLE;
//                spellEffectHandler.AddTriggerBuff(Caster, BuffTriggerType.OnTurnEnd, Ougaa);
//            }
//        }

//        private void Ougaa(TriggerBuff buff, FightActor trigerrer, BuffTriggerType trigger, object token)
//        {
//            Spell spell = new Spell((buff.EffectHandler.Effect as EffectDice).DiceNum, (byte)(buff.EffectHandler.Effect as EffectDice).DiceFace);
//            if (Fight.GetAllFighters(Caster.Position.Point.GetAdjacentCells().Select(x => Caster.Map.GetCell(x.CellId))).Count() > 0)
//            {
//                SpellEffectHandler effectHandler = new SubVitalityPercent(spell.CurrentSpellLevel.Effects[0], Caster, this, Caster.Cell, false);
//                effectHandler.Apply();
//            }
//        }
//    }
//    [SpellCastHandler(SpellIdEnum.PROXIMA)]
//    public class ProximaIdolCastHandler : DefaultSpellCastHandler
//    {
//        public ProximaIdolCastHandler(SpellCastInformations cast) : base(cast)
//        {
//        }
//        public override void Execute()
//        {
//            foreach (var spellEffectHandler in Handlers)
//            {
//                spellEffectHandler.DefaultDispellableStatus = FightDispellableEnum.NOT_DISPELLABLE;
//                spellEffectHandler.AddTriggerBuff(Caster, BuffTriggerType.OnDeath, Proxima);
//            }
//        }

//        private void Proxima(TriggerBuff buff, FightActor trigerrer, BuffTriggerType trigger, object token)
//        {
//            var fighters = Fight.GetAllFighters(Caster.Position.Point.GetAdjacentCells().Select(x => Caster.Map.GetCell(x.CellId)));
//            foreach (var fighter in fighters.Where(x => x.Team != Caster.Team))
//            {
//                fighter.Die();
//            }
//        }
//    }
//    [SpellCastHandler(SpellIdEnum.ZAIHN_5895)]
//    public class ZaihnIdolCastHandler : DefaultSpellCastHandler
//    {
//        public ZaihnIdolCastHandler(SpellCastInformations cast) : base(cast)
//        {
//        }
//        public override void Execute()
//        {
//            foreach (var spellEffectHandler in Handlers)
//            {
//                spellEffectHandler.DefaultDispellableStatus = FightDispellableEnum.NOT_DISPELLABLE;
//                spellEffectHandler.AddTriggerBuff(Caster, BuffTriggerType.Instant, Zaihn);
//            }
//        }

//        private void Zaihn(TriggerBuff buff, FightActor trigerrer, BuffTriggerType trigger, object token)
//        {
//            var spell = new Spell((buff.Effect as EffectDice).DiceNum, (byte)(buff.Effect as EffectDice).DiceFace);
//            Caster.CastAutoSpell(spell, Caster.Cell);
//        }
//    }
//    [SpellCastHandler(SpellIdEnum.ZAIHN_5935)]
//    public class Zaihn2IdolCastHandler : DefaultSpellCastHandler
//    {
//        public Zaihn2IdolCastHandler(SpellCastInformations cast) : base(cast)
//        {
//        }
//        public override void Execute()
//        {
//            foreach (var spellEffectHandler in Handlers)
//            {
//                spellEffectHandler.DefaultDispellableStatus = FightDispellableEnum.NOT_DISPELLABLE;
//                spellEffectHandler.AddTriggerBuff(Caster, BuffTriggerType.OnDeath, Zaihn);
//            }
//        }

//        private void Zaihn(TriggerBuff buff, FightActor trigerrer, BuffTriggerType trigger, object token)
//        {
//            IShape shape = new Lozenge((byte)buff.Effect.ZoneMinSize, (byte)buff.Effect.ZoneSize);
//            var cells = shape.GetCells(Caster.Cell, Caster.Map);
//            var fighters = Fight.GetAllFighters(cells);
//            buff.EffectHandler.SetAffectedActors(fighters.Where(x => x.Team == Fight.ChallengersTeam));
//            buff.EffectHandler.Apply();
//        }
//    }
//    [SpellCastHandler(SpellIdEnum.HULHU_5908)]
//    public class HulhuIdolCastHandler : DefaultSpellCastHandler
//    {
//        public HulhuIdolCastHandler(SpellCastInformations cast) : base(cast)
//        {
//        }
//        public override void Execute()
//        {
//            foreach (var spellEffectHandler in Handlers)
//            {
//                spellEffectHandler.DefaultDispellableStatus = FightDispellableEnum.NOT_DISPELLABLE;
//                spellEffectHandler.AddTriggerBuff(Caster, BuffTriggerType.Instant, Hulhu);
//            }
//        }

//        private void Hulhu(TriggerBuff buff, FightActor trigerrer, BuffTriggerType trigger, object token)
//        {
//            var spell = new Spell((buff.Effect as EffectDice).DiceNum, (byte)(buff.Effect as EffectDice).DiceFace);
//            Caster.CastAutoSpell(spell, Caster.Cell);
//        }
//    }
//    [SpellCastHandler(SpellIdEnum.HULHU_5936)]
//    public class Hulhu2IdolCastHandler : DefaultSpellCastHandler
//    {
//        public Hulhu2IdolCastHandler(SpellCastInformations cast) : base(cast)
//        {
//        }
//        public override void Execute()
//        {
//            foreach (var spellEffectHandler in Handlers)
//            {
//                spellEffectHandler.DefaultDispellableStatus = FightDispellableEnum.NOT_DISPELLABLE;
//                spellEffectHandler.AddTriggerBuff(Caster, BuffTriggerType.OnDeath, Hulhu);
//            }
//        }

//        private void Hulhu(TriggerBuff buff, FightActor trigerrer, BuffTriggerType trigger, object token)
//        {
//            buff.EffectHandler.Apply();
//        }
//    }
//    [SpellCastHandler(SpellIdEnum.BOBLE_5889)]
//    public class BobleIdolCastHandler : DefaultSpellCastHandler
//    {
//        public BobleIdolCastHandler(SpellCastInformations cast) : base(cast)
//        {
//        }
//        public override void Execute()
//        {
//            foreach (var spellEffectHandler in Handlers)
//            {
//                spellEffectHandler.DefaultDispellableStatus = FightDispellableEnum.NOT_DISPELLABLE;
//                spellEffectHandler.AddTriggerBuff(Caster, BuffTriggerType.Instant, Boble);
//            }
//        }

//        private void Boble(TriggerBuff buff, FightActor trigerrer, BuffTriggerType trigger, object token)
//        {
//            var spell = new Spell((buff.Effect as EffectDice).DiceNum, (byte)(buff.Effect as EffectDice).DiceFace);
//            Caster.CastAutoSpell(spell, Caster.Cell);
//        }
//    }
//    [SpellCastHandler(SpellIdEnum.BOBLE_5934)]
//    public class Boble2IdolCastHandler : DefaultSpellCastHandler
//    {
//        public Boble2IdolCastHandler(SpellCastInformations cast) : base(cast)
//        {
//        }
//        public override void Execute()
//        {
//            foreach (var spellEffectHandler in Handlers)
//            {
//                spellEffectHandler.DefaultDispellableStatus = FightDispellableEnum.NOT_DISPELLABLE;
//                spellEffectHandler.AddTriggerBuff(Caster, BuffTriggerType.OnDeath, Boble);
//            }
//        }

//        private void Boble(TriggerBuff buff, FightActor trigerrer, BuffTriggerType trigger, object token)
//        {
//            buff.EffectHandler.Apply();
//        }
//    }
//    [SpellCastHandler(SpellIdEnum.DOMO_5919)]
//    public class DomoIdolCastHandler : DefaultSpellCastHandler
//    {
//        public DomoIdolCastHandler(SpellCastInformations cast) : base(cast)
//        {
//        }
//        public override void Execute()
//        {
//            foreach (var spellEffectHandler in Handlers)
//            {
//                spellEffectHandler.DefaultDispellableStatus = FightDispellableEnum.NOT_DISPELLABLE;
//                spellEffectHandler.AddTriggerBuff(Caster, BuffTriggerType.Instant, Domo);
//            }
//        }

//        private void Domo(TriggerBuff buff, FightActor trigerrer, BuffTriggerType trigger, object token)
//        {
//            var spell = new Spell((buff.Effect as EffectDice).DiceNum, (byte)(buff.Effect as EffectDice).DiceFace);
//            Caster.CastAutoSpell(spell, Caster.Cell);
//        }
//    }
//    [SpellCastHandler(SpellIdEnum.DOMO_5944)]
//    public class Domo2IdolCastHandler : DefaultSpellCastHandler
//    {
//        public Domo2IdolCastHandler(SpellCastInformations cast) : base(cast)
//        {
//        }
//        public override void Execute()
//        {
//            foreach (var spellEffectHandler in Handlers)
//            {
//                spellEffectHandler.DefaultDispellableStatus = FightDispellableEnum.NOT_DISPELLABLE;
//                spellEffectHandler.AddTriggerBuff(Caster, BuffTriggerType.Instant, Domo);
//            }
//        }

//        private void Domo(TriggerBuff buff, FightActor trigerrer, BuffTriggerType trigger, object token)
//        {
//            var spell = new Spell((buff.Effect as EffectDice).DiceNum, (byte)(buff.Effect as EffectDice).DiceFace);
//            Caster.CastAutoSpell(spell, Caster.Cell);
//        }
//    }
//    [SpellCastHandler(SpellIdEnum.DOMO)]
//    public class Domo3IdolCastHandler : DefaultSpellCastHandler
//    {
//        public Domo3IdolCastHandler(SpellCastInformations cast) : base(cast)
//        {
//        }
//        public override void Execute()
//        {
//            foreach (var spellEffectHandler in Handlers)
//            {
//                spellEffectHandler.DefaultDispellableStatus = FightDispellableEnum.NOT_DISPELLABLE;
//                spellEffectHandler.AddTriggerBuff(Caster, BuffTriggerType.OnTurnEnd, Domo);
//            }
//        }

//        private void Domo(TriggerBuff buff, FightActor trigerrer, BuffTriggerType trigger, object token)
//        {
//            IShape shape = new Lozenge((byte)(buff.Effect as EffectDice).ZoneMinSize, (byte)(buff.Effect as EffectDice).ZoneSize);
//            var cells = shape.GetCells(Caster.Cell, Caster.Map);
//            var fighters = Fight.GetAllFighters(cells);
//            if (fighters.Where(x => x.Team == Fight.ChallengersTeam).Count() > 0)
//            {
//                buff.EffectHandler.SetAffectedActors(Fight.DefendersTeam.Fighters);
//                buff.EffectHandler.Apply();
//            }
//        }
//    }
//    [SpellCastHandler(SpellIdEnum.PETUNIA_5914)]
//    public class PetuniaIdolCastHandler : DefaultSpellCastHandler
//    {
//        public PetuniaIdolCastHandler(SpellCastInformations cast) : base(cast)
//        {
//        }
//        public override void Execute()
//        {
//            foreach (var spellEffectHandler in Handlers)
//            {
//                spellEffectHandler.DefaultDispellableStatus = FightDispellableEnum.NOT_DISPELLABLE;
//                spellEffectHandler.AddTriggerBuff(Caster, BuffTriggerType.Instant, Petunia);
//            }
//        }

//        private void Petunia(TriggerBuff buff, FightActor trigerrer, BuffTriggerType trigger, object token)
//        {
//            var spell = new Spell((buff.Effect as EffectDice).DiceNum, (byte)(buff.Effect as EffectDice).DiceFace);
//            Caster.CastAutoSpell(spell, Caster.Cell);
//        }
//    }
//    [SpellCastHandler(SpellIdEnum.PETUNIA_5939)]
//    public class Petunia2IdolCastHandler : DefaultSpellCastHandler
//    {
//        public Petunia2IdolCastHandler(SpellCastInformations cast) : base(cast)
//        {
//        }
//        public override void Execute()
//        {
//            foreach (var spellEffectHandler in Handlers)
//            {
//                spellEffectHandler.DefaultDispellableStatus = FightDispellableEnum.NOT_DISPELLABLE;
//                spellEffectHandler.AddTriggerBuff(Caster, BuffTriggerType.AfterDamaged, Petunia);
//            }
//        }

//        private void Petunia(TriggerBuff buff, FightActor trigerrer, BuffTriggerType trigger, object token)
//        {
//            IShape shape = new Lozenge((byte)(buff.Effect as EffectDice).ZoneMinSize, (byte)(buff.Effect as EffectDice).ZoneSize);
//            var cells = shape.GetCells(Caster.Cell, Caster.Map);
//            var fighters = Fight.GetAllFighters(cells);
//            if (fighters.Where(x => x.Team == Fight.ChallengersTeam).Count() > 0)
//            {
//                buff.EffectHandler.SetAffectedActors(Fight.ChallengersTeam.Fighters);
//                buff.EffectHandler.Apply();
//            }
//        }
//    }
//    [SpellCastHandler(SpellIdEnum.AROUMB)]
//    public class AroumbIdolCastHandler : DefaultSpellCastHandler
//    {
//        public AroumbIdolCastHandler(SpellCastInformations cast) : base(cast)
//        {
//        }
//        public override void Execute()
//        {
//            foreach (var spellEffectHandler in Handlers)
//            {
//                spellEffectHandler.DefaultDispellableStatus = FightDispellableEnum.NOT_DISPELLABLE;
//                spellEffectHandler.AddTriggerBuff(Caster, BuffTriggerType.OnTurnEnd, Aroumb);
//            }
//        }

//        private void Aroumb(TriggerBuff buff, FightActor trigerrer, BuffTriggerType trigger, object token)
//        {
//            List<FightActor> fighters = Fight.GetAllFightersInLine(Caster.Cell, 63, DirectionsEnum.DIRECTION_NORTH_EAST);
//            fighters.AddRange(Fight.GetAllFightersInLine(Caster.Cell, 63, DirectionsEnum.DIRECTION_SOUTH_EAST));
//            fighters.AddRange(Fight.GetAllFightersInLine(Caster.Cell, 63, DirectionsEnum.DIRECTION_SOUTH_WEST));
//            fighters.AddRange(Fight.GetAllFightersInLine(Caster.Cell, 63, DirectionsEnum.DIRECTION_NORTH_WEST));
//            buff.EffectHandler.SetAffectedActors(fighters);
//            buff.EffectHandler.Apply();
//        }
//    }
//    [SpellCastHandler(SpellIdEnum.HOSKAR)]
//    public class HoskarIdolCastHandler : DefaultSpellCastHandler
//    {
//        public HoskarIdolCastHandler(SpellCastInformations cast) : base(cast)
//        {
//        }
//        public override void Execute()
//        {
//            foreach (var spellEffectHandler in Handlers)
//            {
//                spellEffectHandler.DefaultDispellableStatus = FightDispellableEnum.NOT_DISPELLABLE;
//                spellEffectHandler.AddTriggerBuff(Caster, BuffTriggerType.OnTurnEnd, Hoskar);
//            }
//        }

//        private void Hoskar(TriggerBuff buff, FightActor trigerrer, BuffTriggerType trigger, object token)
//        {
//            List<FightActor> fighters = Fight.GetAllFightersInLine(Caster.Cell, 63, DirectionsEnum.DIRECTION_NORTH_EAST);
//            fighters.AddRange(Fight.GetAllFightersInLine(Caster.Cell, 63, DirectionsEnum.DIRECTION_SOUTH_EAST));
//            fighters.AddRange(Fight.GetAllFightersInLine(Caster.Cell, 63, DirectionsEnum.DIRECTION_SOUTH_WEST));
//            fighters.AddRange(Fight.GetAllFightersInLine(Caster.Cell, 63, DirectionsEnum.DIRECTION_NORTH_WEST));
//            if (fighters.Count() > 0)
//            {
//                var buffs = Caster.GetBuffs().Where(x => x.GetType() == typeof(StatBuff));
//                if (buffs.Count() > 0)
//                {
//                    return;
//                }
//                var effectHandler = new SubVitalityPercent(new EffectDice(EffectsEnum.Effect_SubVitalityPercent, 50, 50, 0), Caster, this, Caster.Cell, false);
//                effectHandler.SetAffectedActors(new FightActor[] { Caster });
//                //effectHandler.Dice.Duration = 2;
//                effectHandler.Apply();
//            }
//        }
//    }
//    [SpellCastHandler(SpellIdEnum.MUTA)]
//    public class MutaIdolCastHandler : DefaultSpellCastHandler
//    {
//        public MutaIdolCastHandler(SpellCastInformations cast) : base(cast)
//        {
//        }
//        public override void Execute()
//        {
//            foreach (var spellEffectHandler in Handlers)
//            {
//                spellEffectHandler.DefaultDispellableStatus = FightDispellableEnum.NOT_DISPELLABLE;
//                spellEffectHandler.AddTriggerBuff(Caster, BuffTriggerType.OnTurnEnd, Muta);
//            }
//        }

//        private void Muta(TriggerBuff buff, FightActor trigerrer, BuffTriggerType trigger, object token)
//        {
//            if (Fight.GetAllFighters(Caster.Position.Point.GetAdjacentCells().Select(x => Caster.Map.Cells[x.CellId])).Where(x => x.Team == Fight.DefendersTeam).Count() == 0)
//            {
//                var buffs = Caster.GetBuffs().Where(x => x.GetType() == typeof(StatBuff));
//                if (buffs.Count() > 0)
//                {
//                    return;
//                }
//                var effectHandler = new SubVitalityPercent(new EffectDice(EffectsEnum.Effect_SubVitalityPercent, 50, 50, 0), Caster, this, Caster.Cell, false);
//                effectHandler.SetAffectedActors(new FightActor[] { Caster });
//                //effectHandler.Dice.Duration = 2;
//                effectHandler.Apply();
//            }
//        }
//    }
//    [SpellCastHandler(SpellIdEnum.ULTRAM)]
//    public class UltramIdolCastHandler : DefaultSpellCastHandler
//    {
//        public UltramIdolCastHandler(SpellCastInformations cast) : base(cast)
//        {
//        }
//        public override void Execute()
//        {
//            foreach (var spellEffectHandler in Handlers)
//            {
//                if (Spell.CurrentLevel == 1)
//                {
//                    spellEffectHandler.DefaultDispellableStatus = FightDispellableEnum.NOT_DISPELLABLE;
//                    spellEffectHandler.AddTriggerBuff(Caster, BuffTriggerType.OnTurnBegin, Ultram);
//                }
//                else
//                {
//                    spellEffectHandler.DefaultDispellableStatus = FightDispellableEnum.DISPELLABLE;
//                    spellEffectHandler.AddTriggerBuff(Caster, BuffTriggerType.Instant, Ultram);
//                }
//            }
//        }

//        private void Ultram(TriggerBuff buff, FightActor trigerrer, BuffTriggerType trigger, object token)
//        {
//            buff.EffectHandler.TargetedCell = Caster.Cell;
//            buff.EffectHandler.Apply();
//        }
//    }
//}