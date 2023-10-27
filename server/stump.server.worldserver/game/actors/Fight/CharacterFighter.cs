using System;
using System.Collections.Generic;
using System.Linq;
using Stump.Core.Threading;
using Stump.DofusProtocol.Enums;
using Stump.DofusProtocol.Types;
using Stump.Server.WorldServer.Core.Network;
using Stump.Server.WorldServer.Database.Items.Templates;
using Stump.Server.WorldServer.Database.Spells;
using Stump.Server.WorldServer.Database.World;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Characters;
using Stump.Server.WorldServer.Game.Actors.Stats;
using Stump.Server.WorldServer.Game.Effects;
using Stump.Server.WorldServer.Game.Effects.Handlers.Spells;
using Stump.Server.WorldServer.Game.Effects.Instances;
using Stump.Server.WorldServer.Game.Fights;
using Stump.Server.WorldServer.Game.Fights.Buffs;
using Stump.Server.WorldServer.Game.Fights.Results;
using Stump.Server.WorldServer.Game.Fights.Teams;
using Stump.Server.WorldServer.Game.Maps.Cells;
using Stump.Server.WorldServer.Game.Maps.Cells.Shapes;
using Stump.Server.WorldServer.Game.Spells;
using Spell = Stump.Server.WorldServer.Game.Spells.Spell;
using Stump.Server.WorldServer.Game.Effects.Handlers.Spells.Targets;
using Stump.Server.WorldServer.Game.Spells.Casts;

namespace Stump.Server.WorldServer.Game.Actors.Fight
{
    public sealed class CharacterFighter : NamedFighter
    {
        int m_criticalWeaponBonus;
        int m_damageTakenBeforeFight;
        int m_weaponUses;

        public CharacterFighter(Character character, FightTeam team) : base(team)
        {
            Character = character;

            Look = Character.Look.Clone();
            Look.RemoveAuras();

            Cell cell;
            if (!Fight.FindRandomFreeCell(this, out cell, false))
                return;

            Position = new ObjectPosition(character.Map, cell, character.Direction);

            InitializeCharacterFighter();
        }

        public Character Character
        {
            get;
            private set;
        }

        public ReadyChecker PersonalReadyChecker
        {
            get;
            set;
        }

        public override int Id
        {
            get { return Character.Id; }
        }

        public override string Name
        {
            get { return Character.Name; }
        }

        public override ObjectPosition MapPosition
        {
            get { return Character.Position; }
        }

        public override ushort Level
        {
            get { return Character.Level; }
        }

        public override StatsFields Stats
        {
            get { return Character.Stats; }
        }

        public bool IsDisconnected
        {
            get;
            private set;
        }

        public int ErosionDamageBeforeLeft
        {
            get;
            set;
        }

        public int LeftRound
        {
            get;
            private set;
        }

        void InitializeCharacterFighter()
        {
            m_damageTakenBeforeFight = Stats.Health.DamageTaken;

            Fight.TurnStarted += OnTurnStarted;
            Fight.BeforeTurnStopped += Fight_BeforeTurnStopped;
        }

        private void Fight_BeforeTurnStopped(IFight arg1, FightActor arg2)
        {
            if (Character.Inventory.GetEquipedItems().Where(x => x.Template.Id == 30199).Count() > 0 && arg1.TimeLine.RoundNumber != 1)
            {
                if (arg2 != this)
                    return;

                var buff = new EffectDice(EffectsEnum.Effect_CastSpell_1160, 0, 9089, 1);
                var handlerr = EffectManager.Instance.GetSpellEffectHandler(buff, this, SpellManager.Instance.GetSpellCastHandler(this, new Spell(9088, 1), this.Cell, false), this.Cell, false);
                handlerr.DefaultDispellableStatus = FightDispellableEnum.DISPELLABLE_BY_DEATH;
                handlerr.Apply();
                return;
            }
        }

