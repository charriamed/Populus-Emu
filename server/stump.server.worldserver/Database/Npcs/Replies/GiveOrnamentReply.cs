using Stump.Server.BaseServer.Database;
using Stump.Server.WorldServer.Database.Npcs;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Characters;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Npcs;
using System;

namespace Stump.Server.WorldServer.Database.Npcs.Replies
{
    [Discriminator("GiveOrnament", typeof(NpcReply), new Type[] { typeof(NpcReplyRecord) })]
    public class GiveOrnamentReply : NpcReply
    {
        public GiveOrnamentReply(NpcReplyRecord record)
            : base(record)
        {
        }
		public short ornamentid
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
                character.AddOrnament((short)ornamentid);
                result = true;
            }
            return result;
        }
    }
}
