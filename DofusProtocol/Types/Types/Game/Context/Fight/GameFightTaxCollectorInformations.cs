namespace Stump.DofusProtocol.Types
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using System.Collections.Generic;
    using Stump.Core.IO;

    [Serializable]
    public class GameFightTaxCollectorInformations : GameFightAIInformations
    {
        public new const short Id = 48;
        public override short TypeId
        {
            get { return Id; }
        }
        public ushort FirstNameId { get; set; }
        public ushort LastNameId { get; set; }
        public byte Level { get; set; }

        public GameFightTaxCollectorInformations(double contextualId, EntityLook look, EntityDispositionInformations disposition, sbyte teamId, sbyte wave, bool alive, GameFightMinimalStats stats, ushort[] previousPositions, ushort firstNameId, ushort lastNameId, byte level)
        {
            this.ContextualId = contextualId;
            this.Look = look;
            this.Disposition = disposition;
            this.TeamId = teamId;
            this.Wave = wave;
            this.Alive = alive;
            this.Stats = stats;
            this.PreviousPositions = previousPositions;
            this.FirstNameId = firstNameId;
            this.LastNameId = lastNameId;
            this.Level = level;
        }

        public GameFightTaxCollectorInformations() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteVarUShort(FirstNameId);
            writer.WriteVarUShort(LastNameId);
            writer.WriteByte(Level);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            FirstNameId = reader.ReadVarUShort();
            LastNameId = reader.ReadVarUShort();
            Level = reader.ReadByte();
        }

    }
}
