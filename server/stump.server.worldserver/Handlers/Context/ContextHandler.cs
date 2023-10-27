using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Stump.DofusProtocol.Enums;
using Stump.DofusProtocol.Messages;
using Stump.DofusProtocol.Types;
using Stump.Server.BaseServer.Network;
using Stump.Server.WorldServer.Core.Network;
using Stump.Server.WorldServer.Database.World;
using Stump.Server.WorldServer.Game.Actors;
using Stump.Server.WorldServer.Game.Fights;
using Stump.Server.WorldServer.Game.Maps.Cells;
using Stump.Server.WorldServer.Game.Maps.Pathfinding;
using Stump.Server.WorldServer.Game.Actors.Fight;
using Stump.Server.WorldServer.Game.Fights.Triggers;
using Stump.Server.WorldServer.Game.Fights.Buffs;
using Stump.Server.WorldServer.Game.HavenBags;
using Stump.Server.WorldServer.Game;
using Stump.Server.WorldServer.Game.Maps;
using Stump.Server.WorldServer.Game.Dialogs.Interactives;

namespace Stump.Server.WorldServer.Handlers.Context
{
    public partial class ContextHandler : WorldHandlerContainer
    {
        private ContextHandler()
        {
            
        }

        public static void ModifySpellRange(IPacketReceiver client, Buff buff, bool sub = false)
        {
            if (sub) client.Send(new GameActionFightDispellableEffectMessage((ushort)ActionsEnum.ACTION_DEBOOST_SPELL_RANGE, buff.Caster.Id, buff.GetAbstractFightDispellableEffect()));
            else client.Send(new GameActionFightDispellableEffectMessage((ushort)ActionsEnum.ACTION_BOOST_SPELL_RANGE, buff.Caster.Id, buff.GetAbstractFightDispellableEffect()));
        }

        public static void ModifySpellCastTestLos(IPacketReceiver client, Buff buff)
        {
            client.Send(new GameActionFightDispellableEffectMessage((ushort)ActionsEnum.ACTION_BOOST_SPELL_NOLINEOFSIGHT, buff.Caster.Id, buff.GetAbstractFightDispellableEffect()));
        }

        [WorldHandler(GameContextCreateRequestMessage.Id)]
        public static void HandleGameContextCreateRequestMessage(WorldClient client, GameContextCreateRequestMessage message)
        {
            if (client.Character.IsInWorld)
            {
                client.Character.SendServerMessage("You are already Logged !");
                return;
            }

            client.Character.LogIn();
        }

        [WorldHandler(GameContextReadyMessage.Id)]
        public static void HandleGameContextReadyMessage(WorldClient client, GameContextReadyMessage message)
        {
            client.Character.OnCharacterContextReady((int)message.MapId);
        }

        [WorldHandler(GameMapChangeOrientationRequestMessage.Id)]
        public static void HandleGameMapChangeOrientationRequestMessage(WorldClient client, GameMapChangeOrientationRequestMessage message)
        {
            if (client.Character.IsInFight())
                return;

            client.Character.Direction = (DirectionsEnum) message.Direction;
            SendGameMapChangeOrientationMessage(client.Character.CharacterContainer.Clients, client.Character);
        }

        [WorldHandler(GameCautiousMapMovementRequestMessage.Id)]
        [WorldHandler(GameMapMovementRequestMessage.Id)]
        public static void HandleGameMapMovementRequestMessage(WorldClient client, GameMapMovementRequestMessage message)
        {
            if (client.Character.followMap != null)
                client.Character.UndoFollow();
            if (!client.Character.CanMove())
            {
                SendGameMapNoMovementMessage(client, (short)client.Character.Position.Point.X, (short)client.Character.Position.Point.Y);
                return;
            }

            var movementPath = Path.BuildFromCompressedPath(client.Character.Map, message.KeyMovements);

            if (message is GameCautiousMapMovementRequestMessage)
                movementPath.SetWalk();
            if (client.Character.IsFighting())
            {
                if (client.Character.Fight.FighterPlaying is CompanionActor)
                    client.Character.Fight.FighterPlaying.StartMove(movementPath);
                else
                {
                    if (!client.Character.StartMove(movementPath))
                SendGameMapNoMovementMessage(client, (short)client.Character.Position.Point.X, (short)client.Character.Position.Point.Y);
                }
            }
            else
            {
                if (!client.Character.StartMove(movementPath))
                    SendGameMapNoMovementMessage(client, (short)client.Character.Position.Point.X, (short)client.Character.Position.Point.Y);
            }
        }
    

