using Stump.Server.BaseServer.Database;
using Stump.Server.WorldServer.Database.Npcs;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Characters;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Npcs;
using System;

namespace Stump.Server.WorldServer.Database.Npcs.Replies
{
    [Discriminator("AddSpell", typeof(NpcReply), new Type[] { typeof(NpcReplyRecord) })]
    public class AddSpellReply : NpcReply
    {
        public AddSpellReply(NpcReplyRecord record)
            : base(record)
        {
        }
        public short spellid
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
            { if (character.Spells.HasSpell(spellid))
                {
                    character.SendServerMessage("Vous possèdez déja ce sort");
                }

                else
                    character.Spells.LearnSpell(spellid);
                
                result = true;
            }
            return result;
        }
    }
}
