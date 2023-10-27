﻿using System;
using Stump.DofusProtocol.Enums;
using Stump.DofusProtocol.Types;
using Stump.Server.WorldServer.Core.Network;
using Stump.Server.WorldServer.Database.Monsters;
using Stump.Server.WorldServer.Database.World;
using Stump.Server.WorldServer.Game.Actors.Interfaces;
using Stump.Server.WorldServer.Game.Actors.Stats;
using Stump.Server.WorldServer.Game.Fights;
using Stump.Server.WorldServer.Game.Fights.Teams;
using Stump.Server.WorldServer.Game.Maps.Cells;
using Stump.Server.WorldServer.Game.Spells;
using System.Linq;

namespace Stump.Server.WorldServer.Game.Actors.Fight
{
    public sealed class SummonedTurret : SummonedFighter, ICreature
    {
        protected readonly StatsFields m_stats;
        protected Spell m_spell;

        public SummonedTurret(int id, FightActor summoner, MonsterGrade template, Spell spell, Cell cell)
            : base(id, summoner.Team, template.Spells, summoner, cell, template.Template.Id, template)
        {
            Caster = summoner;
            Monster = template;
            Look = Monster.Template.EntityLook.Clone();

            AlreadyRecursive = false;

            m_spell = spell;
            m_stats = new StatsFields(this);
            m_stats.Initialize(template);

            m_stats.MP.Modified += OnMPModified;

            AdjustStats();

            if (Monster.Template.Id == (int)MonsterIdEnum.TACTIRELLE)
                Team.FighterAdded += OnFighterAdded;
        }

        public bool AlreadyRecursive
        {
            get;
            set;
        }

        public override void OnTurnStarted(IFight fight, FightActor fighter)
        {
            AlreadyRecursive = false;

            if (fighter != this)
                return;

            Spell spellToCast = null;
            FightActor target = null;

            switch ((MonsterIdEnum)Monster.Template.Id)
            {
                case MonsterIdEnum.TACTIRELLE:
                    {
                        if (this.GetStates().FirstOrDefault(x => x.State.Id == (int)SpellStatesEnum.EVOLUTION_III_135 && !x.IsDisabled) != null)
                        {
                            target = Fight.Fighters.FirstOrDefault(x => this != x && x.HasState((int)SpellStatesEnum.TRANSKO_364) &&
                               !x.HasState((int)SpellStatesEnum.ENRACINE_6) && !x.HasState((int)SpellStatesEnum.INDEPLACABLE_97));

                            if (target == null)
                                spellToCast = new Spell((int)SpellIdEnum.TRANSKO_3240, 1);
                            else
                                spellToCast = new Spell((int)SpellIdEnum.TRANSKO_3240, target.GetStates().FirstOrDefault(x => x.State.Id == (int)SpellStatesEnum.TRANSKO_364 && !x.IsDisabled).Spell.CurrentLevel);
                        }
                        break;
                    }
                case MonsterIdEnum.GARDIENNE:
                    {
                        if (!HasState((int)SpellStatesEnum.EVOLUTION_III_135)) break;
                        spellToCast = new Spell((int)SpellIdEnum.RESCUE_3244, 1);
                        target = fighter.Team.Fighters.FirstOrDefault(x => x.HasState((int)SpellStatesEnum.SECOURISME_131));
                        break;
                    }


                case MonsterIdEnum.HARPONNEUSE:
                    {
                        if (!HasState((int)SpellStatesEnum.EVOLUTION_III_135)) break;
                        if (HasState((int)SpellStatesEnum.TELLURIQUE_127))
                            spellToCast = new Spell((int)SpellIdEnum.BOOBOOME, 1);
                        else if (HasState((int)SpellStatesEnum.AQUATIQUE_128))
                            spellToCast = new Spell((int)SpellIdEnum.BWOOBWOOM, 1);
                        else if (HasState((int)SpellStatesEnum.ARDENT_129))
                            spellToCast = new Spell((int)SpellIdEnum.BOOBOOMF, 1);

                        target = fighter.OpposedTeam.Fighters.FirstOrDefault(x => x.HasState((int)SpellStatesEnum.EMBUSCADE_130));
                        break;
                    }

                case MonsterIdEnum.CHALUTIER:
                    {
                        if (!HasState((int)SpellStatesEnum.EVOLUTION_III_135)) break;
                        spellToCast = new Spell(9881, 1);
                        target = Fight.Fighters.FirstOrDefault(x => this != x && (x.HasState((int)SpellStatesEnum.LONGUE_VUE_132))
                                    && !x.HasState((int)SpellStatesEnum.ENRACINE_6) && !x.HasState((int)SpellStatesEnum.INDEPLACABLE_97));
                        if (target == null)
                            target = Fight.Fighters.FirstOrDefault(x => this != x && (x.HasState((int)SpellStatesEnum.COURTE_VUE_147))
                                    && !x.HasState((int)SpellStatesEnum.ENRACINE_6) && !x.HasState((int)SpellStatesEnum.INDEPLACABLE_97));
                        break;
                    }

                case MonsterIdEnum.FOREUSE:
                    {
                        if (!HasState((int)SpellStatesEnum.EVOLUTION_III_135)) break;
                        spellToCast = new Spell(9883, 1);
                        target = fighter.OpposedTeam.Fighters.FirstOrDefault(x => x.HasState((int)SpellStatesEnum.EMBUSCADE_130));
                        break;
                    }

                case MonsterIdEnum.BATHYSCAPHE:
                    {
                        if (!HasState((int)SpellStatesEnum.EVOLUTION_III_135)) break;
                        spellToCast = new Spell(9887, 1);
                        target = fighter.Team.Fighters.FirstOrDefault(x => x.HasState((int)SpellStatesEnum.SECOURISME_131));
                        break;
                    }
            }

            if (spellToCast == null || target == null)
            {
                base.OnTurnStarted(fight, fighter);
                return;
            }

            CastAutoSpell(spellToCast, target.Cell);

            base.OnTurnStarted(fight, fighter);
        }

