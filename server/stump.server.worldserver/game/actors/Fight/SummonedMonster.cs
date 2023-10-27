using Stump.DofusProtocol.Enums;
using Stump.DofusProtocol.Types;
using Stump.Server.WorldServer.Core.Network;
using Stump.Server.WorldServer.Database.Monsters;
using Stump.Server.WorldServer.Database.World;
using Stump.Server.WorldServer.Game.Actors.Interfaces;
using Stump.Server.WorldServer.Game.Actors.Stats;
using Stump.Server.WorldServer.Game.Fights.Teams;
using Stump.Server.WorldServer.Game.Maps.Cells;
using System.Globalization;

namespace Stump.Server.WorldServer.Game.Actors.Fight
{
    public class SummonedMonster : SummonedFighter, ICreature
    {
        readonly StatsFields m_stats;

        public SummonedMonster(int id, FightTeam team, FightActor summoner, MonsterGrade template, Cell cell)
            : base(id, team, template.Spells.ToArray(), summoner, cell, template.MonsterId, template)
        {
            Monster = template;
            Look = Monster.Template.EntityLook.Clone();
            m_stats = new StatsFields(this);
            m_stats.Initialize(template);

            if (Monster.Template.Race.SuperRaceId == 28) //Invocations
            {
                AdjustStats();
            }
        }

        void AdjustStats()
        {
            if (Summoner.Level <= 200)
            {
                m_stats.Health.Base = (short)(Monster.LifePoints * (1 + ((Summoner.Level) / 100d)));

                #region Int Monsters
                //  Dragão )
                if (Monster.MonsterId == (int)MonsterIdEnum.DRAGONNET_ROUGE_4565 || Monster.MonsterId == (int)MonsterIdEnum.DRAGONNET_NOIR)
                {
                    m_stats.Health.Base = (short)(Monster.LifePoints * (1 + ((Summoner.Level) / 100d)));

                    m_stats.Intelligence.Base = (short)(Monster.Intelligence * (1 + ((Summoner.Level) / 100d)));
                }

                // Inflável
                else if (Monster.MonsterId == (int)MonsterIdEnum.LA_GONFLABLE)
                {
                    m_stats.Intelligence.Base = (short)(Monster.Intelligence * (1 + ((Summoner.Level) / 100d)));

                }
                else
                {
                    m_stats.Intelligence.Base = (short)(Monster.Intelligence * (1 + ((Summoner.Level) / 100d)));
                }
                #endregion

                // ( Gato )
                if (Monster.MonsterId == (int)MonsterIdEnum.CHATON_ENRAGE)
                {
                    m_stats.Chance.Base = (short)(Monster.Chance * (1 + ((Summoner.Level) / 100d)));
                }
                else

                {
                    m_stats.Chance.Base = (short)(Monster.Chance * (1 + ((Summoner.Level) / 100d)));
                }

                // ( Gobbal )
                if (Monster.MonsterId == (int)MonsterIdEnum.BOUFTOU || Monster.MonsterId == (int)MonsterIdEnum.BOUFTOU_NOIR_4564)
                {
                    m_stats.Health.Base = (short)(Monster.LifePoints * (1 + ((Summoner.Level) / 100d)));

                    m_stats.Strength.Base = (short)(Monster.Strength * (1 + ((Summoner.Level) / 100d)));
                }
                else
                {
                    m_stats.Strength.Base = (short)(Monster.Strength * (1 + ((Summoner.Level) / 100d)));
                }

                #region Agi Monsters
                // 10% por level ( Tofu )
                if (Monster.MonsterId == (int)MonsterIdEnum.TOFU_DODU_4562 || Monster.MonsterId == (int)MonsterIdEnum.TOFU_NOIR_4561)
                {
                    m_stats.Health.Base = (short)(Monster.LifePoints * (1 + ((Summoner.Level) / 100d)));

                    m_stats.Agility.Base = (short)(Monster.Agility * (1 + ((Summoner.Level) / 100d)));
                }

                // Explosiva
                else if (Monster.MonsterId == (int)MonsterIdEnum.LA_SACRIFIEE)
                {
                    m_stats.Agility.Base = (short)(Monster.Agility * (1 + ((Summoner.Level) / 100d)));
                }
                else
                {
                    m_stats.Agility.Base = (short)(Monster.Agility * (1 + ((Summoner.Level) / 100d)));
                }
                #endregion

                m_stats.Wisdom.Base = (short)(Monster.Wisdom * (1 + ((Summoner.Level) / 100d)));
            }
            else
            {
                m_stats.Health.Base = (short)(Monster.LifePoints * (1 + (200 / 100d)));

                #region Int Monsters
                //  Dragão )
                if (Monster.MonsterId == (int)MonsterIdEnum.DRAGONNET_ROUGE_4565 || Monster.MonsterId == (int)MonsterIdEnum.DRAGONNET_NOIR)
                {
                    m_stats.Health.Base = (short)(Monster.LifePoints * (1 + (200 / 100d)));

                    m_stats.Intelligence.Base = (short)(Monster.Intelligence * (1 + (200 / 100d)));
                }

                // Inflável
                else if (Monster.MonsterId == (int)MonsterIdEnum.LA_GONFLABLE)
                {
                    m_stats.Intelligence.Base = (short)(Monster.Intelligence * (1 + (200 / 100d)));

                }
                else
                {
                    m_stats.Intelligence.Base = (short)(Monster.Intelligence * (1 + (200 / 100d)));
                }
                #endregion

                // ( Gato )
                if (Monster.MonsterId == (int)MonsterIdEnum.CHATON_ENRAGE)
                {
                    m_stats.Chance.Base = (short)(Monster.Chance * (1 + (200 / 100d)));
                }
                else
                {
                    m_stats.Chance.Base = (short)(Monster.Chance * (1 + (200 / 100d)));
                }

                // ( Gobbal )
                if (Monster.MonsterId == (int)MonsterIdEnum.BOUFTOU || Monster.MonsterId == (int)MonsterIdEnum.BOUFTOU_NOIR_4564)
                {
                    m_stats.Health.Base = (short)(Monster.LifePoints * (1 + (200 / 100d)));

                    m_stats.Strength.Base = (short)(Monster.Strength * (1 + (200 / 100d)));
                }
                else
                {
                    m_stats.Strength.Base = (short)(Monster.Strength * (1 + (200 / 100d)));
                }

                #region Agi Monsters
                // 10% por level ( Tofu )
                if (Monster.MonsterId == (int)MonsterIdEnum.TOFU_DODU_4562 || Monster.MonsterId == (int)MonsterIdEnum.TOFU_NOIR_4561)
                {
                    m_stats.Health.Base = (short)(Monster.LifePoints * (1 + (200 / 100d)));

                    m_stats.Agility.Base = (short)(Monster.Agility * (1 + (200 / 100d)));
                }

                // Explosiva
                else if (Monster.MonsterId == (int)MonsterIdEnum.LA_SACRIFIEE)
                {
                    m_stats.Agility.Base = (short)(Monster.Agility * (1 + (200 / 100d)));
                }
                else
                {
                    m_stats.Agility.Base = (short)(Monster.Agility * (1 + (200 / 100d)));
                }
                #endregion

                m_stats.Wisdom.Base = (short)(Monster.Wisdom * (1 + (200 / 100d)));
            }

        }

