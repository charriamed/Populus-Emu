using System.Collections.Generic;
using System.Linq;
using Stump.Core.Reflection;
using Stump.DofusProtocol.Enums;
using Stump.DofusProtocol.Messages;
using Stump.DofusProtocol.Types;
using Stump.Server.BaseServer.Network;
using Stump.Server.WorldServer.Core.Network;
using Stump.Server.WorldServer.Database.World;
using Stump.Server.WorldServer.Game.Actors.Fight;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Characters;
using Stump.Server.WorldServer.Game.Fights;
using Stump.Server.WorldServer.Game.Fights.Buffs;
using Stump.Server.WorldServer.Game.Fights.Teams;
using Stump.Server.WorldServer.Game.Fights.Triggers;
using Stump.Server.WorldServer.Handlers.Context.RolePlay;
using Spell = Stump.Server.WorldServer.Game.Spells.Spell;
using Stump.Core.Extensions;
using Stump.Server.WorldServer.Game.Arena;
using Stump.Server.WorldServer.Game;
using Stump.Core.Threading;
using System.Threading.Tasks;

namespace Stump.Server.WorldServer.Handlers.Context
{
    public partial class ContextHandler
    {

        [WorldHandler(GameActionFightCastRequestMessage.Id)]
        public static void HandleGameActionFightCastRequestMessage(WorldClient client, GameActionFightCastRequestMessage message)
        {
            if (!client.Character.IsFighting())
                return;

            var fighter = client.Character.Fighter;
            if (client.Character.Fight.FighterPlaying is CompanionActor)
            {
                var spell = client.Character.Companion.GetSpell((int)message.SpellId);
                if (spell != null)
                {

                    client.Character.Companion.CastSpell(spell, client.Character.Fight.Map.Cells[message.CellId]);
                }
            }
            else
            {
                var spell = fighter.IsSlaveTurn() ?
                fighter.GetSlave().GetSpell(message.SpellId) : fighter.GetSpell(message.SpellId);

                if (spell == null)
                    return;

                if (fighter.IsSlaveTurn())
                    fighter.GetSlave().CastSpell(spell, client.Character.Fight.Map.Cells[message.CellId]);
                else
                    fighter.CastSpell(spell, client.Character.Fight.Map.Cells[message.CellId]);
            }
        }

        [WorldHandler(GameActionFightCastOnTargetRequestMessage.Id)]
        public static void HandleGameActionFightCastOnTargetRequestMessage(WorldClient client, GameActionFightCastOnTargetRequestMessage message)
        {
            if (!client.Character.IsFighting())
                return;

            var fighter = client.Character.Fighter;

            var spell = fighter.IsSlaveTurn() ?
                fighter.GetSlave().GetSpell(message.SpellId) : fighter.GetSpell(message.SpellId);

            if (spell == null)
                return;

            var target = client.Character.Fight.GetOneFighter((int)message.TargetId);

            if (target == null)
                return;
            if (client.Character.Fight.FighterPlaying is CompanionActor)
            {
                client.Character.Companion.CastSpell(spell, target.Cell);
            }
            else
            {
                if (target.GetVisibleStateFor(fighter) == GameActionFightInvisibilityStateEnum.INVISIBLE)
                {
                    //Impossible de lancer ce sort : la cellule vis�e n'est pas valide !
                    client.Character.SendInformationMessage(TextInformationTypeEnum.TEXT_INFORMATION_ERROR, 193);
                    return;
                }

                if (fighter.IsSlaveTurn())
                    fighter.GetSlave().CastSpell(spell, target.Cell);
                else
                    fighter.CastSpell(spell, target.Cell);
            }
        }


        [WorldHandler(GameFightTurnFinishMessage.Id)]
        public static void HandleGameFightTurnFinishMessage(WorldClient client, GameFightTurnFinishMessage message)
        {
            if (!client.Character.IsFighting())
                return;
            if (client.Character.Fight.FighterPlaying is CompanionActor)
            {
                client.Character.Companion.PassTurn();
                return;
            }
            if (client.Character.Fighter.IsSlaveTurn())
            {
                client.Character.Fighter.GetSlave().PassTurn();
                return;
            }
            if (client.Character.Fighter.IsFighterTurn())
            {
                client.Character.Fighter.PassTurn();
                return;
            }
            client.Character.Fighter.PassTurn();
        }

