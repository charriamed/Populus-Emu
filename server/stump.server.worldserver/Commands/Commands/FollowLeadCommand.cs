using D2pReader.MapInformations;
using Stump.Core.Threading;
using Stump.DofusProtocol.Enums;
using Stump.Server.BaseServer.Commands;
using Stump.Server.WorldServer.Commands.Commands.Patterns;
using Stump.Server.WorldServer.Commands.Trigger;
using Stump.Server.WorldServer.Game;
using Stump.Server.WorldServer.Game.Actors;
using Stump.Server.WorldServer.Game.Actors.Fight;
using Stump.Server.WorldServer.Game.Actors.RolePlay;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Characters;
using Stump.Server.WorldServer.Game.Exchanges.Bank;
using Stump.Server.WorldServer.Game.Fights;
using Stump.Server.WorldServer.Game.Maps;
using Stump.Server.WorldServer.Game.Maps.Cells;
using Stump.Server.WorldServer.Game.Maps.Pathfinding;
using Stump.Server.WorldServer.Game.Parties;
using Stump.Server.WorldServer.Handlers.Inventory;
using System;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace Stump.Server.WorldServer.Commands.Commands
{
    public class FollowLeadCommand : InGameCommand
    {
        Character Lead;
        public FollowLeadCommand()
        {
            Aliases = new[] { "join" };
            Description = "Permet de faire suivre les déplacements de ce personnage à vos autres personnages, et à les faire rejoindre le combat de ce personnage.";
            RequiredRole = RoleEnum.Player;
        }

        public override void Execute(GameTrigger trigger)
        {
            var character = trigger.Character;

            if (!character.isMultiLeadder)
            {
                character.isMultiLeadder = true;
                Lead = character;
                character.StartMoving += OnStartMoving;

                foreach (var perso in WorldServer.Instance.FindClients(x => x.IP == character.Client.IP && x.Character != character))
                {
                    if (character.Map.IsDungeon())
                        break;

                    if (perso.Character.Map.Id == character.Map.Id)
                    {
                        perso.Character.Teleport(character.Map, character.Cell);
                    }
                }

                character.EnterFight += OnEnterFight;
                character.ReadyStatusChanged += OnReadyStatusChanged;

                character.SendServerMessage("Le suivi des personnages est désormais activé, tous vos personnages rejoindront automatiquement les combats et bougeront en même temps que le leadder !");
            }
            else
            {
                character.isMultiLeadder = false;
                Lead = null;
                character.StartMoving -= OnStartMoving;
                character.EnterFight -= OnEnterFight;
                character.ReadyStatusChanged -= OnReadyStatusChanged;
                character.SendServerMessage("Le suivi des personnages est désormais désactivé !");
            }
        }

        private void OnReadyStatusChanged(CharacterFighter fighter)
        {
            if (fighter == null)
                return;

            foreach (var perso in WorldServer.Instance.FindClients(x => x.IP == fighter.Character.Client.IP && x.Character != fighter.Character))
            {
                if (perso.Character.Map.Id == fighter.Map.Id && perso.Character.IsInFight())
                {
                    perso.Character.Fighter.ToggleReady(fighter.IsReady);
                }
            }
        }

        private void OnStartMoving(ContextActor actor, Path path)
        {
            var character = (actor as Character);

            if (character.IsInFight())
                return;

            character.EnterMap += OnEnterMap;
            foreach (var perso in WorldServer.Instance.FindClients(x => x.IP == character.Client.IP && x.Character != character))
            {
                if (perso.Character.Map.Id == character.Map.Id)
                {
                    perso.Character.Teleport(character.Map, character.Cell);
                    perso.Character.StartMove(path);
                }
            }
        }

        private void OnEnterMap(ContextActor actor, Game.Maps.Map map)
        {
            var character = (actor as Character);

            if (character.IsInFight() || character.Map.IsDungeon())
                return;

            character.EnterMap -= OnEnterMap;

            foreach (var perso in WorldServer.Instance.FindClients(x => x.IP == character.Client.IP && x.Character != character))
            {
                perso.Character.Teleport(character.Map, character.Cell);
            }
        }

        private void OnEnterFight(CharacterFighter fighter)
        {
            if (fighter == null || fighter.Map.IsDungeon())
                return;

            foreach (var perso in WorldServer.Instance.FindClients(x => x.IP == fighter.Character.Client.IP && x.Character != fighter.Character))
            {
                if (perso.Character.Map.Id == fighter.Map.Id && !perso.Character.IsInFight())
                {
                    var fighterr = perso.Character.CreateFighter(fighter.Team);
                    fighter.Team.AddFighter(fighterr);
                }
            }
        }
    }
}