        public bool activo; // Si ya tiene un compañero - companion activo no invoca otro.
        public bool plateado;

        void OnTurnStarted(IFight fight, FightActor actor)
        {

            if (fight.TimeLine.RoundNumber != 1)
            {
                fight.TurnStarted -= OnTurnStarted;
                return;
            }

            if (actor != this)
                return;

            var items_ = Character.Inventory.GetItems(CharacterInventoryPositionEnum.INVENTORY_POSITION_NOT_EQUIPED);

            #region Falsos Compañeros - Companions
            if (!fight.IsPvP)
            {
                if (items_.Count() > 0 && activo == false && items_.Any(x => x.Template.Id == 14965))
                {
                    activo = true;
                    var buff = new EffectDice(EffectsEnum.Effect_CastSpell_1175, 0, 392, 4);
                    var handlerr = EffectManager.Instance.GetSpellEffectHandler(buff, this, SpellManager.Instance.GetSpellCastHandler(this, new Spell(392, 4), this.Cell, false), this.Cell, false);
                    handlerr.DefaultDispellableStatus = FightDispellableEnum.DISPELLABLE_BY_DEATH;
                    handlerr.Apply();

                }
                if (items_.Count() > 0 && activo == false && items_.Any(x => x.Template.Id == 14962))
                {
                    activo = true;
                    var buff = new EffectDice(EffectsEnum.Effect_CastSpell_1175, 0, 392, 6);
                    var handlerr = EffectManager.Instance.GetSpellEffectHandler(buff, this, SpellManager.Instance.GetSpellCastHandler(this, new Spell(392, 6), this.Cell, false), this.Cell, false);
                    handlerr.DefaultDispellableStatus = FightDispellableEnum.DISPELLABLE_BY_DEATH;
                    handlerr.Apply();
                }
                if (items_.Count() > 0 && activo == false && items_.Any(x => x.Template.Id == 14963))
                {
                    activo = true;
                    var buff = new EffectDice(EffectsEnum.Effect_CastSpell_1175, 0, 392, 2);
                    var handlerr = EffectManager.Instance.GetSpellEffectHandler(buff, this, SpellManager.Instance.GetSpellCastHandler(this, new Spell(392, 2), this.Cell, false), this.Cell, false);
                    handlerr.DefaultDispellableStatus = FightDispellableEnum.DISPELLABLE_BY_DEATH;
                    handlerr.Apply();
                }
                if (items_.Count() > 0 && activo == false && items_.Any(x => x.Template.Id == 14966))
                {
                    activo = true;
                    var buff = new EffectDice(EffectsEnum.Effect_CastSpell_1175, 0, 392, 5);
                    var handlerr = EffectManager.Instance.GetSpellEffectHandler(buff, this, SpellManager.Instance.GetSpellCastHandler(this, new Spell(392, 5), this.Cell, false), this.Cell, false);
                    handlerr.DefaultDispellableStatus = FightDispellableEnum.DISPELLABLE_BY_DEATH;
                    handlerr.Apply();
                }
                if (items_.Count() > 0 && activo == false && items_.Any(x => x.Template.Id == 15393))
                {
                    activo = true;
                    var buff = new EffectDice(EffectsEnum.Effect_CastSpell_1175, 0, 392, 3);
                    var handlerr = EffectManager.Instance.GetSpellEffectHandler(buff, this, SpellManager.Instance.GetSpellCastHandler(this, new Spell(392, 3), this.Cell, false), this.Cell, false);
                    handlerr.DefaultDispellableStatus = FightDispellableEnum.DISPELLABLE_BY_DEATH;
                    handlerr.Apply();
                }
            }
            #endregion

            var calculovida = (Character.Stats.Health.TotalMaxWithoutPermanentDamages / 100) * 20;

            /*if (Character.Inventory.GetEquipedItems().Where(x => x.Template.Id == 19629).Count() > 0 && plateado == false && Character.Stats.Health.TotalMaxWithoutPermanentDamages <= calculovida)
            {
                plateado = true;


                var buff = GetBuffs(entry => entry.Spell.Id == (int)SpellIdEnum.EMERALD_DOFUS && entry.Dice.Id == 1039).ToList();
                AddStatBuff(actor, shieldAmount, PlayerFields.Shield, (short)EffectsEnum.Effect_AddShield);
                //var buff = new EffectDice(EffectsEnum.Effect_CastSpell_1175, 0, 392, 5);
                //var handlerr = EffectManager.Instance.GetSpellEffectHandler(buff, this, SpellManager.Instance.GetSpellCastHandler(this, new Spell(392, 5), this.Cell, false), this.Cell, false);
                //handlerr.DefaultDispellableStatus = FightDispellableEnum.DISPELLABLE_BY_DEATH;
                ///handlerr.Duration = 2;
                //handlerr.Apply();
            }*/

            /*var dofus = Character.Inventory.eq(CharacterInventoryPositionEnum.INVENTORY_POSITION_DOFUS_1);
            var dofus2 = Character.Inventory.GetItems(CharacterInventoryPositionEnum.INVENTORY_POSITION_DOFUS_2);
            var dofus3 = Character.Inventory.GetItems(CharacterInventoryPositionEnum.INVENTORY_POSITION_DOFUS_3);
            var dofus4 = Character.Inventory.GetItems(CharacterInventoryPositionEnum.INVENTORY_POSITION_DOFUS_4);
            var dofus5 = Character.Inventory.GetItems(CharacterInventoryPositionEnum.INVENTORY_POSITION_DOFUS_5);
            var dofus6 = Character.Inventory.GetItems(CharacterInventoryPositionEnum.INVENTORY_POSITION_DOFUS_6);

            var calculovida = (Character.Stats.Health.TotalMaxWithoutPermanentDamages / 100) * 20;

            if ((dofus.Count() >= 1 || dofus2.Count() >= 1 || dofus3.Count() >= 1 || dofus4.Count() >= 1 || dofus5.Count() >= 1 || dofus6.Count() >= 1) && dofus.Any(x => x.Template.Id == 19629))
            {
                var buff = new EffectDice(EffectsEnum.Effect_CastSpell_1175, 0, 10269, 1);
                var handlerr = EffectManager.Instance.GetSpellEffectHandler(buff, this, SpellManager.Instance.GetSpellCastHandler(this, new Spell(10269, 1), this.Cell, false), this.Cell, false);
                handlerr.DefaultDispellableStatus = FightDispellableEnum.DISPELLABLE_BY_DEATH;
                handlerr.Duration = 2;
                handlerr.Apply();

            }*/

            //Cast Spells of Items has CastSpell effect
            foreach (var effect in Character.Inventory.GetEquipedItems().SelectMany(x => x.Effects).Where(y => y.EffectId == EffectsEnum.Effect_CastSpell_1175))
                EffectManager.Instance.GetSpellEffectHandler((EffectDice)effect, this,
                    new DefaultSpellCastHandler(new SpellCastInformations(this, null, Cell)), Cell, false).Apply();
        }

