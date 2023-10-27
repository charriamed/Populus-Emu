using Stump.Server.BaseServer.Database;
using Stump.Server.WorldServer.Database.Npcs;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Characters;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Npcs;
using System;

namespace Stump.Server.WorldServer.Database.Npcs.Replies
{
    [Discriminator("GiveTitle", typeof(NpcReply), new Type[] { typeof(NpcReplyRecord) })]
    public class GiveTitleReply : NpcReply
    {
        public GiveTitleReply(NpcReplyRecord record)
            : base(record)
        {
        }
        public short titleid
        {
            get
            {
                return base.Record.GetParameter<short>(0u, false);
            }
            set
            {
                base.Record.SetParameter<short>(0u, value);
            }
        }

        public override bool Execute(Npc npc, Character character)
        {
            bool result;
            if (!base.Execute(npc, character))
            {
                result = false;
            }
            else
            {
                character.AddTitle((short)titleid);
                result = true;
            }
            return result;
        }
    }
}
