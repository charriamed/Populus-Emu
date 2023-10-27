using Stump.Server.BaseServer.Database;
using Stump.Server.WorldServer.Database.Ranks;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Characters;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Npcs;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Ranks;
using System.Collections.Generic;
using System.Drawing;
using System;
using Stump.Server.WorldServer.Database.Items.Templates;
using Stump.Server.WorldServer.Game.Items;

namespace Stump.Server.WorldServer.Database.Npcs.Replies
{
    [Discriminator("RankRewardDialog", typeof(NpcReply), typeof(NpcReplyRecord))]
    class RankRewardReply : NpcReply
    {
        public RankRewardReply(NpcReplyRecord record) : base(record)
        {

        }

        public override bool Execute(Npc npc, Character character)
        {
            if (!base.Execute(npc, character))
                return false;
            var rewards = RankRewardManager.Instance.getRewardsByRank(character.CharacterRankId);
            if (rewards.Count > 0)
            {
                if (character.CharacterRankWin < 10)
                {
                    character.SendServerMessage("Il te faut un minimum de victoire pour réclamer une récompense.. Repasse me voir après avoir vaincu dix infidèles dans le battlefield.", Color.OrangeRed);
                    return false;
                }
                var now = DateTime.Now;
                if ((character.CharacterRankReward.Month < now.Month || character.CharacterRankReward.Year < now.Year) || character.CharacterRankReward.Day < now.Day)
                {
                    Random rnd = new Random();
                    var selected = rewards[rnd.Next(rewards.Count)].Value;
                    switch (selected.Type)
                    {
                        case "Item":
                            int quantity = 1;

                            if (selected.Optional2 != null && selected.Optional2.Length > 0)
                                quantity = Int32.Parse(selected.Optional2);
                            ItemTemplate template = ItemManager.Instance.TryGetTemplate(Int32.Parse(selected.Optional1));
                            var item = ItemManager.Instance.CreatePlayerItem(character, template, quantity, true);

                            character.Inventory.AddItem(item, true);
                            character.SendServerMessage("Tu as obtenu l'objet: <b>" + template.Name + " (X" + quantity +")</b>, félicitations pour ta bravoure en DeadMatch ! Repasse me voir demain.");
                            break;

                        case "Kamas":
                                character.Inventory.AddKamas((ulong)Int32.Parse(selected.Optional1));
                            character.SendServerMessage("Tu as obtenu: <b>" + Int32.Parse(selected.Optional1) + "</b> kamas, félicitations pour ta bravoure en DeadMatch ! Repasse me voir demain.");
                            break;

                        case "Exp":
                                character.AddExperience(Int32.Parse(selected.Optional1));
                            character.SendServerMessage("Tu as obtenu: <b>" + Int32.Parse(selected.Optional1) + "</b> points d'expérience, félicitations pour ta bravoure en DeadMatch! Repasse me voir demain.");
                            break;
                    }
                    character.CharacterRankReward = DateTime.Now;
                }
                else
                {
                    character.SendServerMessage("Tu as déjà reçu une récompense aujourd'hui... repasse me voir demain.", Color.OrangeRed);
                }
            }
            else
            {
                character.SendServerMessage("Désolé, il n'y a aucune récompense pour ton grade pour le moment.", Color.OrangeRed);
            }

            character.LeaveDialog();
            return true;
        }
    }
}

