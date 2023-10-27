using NLog;
using Stump.DofusProtocol.Enums;
using Stump.Server.WorldServer.Database.World;
using Stump.Server.WorldServer.Game.Actors.Fight;
using Stump.Server.WorldServer.Game.Actors.Fight.Customs;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Monsters;
using Stump.Server.WorldServer.Game.Effects.Instances;
using Stump.Server.WorldServer.Game.Fights.Triggers;
using Stump.Server.WorldServer.Handlers.Actions;
using Stump.Server.WorldServer.Game.Spells.Casts;
using Stump.Server.WorldServer.Game.Spells;
using Stump.Server.WorldServer.Game.Fights.Buffs.Customs;
using Stump.Server.WorldServer.Game.Fights.Buffs;
using Stump.Server.WorldServer.Game.Effects.Handlers.Spells.States;
using Stump.Server.WorldServer.Game.Effects.Handlers.Spells.Buffs;
using System.Linq;

namespace Stump.Server.WorldServer.Game.Effects.Handlers.Spells.Summon
{
    [EffectHandler(EffectsEnum.Effect_SummonSlave)]
    [EffectHandler(EffectsEnum.Effect_Summon)]
    public class Summon : SpellEffectHandler
    {
        static readonly Logger logger = LogManager.GetCurrentClassLogger();

        public Summon(EffectDice effect, FightActor caster, SpellCastHandler castHandler, Cell targetedCell, bool critical)
            : base(effect, caster, castHandler, targetedCell, critical)
        {
        }

        protected override bool InternalApply()
        {
            var monster = MonsterManager.Instance.GetMonsterGrade(Dice.DiceNum, Dice.DiceFace);

            if (monster == null)
            {
                logger.Error("Cannot summon monster {0} grade {1} (not found)", Dice.DiceNum, Dice.DiceFace);
                return false;
            }

            if (monster.Template.UseSummonSlot && !Caster.CanSummon())
                return false;

            if (Fight.GetOneFighter(TargetedCell) != null)
            {
                if (AffectedCells.Count <= 1)
                    return false;

                var Cell = AffectedCells.Where(x => Fight.GetOneFighter(x) == null && x.Walkable && x.Id != Caster.Cell.Id).OrderBy(y => Caster.Position.Point.ManhattanDistanceTo(new Maps.Cells.MapPoint(y))).FirstOrDefault();

                if (Cell == null)
                    return false;

                TargetedCell = Cell;
            }

            if (!Caster.HasState(600) && (Spell.Template.Id == 6192 || Spell.Template.Id == 6193 || Spell.Template.Id == 6194 || Spell.Template.Id == 6195))
                return false;

            SummonedFighter summon;
            if (monster.Template.Id == (int)MonsterIdEnum.EPEE_VOLANTE && Caster.HasState(489))
            {
                monster = MonsterManager.Instance.GetMonsterGrade((int)MonsterIdEnum.EPEE_ECORCHEUSE, Dice.DiceFace);
            }
            else if (monster.Template.Id == (int)MonsterIdEnum.EPEE_VOLANTE && Caster.HasState(490))
            {
                monster = MonsterManager.Instance.GetMonsterGrade((int)MonsterIdEnum.EPEE_GARDIENNE, Dice.DiceFace);
            }
            else if (monster.Template.Id == (int)MonsterIdEnum.EPEE_VOLANTE && Caster.HasState(488))
            {
                monster = MonsterManager.Instance.GetMonsterGrade((int)MonsterIdEnum.EPEE_VELOCE, Dice.DiceFace);
            }
            if (monster.Template.Id == (int)MonsterIdEnum.HARPONNEUSE || monster.Template.Id == (int)MonsterIdEnum.GARDIENNE || monster.Template.Id == (int)MonsterIdEnum.TACTIRELLE || monster.Template.Id == (int)MonsterIdEnum.CHALUTIER || monster.Template.Id == (int)MonsterIdEnum.FOREUSE || monster.Template.Id == (int)MonsterIdEnum.BATHYSCAPHE)
                summon = new SummonedTurret(Fight.GetNextContextualId(), Caster, monster, Spell, TargetedCell) {SummoningEffect = this};
            else if (monster.Template.Id == (int)MonsterIdEnum.COFFRE_ANIME)
                summon = new LivingChest(Fight.GetNextContextualId(), Caster.Team, Caster, monster, TargetedCell) { SummoningEffect = this };             
            else
                summon = new SummonedMonster(Fight.GetNextContextualId(), Caster.Team, Caster, monster, TargetedCell) {SummoningEffect = this};

            if (Effect.Id == (short)EffectsEnum.Effect_SummonSlave && Caster is CharacterFighter)
                summon.SetController(Caster as CharacterFighter);


            ActionsHandler.SendGameActionFightSummonMessage(Fight.Clients, summon);

            Caster.AddSummon(summon);
            Caster.Team.AddFighter(summon);

            Fight.TriggerMarks(summon.Cell, summon, TriggerType.MOVE);

            //Control Osa
            switch (Spell.Id)
            {

                case 34: //tofu negro
                    TakeControlSummon(summon);
                    break;
                case 9653: //tofu albino
                    TakeControlSummon(summon);
                    break;
                case 35: // jalato
                    TakeControlSummon(summon);
                    break;
                case 38: //tofuleche
                    TakeControlSummon(summon);
                    break;
                case 40: //jalato negro
                    TakeControlSummon(summon);
                    break;
                case 9661: //jalatin
                    TakeControlSummon(summon);
                    break;
                case 39: // dragonito negro
                    TakeControlSummon(summon);
                    break;
                case 9662: //dragonito albino
                    TakeControlSummon(summon);
                    break;
                case 9664: //sapo negro
                    TakeControlSummon(summon);
                    break;
                case 9658: //sapo albino
                    TakeControlSummon(summon);
                    break;
                case 9667: //sapo baboso
                    TakeControlSummon(summon);
                    break;
                case 31: //dragonito rojo
                    TakeControlSummon(summon);
                    break;
                default:
                    break;
            }

            return true;
        }