        [WorldHandler(GameFightTurnReadyMessage.Id)]
        public static void HandleGameFightTurnReadyMessage(WorldClient client, GameFightTurnReadyMessage message)
        {
            if (!client.Character.IsFighting())
                return;

            client.Character.Fighter.ToggleTurnReady(message.IsReady);
        }

        [WorldHandler(GameFightReadyMessage.Id)]
        public static void HandleGameFightReadyMessage(WorldClient client, GameFightReadyMessage message)
        {
            if (!client.Character.IsFighting())
                return;

            if (client.Character.Fight is FightPvT)
                return;

            client.Character.Fighter.ToggleReady(message.IsReady);
        }

        [WorldHandler(GameContextQuitMessage.Id)]
        public static void HandleGameContextQuitMessage(WorldClient client, GameContextQuitMessage message)
        {
            if (client.Character.IsFighting())
            {
                client.Character.Fighter.Character?.Companion?.LeaveFight();
                client.Character.Fighter.LeaveFight();
            }

            else if (client.Character.IsSpectator())
                client.Character.Spectator.Leave();
        }

        [WorldHandler(GameFightPlacementPositionRequestMessage.Id)]
        public static void HandleGameFightPlacementPositionRequestMessage(WorldClient client, GameFightPlacementPositionRequestMessage message)
        {
            if (!client.Character.IsFighting())
                return;

            if (client.Character.Fighter.Position.Cell.Id != message.CellId)
                client.Character.Fighter.ChangePrePlacement(client.Character.Fight.Map.Cells[message.CellId]);
        }

        [WorldHandler(GameRolePlayPlayerFightRequestMessage.Id)]
        public static void HandleGameRolePlayPlayerFightRequestMessage(WorldClient client, GameRolePlayPlayerFightRequestMessage message)
        {
            var target = client.Character.Map.GetActor<Character>((int)message.TargetId);

            if (target == null)
                return;

            if (message.Friendly)
            {
                var reason = client.Character.CanRequestFight(target);
                if (reason != FighterRefusedReasonEnum.FIGHTER_ACCEPTED)
                {
                    SendChallengeFightJoinRefusedMessage(client, client.Character, reason);
                }
                else
                {
                    var fightRequest = new FightRequest(client.Character, target);
                    fightRequest.Open();
                }
            }
            else // agression
            {
                var reason = client.Character.CanAgress(target);
                if (reason != FighterRefusedReasonEnum.FIGHTER_ACCEPTED)
                {
                    SendChallengeFightJoinRefusedMessage(client, client.Character, reason);
                }
                else
                {

                    foreach (var character in target.Map.GetAllCharacters().Where(x => x != target && x != client.Character))
                        ContextRoleplayHandler.SendGameRolePlayAggressionMessage(character.Client, client.Character, target);
                    //<b>%1</b> agresse <b>%2</b>

                    var fight = FightManager.Instance.CreateAgressionFight(target.Map,
                        client.Character.AlignmentSide, target.AlignmentSide);

                    fight.ChallengersTeam.AddFighter(client.Character.CreateFighter(fight.ChallengersTeam));
                    fight.DefendersTeam.AddFighter(target.CreateFighter(fight.DefendersTeam));

                    fight.StartPlacement();
                }
            }
        }

        [WorldHandler(GameRolePlayPlayerFightFriendlyAnswerMessage.Id)]
        public static void HandleGameRolePlayPlayerFightFriendlyAnswerMessage(WorldClient client, GameRolePlayPlayerFightFriendlyAnswerMessage message)
        {
            if (!client.Character.IsInRequest() || !(client.Character.RequestBox is FightRequest))
                return;

            if (message.Accept)
                client.Character.RequestBox.Accept();
            else if (client.Character == client.Character.RequestBox.Target)
                client.Character.RequestBox.Deny();
            else
                client.Character.RequestBox.Cancel();
        }

