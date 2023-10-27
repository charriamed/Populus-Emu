using NLog;
using Stump.ORM;
using Stump.ORM.SubSonic.SQLGeneration.Schema;
using Stump.Server.BaseServer.Database;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Characters;
using Stump.Server.WorldServer.Game.Conditions;
using Stump.Server.WorldServer.Game.Interactives;
using Stump.Server.WorldServer.Game.Interactives.Skills;

namespace Stump.Server.WorldServer.Database.Interactives
{
    public class InteractiveSkillRelator
    {
        public static string FetchQuery = "SELECT * FROM interactives_skills";
    }

    [TableName("interactives_skills")]
    public class InteractiveCustomSkillRecord : ParameterizableRecord, IAutoGeneratedRecord, ISkillRecord
    {

        private static Logger logger = LogManager.GetCurrentClassLogger();

        public InteractiveCustomSkillRecord()
        {
        }

        [PrimaryKey("Id")]
        public int Id
        {
            get;
            set;
        }

        public string Type
        {
            get;
            set;
        }

        // note : not used at the moment
        public int Duration
        {
            get;
            set;
        }

        #region Condition

        private ConditionExpression m_conditionExpression;

        [NullString]
        public string Condition
        {
            get;
            set;
        }

        [Ignore]
        public ConditionExpression ConditionExpression
        {
            get
            {
                if (string.IsNullOrEmpty(Condition) || Condition == "null")
                    return null;

                return m_conditionExpression ?? (m_conditionExpression = ConditionExpression.Parse(Condition));
            }
            set
            {
                m_conditionExpression = value;
                Condition = value.ToString();
            }
        }

        public int? CustomTemplateId
        {
            get;
            set;
        }

        [Ignore]
        public InteractiveSkillTemplate Template => CustomTemplateId.HasValue
            ? InteractiveManager.Instance.GetSkillTemplate(CustomTemplateId.Value)
            : InteractiveManager.Instance.GetDefaultSkillTemplate();

        public bool AreConditionsFilled(Character character)
        {
            return ConditionExpression == null || ConditionExpression.Eval(character);
        }

        #endregion

        public virtual Skill GenerateSkill(int id, InteractiveObject interactiveObject)
        {
            if (Type.Equals("template", System.StringComparison.CurrentCultureIgnoreCase))
            {
                if (Template == null)
                    logger.Error($"Skill {Id} has type 'Template' but CustomTemplateId is invalid");
                else
                    return Template.GenerateSkill(id, interactiveObject);
            }


            return DiscriminatorManager<Skill>.Instance.Generate(Type, id, this, interactiveObject);
        }
    }
}