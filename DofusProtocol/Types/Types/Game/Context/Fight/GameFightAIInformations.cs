namespace Stump.DofusProtocol.Types
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using System.Collections.Generic;
    using Stump.Core.IO;

    [Serializable]
    public class GameFightAIInformations : GameFightFighterInformations
    {
        public new const short Id = 151;
        public override short TypeId
        {
            get { return Id; }
        }

        public GameFightAIInformations(double contextualId, EntityLook look, EntityDispositionInformations disposition, sbyte teamId, sbyte wave, bool alive, GameFightMinimalStats stats, ushort[] previousPositions)
        {
            this.ContextualId = contextualId;
            this.Look = look;
            this.Disposition = disposition;
            this.TeamId = teamId;
            this.Wave = wave;
            this.Alive = alive;
            this.Stats = stats;
            this.PreviousPositions = previousPositions;
        }

        public GameFightAIInformations() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
        }

    }
}
