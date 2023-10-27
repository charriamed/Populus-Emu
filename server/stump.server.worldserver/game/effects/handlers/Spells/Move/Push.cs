using System.Linq;
using Stump.DofusProtocol.Enums;
using Stump.Server.WorldServer.Database.World;
using Stump.Server.WorldServer.Game.Actors.Fight;
using Stump.Server.WorldServer.Game.Effects.Instances;
using Stump.Server.WorldServer.Game.Fights.Buffs;
using Stump.Server.WorldServer.Handlers.Actions;
using Stump.Server.WorldServer.Game.Spells.Casts;
using System;
using Stump.DofusProtocol.Enums.Extensions;
using Stump.Server.WorldServer.Game.Maps.Cells;
using System.Collections.Generic;
using Stump.Server.WorldServer.Game.Spells;
using Stump.Server.WorldServer.Game.Fights.Triggers;

namespace Stump.Server.WorldServer.Game.Effects.Handlers.Spells.Move
{
    [EffectHandler(EffectsEnum.Effect_PushBack)]
    [EffectHandler(EffectsEnum.Effect_PushBack_1103)]
    [EffectHandler(EffectsEnum.Effect_PullForward)]
    public class Push : SpellEffectHandler
    {
        public Push(EffectDice effect, FightActor caster, SpellCastHandler castHandler, Cell targetedCell, bool critical)
            : base(effect, caster, castHandler, targetedCell, critical)
        {
            DamagesDisabled = effect.EffectId == EffectsEnum.Effect_PushBack_1103 ||
                effect.EffectId == EffectsEnum.Effect_PullForward;
            Pull = effect.EffectId == EffectsEnum.Effect_PullForward;
        }

        public bool DamagesDisabled
        {
            get;
            set;
        }

        public DirectionsEnum? PushDirection
        {
            get;
            set;
        }

        public bool Pull
        {
            get;
            set;
        }

        public int Distance
        {
            get;
            set;
        }

        protected override bool InternalApply()
        {
            var integerEffect = GenerateEffect();

            if (integerEffect == null)
                return false;

            var actors = new List<FightActor>();

            if (Pull)
                actors = GetAffectedActors().OrderBy(entry => entry.Position.Point.ManhattanDistanceTo(TargetedPoint)).ToList();
            else
                actors = GetAffectedActors().OrderByDescending(entry => entry.Position.Point.ManhattanDistanceTo(TargetedPoint)).ToList();

            foreach (var actor in actors)
            {
                if (!actor.CanBePushed())
                    continue;

                var referenceCell = TargetedCell.Id == actor.Cell.Id ? CastPoint : TargetedPoint;

                if (Spell.Id == 1503)
                {
                    var glyph = Fight.GetTriggers().OfType<GlyphAura>().FirstOrDefault(x => x.GlyphSpell == Spell || x.GlyphSpell.Id == 5158);
                    if (glyph != null)
                    {
                        referenceCell = glyph.CenterCell;
                    }
                }

                if (Spell.Id == 5390)
                {
                    if (this is Retreat && CastHandler.IsCastByPortal) referenceCell = Map.GetCell(CastHandler.Informations.PortalEntryCellId);
                    DamagesDisabled = true;
                }

                if(Spell.Id == 120)
                {
                    if (this is Push)
                        referenceCell = Caster.Cell;
                }

                if (Spell.Id == 9843)
                {
                    referenceCell = Fight.Fighters.FirstOrDefault(x => x.HasState(610)).Cell;
                }

                if (referenceCell.CellId == actor.Position.Cell.Id)
                    continue;

                var pushDirection = Pull ? actor.Position.Point.OrientationTo(referenceCell) : referenceCell.OrientationTo(actor.Position.Point);

                if (PushDirection != null)
                    pushDirection = PushDirection.Value;

                var startCell = actor.Position.Point;
                var lastCell = startCell;

                if (Distance == 0)
                    Distance = (short)(pushDirection.IsDiagonal() ? Math.Ceiling(integerEffect.Value / 2.0) : integerEffect.Value);

                var stopCell = startCell.GetCellInDirection(pushDirection, Distance);
                
                for (var i = 0; i < Distance; i++)
                {
                    var nextCell = lastCell.GetNearestCellInDirection(pushDirection);

                    // the next cell is blocking, or an adjacent cell is blocking if it's in diagonal
                    if (IsBlockingCell(nextCell, actor) ||
                        (pushDirection.IsDiagonal() && pushDirection.GetDiagonalDecomposition().Any(x => IsBlockingCell(lastCell.GetNearestCellInDirection(x), actor))))
                    {
                        if (nextCell == null)
                        {
                            stopCell = lastCell;
                            nextCell = stopCell;
                        }

                        if (Fight.ShouldTriggerOnMove(Fight.Map.Cells[nextCell.CellId], actor) && Fight.GetOneFighter(Fight.Map.Cells[nextCell.CellId]) == null)
                        {
                            #region PORTALS
                            var PortalSpell = new Spell(5426, 1);
                            var portal = Fight.GetTriggers().FirstOrDefault(x => x is Portal && x.ContainsCell(Fight.Map.Cells[nextCell.CellId])) as Portal;
                            if (portal != null)
                            {
                                if (portal.IsActive && Fight.PortalsManager.GetActivableGamePortalsCount(portal.Caster.Team) >= 2)
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
                                        ActionsHandler.SendGameActionFightSlideMessage(Fight.Clients, Caster, actor, startCell.CellId, Fight.Map.Cells[nextCell.CellId].Id);
                                        ActionsHandler.SendGameActionFightTeleportOnSameMapMessage(Fight.Clients, Caster, actor,
                                               finalPortalCell);
                                        actor.Position.Cell = finalPortalCell;
                                        var spellCastHandler = SpellManager.Instance.GetSpellCastHandler(Fight.FighterPlaying,
                                            PortalSpell, actor.Cell, false);
                                        spellCastHandler.MarkTrigger = portal;
                                        spellCastHandler.Initialize();
                                        spellCastHandler.Execute();
                                        Fight.PortalsManager.RefreshClientsPortals();
                                        actor.TriggerBuffs(actor, Game.Fights.Buffs.BuffTriggerType.UsedPortal);
                                        DamagesDisabled = true;
                                        lastCell = finalPortalCell;
                                        startCell = finalPortalCell;
                                        stopCell = startCell.GetCellInDirection(pushDirection, Distance - (i + 1));
                                        continue;
                                    }
                                    else
                                    {
                                        DamagesDisabled = true;
                                        stopCell = nextCell;
                                    }
                                }
                                else
                                {
                                    stopCell = nextCell;
                                }
                            }
                            #endregion
                            else
                            {
                                DamagesDisabled = true;
                                stopCell = nextCell;
                            }
                        }
                        else
                        {
                            nextCell = lastCell;
                            stopCell = lastCell;
                        }


                        break;
                    }

                    if (nextCell != null)
                        lastCell = nextCell;

                }