        void OnFighterAdded(FightTeam team, FightActor actor)
        {
            if (actor != this)
                return;

            CastAutoSpell(new Spell((int)SpellIdEnum.TRANSKO, 1), Cell);
        }

        void AdjustStats()
        {
            var baseCoef = 0.25;

            switch (Monster.Template.Id)
            {
                case (int)MonsterIdEnum.HARPONNEUSE:
                case (int)MonsterIdEnum.FOREUSE:
                    baseCoef = 0.3;
                    break;
                case (int)MonsterIdEnum.GARDIENNE:
                case (int)MonsterIdEnum.BATHYSCAPHE:
                    baseCoef = 0.25;
                    break;
                case (int)MonsterIdEnum.TACTIRELLE:
                case (int)MonsterIdEnum.CHALUTIER:
                    baseCoef = 0.2;
                    break;
            }

            var coef = baseCoef + (0.02 * (m_spell.CurrentLevel - 1));
            m_stats.Health.Base += (int)(((Summoner.Level - 1) * 5 + 55) * coef) + (int)((Summoner.MaxLifePoints) * coef);

            m_stats.Intelligence.Base = (short)(Summoner.Stats.Intelligence.Base * (1 + (Summoner.Level / 100d)));
            m_stats.Chance.Base = (short)(Summoner.Stats.Chance.Base * (1 + (Summoner.Level / 100d)));
            m_stats.Strength.Base = (short)(Summoner.Stats.Strength.Base * (1 + (Summoner.Level / 100d)));
            m_stats.Agility.Base = (short)(Summoner.Stats.Agility.Base * (1 + (Summoner.Level / 100d)));
            m_stats.Wisdom.Base = (short)(Summoner.Stats.Wisdom.Base * (1 + (Summoner.Level / 100d)));

            m_stats[PlayerFields.DamageBonus].Base = Summoner.Stats[PlayerFields.DamageBonus].Equiped;
            m_stats[PlayerFields.DamageBonusPercent].Base = Summoner.Stats[PlayerFields.DamageBonusPercent].Equiped;
            m_stats[PlayerFields.AirDamageBonus].Base = Summoner.Stats[PlayerFields.AirDamageBonus].Equiped;
            m_stats[PlayerFields.FireDamageBonus].Base = Summoner.Stats[PlayerFields.FireDamageBonus].Equiped;
            m_stats[PlayerFields.WaterDamageBonus].Base = Summoner.Stats[PlayerFields.WaterDamageBonus].Equiped;
            m_stats[PlayerFields.EarthDamageBonus].Base = Summoner.Stats[PlayerFields.EarthDamageBonus].Equiped;
            m_stats[PlayerFields.PushDamageBonus].Base = Summoner.Stats[PlayerFields.PushDamageBonus].Equiped;
        }

        static void OnMPModified(StatsData mpStats, int amount)
        {
            if (amount == 0)
                return;

            mpStats.Context = 0;
        }

