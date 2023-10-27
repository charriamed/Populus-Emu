namespace Stump.DofusProtocol.Types
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class GameFightMinimalStatsPreparation : GameFightMinimalStats
    {
        public new const short Id = 360;
        public override short TypeId
        {
            get { return Id; }
        }
        public uint Initiative { get; set; }

        public GameFightMinimalStatsPreparation(uint lifePoints, uint maxLifePoints, uint baseMaxLifePoints, uint permanentDamagePercent, uint shieldPoints, short actionPoints, short maxActionPoints, short movementPoints, short maxMovementPoints, double summoner, bool summoned, short neutralElementResistPercent, short earthElementResistPercent, short waterElementResistPercent, short airElementResistPercent, short fireElementResistPercent, short neutralElementReduction, short earthElementReduction, short waterElementReduction, short airElementReduction, short fireElementReduction, short criticalDamageFixedResist, short pushDamageFixedResist, short pvpNeutralElementResistPercent, short pvpEarthElementResistPercent, short pvpWaterElementResistPercent, short pvpAirElementResistPercent, short pvpFireElementResistPercent, short pvpNeutralElementReduction, short pvpEarthElementReduction, short pvpWaterElementReduction, short pvpAirElementReduction, short pvpFireElementReduction, ushort dodgePALostProbability, ushort dodgePMLostProbability, short tackleBlock, short tackleEvade, short fixedDamageReflection, sbyte invisibilityState, ushort meleeDamageReceivedPercent, ushort rangedDamageReceivedPercent, ushort weaponDamageReceivedPercent, ushort spellDamageReceivedPercent, uint initiative)
        {
            this.LifePoints = lifePoints;
            this.MaxLifePoints = maxLifePoints;
            this.BaseMaxLifePoints = baseMaxLifePoints;
            this.PermanentDamagePercent = permanentDamagePercent;
            this.ShieldPoints = shieldPoints;
            this.ActionPoints = actionPoints;
            this.MaxActionPoints = maxActionPoints;
            this.MovementPoints = movementPoints;
            this.MaxMovementPoints = maxMovementPoints;
            this.Summoner = summoner;
            this.Summoned = summoned;
            this.NeutralElementResistPercent = neutralElementResistPercent;
            this.EarthElementResistPercent = earthElementResistPercent;
            this.WaterElementResistPercent = waterElementResistPercent;
            this.AirElementResistPercent = airElementResistPercent;
            this.FireElementResistPercent = fireElementResistPercent;
            this.NeutralElementReduction = neutralElementReduction;
            this.EarthElementReduction = earthElementReduction;
            this.WaterElementReduction = waterElementReduction;
            this.AirElementReduction = airElementReduction;
            this.FireElementReduction = fireElementReduction;
            this.CriticalDamageFixedResist = criticalDamageFixedResist;
            this.PushDamageFixedResist = pushDamageFixedResist;
            this.PvpNeutralElementResistPercent = pvpNeutralElementResistPercent;
            this.PvpEarthElementResistPercent = pvpEarthElementResistPercent;
            this.PvpWaterElementResistPercent = pvpWaterElementResistPercent;
            this.PvpAirElementResistPercent = pvpAirElementResistPercent;
            this.PvpFireElementResistPercent = pvpFireElementResistPercent;
            this.PvpNeutralElementReduction = pvpNeutralElementReduction;
            this.PvpEarthElementReduction = pvpEarthElementReduction;
            this.PvpWaterElementReduction = pvpWaterElementReduction;
            this.PvpAirElementReduction = pvpAirElementReduction;
            this.PvpFireElementReduction = pvpFireElementReduction;
            this.DodgePALostProbability = dodgePALostProbability;
            this.DodgePMLostProbability = dodgePMLostProbability;
            this.TackleBlock = tackleBlock;
            this.TackleEvade = tackleEvade;
            this.FixedDamageReflection = fixedDamageReflection;
            this.InvisibilityState = invisibilityState;
            this.MeleeDamageReceivedPercent = meleeDamageReceivedPercent;
            this.RangedDamageReceivedPercent = rangedDamageReceivedPercent;
            this.WeaponDamageReceivedPercent = weaponDamageReceivedPercent;
            this.SpellDamageReceivedPercent = spellDamageReceivedPercent;
            this.Initiative = initiative;
        }

        public GameFightMinimalStatsPreparation() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteVarUInt(Initiative);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            Initiative = reader.ReadVarUInt();
        }

    }
}
