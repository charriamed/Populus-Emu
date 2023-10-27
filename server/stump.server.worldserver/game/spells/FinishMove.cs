using Stump.DofusProtocol.Types;
using Stump.Server.WorldServer.Database.Spells;

namespace Stump.Server.WorldServer.Game.Spells
{
    public class FinishMove
    {
        public FinishMove(int finishMove, bool state)
        {
            Record = SpellManager.Instance.GetFinishMove(finishMove);
            State = state;
        }

        public FinishMoveTemplate Record
        {
            get;
            private set;
        }

        public int Id
        {
            get { return Record.Id; }
        }

        public SpellLevelTemplate FinishSpell
        {
            get { return Record.FinishSpell; }
        }

        public bool State
        {
            get;
            set;
        }

        public FinishMoveInformations GetInformations()
        {
            return new FinishMoveInformations(Id, State);
        }
    }
}