                if (actor.IsAlive())
                {
                    foreach (var character in Fight.GetCharactersAndSpectators().Where(actor.IsVisibleFor))
                        ActionsHandler.SendGameActionFightSlideMessage(character.Client, Caster, actor, startCell.CellId, stopCell.CellId);
                }

                if (!DamagesDisabled)
                {
                    var fightersInline = Fight.GetAllFightersInLine(startCell, Distance, pushDirection);
                    fightersInline.Insert(0, actor);
                    var distance = integerEffect.Value - startCell.ManhattanDistanceTo(stopCell);
                    var targets = 0;

                    foreach (var fighter in fightersInline)
                    {
                        var pushDamages = Formulas.FightFormulas.CalculatePushBackDamages(Caster, fighter, (int)distance, targets);

                        if (pushDamages > 0)
                        {
                            var pushDamage = new Fights.Damage(pushDamages)
                            {
                                Source = actor,
                                School = EffectSchoolEnum.Pushback,
                                IgnoreDamageBoost = true,
                                IgnoreDamageReduction = false
                            };

                            fighter.InflictDamage(pushDamage);
                            fighter.TriggerBuffs(Caster, BuffTriggerType.OnPushDamaged);
                        }

                        if (targets > 0)
                            fighter.TriggerBuffs(Caster, BuffTriggerType.OnInderctlyPush);

                        targets++;
                    }             
                }

                if (actor.IsCarrying() && stopCell != startCell)
                    actor.ThrowActor(Map.Cells[startCell.CellId], true);

                actor.Position.Cell = Map.Cells[stopCell.CellId];

                if(Fight.Fighters.Any(x => actor.Position.Point.IsAdjacentTo(x.Position.Point)))
                    actor.TriggerBuffs(Caster, BuffTriggerType.OnPushDamagedInMelee);

                if (Effect.EffectId != EffectsEnum.Effect_PullForward)
                    actor.TriggerBuffs(Caster, BuffTriggerType.OnPushed);
                actor.TriggerBuffs(Caster, BuffTriggerType.OnMoved);
            }

            return true;
        }

        private bool IsBlockingCell(MapPoint cell, FightActor target)
        {
            return cell == null || !Fight.IsCellFree(Map.Cells[cell.CellId]) || Fight.ShouldTriggerOnMove(Fight.Map.Cells[cell.CellId], target);
        }
    }
}