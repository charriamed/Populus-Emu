using Stump.DofusProtocol.Enums;
using Stump.DofusProtocol.Messages;
using Stump.DofusProtocol.Types;
using Stump.Server.BaseServer.Network;
using Stump.Server.WorldServer.Core.Network;
using Stump.Server.WorldServer.Database.Shortcuts;
using Stump.Server.WorldServer.Game.Spells;
using System.Linq;

namespace Stump.Server.WorldServer.Handlers.Context.RolePlay
{
    public partial class ContextRoleplayHandler : WorldHandlerContainer
    {
        //[WorldHandler(SpellModifyRequestMessage.Id)]
        //public static void HandleSpellModifyRequestMessage(WorldClient client, SpellModifyRequestMessage message)
        //{
        //    client.Character.Spells.BoostSpell(message.spellId, (ushort)message.spellLevel);
        //    client.Character.RefreshStats();
        //}

        //public static void SendSpellModifySuccessMessage(IPacketReceiver client, Spell spell)
        //{
        //    client.Send(new SpellModifySuccessMessage(spell.Id, (sbyte)spell.CurrentLevel));
        //}

        //public static void SendSpellModifySuccessMessage(IPacketReceiver client, int spellId, sbyte level)
        //{
        //    client.Send(new SpellModifySuccessMessage(spellId, level));
        //}

        //public static void SendSpellModifyFailureMessage(IPacketReceiver client)
        //{
        //    client.Send(new SpellModifyFailureMessage());
        //}

        //public static void SendSpellItemBoostMessage(IPacketReceiver client, int statId, short spellId, short value)
        //{
        //    client.Send(new SpellItemBoostMessage(statId, spellId, value));
        //}

        [WorldHandler(SpellVariantActivationRequestMessage.Id)]
        public static void HandleSpellVariantActivationMessage(WorldClient client, SpellVariantActivationRequestMessage message)
        {
            var spellVariants = SpellManager.Instance.GetSpellVariants();

            if (!client.Character.IsFighting())
            {
                foreach (var variant in spellVariants)
                {
                    if (variant.SpellIds[1] == message.SpellId || variant.SpellIds[0] == message.SpellId)
                    {
                        foreach (SpellShortcut sh in client.Character.Shortcuts.GetShortcuts(ShortcutBarEnum.SPELL_SHORTCUT_BAR))
                        {
                            if (sh.SpellId == variant.SpellIds[0] || sh.SpellId == variant.SpellIds[1])
                            {
                                client.Character.Spells.Where(x => x.Template.Id == sh.SpellId).FirstOrDefault().Record.Position = 0;
                                client.Character.Spells.Where(x => x.Template.Id == message.SpellId).FirstOrDefault().Record.Position = 63;

                                client.Send(new ShortcutBarReplacedMessage((sbyte)ShortcutBarEnum.SPELL_SHORTCUT_BAR, new ShortcutSpell((sbyte)sh.Slot, message.SpellId)));
                                client.Character.Shortcuts.SwapSpellShortcuts(sh.SpellId, (short)message.SpellId);
                                client.Character.Shortcuts.Save();
                                client.Send(new SpellVariantActivationMessage(message.SpellId, true));
                            }
                        }
                    }
                }
            }

        }
    }
}