        [WorldHandler(GameMapMovementConfirmMessage.Id)]
        public static void HandleGameMapMovementConfirmMessage(WorldClient client, GameMapMovementConfirmMessage message)
        {
            client.Character.StopMove();
            if (client.Character.followMap != null)
                client.Character.checkFollowMap();
        }

        [WorldHandler(GameMapMovementCancelMessage.Id)]
        public static void HandleGameMapMovementCancelMessage(WorldClient client, GameMapMovementCancelMessage message)
        {
            client.Character.StopMove(new ObjectPosition(client.Character.Map, (short)message.CellId,
                                                               client.Character.Position.Direction));
        }

        [WorldHandler(ShowCellRequestMessage.Id)]
        public static void HandleShowCellRequestMessage(WorldClient client, ShowCellRequestMessage message)
        {
            if (client.Character.IsFighting())
                client.Character.Fighter.ShowCell(client.Character.Map.Cells[message.CellId]);
            else if (client.Character.IsSpectator())
                client.Character.Spectator.ShowCell(client.Character.Map.Cells[message.CellId]);
        }

        public static void SendGameMapNoMovementMessage(IPacketReceiver client, short cellX, short cellY)
        {
            client.Send(new GameMapNoMovementMessage(cellX, cellY));
        }

        public static void SendGameContextCreateMessage(IPacketReceiver client, sbyte context)
        {
            client.Send(new GameContextCreateMessage(context));
        }

        public static void SendGameContextDestroyMessage(IPacketReceiver client)
        {
            client.Send(new GameContextDestroyMessage());
        }

        public static void SendGameMapChangeOrientationMessage(IPacketReceiver client, ContextActor actor)
        {
            client.Send(new GameMapChangeOrientationMessage(new ActorOrientation(actor.Id,
                                                                         (sbyte) actor.Position.Direction)));
        }

        public static void SendGameContextRemoveElementMessage(IPacketReceiver client, ContextActor actor)
        {
            client.Send(new GameContextRemoveElementMessage(actor.Id));
        }

        public static void SendShowCellSpectatorMessage(IPacketReceiver client, FightSpectator spectator, Cell cell)
        {
            client.Send(new ShowCellSpectatorMessage(spectator.Character.Id, (ushort)cell.Id, spectator.Character.Name));
        }

        public static void SendShowCellMessage(IPacketReceiver client, ContextActor source, Cell cell)
        {
            client.Send(new ShowCellMessage(source.Id, (ushort)cell.Id));
        }

        public static void SendGameContextRefreshEntityLookMessage(IPacketReceiver client, ContextActor actor)
        {
            client.Send(new GameContextRefreshEntityLookMessage(actor.Id, actor.Look.GetEntityLook()));
        }

        public static void SendGameMapMovementMessage(IPacketReceiver client, IEnumerable<short> movementsKey, ContextActor actor)
        {
            client.Send(new GameMapMovementMessage(movementsKey.ToArray(), 0, actor.Id));
        }

        public static void SendGameCautiousMapMovementMessage(IPacketReceiver client, IEnumerable<short> movementsKey, ContextActor actor)
        {
            client.Send(new GameCautiousMapMovementMessage(movementsKey.ToArray(), 0, actor.Id));
        }

        public static void SendGameEntitiesDispositionMessage(IPacketReceiver client, IEnumerable<ContextActor> actors)
        {
            client.Send(new GameEntitiesDispositionMessage(actors.Select(entry => entry.GetIdentifiedEntityDispositionInformations()).ToArray()));
        }

        public static void SendGameActionFightActivateGlyphTrapMessage(IPacketReceiver client, MarkTrigger trigger, short action, FightActor source, bool active)
        {
            client.Send(new GameActionFightActivateGlyphTrapMessage((ushort)action, source.Id, trigger.Id, active));
        }