        private void TakeControlSummon(SummonedFighter summon)
        {
            TakeControlBuff controlBuff = null;
            StateBuff stateBuff = null;
            SpellImmunityBuff immun = null;
            StatBuff apbuff = null;

            foreach (var item in Caster.GetBuffs())
            {
                if (item.Dice.Value == 432 || item.Dice.Value == 433 || item.Dice.Value == 434 || item.Dice.Value == 599)
                {
                    var id = Caster.PopNextBuffId();
                    controlBuff = new TakeControlBuff(id, Caster, Caster, new TakeControl(item.Spell.CurrentSpellLevel.Effects.Find(x => x.EffectId == EffectsEnum.Effect_TakeControl), Caster, null, summon.Cell, false), item.Spell, FightDispellableEnum.DISPELLABLE_BY_DEATH, summon as SummonedMonster) { Duration = (short)item.Duration };
                    stateBuff = new StateBuff(id + 1, summon, Caster, new AddState(item.Spell.CurrentSpellLevel.Effects.Find(x => x.EffectId == EffectsEnum.Effect_AddState && x.Value != 447), Caster, null, summon.Cell, false), item.Spell, FightDispellableEnum.DISPELLABLE_BY_DEATH, SpellManager.Instance.GetSpellState((uint)item.Dice.Value)) { Duration = (short)item.Duration };
                    immun = new SpellImmunityBuff(id + 2, summon, Caster, new SpellImmunity(item.Spell.CurrentSpellLevel.Effects.Find(x => x.EffectId == EffectsEnum.Effect_SpellImmunity), Caster, null, summon.Cell, false), item.Spell, item.Spell.CurrentSpellLevel.Effects.Find(x => x.EffectId == EffectsEnum.Effect_SpellImmunity).DiceNum, false, FightDispellableEnum.DISPELLABLE_BY_DEATH) { Duration = (short)item.Duration };
                    apbuff = new StatBuff(id + 3, summon, Caster, new APBuff(item.Spell.CurrentSpellLevel.Effects.Find(x => x.EffectId == EffectsEnum.Effect_AddAP_111), Caster, null, summon.Cell, false), item.Spell, 2, PlayerFields.AP, false, FightDispellableEnum.DISPELLABLE_BY_DEATH) { Duration = (short)item.Duration };
                }
            }

            if (controlBuff == null || stateBuff == null)
                return;


            if ((summon as SummonedMonster).Monster.Template.Id == (int)MonsterIdEnum.TOFU_NOIR_4561 ||
                (summon as SummonedMonster).Monster.Template.Id == (int)MonsterIdEnum.TOFU_DODU_4562 ||
                (summon as SummonedMonster).Monster.Template.Id == (int)MonsterIdEnum.TOFU_ALBINOS)

            {
                if (Caster.HasState(432))
                {
                    Caster.AddBuff(controlBuff);
                    summon.AddBuff(stateBuff);
                    summon.AddBuff(immun);
                    summon.AddBuff(apbuff);
                }
            }
            else if ((summon as SummonedMonster).Monster.Template.Id == (int)MonsterIdEnum.BOUFTOU ||
              (summon as SummonedMonster).Monster.Template.Id == (int)MonsterIdEnum.BOUFTOU_NOIR_4564 ||
              (summon as SummonedMonster).Monster.Template.Id == (int)MonsterIdEnum.BOUFTON)

            {
                if (Caster.HasState(433))
                {
                    Caster.AddBuff(controlBuff);
                    summon.AddBuff(stateBuff);
                    summon.AddBuff(immun);
                    summon.AddBuff(apbuff);
                }
            }
            else if ((summon as SummonedMonster).Monster.Template.Id == (int)MonsterIdEnum.DRAGONNET_ROUGE_4565 ||
              (summon as SummonedMonster).Monster.Template.Id == (int)MonsterIdEnum.DRAGONNET_NOIR ||
              (summon as SummonedMonster).Monster.Template.Id == (int)MonsterIdEnum.DRAGONNET_ALBINOS)
            {
                if (Caster.HasState(434))
                {
                    Caster.AddBuff(controlBuff);
                    summon.AddBuff(stateBuff);
                    summon.AddBuff(immun);
                    summon.AddBuff(apbuff);
                }
            }
            else if ((summon as SummonedMonster).Monster.Template.Id == (int)MonsterIdEnum.CRAPAUD_NOIR ||
              (summon as SummonedMonster).Monster.Template.Id == (int)MonsterIdEnum.CRAPAUD_ALBINOS ||
              (summon as SummonedMonster).Monster.Template.Id == (int)MonsterIdEnum.CRAPAUD_BAVEUX)
            {
                if (Caster.HasState(599))
                {
                    Caster.AddBuff(controlBuff);
                    summon.AddBuff(stateBuff);
                    summon.AddBuff(immun);
                    summon.AddBuff(apbuff);
                }
            }
        }
    }
}