        [WorldHandler(GameFightOptionToggleMessage.Id)]
        public static void HandleGameFightOptionToggleMessage(WorldClient client, GameFightOptionToggleMessage message)
        {
            if (message.Option == (sbyte)FightOptionsEnum.FIGHT_OPTION_SET_TO_PARTY_ONLY && client.Character.IsPartyLeader())
                client.Character.Party.TogglePartyFightRestriction();

            if (!client.Character.IsFighting())
                return;

            if (!client.Character.Fighter.IsTeamLeader())
                return;

            if (!client.Character.Fight.IsStarted)
                client.Character.Team.ToggleOption((FightOptionsEnum)message.Option);

            if (message.Option == (sbyte)FightOptionsEnum.FIGHT_OPTION_SET_SECRET)
                client.Character.Fight.ToggleSpectatorClosed(client.Character, !client.Character.Fight.SpectatorClosed);
        }

        [WorldHandler(GameFightSpectatePlayerRequestMessage.Id)]
        public static void HandleGameFightSpectatePlayerRequestMessage(WorldClient client, GameFightSpectatePlayerRequestMessage message)
        {
            if (client.Character.IsFighting())
                return;

            var player = World.Instance.GetCharacter((int)message.PlayerId);

            if (player == null)
                return;

            if (!client.Character.FriendsBook.IsFriend(player.Account.Id) && !client.Character.Guild.Members.Any(x => x.Character?.Id == player.Id))
                return;

            if (!player.IsFighting())
            {
                SendChallengeFightJoinRefusedMessage(client, client.Character, FighterRefusedReasonEnum.TOO_LATE);
                return;
            }

            var fight = FightManager.Instance.GetFight(player.Fight.Id);

            if (fight == null)
            {
                SendChallengeFightJoinRefusedMessage(client, client.Character, FighterRefusedReasonEnum.TOO_LATE);
                return;
            }

            if (!fight.IsStarted)
                return;

            if (!fight.CanSpectatorJoin(client.Character) || client.Character.IsInFight())
                return;

            ContextRoleplayHandler.SendCurrentMapMessage(client, fight.Map.Id);
            fight.AddSpectator(client.Character.CreateSpectator(fight));
        }

        [WorldHandler(GameFightJoinRequestMessage.Id)]
        public static void HandleGameFightJoinRequestMessage(WorldClient client, GameFightJoinRequestMessage message)
        {
            if (client.Character.IsFighting())
                return;

            var fight = FightManager.Instance.GetFight(message.FightId);

            if (fight == null)
            {
                SendChallengeFightJoinRefusedMessage(client, client.Character, FighterRefusedReasonEnum.TOO_LATE);
                return;
            }

            if (client.Character.Map.Id != ArenaManager.KolizeumMapId && fight.Map != client.Character.Map)
            {
                SendChallengeFightJoinRefusedMessage(client, client.Character, FighterRefusedReasonEnum.WRONG_MAP);
                return;
            }

            if (message.FighterId == 0 && fight.CanSpectatorJoin(client.Character) && !client.Character.IsInFight())
            {
                if (client.Character.Map.Id == ArenaManager.KolizeumMapId)
                    ContextRoleplayHandler.SendCurrentMapMessage(client, fight.Map.Id);

                fight.AddSpectator(client.Character.CreateSpectator(fight));
            }

            if (fight.IsStarted)
                return;


            FightTeam team;
            if (fight.ChallengersTeam.Leader.Id == message.FighterId)
            {
                team = fight.ChallengersTeam;
                if (fight is FightAgression)
                {
                    CharacterFighter fighterCannotBeAgressed = (CharacterFighter)fight.DefendersTeam.Fighters.Where(x => x is CharacterFighter).FirstOrDefault(x => client.Character.CanAgress((x as CharacterFighter).Character) != FighterRefusedReasonEnum.FIGHTER_ACCEPTED);
                    if (fighterCannotBeAgressed != null)
                    {
                        SendChallengeFightJoinRefusedMessage(client, client.Character, client.Character.CanAgress(fighterCannotBeAgressed.Character));
                        return;
                    }
                }
            }
            else if (fight.DefendersTeam.Leader.Id == message.FighterId)
            {
                /* if (fight.DefendersTeam.Leader.Id == message.fighterId)
                 {
                     SendChallengeFightJoinRefusedMessage(client, client.Character,
                         FighterRefusedReasonEnum.WRONG_MAP);
                     return;
                 }*/
                team = fight.DefendersTeam;
                if (fight is FightAgression)
                {
                    CharacterFighter fighterCannotBeAgressed = (CharacterFighter)fight.ChallengersTeam.Fighters.Where(x => x is CharacterFighter).FirstOrDefault(x => client.Character.CanAgress((x as CharacterFighter).Character) != FighterRefusedReasonEnum.FIGHTER_ACCEPTED);
                    if (fighterCannotBeAgressed != null)
                    {
                        SendChallengeFightJoinRefusedMessage(client, client.Character, client.Character.CanAgress(fighterCannotBeAgressed.Character));
                        return;
                    }
                }
            }
            else
            {
                SendChallengeFightJoinRefusedMessage(client, client.Character, FighterRefusedReasonEnum.WRONG_MAP);
                return;
            }

            FighterRefusedReasonEnum error;
            if ((error = team.CanJoin(client.Character)) != FighterRefusedReasonEnum.FIGHTER_ACCEPTED)
                SendChallengeFightJoinRefusedMessage(client, client.Character, error);
            else
                team.AddFighter(client.Character.CreateFighter(team));
        }

