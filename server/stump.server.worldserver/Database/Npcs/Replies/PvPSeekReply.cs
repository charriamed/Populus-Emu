using Stump.Core.Collections;
using Stump.Core.Extensions;
using Stump.Core.Reflection;
using Stump.DofusProtocol.Enums;
using Stump.Server.BaseServer.Database;
using Stump.Server.WorldServer.Database.Arena;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Characters;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Npcs;
using Stump.Server.WorldServer.Game.Arena;
using Stump.Server.WorldServer.Game.Effects.Instances;
using Stump.Server.WorldServer.Game.Fights;
using Stump.Server.WorldServer.Game.Items;
using Stump.Server.WorldServer.Game.Maps;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;

namespace Stump.Server.WorldServer.Database.Npcs.Replies
{
    [Discriminator("PvPSeek", typeof(NpcReply), typeof(NpcReplyRecord))]
    public class PvPSeekReply : NpcReply
    {
        private TimedStack<Pair<int, int>> m_pvpSeekHistory = new TimedStack<Pair<int, int>>(60 * 5);
        private Timer BattlefieldTimer;
        private int retryBattleField;

        public PvPSeekReply(NpcReplyRecord record)
            : base(record)
        {
        }

        public void startBattleField(Character first, Character second)
        {
            first.SendServerMessage("Un adversaire a été trouvé: <b>" + second.Name + "</b> du rang <b>" + second.GetCharacterRankName() + " (" + second.CharacterRankExp + " CP)" + "</b>, lancement du combat..", Color.Chartreuse);
            second.SendServerMessage("Un adversaire vous défi en mode DeadMatch! <b>" + first.Name + "</b> du rang <b>" + first.GetCharacterRankName() + " (" + first.CharacterRankExp + " CP)" + "</b>, lancement du combat..", Color.Chartreuse);

            first.updateBattleFieldPosition();
            second.updateBattleFieldPosition();

            /* Save map/cell before fight was started */

            var random = ArenaManager.Instance.Arenas_1vs1.RandomElementOrDefault();
            Map m_pvpmap = null;
            if (random.Value != null && random.Value.Map != null)
                m_pvpmap = random.Value.Map;
            if (m_pvpmap != null)
            {
                var preFight = FightManager.Instance.CreateAgressionFight(m_pvpmap, second.AlignmentSide, first.AlignmentSide, true);
                try
                {
                    second.Area.AddMessage(() =>
                    {
                        second.LeaveDialog();
                        lock (preFight.m_playersMaps)
                            preFight.m_playersMaps.Add(second, second.Map);
                        second.Teleport(m_pvpmap, second.Cell);

                        m_pvpmap.Area.ExecuteInContext(() =>
                        {
                            preFight.ChallengersTeam.AddFighter(second.CreateFighter(preFight.ChallengersTeam));
                        });
                    });

                    first.Area.AddMessage(() =>
                    {
                        first.LeaveDialog();
                        lock (preFight.m_playersMaps)
                            preFight.m_playersMaps.Add(first, first.Map);
                        first.Teleport(m_pvpmap, first.Cell);

                        m_pvpmap.Area.ExecuteInContext(() =>
                        {
                            preFight.DefendersTeam.AddFighter(first.CreateFighter(preFight.DefendersTeam));
                            preFight.StartPlacement();
                        });
                    });
                }
                catch (Exception ex)
                {
                    preFight.EndFight();
                    second.Teleport(Stump.Server.WorldServer.Game.World.Instance.GetMap(100270593), second.Cell);
                    first.Teleport(Stump.Server.WorldServer.Game.World.Instance.GetMap(100270593), first.Cell);
                }
            }
            else
            {
                first.SendServerMessage("Le combat DeadMatch a été annulé.", Color.DarkOrange);
                second.SendServerMessage("Le combat DeadMatch a été annulé.", Color.DarkOrange);
            }
        }

        private static void OnQueueBattleField(Character character, PvPSeekReply obj)
        {
            if (character.IsInFight())
                return;
            Character target = null;

            target = Game.World.Instance.GetCharacters(x => character.CanBattlefield(x) == true).RandomElementOrDefault();
            if (target != null && target.Account.Nickname == character.CharacterToSeekName && !character.IsGameMaster())
                target = null;
            obj.retryBattleField++;
            character.SendServerMessage("Recherche en cours d'adversaire... (" + obj.retryBattleField + " essai)", Color.Aqua);

            if (target != null && !character.IsBusy())
            {
                character.CharacterToSeekName = target.Account.Nickname;
                obj.startBattleField(character, target);
                return;
            }
            else if (target != null)
            {
                character.SendServerMessage("Un adversaire a été trouvé pour le DeadMatch, mais vous étiez occupé, le combat a été annulé.", Color.Red);
                return;
            }
            if (obj.retryBattleField == 6)
            {
                character.SendServerMessage("Personne n'est actuellement disponible pour le DeadMatch, veuillez réessayer plus tard...", Color.DarkOrange);
                return;
            }
            obj.BattlefieldTimer = new Timer(_ => OnQueueBattleField(character, obj), null, 1000 * 10, Timeout.Infinite);
        }

        public override bool Execute(Npc npc, Character character)
        {
            if (!base.Execute(npc, character))
                return false;

            m_pvpSeekHistory.Clean();
            if (m_pvpSeekHistory.Any(x => x.First.First == character.Id))
            {
                character.SendServerMessage("Vous devez attendre 5 minutes entre chaque combat.", Color.DarkOrange);
                return false;
            }

            if (character.AgressionPenality >= DateTime.Now)
            {
                character.SendServerMessage("Vous devez attendre de ne plus être banni pour chercher un combat sur le DeadMatch.", Color.DarkOrange);
                return false;
            }
            
                if (!character.battleFieldOn)
                character.battleFieldOn = true;
            this.retryBattleField = 0;
            character.SendServerMessage("Recherche d'adversaire pour le DeadMatch en cours... (60 secondes)", Color.Aqua);
            this.BattlefieldTimer = new Timer(_ => OnQueueBattleField(character, this), null, 1000 * 10, Timeout.Infinite);

            /*
             * OLDER SYSTEM WITH CONTRACT
             * 
             */

            /*foreach (var contract in character.Inventory.GetItems(x => x.Template.Id == (int)ItemIdEnum.ORDRE_DEXECUTION_10085))
                character.Inventory.RemoveItem(contract);
            var item = ItemManager.Instance.CreatePlayerItem(character, (int)ItemIdEnum.ORDRE_DEXECUTION_10085, 25);
            var seekEffect = item.Effects.FirstOrDefault(x => x.EffectId == EffectsEnum.Effect_Seek);
            if (seekEffect != null)
                item.Effects.Remove(seekEffect);
            item.Effects.Add(new EffectString(EffectsEnum.Effect_Seek, target.Name));
            item.Effects.Add(new EffectInteger(EffectsEnum.Effect_Alignment, (short)target.AlignmentSide));
            item.Effects.Add(new EffectInteger(EffectsEnum.Effect_Grade, target.AlignmentGrade));
            item.Effects.Add(new EffectInteger(EffectsEnum.Effect_Level, target.Level));
            item.Effects.Add(new EffectInteger(EffectsEnum.Effect_NonExchangeable_981, 0));
            character.Inventory.AddItem(item);
            character.CharacterToSeekName = target.Account.Nickname;
            m_pvpSeekHistory.Push(new Pair<int, int>(character.Id, target.Id));
            character.SendServerMessage($"Você encontrou o jogador {target.Name} como alvo, e recebeu 25 pergaminhos de busca.", Color.DarkOrange);
            */

            return true;
        }
    }
}