        public override ObjectPosition GetLeaderBladePosition() => Character.GetPositionBeforeMove();

        public void ToggleTurnReady(bool ready)
        {
            if (PersonalReadyChecker != null)
                PersonalReadyChecker.ToggleReady(this, ready);
            else if (Fight.ReadyChecker != null)
                Fight.ReadyChecker.ToggleReady(this, ready);
        }

        #region Leave

        public void LeaveFight(bool force = false)
        {
            if (HasLeft())
                return;

            m_left = !force;

            OnLeft();
        }

        private bool m_left;
        public override bool HasLeft()
        {
            return m_left;
        }

        public override bool CanPlay()
        {
            return !HasLeft() || IsDisconnected;
        }

        #endregion

        public override void ResetUsedPoints()
        {
            base.ResetUsedPoints();
            m_weaponUses = 0;
        }

        public override bool CastSpell(SpellCastInformations cast)
        {
            if (!IsFighterTurn() && !cast.Force)
                return false;

            // not a weapon attack
            if (cast.Spell.Id != 0 || Character.Inventory.TryGetItem(CharacterInventoryPositionEnum.ACCESSORY_POSITION_WEAPON) == null)
                return base.CastSpell(cast);

            var weapon = Character.Inventory.TryGetItem(CharacterInventoryPositionEnum.ACCESSORY_POSITION_WEAPON);

            if (!(weapon.Template is WeaponTemplate weaponTemplate) || !CanUseWeapon(cast.TargetedCell, weaponTemplate))
            {
                OnSpellCastFailed(cast);
                return false;
            }

            using (Fight.StartSequence(SequenceTypeEnum.SEQUENCE_WEAPON))
            {
                var random = new AsyncRandom();
                var critical = RollCriticalDice(weaponTemplate);

                switch (critical)
                {
                    case FightSpellCastCriticalEnum.CRITICAL_FAIL:
                        OnWeaponUsed(weaponTemplate, cast.TargetedCell, critical, false);

                        if (!cast.ApFree)
                            UseAP((short)weaponTemplate.ApCost);

                        PassTurn();

                        return false;
                    case FightSpellCastCriticalEnum.CRITICAL_HIT:
                        m_criticalWeaponBonus = weaponTemplate.CriticalHitBonus;
                        break;
                }

                m_isUsingWeapon = true;
                var effects =
                    weapon.Effects.Where(entry => EffectManager.Instance.IsUnRandomableWeaponEffect(entry.EffectId)).OfType<EffectDice>();
                var handlers = new List<SpellEffectHandler>();
                foreach (var effect in effects)
                {
                    if (effect.Random > 0)
                    {
                        if (random.NextDouble() > effect.Random / 100d)
                        {
                            // effect ignored
                            continue;
                        }
                    }

                    var handler = EffectManager.Instance.GetSpellEffectHandler(effect, this, new DefaultSpellCastHandler(cast), cast.TargetedCell,
                        critical == FightSpellCastCriticalEnum.CRITICAL_HIT);

                    handler.EffectZone = new Zone(weaponTemplate.Type.ZoneShape, (byte)weaponTemplate.Type.ZoneSize, (byte)weaponTemplate.Type.ZoneMinSize,
                        handler.CastPoint.OrientationTo(handler.TargetedPoint), (int)weaponTemplate.Type.ZoneEfficiencyPercent, (int)weaponTemplate.Type.ZoneMaxEfficiency);

                    handler.Targets = new TargetCriterion[] { new TargetTypeCriterion(SpellTargetType.ALLY_ALL_EXCEPT_SELF, false), new TargetTypeCriterion(SpellTargetType.ENEMY_ALL, false) }; // everyone but caster

                    handlers.Add(handler);
                }

                var silentCast = handlers.Any(entry => entry.RequireSilentCast());

                OnWeaponUsed(weaponTemplate, cast.TargetedCell, critical, silentCast);

                if (!cast.ApFree)
                    UseAP((short)weaponTemplate.ApCost);

                foreach (var handler in handlers)
                    handler.Apply();
            }

            m_isUsingWeapon = false;
            m_criticalWeaponBonus = 0;

            // is it the right place to do that ?
            Fight.CheckFightEnd();

            return true;
        }

