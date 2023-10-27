using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stump.DofusProtocol.D2oClasses.Tools.Bin
{
    public class Transition
    {
        public TransitionTypeEnum Type
        {
            get;
            set;
        }

        public DirectionEnum Direction
        {
            get;
            set;
        }

        public int SkillId
        {
            get;
            set;
        }

        public string Criterion
        {
            get;
            set;
        }

        public double TransitionMapId
        {
            get;
            set;
        }

        public int Cell
        {
            get;
            set;
        }

        public double Id
        {
            get;
            set;
        }

        public Transition(int type, int direction, int skillId, string criterion, double transitionMapId, int cell, double id)
        {
            this.Type = (TransitionTypeEnum)type;
            this.Direction = (DirectionEnum)direction;
            this.SkillId = skillId;
            this.Criterion = criterion;
            this.TransitionMapId = transitionMapId;
            this.Cell = cell;
            this.Id = id;
        }

        public override string ToString()
        {
            return "Transition{_type=" + this.Type + ",_direction=" + Direction + ",_skillId=" + SkillId + ",_criterion=" + Criterion + ",_transitionMapId=" + TransitionMapId + ",_cell=" + Cell + ",_id=" + Id + "}";
        }
    }
}
