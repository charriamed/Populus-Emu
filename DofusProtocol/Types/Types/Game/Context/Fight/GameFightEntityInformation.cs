namespace Stump.DofusProtocol.Types
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using System.Collections.Generic;
    using Stump.Core.IO;

    [Serializable]
    public class GameFightEntityInformation : GameFightFighterInformations
    {
        public new const short Id = 551;
        public override short TypeId
        {
            get { return Id; }
        }
        public sbyte EntityModelId { get; set; }
        public ushort Level { get; set; }
        public double MasterId { get; set; }

        public GameFightEntityInformation(double contextualId, EntityLook look, EntityDispositionInformations disposition, sbyte teamId, sbyte wave, bool alive, GameFightMinimalStats stats, ushort[] previousPositions, sbyte entityModelId, ushort level, double masterId)
        {
            this.ContextualId = contextualId;
            this.Look = look;
            this.Disposition = disposition;
            this.TeamId = teamId;
            this.Wave = wave;
            this.Alive = alive;
            this.Stats = stats;
            this.PreviousPositions = previousPositions;
            this.EntityModelId = entityModelId;
            this.Level = level;
            this.MasterId = masterId;
        }

        public GameFightEntityInformation() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteSByte(EntityModelId);
            writer.WriteVarUShort(Level);
            writer.WriteDouble(MasterId);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            EntityModelId = reader.ReadSByte();
            Level = reader.ReadVarUShort();
            MasterId = reader.ReadDouble();
        }

    }
}
