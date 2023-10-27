using Stump.Core.Reflection;
using Stump.DofusProtocol.Enums;
using Stump.DofusProtocol.Messages;
using Stump.Server.BaseServer.Database;
using Stump.Server.WorldServer.Database.Items.Templates;
using Stump.Server.WorldServer.Database.Npcs;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Characters;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Npcs;
using Stump.Server.WorldServer.Game.Items;
using Stump.Server.WorldServer.Game.Items.Player;
using System;

namespace Stump.Server.WorldServer.Database.Npcs.Replies.StartQuest
{
    [Discriminator("Cinematic", typeof(NpcReply), new Type[] { typeof(NpcReplyRecord) })]
    public class CinematicReply : NpcReply
    {
     
        public short CinematicId
        {
            get
            {
                return this.Record.GetParameter<short>(0U, false);
            }
            set
            {
                this.Record.SetParameter<short>(0U, value);
            }
        }

        public CinematicReply(NpcReplyRecord record)
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
                character.Client.Send(new CinematicMessage((ushort)CinematicId));
                flag = true;
            }
            return flag;
        }
    }
}
