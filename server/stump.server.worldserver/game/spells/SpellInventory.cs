using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Stump.Server.WorldServer.Database.Characters;
using Stump.Server.WorldServer.Database.Spells;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Characters;
using Stump.Server.WorldServer.Handlers.Context.RolePlay;
using Stump.Server.WorldServer.Handlers.Inventory;
using Stump.Server.WorldServer.Database.Shortcuts;

namespace Stump.Server.WorldServer.Game.Spells
{
    public class SpellInventory : IEnumerable<CharacterSpell>
    {
        private readonly Dictionary<int, CharacterSpell> m_spells = new Dictionary<int, CharacterSpell>();
        private readonly Queue<CharacterSpellRecord> m_spellsToDelete = new Queue<CharacterSpellRecord>();
        private List<CharacterSpell> m_CustomSpells = new List<CharacterSpell>();
        private readonly object m_locker = new object();

        public SpellInventory(Character owner)
        {
            Owner = owner;
            
        }

        public Character Owner
        {
            get;
            private set;
        }

        internal void LoadSpells()
        {
            var database = WorldServer.Instance.DBAccessor.Database;
            
            foreach (var spell in database.Query<CharacterSpellRecord>(string.Format(CharacterSpellRelator.FetchByOwner, Owner.Id)).Select(record => new CharacterSpell(record)))
            {
                if (m_spells.ContainsKey(spell.Id))
                    continue;

                m_spells.Add(spell.Id, spell);
            }

            var spellsToLearn = from spell in Owner.Breed.Spells
                                where spell.ObtainLevel <= Owner.Level
                                orderby spell.ObtainLevel, spell.Spell ascending
                                select spell;

            var slot = 0;
            foreach (var spellRecord in spellsToLearn.Select(learnableSpell => SpellManager.Instance.CreateSpellRecord(Owner.Record,
                SpellManager.Instance.
                    GetSpellTemplate(
                        learnableSpell.
                            Spell), 63)))
            {
                if (!m_spells.ContainsKey(spellRecord.SpellId))
                {
                    m_spells.Add(spellRecord.SpellId, new CharacterSpell(spellRecord));
                    database.Insert(spellRecord);

                    var shortcut = new SpellShortcut(Owner.Record, slot, (short)spellRecord.SpellId);
                    database.Insert(shortcut);
                    slot++;
                }
            }

            foreach (var spellRecord in spellsToLearn.Select(learnableSpell => SpellManager.Instance.CreateSpellRecord(Owner.Record,
                SpellManager.Instance.
                    GetSpellTemplate(
                        learnableSpell.
                            VariantId), 0)))
            {
                if (!m_spells.ContainsKey(spellRecord.SpellId))
                {
                    m_spells.Add(spellRecord.SpellId, new CharacterSpell(spellRecord));
                    database.Insert(spellRecord);
                }
            }
            InventoryHandler.SendSpellListMessage(Owner.Client, true);
        }

        public void SetCustomSpells(List<CharacterSpell> spells)
        {
            m_CustomSpells = spells;
        }

        public void ResetCustomSpells()
        {
            m_CustomSpells = new List<CharacterSpell>();
        }

        public CharacterSpell GetSpell(int id, bool WithoutCustom = false)
        {
            CharacterSpell spell;
            if (m_CustomSpells.Count > 0 && !WithoutCustom) return m_CustomSpells.FirstOrDefault(x => x.Id == id);
            return m_spells.TryGetValue(id, out spell) ? spell : null;
        }

        public IEnumerable<CharacterSpell> GetPlayableSpells()
        {
            if (m_CustomSpells.Count > 0) return m_CustomSpells;
            return m_spells.Select(x => x.Value);
        }

        public bool HasSpell(int id, bool WithoutCustom = false)
        {
            if (m_CustomSpells.Count > 0 && !WithoutCustom) return m_CustomSpells.Any(x => x.Id == id);
            return m_spells.ContainsKey(id);
        }

        public bool HasSpell(CharacterSpell spell, bool WithoutCustom = false)
        {
            if (m_CustomSpells.Count > 0 && !WithoutCustom) return m_CustomSpells.Any(x => x.Id == spell.Id);
            return m_spells.ContainsKey(spell.Id);
        }

        public IEnumerable<CharacterSpell> GetSpells(bool WithoutCustom = false)
        {
            if (m_CustomSpells.Count > 0 && !WithoutCustom) return m_CustomSpells;
            return m_spells.Values;
        }

        public CharacterSpell LearnSpell(int id, short position = 63)
        {
            var template = SpellManager.Instance.GetSpellTemplate(id);

            return template == null ? null : LearnSpell(template, position);
        }

        public CharacterSpell LearnSpell(SpellTemplate template, int position = 63)
        {
            var record = SpellManager.Instance.CreateSpellRecord(Owner.Record, template, (short)position);

            var spell = new CharacterSpell(record);
            m_spells.Add(spell.Id, spell);

            if (m_CustomSpells.Count < 1)
            {
                //ContextRoleplayHandler.SendSpellModifySuccessMessage(Owner.Client, spell);
            }

            return spell;
        }

