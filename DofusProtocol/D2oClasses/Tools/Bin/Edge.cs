using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stump.DofusProtocol.D2oClasses.Tools.Bin
{
    public class Edge
    {
        public Vertex From
        {
            get;
            set;
        }

        public Vertex To
        {
            get;
            set;
        }

        public List<Transition> Transitions
        {
            get;
            set;
        }

        public Edge(Vertex from, Vertex to)
        {
            From = from;
            To = to;
            Transitions = new List<Transition>();
        }

        public void AddTransition(int dir, int type, int skill, string criterion, double transitionMapId, int cell, double id)
        {
            this.Transitions.Add(new Transition(type, dir, skill, criterion, transitionMapId, cell, id));
        }


        public int CountTransitionWithValidDirections()
        {
            var count = 0;

            foreach(var transition in Transitions)
            {
                if (transition.Direction != DirectionEnum.INVALID)
                    count++;
            }

            return count;
        }

        public override string ToString()
        {
            return "Edge{_from=" + From + ",_to=" + To + ",_transitions=" + Transitions + "}";
        }
    }
}