        protected override void OnWeaponUsed(WeaponTemplate weapon, Cell cell, FightSpellCastCriticalEnum critical, bool silentCast)
        {
            m_weaponUses++;
            base.OnWeaponUsed(weapon, cell, critical, silentCast);
        }

        public override SpellCastResult CanCastSpell(SpellCastInformations cast)
        {
            var result = base.CanCastSpell(cast);

            if (result == SpellCastResult.OK || cast.IsConditionBypassed(result))
                return result;

            if (cast.Silent)
                return result;

            switch (result)
            {
                case SpellCastResult.NO_LOS:
                    // Impossible de lancer ce sort : un obstacle g�ne votre vue !
                    Character.SendInformationMessage(TextInformationTypeEnum.TEXT_INFORMATION_ERROR, 174);
                    break;
                case SpellCastResult.HAS_NOT_SPELL:
                    // Impossible de lancer ce sort : vous ne le poss�dez pas !
                    Character.SendInformationMessage(TextInformationTypeEnum.TEXT_INFORMATION_ERROR, 169);
                    break;
                case SpellCastResult.NOT_ENOUGH_AP:
                    // Impossible de lancer ce sort : Vous avez %1 PA disponible(s) et il vous en faut %2 pour ce sort !
                    Character.SendInformationMessage(TextInformationTypeEnum.TEXT_INFORMATION_ERROR, 170, AP, cast.SpellLevel.ApCost);
                    break;
                case SpellCastResult.UNWALKABLE_CELL:
                    // Impossible de lancer ce sort : la cellule vis�e n'est pas disponible !
                    Character.SendInformationMessage(TextInformationTypeEnum.TEXT_INFORMATION_ERROR, 172);
                    break;
                case SpellCastResult.CELL_NOT_FREE:
                    //Impossible de lancer ce sort : la cellule vis�e n'est pas valide !
                    Character.SendInformationMessage(TextInformationTypeEnum.TEXT_INFORMATION_ERROR, 193);
                    break;
                default:
                    //Impossible de lancer ce sort actuellement.
                    Character.SendInformationMessage(TextInformationTypeEnum.TEXT_INFORMATION_ERROR, 175);
                    break;
            }

            return result;
        }