        public CharacterSpell LearnSpell(SpellTemplate template, short level, short position)
        {
            var record = SpellManager.Instance.CreateSpellRecord(Owner.Record, template, position);

            var spell = new CharacterSpell(record);
            m_spells.Add(spell.Id, spell);

            if (m_CustomSpells.Count < 1)
            {
                //ContextRoleplayHandler.SendSpellModifySuccessMessage(Owner.Client, spell);
            }

            return spell;
        }

        public bool UnLearnSpell(int id)
        {
            var spell = GetSpell(id);

            if (spell == null)
                return true;

            m_spells.Remove(id);
            m_spellsToDelete.Enqueue(spell.Record);

            //Owner.SpellsPoints += (ushort)CalculateSpellPoints(spell.CurrentLevel);

            InventoryHandler.SendSpellListMessage(Owner.Client, true);
            return true;
        }

        public bool UnLearnSpell(CharacterSpell spell) => UnLearnSpell(spell.Id);

        public bool UnLearnSpell(SpellTemplate spell) => UnLearnSpell(spell.Id);

        public int CalculateSpellPoints(int level, int currentLevel = 1)
        {
            var spentPoints = 0;
            if (currentLevel > 1)
                spentPoints = CalculateSpellPoints(currentLevel);

            return ((level * (level - 1)) / 2) - spentPoints;
        }

        public bool CanBoostSpell(Spell spell, ushort level, bool send = true)
        {
            if (Owner.IsFighting())
            {
                if (send)
                    //ContextRoleplayHandler.SendSpellModifyFailureMessage(Owner.Client);

                return false;
            }

            //if (spell.CurrentLevel == level || level > 3)
            //{
            //    if (send)
            //        //ContextRoleplayHandler.SendSpellModifyFailureMessage(Owner.Client);

            //    return false;
            //}

            

            if (spell.ByLevel[level].MinPlayerLevel > Owner.Level)
            {
                if (send)
                    //ContextRoleplayHandler.SendSpellModifyFailureMessage(Owner.Client);

                return false;
            }

            return true;
        }

        public bool BoostSpell(int id, ushort level)
        {
            var spell = GetSpell(id);

            if (spell == null)
            {
                return false;
            }
            spell.CurrentLevel = (byte)level;

            return true;
        }

        public bool ForgetSpell(SpellTemplate spell)
        {
            return ForgetSpell(spell.Id);
        }

        public bool ForgetSpell(int id)
        {
            if (!HasSpell(id))
                return false;

            var spell = GetSpell(id);

            return ForgetSpell(spell);
        }

        public bool ForgetSpell(CharacterSpell spell)
        {
            if (!HasSpell(spell.Id))
                return false;

            var level = spell.CurrentLevel;
            for (var i = 1; i < level; i++)
            {
                DowngradeSpell(spell, false);
            }

            InventoryHandler.SendSpellListMessage(Owner.Client, true);
            return true;
        }

        public void ForgetAllSpells()
        {
            foreach (var spell in m_spells)
            {
                var level = spell.Value.CurrentLevel;
                for (var i = 1; i < level; i++)
                {
                    DowngradeSpell(spell.Value, false);
                }
            }

            InventoryHandler.SendSpellListMessage(Owner.Client, true);
            Owner.RefreshStats();
        }

        public int DowngradeSpell(SpellTemplate spell)
        {
            return DowngradeSpell(spell.Id);
        }

        public int DowngradeSpell(int id)
        {
            if (!HasSpell(id))
                return 0;

            var spell = GetSpell(id);

            return DowngradeSpell(spell);
        }

        public int DowngradeSpell(CharacterSpell spell, bool send = true)
        {
            if (!HasSpell(spell.Id))
                return 0;

            if (spell.CurrentLevel <= 1)
                return 0;

            spell.CurrentLevel -= 1;
            //Owner.SpellsPoints += spell.CurrentLevel;

            if (!send)
                return spell.CurrentLevel;

            InventoryHandler.SendSpellListMessage(Owner.Client, true);
            //ContextRoleplayHandler.SendSpellModifySuccessMessage(Owner.Client, spell);

            Owner.RefreshStats();

            return spell.CurrentLevel;
        }

        public void MoveSpell(int id, byte position)
        {
            var spell = GetSpell(id);

            if (spell == null)
                return;

            Owner.Shortcuts.AddSpellShortcut(position, (short)id);
        }

        public int CountSpentBoostPoint()
        {
            var count = 0;
            foreach (var spell in this)
            {
                for (var i = 1; i < spell.CurrentLevel; i++)
                {
                    count += i;
                }
            }

            return count;
        }

        public void Save()
        {
            lock (m_locker)
            {
                var database = WorldServer.Instance.DBAccessor.Database;
                foreach (var characterSpell in m_spells)
                {
                    database.Save(characterSpell.Value.Record);
                }

                while (m_spellsToDelete.Count > 0)
                {
                    var record = m_spellsToDelete.Dequeue();

                    database.Delete(record);
                }
            }
        }

        public IEnumerator<CharacterSpell> GetEnumerator() => m_spells.Values.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}