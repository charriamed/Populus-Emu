using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Stump.Core.Attributes;
using Stump.DofusProtocol.Enums;
using Stump.Server.BaseServer.Database;
using Stump.Server.BaseServer.Initialization;
using Stump.Server.WorldServer.Database.Breeds;
using Stump.Server.WorldServer.Database.Characters;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Characters;
using Stump.Server.WorldServer.Database.Spells;
using Stump.Server.WorldServer.Game.Spells;

namespace Stump.Server.WorldServer.Game.Breeds
{
    public class BreedManager : DataManager<BreedManager>
    {
        /// <summary>
        /// List of available breeds
        /// </summary>
        [Variable]
        public readonly static List<PlayableBreedEnum> AvailableBreeds = new List<PlayableBreedEnum>
            {
                PlayableBreedEnum.Feca,
                PlayableBreedEnum.Osamodas,
                PlayableBreedEnum.Enutrof,
                PlayableBreedEnum.Sram,
                PlayableBreedEnum.Xelor,
                PlayableBreedEnum.Ecaflip,
                PlayableBreedEnum.Eniripsa,
                PlayableBreedEnum.Iop,
                PlayableBreedEnum.Cra,
                PlayableBreedEnum.Sadida,
                PlayableBreedEnum.Sacrieur,
                PlayableBreedEnum.Pandawa,
                PlayableBreedEnum.Roublard,
                PlayableBreedEnum.Zobal,
                PlayableBreedEnum.Steamer,
                PlayableBreedEnum.Eliotrope,
                PlayableBreedEnum.Ouginak,
                PlayableBreedEnum.Huppermage,
            };

        public uint AvailableBreedsFlags
        {
            get
            {
                return (uint)AvailableBreeds.Aggregate(0, (current, breedEnum) => current | ( 1 << ((int)breedEnum - 1) ));
            }
        }

        private readonly Dictionary<int, Breed> m_breeds = new Dictionary<int, Breed>();
        private Dictionary<int, Head> m_heads = new Dictionary<int, Head>();

        public IReadOnlyDictionary<int, Head> Heads => new ReadOnlyDictionary<int, Head>(m_heads);

        [Initialization(InitializationPass.Third)]
        public override void Initialize()
        {
            base.Initialize();
            m_breeds.Clear();
            int index = 1;
            var dq = Database.Query<Breed, BreedItem, BreedSpell, Breed>(new BreedRelator().Map, BreedRelator.FetchQuery).OrderBy(v => v.Id).ToList();
            foreach (var breed in dq)
            {
                m_breeds.Add(index, breed);
                index++;
            }
            m_heads = Database.Query<Head>(HeadRelator.FetchQuery).ToDictionary(x => x.Id);
        }

        public Breed GetBreed(PlayableBreedEnum breed)
        {
            return GetBreed((int)breed);
        }

        /// <summary>
        /// Get the breed associated to the given id
        /// </summary>
        /// <param name="id"></param>
        public Breed GetBreed(int id)
        {
            Breed breed;
            m_breeds.TryGetValue(id, out breed);

            return breed;
        }

        public Head GetHead(int id)
        {
            Head head;
            m_heads.TryGetValue(id, out head);

            return head;
        }

        public Head GetHead(Predicate<Head> predicate)
        {
            return m_heads.Values.FirstOrDefault(x => predicate(x));
        }

        public bool IsBreedAvailable(int id) => AvailableBreeds.Contains((PlayableBreedEnum)id);

        /// <summary>
        /// Add a breed instance to the database
        /// </summary>
        /// <param name="breed">Breed instance to add</param>
        /// <param name="defineId">When set to true the breed id will be auto generated</param>
        public void AddBreed(Breed breed, bool defineId = false)
        {
            if(defineId)
            {
                var id = m_breeds.Keys.Max() + 1;
                breed.Id = id;
            }

            if (m_breeds.ContainsKey(breed.Id))
                throw new Exception(string.Format("Breed with id {0} already exists", breed.Id));

            m_breeds.Add(breed.Id, breed);

            Database.Insert(breed);
        }

        /// <summary>
        /// Remove a breed from the database
        /// </summary>
        /// <param name="breed"></param>
        public void RemoveBreed(Breed breed)
        {
            RemoveBreed(breed.Id);
        }

        /// <summary>
        /// Remove a breed from the database by his id
        /// </summary>
        /// <param name="id"></param>
        public void RemoveBreed(int id)
        {
            if (!m_breeds.ContainsKey(id))
                throw new Exception(string.Format("Breed with id {0} does not exist", id));

            // it's safer to delete the breed in the dictionary first next in the database
            var breed = m_breeds[id];
            m_breeds.Remove(id);

            Database.Delete(breed);
        }

