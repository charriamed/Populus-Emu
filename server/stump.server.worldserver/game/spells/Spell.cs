using System.Collections.Generic;
using System.Linq;
using Stump.DofusProtocol.Types;
using Stump.Server.WorldServer.Database.Spells;
using SpellType = Stump.Server.WorldServer.Database.Spells.SpellType;

namespace Stump.Server.WorldServer.Game.Spells
{
    public class Spell
    {
        private readonly ISpellRecord m_record;
        private readonly int m_id;
        private short m_level;
        private SpellLevelTemplate m_currentLevel;
        private int? m_maxLevel;
        private uint m_ApCostReduction = 0;
        private uint m_DelayReduction = 0;
        private uint m_AdditionalCastPerTurn = 0;
        private uint m_AdditionalCastPerTarget = 0;
        private uint m_AdditionalDamage = 0;
        private uint m_AdditionalHeal = 0;
        private int m_AdditionalRange = 0;
        private uint m_AdditionalCriticalPercent = 0;
        private bool m_SpellObstaclesDisable = false;
        private bool m_LineCastDisable = false;
        private bool m_RangeableEnable = false;

        public Spell(ISpellRecord record)
        {
            m_record = record;
            m_id = m_record.SpellId;
            m_level = (byte) m_record.Level;

            Template = SpellManager.Instance.GetSpellTemplate(Id);
            SpellType = SpellManager.Instance.GetSpellType(Template.TypeId);
            var counter = 1;
            ByLevel = SpellManager.Instance.GetSpellLevels(Template).ToDictionary(entry => counter++);
        }

        public Spell(int id, short level)
        {
            m_id = id;
            m_level = level;

            Template = SpellManager.Instance.GetSpellTemplate(Id);
            SpellType = SpellManager.Instance.GetSpellType(Template.TypeId);
            var counter = 1;
            ByLevel = SpellManager.Instance.GetSpellLevels(Template).ToDictionary(entry => counter++);
        }        
        
        public Spell(SpellTemplate template, byte level)
        {
            m_id = template.Id;
            m_level = level;

            Template = template;
            SpellType = SpellManager.Instance.GetSpellType(Template.TypeId);
            var counter = 1;
            ByLevel = SpellManager.Instance.GetSpellLevels(Template).ToDictionary(entry => counter++);
        }


        #region Properties

        public int Id => m_id;

        public SpellTemplate Template
        {
            get;
        }

        public SpellType SpellType
        {
            get;
        }

        #region Boosts

        public uint DelayReduction
        {
            get
            {
                return m_DelayReduction;
            }
            set
            {
                m_DelayReduction = value;
            }
        }

        public uint AdditionalCastPerTurn
        {
            get
            {
                return m_AdditionalCastPerTurn;
            }
            set
            {
                m_AdditionalCastPerTurn = value;
            }
        }

        public uint AdditionalCastPerTarget
        {
            get
            {
                return m_AdditionalCastPerTarget;
            }
            set
            {
                m_AdditionalCastPerTarget = value;
            }
        }

        public uint AdditionalDamage
        {
            get
            {
                return m_AdditionalDamage;
            }
            set
            {
                m_AdditionalDamage = value;
            }
        }

        public uint AdditionalHeal
        {
            get
            {
                return m_AdditionalHeal;
            }
            set
            {
                m_AdditionalHeal = value;
            }
        }

        public int AdditionalRange
        {
            get
            {
                return m_AdditionalRange;
            }
            set
            {
                m_AdditionalRange = value;
            }
        }

        public uint AdditionalCriticalPercent
        {
            get
            {
                return m_AdditionalCriticalPercent;
            }
            set
            {
                m_AdditionalCriticalPercent = value;
            }
        }

        public uint ApCostReduction
        {
            get
            {
                return m_ApCostReduction;
            }
            set
            {
                m_ApCostReduction = value;
            }
        }

        public bool SpellObstaclesDisable
        {
            get
            {
                return m_SpellObstaclesDisable;
            }
            set
            {
                m_SpellObstaclesDisable = value;
            }
        }

        public bool LineCastDisable
        {
            get
            {
                return m_LineCastDisable;
            }
            set
            {
                m_LineCastDisable = value;
            }
        }

        public bool RangeableEnable
        {
            get
            {
                return m_RangeableEnable;
            }
            set
            {
                m_RangeableEnable = value;
            }
        }

        #endregion



        public int MaxLevel => (m_maxLevel ?? (m_maxLevel = ByLevel.Keys.Max())).Value;

        public short CurrentLevel
        {
            get
            {
                return ByLevel.ContainsKey(m_level) ? m_level : (short)MaxLevel;
            }
            set
            {            
                if (m_record != null)
                    m_record.Level = value;

                m_level = value;
                m_currentLevel = !ByLevel.ContainsKey(CurrentLevel) ? ByLevel[MaxLevel] : ByLevel[CurrentLevel];
            }
        }

        public SpellLevelTemplate CurrentSpellLevel => m_currentLevel ?? (m_currentLevel = !ByLevel.ContainsKey(CurrentLevel) ? ByLevel[MaxLevel] : ByLevel[CurrentLevel]);

        public byte Position => 63;

        public Dictionary<int, SpellLevelTemplate> ByLevel
        {
            get;
        }

        #endregion

        public bool CanBoostSpell() => ByLevel.ContainsKey(CurrentLevel + 1);

        public bool BoostSpell()
        {
            if (!CanBoostSpell())
                return false;

            m_level++;
            if (m_record != null)
                m_record.Level = m_level;
            m_currentLevel = ByLevel[m_level];
            return true;
        }

        public bool UnBoostSpell()
        {
            if (!ByLevel.ContainsKey(CurrentLevel - 1))
                return false;

            m_level--;
            if (m_record != null)
                m_record.Level = m_level;
            m_currentLevel = ByLevel[m_level];
            return true;
        }

        public SpellItem GetSpellItem() => new SpellItem(Id, (sbyte)CurrentLevel);

        public override string ToString() => string.Format("{0} ({1}) (lvl:{2})", Template.Name, Template.Id, CurrentLevel);
    }
}