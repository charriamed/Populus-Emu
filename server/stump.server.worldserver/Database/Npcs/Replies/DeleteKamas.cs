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
    [Discriminator("DeleteKamas", typeof(NpcReply), new Type[] { typeof(NpcReplyRecord) })]
    public class DeleteKamas : NpcReply
    {
       

        public ulong KamasQuantitie
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


        public DeleteKamas(NpcReplyRecord record)
            : base(record)
        {
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
                character.Inventory.SubKamas(this.KamasQuantitie);

                flag = true;
            }
            character.OpenPopup($"Tu viens de perdre {KamasQuantitie} kamas.");
            return flag;
        }
    }
}
