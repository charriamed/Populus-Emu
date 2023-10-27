using Stump.Core.Reflection;
using Stump.DofusProtocol.Enums;
using Stump.Server.BaseServer.Database;
using Stump.Server.WorldServer.Database.Items.Templates;
using Stump.Server.WorldServer.Database.Npcs;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Characters;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Npcs;
using Stump.Server.WorldServer.Game.Items;
using Stump.Server.WorldServer.Game.Items.Player;
using System;
using System.Drawing;
using System.Linq;

namespace Stump.Server.WorldServer.Database.Npcs.Replies
{
    [Discriminator("KamasToOgrines", typeof(NpcReply), new Type[] { typeof(NpcReplyRecord) })]
    public class KamasToOgrines : NpcReply
    {
        public KamasToOgrines(NpcReplyRecord record)
            : base(record)
        {
        }

        public ulong AmountKamas
        {
            get
            {
                return this.Record.GetParameter<ulong>(0U, false);
            }
            set
            {
                this.Record.SetParameter<ulong>(0U, value);
            }
        }
        
        public uint AmountOgrines
        {
            get
            {
                return this.Record.GetParameter<uint>(1U, false);
            }
            set
            {
                this.Record.SetParameter<uint>(1U, value);
            }
        }

        public override bool Execute(Npc npc, Character character)
        {
            bool flag;
            if (!base.Execute(npc, character))
            {
                flag = false;
            }
            else
            {
                if(character.Inventory.Kamas < AmountKamas) {
                    character.SendInformationMessage(TextInformationTypeEnum.TEXT_INFORMATION_ERROR, 82);
                    flag = false;
                }

                else
                {
                    var item = ItemManager.Instance.CreatePlayerItem(character, (int)ItemIdEnum.PIECE_DE_KAMA_GEANTE_12124, (int)this.AmountOgrines);
                    character.Inventory.AddItem(item);
                    character.Inventory.SubKamas(AmountKamas);
                    character.SendServerMessage("Félicitations, vous avez reçu " + AmountOgrines + " Erosinos.", Color.DarkOrange); 
                    flag = true;
                }
            }
            return flag;
        }
    }
}
