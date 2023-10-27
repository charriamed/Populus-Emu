﻿using Stump.DofusProtocol.Enums;
using Stump.DofusProtocol.Messages;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Characters;
using Stump.Server.WorldServer.Game.Actors.RolePlay.TaxCollectors;
using Stump.Server.WorldServer.Handlers.Dialogs;

namespace Stump.Server.WorldServer.Game.Dialogs.TaxCollector
{
    public class TaxCollectorInfoDialog : IDialog
    {
        public TaxCollectorInfoDialog(Character character, TaxCollectorNpc taxCollector)
        {
            TaxCollector = taxCollector;
            Character = character;
        }

        public TaxCollectorNpc TaxCollector
        {
            get;
            private set;
        }

        public Character Character
        {
            get;
            private set;
        }

        public DialogTypeEnum DialogType
        {
            get { return DialogTypeEnum.DIALOG_DIALOG; }
        }

        public void Close()
        {
            Character.CloseDialog(this);
            TaxCollector.OnDialogClosed(this);

            DialogHandler.SendLeaveDialogMessage(Character.Client, DialogType);
        }

        public void Open()
        {
            Character.SetDialog(this);
            TaxCollector.OnDialogOpened(this);

            Character.Client.Send(new NpcDialogCreationMessage(TaxCollector.Map.Id, TaxCollector.Id));
            Character.Client.Send(
                new TaxCollectorDialogQuestionExtendedMessage(TaxCollector.Guild.GetBasicGuildInformations(),
                    (ushort) TaxCollector.Guild.TaxCollectorPods,
                    (ushort) TaxCollector.Guild.TaxCollectorProspecting, (ushort) TaxCollector.Guild.TaxCollectorWisdom,
                    (sbyte) TaxCollector.Guild.TaxCollectors.Count, 0, (ulong)TaxCollector.GatheredKamas, (ulong)TaxCollector.GatheredExperience,
                    (ushort)TaxCollector.Bag.BagWeight, (ulong)TaxCollector.Bag.BagValue));
        }
    }
}