        public override FightSpellCastCriticalEnum RollCriticalDice(SpellLevelTemplate spell) => Character.CriticalMode ? FightSpellCastCriticalEnum.CRITICAL_HIT : base.RollCriticalDice(spell);

        public override FightSpellCastCriticalEnum RollCriticalDice(WeaponTemplate weapon) => Character.CriticalMode ? FightSpellCastCriticalEnum.CRITICAL_HIT : base.RollCriticalDice(weapon);

        public override Damage CalculateDamageBonuses(Damage damage, bool isMeele)
        {
            if (Character.GodMode)
                damage.Amount = short.MaxValue;

            if (m_isUsingWeapon)
                damage.Amount += m_criticalWeaponBonus;

            return base.CalculateDamageBonuses(damage, isMeele);
        }

        public bool CanUseWeapon(Cell cell, WeaponTemplate weapon)
        {
            if (!IsFighterTurn())
                return false;

            if (HasState((int)SpellStatesEnum.AFFAIBLI_42))
                return false;

            var point = new MapPoint(cell);

            if ((weapon.CastInDiagonal && (point.EuclideanDistanceTo(Position.Point) > weapon.WeaponRange || point.EuclideanDistanceTo(Position.Point) < weapon.MinRange)) ||
                (!weapon.CastInDiagonal && point.ManhattanDistanceTo(Position.Point) > weapon.WeaponRange || point.ManhattanDistanceTo(Position.Point) < weapon.MinRange))
                return false;

            if (m_weaponUses >= weapon.MaxCastPerTurn)
                return false;

            return AP >= weapon.ApCost && Fight.CanBeSeen(cell, Position.Cell);
        }

        public override Spell GetSpell(int id) => Character.Spells.GetSpell(id);

        public override bool HasSpell(int id) => Character.Spells.HasSpell(id);

        public bool IsSlaveTurn() => (Fight.TimeLine.Current as SummonedFighter)?.Controller == this;

        public SummonedFighter GetSlave()
        {
            var summon = (Fight.TimeLine.Current as SummonedFighter);

            if (summon == null)
                return null;

            return summon.Controller == this ? summon : null;
        }

