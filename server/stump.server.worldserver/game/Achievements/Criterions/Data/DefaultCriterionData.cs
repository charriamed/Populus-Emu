using Stump.Server.WorldServer.Game.Conditions;
using System;

namespace Stump.Server.WorldServer.Game.Achievements.Criterions.Data
{
    public class DefaultCriterionData : CriterionData
    {
        // FIELDS
        private int[] m_params;

        // PROPERTIES
        public int this[int offset]
        {
            get
            {
                return this.m_params[offset];
            }
        }

        // CONSTRUCTORS
        public DefaultCriterionData(ComparaisonOperatorEnum @operator, params string[] parameters)
            : base(@operator, parameters)
        {
            this.m_params = new int[parameters.Length];

            for (int i = 0; i < parameters.Length; i++)
            {
                if (int.TryParse(base[i], out this.m_params[i]))
                { }
                else if (base[i] == "d") { }
                else
                {
                    throw new Exception();
                }
            }
        }

        // METHODS
    }
}
