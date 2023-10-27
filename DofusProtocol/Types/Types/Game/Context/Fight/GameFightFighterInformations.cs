using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Stump.Core.IO;
namespace Stump.DofusProtocol.Types
{
    public class GameFightFighterInformations : GameContextActorInformations
    {
        public new const short Id = 143;
        public sbyte TeamId;
        public sbyte Wave;
        public bool Alive;
        public GameFightMinimalStats Stats;
        public IEnumerable<ushort> PreviousPositions;
        public override short TypeId
        {
            get { return Id; }
        }
        public GameFightFighterInformations() { }
        public GameFightFighterInformations(double contextualId, EntityLook look, EntityDispositionInformations disposition,
            sbyte teamId, sbyte wave, bool alive, GameFightMinimalStats stats, IEnumerable<ushort> previousPositions) :
            base(contextualId, look, disposition)
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
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteSByte(this.TeamId);
            writer.WriteSByte(this.Wave);
            writer.WriteBoolean((bool)this.Alive);
            writer.WriteShort(this.Stats.TypeId);
            this.Stats.Serialize(writer);
            writer.WriteShort((short)PreviousPositions.Count());
            foreach (var objectToSend in PreviousPositions)
            {
                writer.WriteVarUShort(objectToSend);
            }
        }
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            TeamId = reader.ReadSByte();
            Wave = reader.ReadSByte();
            Alive = reader.ReadBoolean();
            this.Stats = ProtocolTypeManager.GetInstance<GameFightMinimalStats>(reader.ReadShort());
            Stats.Deserialize(reader);
            var previousPositionsCount = reader.ReadUShort();
            var previousPositions_ = new ushort[previousPositionsCount];
            for (var previousPositionsIndex = 0; previousPositionsIndex < previousPositionsCount; previousPositionsIndex++)
            {
                previousPositions_[previousPositionsIndex] = reader.ReadVarUShort();
            }
            PreviousPositions = previousPositions_;
        }
    }
}