        public override int CalculateArmorValue(int reduction)
        {
            if (Summoner.Level <= 200)
            {
                return (int)(reduction * (100 + 5 * (Summoner.Level)) / 100d);
            }
            else
            {
                return (int)(reduction * (100 + 5 * 200) / 100d);

            }
        }

        public override bool CanPlay() => base.CanPlay() && Monster.Template.CanPlay;

        public override bool CanMove() => base.CanMove() && MonsterGrade.MovementPoints > 0;

        public override bool CanTackle(FightActor fighter) => base.CanTackle(fighter) && Monster.Template.CanTackle;

        public MonsterGrade Monster
        {
            get;
        }

        public override ObjectPosition MapPosition
        {
            get { return Position; }
        }

        public override ushort Level
        {
            get { return (byte)Monster.Level; }
        }

        public MonsterGrade MonsterGrade
        {
            get { return Monster; }
        }

        public override StatsFields Stats
        {
            get { return m_stats; }
        }

        public override string GetMapRunningFighterName()
        {
            return Monster.Id.ToString(CultureInfo.InvariantCulture);
        }

        public override string Name
        {
            get { return Monster.Template.Name; }
        }

        public override bool CanBePushed()
        {
            return base.CanBePushed() && Monster.Template.CanBePushed;
        }

        public override bool CanSwitchPos()
        {
            return base.CanSwitchPos() && Monster.Template.CanSwitchPos;
        }

        public override FightTeamMemberInformations GetFightTeamMemberInformations()
        {
            return new FightTeamMemberMonsterInformations(Id, Monster.Template.Id, (sbyte)Monster.GradeId);
        }

        public override GameFightFighterInformations GetGameFightFighterInformations(WorldClient client = null)
        {
            return new GameFightMonsterInformations(
                Id,
                Look.GetEntityLook(),
                GetEntityDispositionInformations(client),
                (sbyte)Team.Id,
                0,
                IsAlive(),
                GetGameFightMinimalStats(client),
                new ushort[0],
                (ushort)Monster.Template.Id,
                (sbyte)Monster.GradeId, (short)Monster.Level);
        }

        public override GameFightFighterLightInformations GetGameFightFighterLightInformations(WorldClient client = null)
        {
            return new GameFightFighterMonsterLightInformations(
                true,
                IsAlive(),
                Id,
                0,
                Level,
                (sbyte)BreedEnum.MONSTER,
                (ushort)Monster.Template.Id);
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
                (ushort)(100 + Stats[PlayerFields.MeleeDamageDonePercent].Total),
                    (ushort)(100 + Stats[PlayerFields.RangedDamageReceivedPercent].Total),
                    (ushort)(100 + Stats[PlayerFields.WeaponDamageReceivedPercent].Total),
                    (ushort)(100 + Stats[PlayerFields.SpellDamageReceivedPercent].Total)
                );
        }
    }
}