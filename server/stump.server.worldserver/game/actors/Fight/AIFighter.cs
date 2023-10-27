using System;
using System.Collections.Generic;
using System.Linq;
using Stump.DofusProtocol.Enums;
using Stump.Server.WorldServer.AI.Fights.Brain;
using Stump.Server.WorldServer.Game.Actors.Interfaces;
using Stump.Server.WorldServer.Game.Fights;
using Stump.Server.WorldServer.Game.Fights.Teams;
using Stump.Server.WorldServer.Game.Spells;
using Stump.Server.WorldServer.Handlers.Chat;
using Stump.Server.WorldServer.Database.Monsters;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Monsters;

namespace Stump.Server.WorldServer.Game.Actors.Fight
{
    public abstract class AIFighter : FightActor, INamedActor
    {
        protected AIFighter(FightTeam team, IEnumerable<Spell> spells)
            : base(team)
        {
            Spells = spells.ToDictionary(entry => entry.Id);
            Brain = BrainManager.Instance.GetDefaultBrain(this);
            Fight.TurnStarted += OnTurnStarted;
        }

        protected AIFighter(FightTeam team, IEnumerable<Spell> spells, int identifier, MonsterGrade template = null)
            : base(team)
        {
            Spells = new Dictionary<int, Spell>();
            if ((spells == null || spells.Count() == 0) && !(this is TaxCollectorFighter))
            {
                MonsterTemplate monsterTemplate = MonsterManager.Instance.GetTemplate(template == null ? identifier : template.MonsterId);
                if (monsterTemplate.SpellsCSV != "")
                {
                    if (monsterTemplate.SpellsCSV != null)
                    {
                        foreach (var spellChar in monsterTemplate.SpellsCSV.Split(','))
                        {
                            int spellId;

                            if (int.TryParse(spellChar, out spellId) && SpellManager.Instance.GetSpellTemplate(spellId) != null)
                            {
                                Spell spell = new Spell(SpellManager.Instance.GetSpellTemplate(Convert.ToInt32(spellChar)), (byte)(template == null ? 1 : template.GradeId));
                                Spells.Add(spell.Id, spell);
                            }
                        }
                        
                    }
                }
            }
            else
            {
                foreach (var spell in spells)
                {
                    if (!Spells.ContainsKey(spell.Id))
                        Spells.Add(spell.Id, spell);
                }
            }
            //Spells = spells.ToDictionary(entry => entry.Id);
            Brain = BrainManager.Instance.GetBrain(identifier, this);
            Fight.TurnStarted += OnTurnStarted;
        }
        public Brain Brain
        {
            get;
            protected set;
        }

        public Dictionary<int, Spell> Spells
        {
            get;
            set;
        }

        public override Spell GetSpell(int id) => Spells.ContainsKey(id) ? Spells[id] : null;

        public override bool HasSpell(int id) => Spells.ContainsKey(id);

        public abstract string Name
        {
            get;
        }

        public override bool IsReady => true;

        public virtual void OnTurnStarted(IFight fight, FightActor currentfighter)
        {
            if (!IsFighterTurn())
                return;

            PlayIA();
        }

        private void PlayIA()
        {
            try
            {
                if (CanPlay())
                    Brain.Play();
            }
            catch (Exception ex)
            {
                logger.Error("Monster {0}, AI engine failed : {1}", this, ex);

                if (Brain.DebugMode)
                    Say("My AI has just failed :s (" + ex.Message + ")");
            }
            finally
            {
                if (!Fight.AIDebugMode)
                    Fight.StopTurn();
            }
        }

        public void Say(string msg)
        {
            ChatHandler.SendChatServerMessage(Fight.Clients, this, ChatActivableChannelsEnum.CHANNEL_GLOBAL, msg);
        }
    }
}