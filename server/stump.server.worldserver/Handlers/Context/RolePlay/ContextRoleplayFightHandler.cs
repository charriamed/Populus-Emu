using Stump.DofusProtocol.Messages;
using Stump.DofusProtocol.Types;
using Stump.Server.BaseServer.Network;
using Stump.Server.WorldServer.Core.Network;
using Stump.Server.WorldServer.Game.Actors;
using Stump.Server.WorldServer.Game.Actors.Fight;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Characters;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Monsters;
using Stump.Server.WorldServer.Game.Fights;
using System.Collections.Generic;
using System.Linq;

namespace Stump.Server.WorldServer.Handlers.Context.RolePlay
{
    public partial class ContextRoleplayHandler : WorldHandlerContainer
    {
        [WorldHandler(GameRolePlayAttackMonsterRequestMessage.Id)]
        public static void HandleGameRolePlayAttackMonsterRequestMessage(WorldClient client, GameRolePlayAttackMonsterRequestMessage message)
        {
            var map = client.Character.Map;
            var monster = map.GetActor<MonsterGroup>(entry => entry.Id == message.MonsterGroupId);

            if (monster != null && (monster.Position.Cell == client.Character.Position.Cell || monster.Position.Point.ManhattanDistanceTo(client.Character.Position.Cell) <= 2))
                monster.FightWith(client.Character);
        }

        [WorldHandler(GameFightPlacementSwapPositionsRequestMessage.Id)]
        public static void HandleGameFightPlacementSwapPositionsRequestMessage(WorldClient client, GameFightPlacementSwapPositionsRequestMessage message)
        {
            if (!client.Character.IsFighting())
                return;

            if (client.Character.Fighter.Position.Cell.Id != message.CellId)
            {
                var cell = client.Character.Fight.Map.Cells[message.CellId];
                var target = client.Character.Fighter.Team.GetOneFighter(cell);

                if (target == null)
                    return;
                if (target is CharacterFighter)
                {
                    if (client.Character.Fighter.IsTeamLeader())
                        client.Character.Fighter.SwapPrePlacement(target);
                    else
                    {
                        var swapRequest = new SwapRequest(client.Character, (target as CharacterFighter).Character);
                        swapRequest.Open();
                    }
                }
                else if (target is CompanionActor)
                {
                    client.Character.Fighter.SwapPrePlacement(target);
                }
            }
        }

        [WorldHandler(GameFightPlacementSwapPositionsAcceptMessage.Id)]
        public static void HandleGameFightPlacementSwapPositionsAcceptMessage(WorldClient client, GameFightPlacementSwapPositionsAcceptMessage message)
        {
            if (!client.Character.IsInRequest() || !(client.Character.RequestBox is SwapRequest))
                return;

            if (message.RequestId == client.Character.RequestBox.Source.Id)
                client.Character.RequestBox.Accept();
        }

        [WorldHandler(GameFightPlacementSwapPositionsCancelMessage.Id)]
        public static void HandleGameFightPlacementSwapPositionsCancelMessage(WorldClient client, GameFightPlacementSwapPositionsCancelMessage message)
        {
            if (!client.Character.IsInRequest() || !(client.Character.RequestBox is SwapRequest))
                return;

            if (message.RequestId == client.Character.RequestBox.Source.Id)
            {
                if (client.Character == client.Character.RequestBox.Source)
                    client.Character.RequestBox.Cancel();
                else
                    client.Character.RequestBox.Deny();
            }
        }

        public static void SendGameRolePlayPlayerFightFriendlyAnsweredMessage(IPacketReceiver client, Character replier,
                                                                              Character source, Character target,
                                                                              bool accepted)
        {
            client.Send(new GameRolePlayPlayerFightFriendlyAnsweredMessage((ushort)replier.Id,
                                                                           (ulong)source.Id,
                                                                           (ulong)target.Id,
                                                                           accepted));
        }

        public static void SendGameRolePlayPlayerFightFriendlyRequestedMessage(IPacketReceiver client, Character requester,
                                                                               Character source,
                                                                               Character target)
        {
            client.Send(new GameRolePlayPlayerFightFriendlyRequestedMessage((ushort)requester.Id, (ulong)source.Id,
                                                                            (ulong)target.Id));
        }

        public static void SendGameRolePlayArenaUpdatePlayerInfosMessage(WorldClient client, Character character)
        {
            var ranking = new ArenaRanking((ushort)character.ArenaRank_3vs3, (ushort)character.ArenaMaxRank_3vs3);
            var rankingLeague = new ArenaLeagueRanking((ushort)character.ArenaRank_3vs3, 0, 0, 0, 0);

            client.Send(new GameRolePlayArenaUpdatePlayerInfosAllQueuesMessage(new ArenaRankInfos(ranking, rankingLeague, (ushort)character.ArenaDailyMatchsWon_3vs3, (ushort)character.ArenaDailyMatchsCount_3vs3, 10), new ArenaRankInfos(), new ArenaRankInfos()));
        }

        public static void SendGameRolePlayAggressionMessage(IPacketReceiver client, Character challenger, Character defender)
        {
            client.Send(new GameRolePlayAggressionMessage((ulong)challenger.Id, (ulong)defender.Id));
        }

        public static void SendGameFightPlacementSwapPositionsMessage(IPacketReceiver client, IEnumerable<ContextActor> actors)
        {
            //client.Send(new GameFightPlacementSwapPositionsMessage(actors.Select(entry => entry.GetIdentifiedEntityDispositionInformations())));
        }

        public static void SendGameFightPlacementSwapPositionsOfferMessage(IPacketReceiver client, Character source, Character target)
        {
            client.Send(new GameFightPlacementSwapPositionsOfferMessage(source.Id, source.Fighter.Id, (ushort)source.Cell.Id, target.Fighter.Id, (ushort)target.Cell.Id));
        }

        public static void SendGameFightPlacementSwapPositionsCancelledMessage(IPacketReceiver client, Character source, Character canceller)
        {
            client.Send(new GameFightPlacementSwapPositionsCancelledMessage(source.Fighter.Id, canceller.Fighter.Id));
        }
    }
}