        public override void ResetFightProperties()
        {
            base.ResetFightProperties();

            if (Fight.IsDeathTemporarily)
                Stats.Health.DamageTaken = m_damageTakenBeforeFight;
            else if (Stats.Health.Total <= 0)
                Stats.Health.DamageTaken = (Stats.Health.TotalMax - 1);
        }

        public override bool MustSkipTurn() => base.MustSkipTurn() || (IsDisconnected && Team.GetAllFighters<CharacterFighter>().Any(x => x.IsAlive() && !x.IsDisconnected));

        public void EnterDisconnectedState()
        {
            IsDisconnected = true;
            ErosionDamageBeforeLeft = this.Character.Stats.Health.PermanentDamages;
            LeftRound = Fight.TimeLine.RoundNumber;
        }

        public void LeaveDisconnectedState(bool left = true)
        {
            IsDisconnected = false;

            if (left)
                m_left = false;
        }

        public void RestoreFighterFromDisconnection(Character character)
        {
            if (!IsDisconnected)
            {
                throw new Exception("Fighter wasn't disconnected");
            }

            character.Stats.InitializeFromStats(Character.Stats);
            Character = character;
        }

        public override IFightResult GetFightResult(FightOutcomeEnum outcome) => new FightPlayerResult(this, outcome, Loot);

        public override FightTeamMemberInformations GetFightTeamMemberInformations() =>
            new FightTeamMemberCharacterInformations(Id, Name, (ushort)Character.Level);

        public override GameFightFighterInformations GetGameFightFighterInformations(WorldClient client = null)
        {

            return new GameFightCharacterInformations(
                Id,
                Look.GetEntityLook(),
                GetEntityDispositionInformations(client),
                (sbyte)Team.Id,
                0,
                IsAlive(),
                GetGameFightMinimalStats(client),
                new ushort[0],
                Name,
                Character.Status,
                0,
                0,
                false,
                Character.Level,
                Character.GetActorAlignmentInformations(),
                (sbyte)Character.Breed.Id,
                Character.Sex != SexTypeEnum.SEX_MALE);
        }

        public override GameFightFighterLightInformations GetGameFightFighterLightInformations(WorldClient client = null) =>
            new GameFightFighterNamedLightInformations(
            Id,
            0,
            Level,
            (sbyte)Character.Breed.Id,
            Character.Sex == SexTypeEnum.SEX_FEMALE,
            IsAlive(),
            Name);

        public override string ToString()
        {
            return Character.ToString();
        }

        #region God state
        public override bool UseAP(short amount)
        {
            if (!Character.GodMode)
                return base.UseAP(amount);

            base.UseAP(amount);
            RegainAP(amount);

            return true;
        }

        public override bool UseMP(short amount)
        {
            return Character.GodMode || base.UseMP(amount);
        }

        public override bool LostAP(short amount, FightActor source)
        {
            if (!Character.GodMode)
                return base.LostAP(amount, source);

            base.LostAP(amount, source);
            RegainAP(amount);

            return true;
        }

        public override bool LostMP(short amount, FightActor source)
        {
            return Character.GodMode || base.LostMP(amount, source);
        }

        public override int InflictDamage(Damage damage)
        {
            if (!Character.GodMode)
                return base.InflictDamage(damage);

            damage.GenerateDamages();
            OnBeforeDamageInflicted(damage);

            damage.Source.TriggerBuffs(damage.Source, BuffTriggerType.BeforeAttack, damage);
            TriggerBuffs(damage.Source, BuffTriggerType.BeforeDamaged, damage);
            TriggerBuffs(damage.Source, BuffTriggerType.OnDamaged, damage);

            OnDamageReducted(damage.Source, damage.Amount);

            damage.Source.TriggerBuffs(damage.Source, BuffTriggerType.AfterAttack, damage);
            TriggerBuffs(damage.Source, BuffTriggerType.AfterDamaged, damage);

            OnDamageInflicted(damage);

            return 0;
        }

        #endregion
    }
}