using System;
using System.Collections.Generic;
using System.Linq;
using Stump.Core.Cache;
using Stump.DofusProtocol.Enums;
using Stump.DofusProtocol.Types;
using Stump.Server.WorldServer.Database.Npcs;
using Stump.Server.WorldServer.Database.Npcs.Actions;
using Stump.Server.WorldServer.Game.Actors.Interfaces;
using Stump.Server.WorldServer.Game.Actors.Look;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Characters;
using Stump.Server.WorldServer.Game.Maps.Cells;
using Stump.Server.WorldServer.Game.Quests;

namespace Stump.Server.WorldServer.Game.Actors.RolePlay.Npcs
{
    public sealed class Npc : RolePlayActor, IInteractNpc, IContextDependant
    {
        private readonly List<NpcAction> m_actions = new List<NpcAction>();

        public Npc(int id, NpcTemplate template, ObjectPosition position, ActorLook look)
        {
            Id = id;
            Template = template;
            Position = position;
            Look = look;

            m_gameContextActorInformations = new ObjectValidator<GameContextActorInformations>(BuildGameContextActorInformations);
            m_actions.AddRange(Template.Actions);
        }

        public Npc(int id, NpcSpawn spawn)
            : this(id, spawn.Template, spawn.GetPosition(), spawn.Look)
        {
            Spawn = spawn;
        }


        public NpcTemplate Template
        {
            get;
        }

        public NpcSpawn Spawn
        {
            get;
        }

        public override int Id
        {
            get;
            protected set;
        }

        public int TemplateId => Template.Id;

        public override ActorLook Look
        {
            get;
            set;
        }

        public List<NpcAction> Actions => m_actions;

        public event Action<Npc, NpcActionTypeEnum, NpcAction, Character> Interacted;

        private void OnInteracted(NpcActionTypeEnum actionType, NpcAction action, Character character)
        {
            character.OnInteractingWith(this, actionType, action);
            var handler = Interacted;
            if (handler != null) handler(this, actionType, action, character);
        }

        public void Refresh()
        {
            m_gameContextActorInformations.Invalidate();

            if (Map != null)
                Map.Refresh(this);
        }

        public void InteractWith(NpcActionTypeEnum actionType, Character dialoguer)
        {
            if (!CanInteractWith(actionType, dialoguer))
                return;

            var action = Actions.Where(entry => entry.ActionType.Contains(actionType) && entry.CanExecute(this, dialoguer)).OrderBy(x => x.Priority).First();

            action.Execute(this, dialoguer);
            OnInteracted(actionType, action, dialoguer);
        }

        public bool CanInteractWith(NpcActionTypeEnum action, Character dialoguer)
        {
            if (dialoguer.Map != Position.Map || dialoguer.IsFighting() || dialoguer.IsInRequest() || dialoguer.IsGhost())
                return false;

            if (dialoguer.IsBusy())
                dialoguer.Dialog.Close();

            return Actions.Count > 0 && Actions.Any(entry => entry.ActionType.Contains(action) && entry.CanExecute(this, dialoguer));
        }

        public void SpeakWith(Character dialoguer)
        {
            if (!CanInteractWith(NpcActionTypeEnum.ACTION_TALK, dialoguer))
                return;

            InteractWith(NpcActionTypeEnum.ACTION_TALK, dialoguer);
        }

        public override string ToString() => string.Format("{0} ({1}) [{2}]", Template.Name, Id, TemplateId);

        #region GameContextActorInformations

        private readonly ObjectValidator<GameContextActorInformations> m_gameContextActorInformations;

        private GameContextActorInformations BuildGameContextActorInformations() => new GameRolePlayNpcInformations(Id,
                                                   Look.GetEntityLook(),
                                                   GetEntityDispositionInformations(),
                                                   (ushort)Template.Id,
                                                   Template.Gender != 0,
                                                   (ushort)Template.SpecialArtworkId);

        public override GameContextActorInformations GetGameContextActorInformations(Character character)
        {
            if (GiveQuestCanActive(character).Count() > 0)
                return new GameRolePlayNpcWithQuestInformations(Id, Look.GetEntityLook(), GetEntityDispositionInformations(), (ushort)Template.Id, Template.Gender != 0, (ushort)Template.SpecialArtworkId, new GameRolePlayNpcQuestFlag(new ushort[0], GiveQuestCanActive(character).ToArray()));
            else if (character.Quests.Any(x => !x.Finished && x.CurrentStep.Objectives.Any(y => !y.ObjectiveRecord.Status && y.CanSee() && y.Template.Parameter0 == Template.Id)))
            {
                foreach (var test in character.Quests.Where(x => !x.Finished && x.CurrentStep.Objectives.Any(y => y.Template.Parameter0 == Template.Id)))
                    return new GameRolePlayNpcWithQuestInformations(Id, Look.GetEntityLook(), GetEntityDispositionInformations(), (ushort)Template.Id, Template.Gender != 0, (ushort)Template.SpecialArtworkId, new GameRolePlayNpcQuestFlag(new ushort[] { test.Id }, new ushort[0]));
                return null;
            }
            else
                return BuildGameContextActorInformations();
        }

        private IEnumerable<ushort> GiveQuestCanActive(Character character)
        {
            List<NpcActionRecord> actionsTalk = NpcManager.Instance.GetNpcActionsRecords(Template.Id).Where(x => x.Type == "Talk").ToList();
            for (int i = 0; i < actionsTalk.Count; i++)
            {
                var replys = NpcManager.Instance.GetMessageRepliesRecords(int.Parse(actionsTalk[i].Parameter0));
                foreach (var reply in replys.Where(x => x.Type == "Quest").Select(x => short.Parse(x.Parameter0)))
                {
                    if (!character.Quests.Any(x => x.Template.StepIds.Contains(reply)))
                        yield return (ushort)QuestManager.Instance.GetQuestTemplateWithStepId(reply).Id;
                }
            }
        }

        #endregion
    }
}