        [WorldHandler(GameContextKickMessage.Id)]
        public static void HandleGameContextKickMessage(WorldClient client, GameContextKickMessage message)
        {
            if (!client.Character.IsFighting() ||
                !client.Character.Fighter.IsTeamLeader())
                return;

            if (!client.Character.Fight.CanKickPlayer)
                return;

            var target = client.Character.Fight.GetOneFighter<CharacterFighter>((int)message.TargetId);

            if (target == null)
                return;

            if (!target.Character.IsFighting())
                return;

            if (client.Character.Fight != target.Character.Fight)
                return;

            client.Character.Fight.KickFighter(client.Character.Fighter, target);
        }

        public static void SendGameFightStartMessage(IPacketReceiver client, IEnumerable<Idol> idols)
        {
            client.Send(new GameFightStartMessage(idols.ToArray()));
        }

        public static void SendGameFightStartingMessage(IPacketReceiver client, FightTypeEnum
             fightTypeEnum, int attackerId, int defenderId, IFight figth)
        {
            client.Send(new GameFightStartingMessage((sbyte)fightTypeEnum, (ushort)figth.Id,
                attackerId,
                defenderId));
        }

        public static void SendGameRolePlayShowChallengeMessage(IPacketReceiver client, IFight fight)
        {
            client.Send(new GameRolePlayShowChallengeMessage(fight.GetFightCommonInformations()));
        }

        public static void SendGameRolePlayRemoveChallengeMessage(IPacketReceiver client, IFight fight)
        {
            client.Send(new GameRolePlayRemoveChallengeMessage((ushort)fight.Id));
        }

        public static void SendGameFightEndMessage(IPacketReceiver client, IFight fight)
        {
            client.Send(new GameFightEndMessage((int)fight.GetFightDuration().TotalMilliseconds, fight.AgeBonus, 0, new FightResultListEntry[0], fight.GetPartiesNameWithOutcome().ToArray()));
        }

        public static void SendGameFightEndMessage(IPacketReceiver client, IFight fight, IEnumerable<FightResultListEntry> results)
        {
            client.Send(new GameFightEndMessage((int)fight.GetFightDuration().TotalMilliseconds, fight.AgeBonus, 0, results.ToArray(), fight.GetPartiesNameWithOutcome().ToArray()));
        }

        public static void SendGameFightJoinMessage(IPacketReceiver client, bool canBeCancelled, bool canSayReady,
            bool isFightStarted, int timeMaxBeforeFightStart, FightTypeEnum fightTypeEnum)
        {
            client.Send(new GameFightJoinMessage(!isFightStarted, canBeCancelled, canSayReady, isFightStarted,
                                                 (short)timeMaxBeforeFightStart, (sbyte)fightTypeEnum));
        }

        public static void SendGameFightSpectatorJoinMessage(IPacketReceiver client, IFight fight)
        {
            client.Send(new GameFightSpectatorJoinMessage(false, false, false, fight.IsStarted,
                fight.IsStarted ? (short)0 : (short)(fight.GetPlacementTimeLeft().TotalMilliseconds / 100), (sbyte)fight.FightType, fight.GetPartiesName().ToArray()));
        }

