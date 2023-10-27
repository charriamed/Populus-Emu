
using Stump.Core.Attributes;
using Stump.DofusProtocol.Enums;
using Stump.DofusProtocol.Messages;
using Stump.DofusProtocol.Types;
using Stump.Server.BaseServer.Database;
using Stump.Server.WorldServer.Core.Network;
using Stump.Server.WorldServer.Database.Npcs;
using Stump.Server.WorldServer.Database.Npcs.Replies;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Characters;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Npcs;
using Stump.Server.WorldServer.Game.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Npcs.Replies
{
    [Discriminator("PrestigeHonor", typeof(NpcReply), new System.Type[]
 {
  typeof(NpcReplyRecord)
 })]
    public class PrestigeGradeReply : NpcReply
    {
        public PrestigeGradeReply(NpcReplyRecord record)
            : base(record)
        {
        }

        public override bool Execute(Npc npc, Character character)
        {

            if (character.Record.Honor < 17500)
            {
                character.OpenPopup("Vous devez être Grade 10");
            }
            else  if (character.Record.AlignmentSide == AlignmentSideEnum.ALIGNMENT_EVIL)
            {
                character.Record.AlignmentSide = AlignmentSideEnum.ALIGNMENT_ANGEL;
                character.Record.Honor = 0;
                character.Record.PrestigeHonor = 1;
                /* var jeton = ItemManager.Instance.TryGetTemplate(1920);
                 var token = character.Inventory.TryGetItem(jeton);
                 character.Inventory.AddItem(jeton, 1); */
                character.AddOrnament(62);
                character.OpenPopup("Félicitation vous êtes passé Prestige ange.");
            }
            else if (character.Record.AlignmentSide == AlignmentSideEnum.ALIGNMENT_ANGEL)
            {
                character.Record.AlignmentSide = AlignmentSideEnum.ALIGNMENT_EVIL;                
                character.Record.Honor = 0;
                character.Record.PrestigeHonor = 2;
                /*  var badge = ItemManager.Instance.TryGetTemplate(2308);
                  var token = character.Inventory.TryGetItem(badge);
                  character.Inventory.AddItem(badge, 1); */
                character.AddOrnament(77);
                character.OpenPopup("Félicitation vous êtes passé Prestige démon.");
            }           
            return true;
        }
    }
}



