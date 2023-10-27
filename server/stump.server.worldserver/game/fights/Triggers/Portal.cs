using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using NLog;
using Stump.Core.Reflection;
using Stump.DofusProtocol.Enums;
using Stump.DofusProtocol.Types;
using Stump.DofusProtocol.Messages;
using Stump.Server.WorldServer.Database.World;
using Stump.Server.WorldServer.Game.Effects.Instances;
using Stump.Server.WorldServer.Game.Maps.Cells;
using Stump.Server.WorldServer.Game.Spells;
using Stump.Server.WorldServer.Handlers.Actions;
using Stump.Server.WorldServer.Handlers.Context;
using Spell = Stump.Server.WorldServer.Game.Spells.Spell;
using Stump.Server.WorldServer.Game.Actors.Fight;

namespace Stump.Server.WorldServer.Game.Fights.Triggers
{
    public class Portal : MarkTrigger
    {
        protected static readonly Logger logger = LogManager.GetCurrentClassLogger();
        public Portal(short id, FightActor caster, Spell castedSpell, Cell centerCell, EffectDice originEffect,
           SpellShapeEnum shapes, byte minsize, byte size, Color color, Spell portalSpell) :
                base(
                id, caster, castedSpell, originEffect, centerCell,
                new MarkShape(caster.Fight, centerCell, shapes, GameActionMarkCellsTypeEnum.CELLS_CIRCLE, minsize, size, color))
        {
            PortalSpell = portalSpell;
            IsActive = true;
            IsNeutral = false;
            IsInGameActive = true;
        }
        
        public Spell PortalSpell { get; }
        public override bool CanTrigger(FightActor actor)
        { return true; }

        public bool IsActive
        {
            get;
            set;
        }
        public void Disable()
        {
            IsActive = false;
            if (IsInGameActive)
            {
                var clients = Fight.Clients;
                IsInGameActive = false;
                ContextHandler.SendGameActionFightActivateGlyphTrapMessage(clients, this, 1181, Caster, false);
            }
        }
        public bool IsInGameActive
        {
            get;
            set;
        }
        public bool IsNeutral { get; set; }

        public override bool StopMovement => IsActive;

        public override GameActionMarkTypeEnum Type => GameActionMarkTypeEnum.PORTAL;

        public override TriggerType TriggerType => TriggerType.MOVE;

        public override bool DoesSeeTrigger(FightActor fighter) => true;

        public override bool DecrementDuration() => false;

        public override GameActionMark GetGameActionMark()
        {
            return new GameActionMark(Caster.Id, (sbyte)Caster.Team.Id, CastedSpell.Id, (sbyte)CastedSpell.CurrentLevel, Id,
                (sbyte)Type, CenterCell.Id, Shape.GetGameActionMarkedCells(), IsActive);
        }
        public override GameActionMark GetHiddenGameActionMark() => new GameActionMark(Caster.Id, (sbyte)Caster.Team.Id, CastedSpell.Id, (sbyte)CastedSpell.CurrentLevel, Id,
                (sbyte)Type, CenterCell.Id, new GameActionMarkedCell[0], IsActive);

        public override void Trigger(FightActor actor, Cell castcell)
        {
            var portal = Fight.GetTriggers().FirstOrDefault(x => x is Portal && x.ContainsCell(CenterCell)) as Portal;
            if (portal != null && portal.IsInGameActive)
            {
                if (portal.IsActive && Fight.PortalsManager.GetActivableGamePortalsCount(portal.Caster.Team) >= 1)
                {
                    var mpWithPortals = Fight.PortalsManager.GetMarksMapPoint(portal.Caster.Team);
                    var links = Fight.PortalsManager.GetLinks(new MapPoint(portal.CenterCell), mpWithPortals);
                    if (links.Count > 0)
                    {
                        short finalPortal = 0;
                        foreach (var link in links)
                        {
                            if (Fight.GetOneFighter(link) == null)
                            {
                                var finalPortalCellx = Fight.Map.GetCell(link);
                                var portaloutx = Fight.GetTriggers().FirstOrDefault(x => x is Portal && x.ContainsCell(finalPortalCellx)) as Portal;
                                if (portaloutx.IsActive)
                                    finalPortal = link;
                            }
                        }

                        var finalPortalCell = Fight.Map.GetCell(finalPortal);
                        var portalout = Fight.GetTriggers().FirstOrDefault(x => x is Portal && x.ContainsCell(finalPortalCell)) as Portal;
                        portal.Disable();
                        portalout.Disable();

                        ActionsHandler.SendGameActionFightTeleportOnSameMapMessage(Fight.Clients, Caster, actor,
                               finalPortalCell);
                        actor.Position.Cell = finalPortalCell;
                        NotifyTriggered(actor, PortalSpell);
                        var spellCastHandler = Singleton<SpellManager>.Instance.GetSpellCastHandler(Fight.FighterPlaying,
                            PortalSpell, actor.Cell, false);
                        spellCastHandler.MarkTrigger = this;
                        spellCastHandler.Initialize();
                        spellCastHandler.Execute();
                        Fight.PortalsManager.RefreshClientsPortals();
                        actor.TriggerBuffs(actor, Buffs.BuffTriggerType.UsedPortal);
                    }
                }
            }
        }
    }
}