        public static void SendGameFightSpectateMessage(IPacketReceiver client, IFight fight)
        {
            client.Send(new GameFightSpectateMessage(
                fight.GetBuffs().Select(entry => entry.GetFightDispellableEffectExtendedInformations()).ToArray(),
                fight.GetTriggers().Select(entry => entry.GetHiddenGameActionMark()).ToArray(),
                (ushort)fight.TimeLine.RoundNumber, !fight.IsStarted ? 0 : fight.StartTime.GetUnixTimeStamp(), fight.ActiveIdols.Select(x => x.GetNetworkIdol()).ToArray()));
        }

        public static void SendGameFightTurnResumeMessage(IPacketReceiver client, FightActor fighterPlaying)
        {
            client.Send(new GameFightTurnResumeMessage(fighterPlaying.Id, (uint)fighterPlaying.TurnTime / 100, (uint)fighterPlaying.Fight.GetTurnTimeLeft().TotalMilliseconds / 100));
        }

        public static void SendChallengeFightJoinRefusedMessage(IPacketReceiver client, Character character,
                                                                FighterRefusedReasonEnum reason)
        {
            client.Send(new ChallengeFightJoinRefusedMessage((ulong)character.Id, (sbyte)reason));
        }

        public static void SendGameFightHumanReadyStateMessage(IPacketReceiver client, FightActor fighter)
        {
            client.Send(new GameFightHumanReadyStateMessage((ulong)fighter.Id, fighter.IsReady));
        }


        public static void SendGameFightSynchronizeMessage(WorldClient client, IFight fight)
        {
            client.Send(new GameFightSynchronizeMessage(
                    fight.GetAllFighters().Select(entry => entry.GetGameFightFighterInformations(client)).ToArray()));
        }

        public static void SendFighterStatsListMessage(IPacketReceiver client, Character character)
        {
            client.Send(new FighterStatsListMessage(character.GetCharacterCharacteristicsInformations()));
        }

        public static void SendGameFightNewRoundMessage(IPacketReceiver client, int roundNumber)
        {
            client.Send(new GameFightNewRoundMessage((uint)roundNumber));
        }

        public static void SendGameFightTurnListMessage(IPacketReceiver client, IFight fight)
        {
            client.Send(new GameFightTurnListMessage(fight.GetAliveFightersIds().Select(x => (double)x).ToArray(), fight.GetDeadFightersIds().Select(x => (double)x).ToArray()));
        }

        public static void SendGameFightTurnStartMessage(IPacketReceiver client, int id, int waitTime)
        {
            client.Send(new GameFightTurnStartMessage(id, (uint)waitTime));
        }

        public static void SendGameFightTurnStartPlayingMessage(IPacketReceiver client)
        {
            client.Send(new GameFightTurnStartPlayingMessage());
        }

        public static void SendGameFightTurnFinishMessage(IPacketReceiver client)
        {
            client.Send(new GameFightTurnFinishMessage());
        }

        public static void SendGameFightTurnEndMessage(IPacketReceiver client, FightActor fighter)
        {
            client.Send(new GameFightTurnEndMessage(fighter.Id));
        }

        public static void SendGameFightUpdateTeamMessage(IPacketReceiver client, IFight fight, FightTeam team)
        {
            client.Send(new GameFightUpdateTeamMessage(
                            (ushort)fight.Id,
                            team.GetFightTeamInformations()));
        }

        public static void SendGameFightShowFighterMessage(WorldClient client, FightActor fighter)
        {
            client.Send(new GameFightShowFighterMessage(fighter.GetGameFightFighterInformations(client)));
        }

        public static void SendGameFightRefreshFighterMessage(WorldClient client, FightActor fighter)
        {
            client.Send(new GameFightRefreshFighterMessage(fighter.GetGameFightFighterInformations(client)));
        }

        public static void SendGameFightRemoveTeamMemberMessage(IPacketReceiver client, FightActor fighter)
        {
            client.Send(new GameFightRemoveTeamMemberMessage((ushort)fighter.Fight.Id, (sbyte)fighter.Team.Id, fighter.Id));
        }

        public static void SendGameFightLeaveMessage(IPacketReceiver client, FightActor fighter)
        {
            client.Send(new GameFightLeaveMessage(fighter.Id));
        }

        public static void SendGameFightLeaveMessage(IPacketReceiver client, FightSpectator spectator)
        {
            client.Send(new GameFightLeaveMessage(spectator.Character.Id));
        }

