using NLog;
using Stump.Core.Extensions;
using Stump.Core.IO;
//BISMILLAH
using Stump.DofusProtocol.Enums;
using Stump.DofusProtocol.Types;
using Stump.Server.WorldServer.Handlers.Context;
using Stump.Server.BaseServer.Database;
using Stump.Server.WorldServer.Database.Items;
using Stump.Server.WorldServer.Core.Network;
using Stump.DofusProtocol.Messages;
using Stump.Server.WorldServer.Database.Items.Shops;
using Stump.Server.WorldServer.Database.Items.Templates;
using Stump.Server.BaseServer.Initialization;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Characters;
using Stump.Server.WorldServer.Game.Items.Player;
using System.Collections.Generic;
using Stump.Server.WorldServer.Database.Spells;
using System.IO;
using System.Linq;
using Stump.Server.WorldServer.Database.Characters;
using Stump.Server.WorldServer.Game.Actors.Look;
using Stump.Server.WorldServer.Handlers.Inventory;
using Stump.Server.WorldServer.Handlers.Shortcuts;
using Stump.Server.WorldServer.Database.Npcs.Replies;
using Stump.Server.WorldServer.Game.Maps;

namespace Stump.Server.WorldServer.Game.Spells
{
    // SAAD LE BG
    public class IncarnationManager : DataManager<IncarnationManager>
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        private List<CustomIncarnationRecord> CustomIncarnationRecords = new List<CustomIncarnationRecord>();
        public List<Jesaispasquoimettre> handlers = new List<Jesaispasquoimettre>();

        [Initialization(InitializationPass.Fourth)]
        public override void Initialize()
        {
            var database = WorldServer.Instance.DBAccessor.Database;
            foreach (var record in database.Fetch<CustomIncarnationRecord>(CustomIncarnationRelator.FetchQuery).ToList())
            {
                CustomIncarnationRecords.Add(record);
            }
        }

        public CustomIncarnationRecord GetCustomIncarnationRecord(int Id)
        {
            return CustomIncarnationRecords.FirstOrDefault(x => x.Id == Id);
        }

        public CustomIncarnationRecord GetCustomIncarnationRecordByItem(int ItemId)
        {
            return CustomIncarnationRecords.FirstOrDefault(x => x.ItemId == ItemId);
        }

        public void ApplyCustomStats(Character character, CustomIncarnationRecord record)
        {          
            character.CustomStatsActivated = true;
            character.Stats = new Actors.Stats.StatsFields(character);
            character.Stats.Initialize(record);
            character.RefreshStats();
        }

        public void UnApplyCustomStats(Character character)
        {
            character.CustomStatsActivated = false;
            character.RefreshStats();
        }

        public void ApplyCustomIncarnation(Character character, CustomIncarnationRecord record)
        {
            var look = record.CustomLookString;
            var charspells = record.Spells.Select(x => GetCharacterSpell(x, character.Id)).ToList();
            foreach (var item in character.Inventory.GetEquipedItems())
            {
                character.Inventory.MoveItem(item, CharacterInventoryPositionEnum.INVENTORY_POSITION_NOT_EQUIPED);
            }
            character.CustomLook = ActorLook.Parse(look);
            character.CustomLookActivated = true;
            character.RefreshActor();
            character.IsInIncarnation = true;
            character.IncarnationId = record.Id;
            ApplyCustomStats(character, record);
            character.Spells.SetCustomSpells(charspells);
            InventoryHandler.SendSpellListMessage(character.Client, false);

            int index = 0;
            foreach (var spell in charspells)
            {
                character.Shortcuts.AddSpellShortcut(index, (short)spell.Id, true);
                index++;
            }

            ShortcutHandler.SendShortcutBarContentMessage(character.Client, ShortcutBarEnum.SPELL_SHORTCUT_BAR);
            character.SaveLater();
        }

        public void ConnectWithCustomIncarnation(Character character, int incarnId)
        {
            var record = GetCustomIncarnationRecord(incarnId);
            var charspells = record.Spells.Select(x => GetCharacterSpell(x, character.Id)).ToList();
            ApplyCustomStats(character, record);
            character.Spells.SetCustomSpells(charspells);
            InventoryHandler.SendSpellListMessage(character.Client, false);

            int index = 0;
            foreach (var spell in charspells)
            {
                character.Shortcuts.AddSpellShortcut(index, (short)spell.Id, true);
                index++;
            }
            character.SaveLater();
        }

        public CharacterSpell GetCharacterSpell(Spell spell, int ChracterId)
        {
            CharacterSpellRecord record = new CharacterSpellRecord()
            {
                Level = 1,
                SpellId = spell.Id,
                OwnerId = ChracterId
            };

            return new CharacterSpell(record);
        }

        public void UnApplyCustomIncarnation(Character character)
        {
            character.CustomLookActivated = false;
            character.Spells.ResetCustomSpells();
            character.RefreshActor();
            character.Shortcuts.ResetCustomSpellsShortcuts();
            character.IsInIncarnation = false;
            character.IncarnationId = 0;


            InventoryHandler.SendSpellListMessage(character.Client, true);
            ShortcutHandler.SendShortcutBarContentMessage(character.Client, ShortcutBarEnum.SPELL_SHORTCUT_BAR);

            UnApplyCustomStats(character);

            character.SaveLater();
        }

        public void CheckArea(Character character, Map map)
        {
            var handlr = handlers.FirstOrDefault(x => x.chracter == character.Id);
            if (handlr == null)
            {
                IncarnationManager.Instance.UnApplyCustomIncarnation(character);
                character.Teleport(character.Breed.GetStartPosition());
                return;
            }
            var areas = handlr.areas;
            if (!areas.Contains(map.SubArea))
            {
                IncarnationManager.Instance.UnApplyCustomIncarnation(character);
                IncarnationManager.Instance.handlers.Remove(handlr);
            }
        }
    }
}