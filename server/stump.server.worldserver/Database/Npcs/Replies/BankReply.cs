using System;
using Stump.Server.BaseServer.Database;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Characters;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Npcs;
using Stump.Server.WorldServer.Game.Exchanges.Bank;

namespace Stump.Server.WorldServer.Database.Npcs.Replies
{
    [Discriminator("Bank", typeof(NpcReply), new Type[] { typeof(NpcReplyRecord) })]
    public class BankReply : NpcReply
    {
        public BankReply(NpcReplyRecord record)
          : base(record)
        {
        }

        public override bool Execute(Npc npc, Character character)
        {
            new BankDialog(character).Open();
            return true;
        }
    }
}