        public static void SendGameFightPlacementPossiblePositionsMessage(IPacketReceiver client, IFight fight, sbyte team)
        {
            client.Send(new GameFightPlacementPossiblePositionsMessage(
                            fight.ChallengersTeam.PlacementCells.Select(entry => (ushort)entry.Id).ToArray(),
                            fight.DefendersTeam.PlacementCells.Select(entry => (ushort)entry.Id).ToArray(),
                            team));
        }

        public static void SendGameFightOptionStateUpdateMessage(IPacketReceiver client, FightTeam team, FightOptionsEnum option, bool state)
        {
            client.Send(new GameFightOptionStateUpdateMessage((ushort)team.Fight.Id, (sbyte)team.Id, (sbyte)option, state));
        }

        public static void SendGameActionFightSpellCastMessage(IPacketReceiver client, ActionsEnum actionId, FightActor caster, FightActor target,
                                                               Cell cell, FightSpellCastCriticalEnum critical, bool silentCast,
                                                               Spell spell, short[] portals)
        {
            client.Send(new GameActionFightSpellCastMessage((ushort)actionId, caster.Id, silentCast, true, target == null ? 0 : target.Id, silentCast ? (short)-1 : cell.Id, (sbyte)(critical),
                                                             (ushort)spell.Id, (sbyte)spell.CurrentLevel, portals.AsEnumerable().ToArray()
                                                             ));
        }

        public static void SendGameActionFightSpellCastMessage(IPacketReceiver client, ActionsEnum actionId, FightActor caster, FightActor target,
                                                       Cell cell, FightSpellCastCriticalEnum critical, bool silentCast,
                                                       short spellId, sbyte spellLevel, short[] portals)
        {
            client.Send(new GameActionFightSpellCastMessage((ushort)actionId, caster.Id, silentCast, false, target == null ? 0 : target.Id, cell.Id, (sbyte)(critical),
                                                            (ushort)spellId, spellLevel, portals.AsEnumerable().ToArray()));
        }

        public static void SendGameActionFightNoSpellCastMessage(IPacketReceiver client, Spell spell)
        {
            client.Send(new GameActionFightNoSpellCastMessage(spell.Id == 0 ? 0 : (uint)spell.CurrentSpellLevel.Id));
        }

        public static void SendGameActionFightModifyEffectsDurationMessage(IPacketReceiver client, short actionId, FightActor source, FightActor target, short delta)
        {
            client.Send(new GameActionFightModifyEffectsDurationMessage((ushort)actionId, source.Id, target.Id, delta));
        }

        public static void SendGameActionFightDispellableEffectMessage(IPacketReceiver client, Buff buff, bool update = false)
        {
            client.Send(new GameActionFightDispellableEffectMessage(update ? (ushort)ActionsEnum.ACTION_CHARACTER_UPDATE_BOOST : (ushort)buff.GetActionId(), buff.Caster.Id, buff.GetAbstractFightDispellableEffect()));
        }

        public static void SendGameActionFightMarkCellsMessage(IPacketReceiver client, MarkTrigger trigger, bool visible = true)
        {
            var action = ActionsEnum.ACTION_FIGHT_ADD_GLYPH_CASTING_SPELL;

            switch (trigger.Type)
            {
                case GameActionMarkTypeEnum.WALL:
                case GameActionMarkTypeEnum.GLYPH:
                    action = trigger.TriggerType == TriggerType.OnTurnEnd ?
                        ActionsEnum.ACTION_FIGHT_ADD_GLYPH_CASTING_SPELL_ENDTURN :
                        ActionsEnum.ACTION_FIGHT_ADD_GLYPH_CASTING_SPELL;
                    break;
                case GameActionMarkTypeEnum.TRAP:
                    action = ActionsEnum.ACTION_FIGHT_ADD_TRAP_CASTING_SPELL;
                    break;
            }

            client.Send(new GameActionFightMarkCellsMessage((ushort)action, trigger.Caster.Id, visible ? trigger.GetGameActionMark() : trigger.GetHiddenGameActionMark()));
        }

        public static void SendGameActionFightUnmarkCellsMessage(IPacketReceiver client, MarkTrigger trigger)
        {
            client.Send(new GameActionFightUnmarkCellsMessage(310, trigger.Caster.Id, trigger.Id));
        }

