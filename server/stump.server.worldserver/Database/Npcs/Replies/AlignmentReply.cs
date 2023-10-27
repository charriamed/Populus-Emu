﻿using Stump.DofusProtocol.Enums;
using Stump.Server.BaseServer.Database;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Characters;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Npcs;
using Stump.Server.WorldServer.Game.Items;
using Stump.Server.WorldServer.Game.Items.Player;
using System.Collections.Generic;
using System.Linq;

namespace Stump.Server.WorldServer.Database.Npcs.Replies
{
    [Discriminator("Alignment", typeof(NpcReply), typeof(NpcReplyRecord))]
    public class AlignmentReply : NpcReply
    {
        public AlignmentReply(NpcReplyRecord record)
            : base(record)
        {
        }

        /// <summary>
        /// Parameter 0
        /// </summary>
        public int Side
        {
            get
            {
                return Record.GetParameter<int>(0);
            }
            set
            {
                Record.SetParameter(0, value);
            }
        }

        /// <summary>
        /// Parameter 1
        /// </summary>
        public string ItemsParameter
        {
            get
            {
                return Record.GetParameter<string>(1, true);
            }
            set
            {
                Record.SetParameter(1, value);
            }
        }

        /// <summary>
        /// Parameter 2
        /// </summary>
        public long KamasParameter
        {
            get
            {
                return Record.GetParameter<long>(2, true);
            }
            set
            {
                Record.SetParameter(2, value);
            }
        }

        public override bool Execute(Npc npc, Character character)
        {
            if (!base.Execute(npc, character))
                return false;

            if (string.IsNullOrEmpty(ItemsParameter) && KamasParameter == 0)
            {
                character.ChangeAlignementSide((AlignmentSideEnum)Side);
                character.AlignmentGrade = 1;
                return true;
            }

            if ((long)character.Kamas < KamasParameter)
            {
                //Vous n'avez pas assez de kamas pour effectuer cette action.
                character.SendInformationMessage(TextInformationTypeEnum.TEXT_INFORMATION_ERROR, 82);
                return false;
            }

            if (ItemsParameter != null)
            {
                var parameter = ItemsParameter.Split(',');
                var itemsToDelete = new Dictionary<BasePlayerItem, int>();

                foreach (var itemParameter in parameter.Select(x => x.Split('_')))
                {
                    if (!int.TryParse(itemParameter[0], out int itemId))
                        return false;

                    if (!int.TryParse(itemParameter[1], out int amount))
                        return false;

                    var template = ItemManager.Instance.TryGetTemplate(itemId);
                    if (template == null)
                        return false;

                    var item = character.Inventory.TryGetItem(template);

                    if (item == null)
                    {
                        //Vous ne possédez pas l'objet nécessaire.
                        character.SendInformationMessage(TextInformationTypeEnum.TEXT_INFORMATION_ERROR, 4);
                        return false;
                    }

                    if (item.Stack < amount)
                    {
                        //Vous ne possédez pas l'objet en quantité suffisante.
                        character.SendInformationMessage(TextInformationTypeEnum.TEXT_INFORMATION_ERROR, 252);
                        return false;
                    }

                    itemsToDelete.Add(item, amount);
                }

                foreach (var itemToDelete in itemsToDelete)
                {
                    character.Inventory.RemoveItem(itemToDelete.Key, itemToDelete.Value);
                }
            }

            character.Inventory.SubKamas((ulong)KamasParameter);
            character.ChangeAlignementSide((AlignmentSideEnum)Side);

            return true;
        }
    }
}