        public static void ChangeBreed(Character character, PlayableBreedEnum breed)
        {
            //character.Spells.ForgetAllSpells();
            //ForgetSpecialSpells(character);
            character.ResetStats();

            character.Inventory.CheckItemsCriterias();

            foreach (var breedSpell in character.Breed.Spells)
            {
                foreach (var shortcut in character.Shortcuts.SpellsShortcuts.Where(x => x.Value.SpellId == breedSpell.Spell).ToArray())
                    character.Shortcuts.RemoveShortcut(ShortcutBarEnum.SPELL_SHORTCUT_BAR, shortcut.Key);

                character.Spells.UnLearnSpell(breedSpell.Spell);
                character.Spells.UnLearnSpell(breedSpell.VariantId);
            }

            character.SetBreed(breed);

            var shortcuts = character.Shortcuts.SpellsShortcuts;

            foreach (var spell in character.Breed.Spells)
            {
                if (spell.ObtainLevel > character.Level)
                {
                    foreach (var shortcut in shortcuts.Where(x => x.Value.SpellId == spell.Spell).ToArray())
                        character.Shortcuts.RemoveShortcut(ShortcutBarEnum.SPELL_SHORTCUT_BAR, shortcut.Key);

                    if (character.Spells.HasSpell(spell.Spell, true))
                    {
                        character.Spells.UnLearnSpell(spell.Spell);
                    }
                }
                else if (spell.ObtainLevel <= character.Level && !character.Spells.HasSpell(spell.Spell, true))
                {
                    character.Spells.LearnSpell(spell.Spell);

                    character.Shortcuts.AddSpellShortcut(character.Shortcuts.GetNextFreeSlot(ShortcutBarEnum.SPELL_SHORTCUT_BAR),
                        (short)spell.Spell);
                }

                if (spell.VariantLevel > character.Level)
                {
                    foreach (var shortcut in shortcuts.Where(x => x.Value.SpellId == spell.VariantId).ToArray())
                        character.Shortcuts.RemoveShortcut(ShortcutBarEnum.SPELL_SHORTCUT_BAR, shortcut.Key);

                    if (character.Spells.HasSpell(spell.VariantId, true))
                    {
                        character.Spells.UnLearnSpell(spell.VariantId);
                    }
                }

                if (spell.VariantLevel <= character.Level && !character.Spells.HasSpell(spell.VariantId, true))
                {
                    character.Spells.LearnSpell(spell.VariantId, 0);
                }

                //BOOST SPELLS WHEN LEVELUP
                if (spell.Spell != 0)
                {
                    var count = SpellManager.Instance.GetSpellLevels(spell.Spell).Count();
                    foreach (var spelllevel in SpellManager.Instance.GetSpellLevels(spell.Spell).OrderByDescending(x => x.MinPlayerLevel))
                    {
                        if (spelllevel.MinPlayerLevel <= character.Level)
                        {
                            character.Spells.BoostSpell(spell.Spell, (ushort)count);
                            break;
                        }
                        count--;
                    }
                }
            }
        }

        //static void ForgetSpecialSpells(Character character)
        //{
        //    var specialSpellsList = new List<SpellIdEnum>
        //    {
        //        SpellIdEnum.REINFORCED_PROTECTION,
        //        SpellIdEnum.SPIRITUAL_LEASH_420,
        //        SpellIdEnum.PULL_OUT,
        //        SpellIdEnum.JINX,
        //        SpellIdEnum.RHOL_BAK,
        //        SpellIdEnum.FELINTION,
        //        //SpellIdEnum.DECISIVE_WORD,
        //        //SpellIdEnum.BROKLE,
        //        SpellIdEnum.DISPERSING_ARROW,
        //        SpellIdEnum.THE_TREE_OF_LIFE,
        //        //SpellIdEnum.PAIN_SHARED,
        //        SpellIdEnum.DIFFRACTION,
        //        SpellIdEnum.FOCUS,
        //        SpellIdEnum.BOOMBOT,
        //        SpellIdEnum.DRUNKENNESS,
        //        SpellIdEnum.BREAKWATER_3277,
        //        SpellIdEnum.FOCUS,
        //        SpellIdEnum.JOURNEY
        //    };

        //    specialSpellsList.ForEach(x => character.Spells.UnLearnSpell((int)x));
        //}
    }
}
