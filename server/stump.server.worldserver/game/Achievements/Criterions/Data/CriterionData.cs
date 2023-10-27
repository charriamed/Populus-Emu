using Stump.Server.WorldServer.Game.Conditions;
using System;

namespace Stump.Server.WorldServer.Game.Achievements.Criterions.Data
{
    public class CriterionData
    {
        // FIELDS
        protected ComparaisonOperatorEnum m_operator;
        protected string[] m_parameters;

        // PROPERTIES
        public ComparaisonOperatorEnum Operator
        {
            get
            {
                return this.m_operator;
            }
        }
        public string this[int index]
        {
            get
            {
                return this.m_parameters[index];
            }
        }

        // CONSTRUCTORS
        public CriterionData(ComparaisonOperatorEnum @operator)
        {
            this.m_operator = @operator;
        }
        public CriterionData(ComparaisonOperatorEnum @operator, params string[] parameters)
        {
            if (parameters == null) throw new ArgumentNullException("parameters");
            if (parameters.Length == 0) throw new ArgumentException("parameters");

            this.m_operator = @operator;
            this.m_parameters = parameters;
        }

        // METHODS
    }
}
