using Stump.Server.BaseServer.Database;
using System;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Characters;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Npcs;
using Stump.DofusProtocol.Enums;

namespace Stump.Server.WorldServer.Database.Npcs.Replies.AlignReplies
{
    [Discriminator("Demon", typeof(NpcReply), new Type[] { typeof(NpcReplyRecord) })]
    public class DemonReply : NpcReply
    {
        public DemonReply(NpcReplyRecord record) : base(record)
        {
        }

        public override bool Execute(Npc npc, Character character)
        {
            character.ChangeAlignementSide(AlignmentSideEnum.ALIGNMENT_EVIL);
            return true;
        }
    }
}
