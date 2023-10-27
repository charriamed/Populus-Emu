using Stump.DofusProtocol.Enums;
using Stump.Server.BaseServer.Database;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Characters;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Npcs;
using Stump.Server.WorldServer.Game.Dialogs.Guilds;
using Stump.Server.WorldServer.Handlers.Guilds;

namespace Stump.Server.WorldServer.Database.Npcs.Replies
{
    [Discriminator("GuildCreation", typeof(NpcReply), typeof(NpcReplyRecord))]
    class GuildCreate : NpcReply
    {
        public GuildCreate(NpcReplyRecord record) : base(record)
        {
        }
        public override bool Execute(Npc npc, Character character)
        {
            if (character.Guild != null)
            {
                character.SendServerMessage("Vous devez quitter votre guilde avant d'en créer une nouvelle.", System.Drawing.Color.DarkOrange);
                return false;
            }

            try
            {
                GuildCreationPanel panel = new GuildCreationPanel(character);
                panel.Open();
                character.SendServerMessage("Vous avez perdu une guildalogemme.", System.Drawing.Color.DarkOrange);
                return true;
            }
            catch{ return false; }
        }
    }
}