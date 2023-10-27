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

namespace Stump.Server.WorldServer.Database.Npcs.Replies
{
    [Discriminator("AddKamas", typeof(NpcReply), new Type[] { typeof(NpcReplyRecord) })]
    public class AddKamas : NpcReply
    {
        public AddKamas(NpcReplyRecord record)
                : base(record)
        {
        }

        public int Amount
        {
            get
            {
                return this.Record.GetParameter<int>(0U, false);
            }
            set
            {
                this.Record.SetParameter<int>(0U, value);
            }
        }

        public ulong MinAmount
        {
            get
            {
                return this.Record.GetParameter<ulong>(1U, false);
            }
            set
            {
                this.Record.SetParameter<ulong>(1U, value);
            }
        }


        public override bool Execute(Npc npc, Character character)
        {
            bool flag;
            if (!base.Execute(npc, character))
            {
                flag = false;
            }
            else if (character.Inventory.Kamas > MinAmount || character.Bank.Kamas > MinAmount)
            {
                character.SendServerMessage("Vous avez déjà assez de kamas !", System.Drawing.Color.DarkOrange);
                flag = false;
            }
            else
            {
                character.Inventory.AddKamas((ulong)Amount);
                character.SendServerMessage("Vous avez reçu " + Amount + " kamas.", System.Drawing.Color.DarkOrange);
                flag = true;
            }
            return flag;
        }
    }
}