        #region HavenBags

        [WorldHandler(EnterHavenBagRequestMessage.Id)]
        public static void HandleEnterHavenBagRequestMessage(WorldClient client, EnterHavenBagRequestMessage message)
        {
            Game.HavenBags.HavenBagManager.Instance.HandleHavenBagEnter(client, (int)message.HavenBagOwner);
        }

        [WorldHandler(HavenBagFurnituresRequestMessage.Id)]
        public static void HandleHavenBagFurnituresRequestMessage(WorldClient client, HavenBagFurnituresRequestMessage message)
        {
            Game.HavenBags.HavenBagManager.Instance.HavenBagFurnituresUpdate(client, message);
        }

        [WorldHandler(ExitHavenBagRequestMessage.Id)]
        public static void HandleExitHavenBagRequestMessage(WorldClient client, ExitHavenBagRequestMessage message)
        {
            Game.HavenBags.HavenBagManager.Instance.ExitHavenBag(client);
        }

        public static void SendHavenBagFurnituresMessage(WorldClient client, HavenBagFurnitureInformation[] infos)
        {
            client.Send(new HavenBagFurnituresMessage(infos));
        }

        public static void SendHavenBagPermissionsUpdateMessage(WorldClient client, int permissions)
        {
            client.Send(new HavenBagPermissionsUpdateMessage(permissions));
        }

        [WorldHandler(EditHavenBagRequestMessage.Id)]
        public static void HandleEditHavenBagRequestMessage(WorldClient client, EditHavenBagRequestMessage message)
        {
            client.Send(new EditHavenBagStartMessage());
        }

        [WorldHandler(EditHavenBagCancelRequestMessage.Id)]
        public static void HandleEditHavenBagCancelRequestMessage(WorldClient client, EditHavenBagCancelRequestMessage message)
        {
            client.Send(new EditHavenBagFinishedMessage());
        }

        [WorldHandler(HavenBagPermissionsUpdateRequestMessage.Id)]
        public static void HandleHavenBagPermissionsUpdateRequestMessage(WorldClient client, HavenBagPermissionsUpdateRequestMessage message)
        {
            Game.HavenBags.HavenBagManager.Instance.UpdateHavenBagPermissions(client, message);
        }

        [WorldHandler(TeleportHavenBagRequestMessage.Id)]
        public static void HandleTeleportHavenBagRequestMessage(WorldClient client, TeleportHavenBagRequestMessage message)
        {
            var character = World.Instance.GetCharacter((int)message.GuestId);
            if (character == null) return;
            if (character.IsInFight() || character.IsBusy()) return;

            HavenBagInvitationRequest v = new HavenBagInvitationRequest(client.Character, character);
            HavenBagManager.Instance.AddInvitation(v);
        }

        [WorldHandler(TeleportHavenBagAnswerMessage.Id)]
        public static void HandleTeleportHavenBagAnswerMessage(WorldClient client, TeleportHavenBagAnswerMessage message)
        {
            HavenBagManager.Instance.HandleInvitation(client.Character, message.Accept);
        }

        [WorldHandler(ChangeThemeRequestMessage.Id)]
        public static void HandleChangeThemeRequestMessage(WorldClient client, ChangeThemeRequestMessage message)
        {
            Game.HavenBags.HavenBagManager.Instance.UpdateHavenBagTheme(client, message);
        }

        [WorldHandler(OpenHavenBagFurnitureSequenceRequestMessage.Id)]
        public static void HandleOpenHavenBagFurnitureSequenceRequestMessage(WorldClient client, OpenHavenBagFurnitureSequenceRequestMessage message)
        {
            client.Send(new EditHavenBagStartMessage());
        }

        [WorldHandler(CloseHavenBagFurnitureSequenceRequestMessage.Id)]
        public static void HandleCloseHavenBagFurnitureSequenceRequestMessage(WorldClient client, CloseHavenBagFurnitureSequenceRequestMessage message)
        {
            client.Send(new EditHavenBagFinishedMessage());
        }

        #endregion
    }
}