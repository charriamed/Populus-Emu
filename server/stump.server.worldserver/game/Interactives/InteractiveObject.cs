using System.Collections.Generic;
using System.Linq;
using Stump.DofusProtocol.Enums;
using Stump.DofusProtocol.Types;
using Stump.Server.WorldServer.Database.Interactives;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Characters;
using Stump.Server.WorldServer.Game.Interactives.Skills;
using Stump.Server.WorldServer.Game.Maps;
using Stump.Server.WorldServer.Game.Maps.Cells;
using Stump.Server.WorldServer.Handlers.Interactives;
using System;

namespace Stump.Server.WorldServer.Game.Interactives
{
    public class InteractiveObject : WorldObject
    {
        readonly Dictionary<int, Skill> m_skills = new Dictionary<int, Skill>();

        public InteractiveObject(Map map, InteractiveSpawn spawn)
        {
            Spawn = spawn;
            Position = new ObjectPosition(map, spawn.CellId);
            CharacterCanSee = new List<Character>();

            GenerateSkills();
        }

        public InteractiveSpawn Spawn
        {
            get;
        }

        public List<Character> CharacterCanSee;

        public bool Animated => Spawn.Animated || m_skills.Values.Any(x => x is SkillHarvest);

        public InteractiveStateEnum State
        {
            get;
            set;
        }

        public override int Id
        {
            get { return Spawn.Id; }
            protected set { Spawn.Id = value; }
        }

        /// <summary>
        /// Can be null
        /// </summary>
        public InteractiveTemplate Template => Spawn.Template;

        public override bool CanBeSee(WorldObject byObj)
        {
            if(Map.IsInstantiated && CharacterCanSee.Count != 0)
            {
                if(base.CanBeSee(byObj) && CharacterCanSee.Contains((byObj as Character)))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            return base.CanBeSee(byObj);
        }

        public void SetInteractiveState(InteractiveStateEnum state)
        {
            if (state == InteractiveStateEnum.STATE_ANIMATED && !Animated)
                return;

            State = state;

            InteractiveHandler.SendStatedElementUpdatedMessage(Map.Clients, Id, Cell.Id, (int) State);
        }

        void GenerateSkills()
        {
            foreach (var skillRecord in Spawn.GetSkills())
            {
                try
                {
                    var id = InteractiveManager.Instance.PopSkillId();
                    var skill = skillRecord.GenerateSkill(id, this);

                    m_skills.Add(id, skill);
                }
                catch (Exception ex)
                {
                    logger.Error($"Cannot generate skills of spawn {Spawn.Id} interactive ({Spawn.Template}) : {ex.Message}");
                }
            }
        }

        public Skill GetSkill(int id)
        {
            Skill result;
            return !m_skills.TryGetValue(id, out result) ? null : result;
        }

        public IEnumerable<Skill> GetSkills() => m_skills.Values;

        public IEnumerable<Skill> GetEnabledSkills(Character character) => m_skills.Values.Where(entry => entry.IsEnabled(character));

        public IEnumerable<Skill> GetDisabledSkills(Character character) => m_skills.Values.Where(entry => entry is SkillHarvest && !entry.IsEnabled(character));

        public IEnumerable<InteractiveElementSkill> GetEnabledElementSkills(Character character)
            => m_skills.Values.Where(entry => entry.IsEnabled(character) && entry.SkillTemplate.ClientDisplay).Select(entry => entry.GetInteractiveElementSkill());

        public IEnumerable<InteractiveElementSkill> GetDisabledElementSkills(Character character)
            => m_skills.Values.Where(entry => entry is SkillHarvest && !entry.IsEnabled(character) && entry.SkillTemplate.ClientDisplay).Select(entry => entry.GetInteractiveElementSkill());

        public InteractiveElement GetInteractiveElement(Character character)
        {
            return new InteractiveElement(Id, Template?.Id ?? -1, GetEnabledElementSkills(character).ToArray(), GetDisabledElementSkills(character).ToArray(), true);
        }

        public InteractiveElement GetInteractiveElementWithBonus(Character character)
        {
            if (m_skills.Values.Any(x => x is ISkillWithAgeBonus))
                return new InteractiveElementWithAgeBonus(Id, Template?.Id ?? -1, GetEnabledElementSkills(character).ToArray(), GetDisabledElementSkills(character).ToArray(), true, m_skills.Values.OfType<ISkillWithAgeBonus>().Max(x => x.AgeBonus));

            return new InteractiveElement(Id, Template?.Id ?? -1, GetEnabledElementSkills(character).ToArray(), GetDisabledElementSkills(character).ToArray(), true);
        }

        public StatedElement GetStatedElement() => new StatedElement(Id, (ushort)Cell.Id, (uint)State, true);
    }
}