        public override bool CanSwitchPos() => false;

        public override bool CanTackle(FightActor fighter) => false;

        public override bool CanMove() => base.CanMove() && MonsterGrade.MovementPoints > 0;

        public FightActor Caster
        {
            get;
        }

        public MonsterGrade Monster
        {
            get;
        }

        public MonsterGrade MonsterGrade => Monster;

        public override string Name => Monster.Template.Name;

        public override ObjectPosition MapPosition => Position;

        public override ushort Level => Summoner.Level;

        public override StatsFields Stats => m_stats;

        public override string GetMapRunningFighterName() => Name;

        protected override void OnDisposed()
        {
            m_stats.MP.Modified -= OnMPModified;
            base.OnDisposed();
        }

        public override GameFightFighterInformations GetGameFightFighterInformations(WorldClient client = null)
        {
            return new GameFightMonsterInformations(Id, Look.GetEntityLook(), GetEntityDispositionInformations(),
                (sbyte)Team.Id, 0, IsAlive(), GetGameFightMinimalStats(), new ushort[0], (ushort)Monster.MonsterId, (sbyte)Monster.GradeId, (short)Monster.Level);
        }

        public override FightTeamMemberInformations GetFightTeamMemberInformations()
        {
            return new FightTeamMemberMonsterInformations(Id, Monster.Template.Id, (sbyte)Monster.GradeId);
        }

        public override GameFightMinimalStats GetGameFightMinimalStats(WorldClient client = null)
        {
            return new GameFightMinimalStats(
                (uint)Stats.Health.Total,
                (uint)Stats.Health.TotalMax,
                (uint)Stats.Health.TotalMaxWithoutPermanentDamages,
                (uint)Stats[PlayerFields.PermanentDamagePercent].Total,
                (uint)Stats.Shield.TotalSafe,
                (short)Stats.AP.Total,
                (short)Stats.AP.TotalMax,
                (short)Stats.MP.Total,
                (short)Stats.MP.TotalMax,
                Summoner.Id,
                true,
                (short)Stats[PlayerFields.NeutralResistPercent].Total,
                (short)Stats[PlayerFields.EarthResistPercent].Total,
                (short)Stats[PlayerFields.WaterResistPercent].Total,
                (short)Stats[PlayerFields.AirResistPercent].Total,
                (short)Stats[PlayerFields.FireResistPercent].Total,
                (short)Stats[PlayerFields.NeutralElementReduction].Total,
                (short)Stats[PlayerFields.EarthElementReduction].Total,
                (short)Stats[PlayerFields.WaterElementReduction].Total,
                (short)Stats[PlayerFields.AirElementReduction].Total,
                (short)Stats[PlayerFields.FireElementReduction].Total,
                (short)Stats[PlayerFields.CriticalDamageReduction].Total,
                (short)Stats[PlayerFields.PushDamageReduction].Total,
                (short)Stats[PlayerFields.PvpNeutralResistPercent].Total,
                (short)Stats[PlayerFields.PvpEarthResistPercent].Total,
                (short)Stats[PlayerFields.PvpWaterResistPercent].Total,
                (short)Stats[PlayerFields.PvpAirResistPercent].Total,
                (short)Stats[PlayerFields.PvpFireResistPercent].Total,
                (short)Stats[PlayerFields.PvpNeutralElementReduction].Total,
                (short)Stats[PlayerFields.PvpEarthElementReduction].Total,
                (short)Stats[PlayerFields.PvpWaterElementReduction].Total,
                (short)Stats[PlayerFields.PvpAirElementReduction].Total,
                (short)Stats[PlayerFields.PvpFireElementReduction].Total,
                (ushort)Stats[PlayerFields.DodgeAPProbability].Total,
                (ushort)Stats[PlayerFields.DodgeMPProbability].Total,
                (short)Stats[PlayerFields.TackleBlock].Total,
                (short)Stats[PlayerFields.TackleEvade].Total,
                0,
                (sbyte)(client == null ? VisibleState : GetVisibleStateFor(client.Character)), // invisibility state
                (ushort)(100 + Stats[PlayerFields.MeleeDamageReceivedPercent].Total),
                    (ushort)(100 + Stats[PlayerFields.RangedDamageReceivedPercent].Total),
                    (ushort)(100 + Stats[PlayerFields.WeaponDamageReceivedPercent].Total),
                    (ushort)(100 + Stats[PlayerFields.SpellDamageReceivedPercent].Total)
                );
        }
    }
}
