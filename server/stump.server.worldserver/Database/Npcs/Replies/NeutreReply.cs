using Stump.DofusProtocol.Enums;
using Stump.Server.BaseServer.Database;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Characters;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Npcs;
using System;

namespace Stump.Server.WorldServer.Database.Npcs.Replies
{
    [Discriminator("Neutre", typeof(NpcReply), new Type[] { typeof(NpcReplyRecord) })]
    public class NeutreReply : NpcReply
    {
        public NeutreReply(NpcReplyRecord record) : base(record)
        {
        }

        public override bool Execute(Npc npc, Character character)
        {
            character.ChangeAlignementSide(AlignmentSideEnum.ALIGNMENT_NEUTRAL);
            return true;
        }
    }
}