        public static void SendGameActionFightTriggerGlyphTrapMessage(IPacketReceiver client, MarkTrigger trigger, FightActor target, Spell triggeredSpell)
        {
            var action = trigger.Type == GameActionMarkTypeEnum.GLYPH ? ActionsEnum.ACTION_FIGHT_TRIGGER_GLYPH : ActionsEnum.ACTION_FIGHT_TRIGGER_TRAP;
            client.Send(new GameActionFightTriggerGlyphTrapMessage((ushort)action, trigger.Caster.Id, trigger.Id, (ushort)target.Cell.Id, target.Id, (ushort)triggeredSpell.Id));
        }

        public static void SendGameFightTurnReadyRequestMessage(IPacketReceiver client, FightActor current)
        {
            client.Send(new GameFightTurnReadyRequestMessage(current.Id));
        }

        public static void SendSlaveSwitchContextMessage(IPacketReceiver client, SummonedFighter actor)
        {
            sbyte slotIndex = 0;
            client.Send(new SlaveSwitchContextMessage(actor.Summoner.Id, actor.Id, actor.Spells.Select(x => x.Value.GetSpellItem()).ToArray(),
                actor.GetSlaveCharacteristicsInformations(), actor.Spells.Select(x => new ShortcutSpell(slotIndex++, (ushort)x.Value.Template.Id)).ToArray()));
        }
        public static void SendSlaveSwitchContextMessage(IPacketReceiver client, CompanionActor actor)
        {
            sbyte slotIndex = 0;
            client.Send(new SlaveSwitchContextMessage(actor.Master.Id, actor.Id, actor.CompanionSpell.Select(x => x.GetSpellItem()).ToArray(),
                actor.GetCompanionStatsMessage(), actor.CompanionSpell.Select(x => new ShortcutSpell(slotIndex++, (ushort)x.Template.Id)).ToArray()));
        }
        public static void SendGameFightResumeMessage(IPacketReceiver client, CharacterFighter fighter)
        {
            var slaves = fighter.Summons.Where(x => x.IsControlled());

            if (slaves.Any())
            {
                client.Send(new GameFightResumeWithSlavesMessage(
                    fighter.Fight.GetBuffs().Select(entry => entry.GetFightDispellableEffectExtendedInformations()).ToArray(),
                    fighter.Fight.GetTriggers().Select(entry => entry.GetGameActionMark(fighter)).ToArray(),
                    (ushort)fighter.Fight.TimeLine.RoundNumber,
                    !fighter.Fight.IsStarted ? 0 : fighter.Fight.StartTime.GetUnixTimeStamp(),
                    fighter.Fight.ActiveIdols.Select(x => x.GetNetworkIdol()).ToArray(),
                    fighter.SpellHistory.GetCooldowns(),
                    (sbyte)fighter.SummonedCount,
                    (sbyte)fighter.BombsCount,
                    slaves.Select(x => new GameFightResumeSlaveInfo(x.Id, x.SpellHistory.GetCooldowns(), (sbyte)x.SummonedCount, (sbyte)x.BombsCount)).ToArray()));
            }
            else
            {
                client.Send(new GameFightResumeMessage(
                    fighter.Fight.GetBuffs().Select(entry => entry.GetFightDispellableEffectExtendedInformations()).ToArray(),
                    fighter.Fight.GetTriggers().Select(entry => entry.GetGameActionMark(fighter)).ToArray(),
                    (ushort)fighter.Fight.TimeLine.RoundNumber,
                    !fighter.Fight.IsStarted ? 0 : fighter.Fight.StartTime.GetUnixTimeStamp(),
                   fighter.Fight.ActiveIdols.Select(x => x.GetNetworkIdol()).ToArray(),
                    fighter.SpellHistory.GetCooldowns(),
                    (sbyte)fighter.SummonedCount,
                    (sbyte)fighter.BombsCount));
            }
        }

        public static void SendGameActionFightKillMessage(IPacketReceiver client, FightActor source, FightActor target)
        {
            client.Send(new GameActionFightKillMessage((ushort)ActionsEnum.ACTION_CHARACTER_KILL, source.Id, target.Id));
        }
    }
}