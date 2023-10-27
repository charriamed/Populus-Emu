namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class GameFightNewWaveMessage : Message
    {
        public const uint Id = 6490;
        public override uint MessageId
        {
            get { return Id; }
        }
        public sbyte ObjectId { get; set; }
        public sbyte TeamId { get; set; }
        public short NbTurnBeforeNextWave { get; set; }

        public GameFightNewWaveMessage(sbyte objectId, sbyte teamId, short nbTurnBeforeNextWave)
        {
            this.ObjectId = objectId;
            this.TeamId = teamId;
            this.NbTurnBeforeNextWave = nbTurnBeforeNextWave;
        }

        public GameFightNewWaveMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteSByte(ObjectId);
            writer.WriteSByte(TeamId);
            writer.WriteShort(NbTurnBeforeNextWave);
        }

        public override void Deserialize(IDataReader reader)
        {
            ObjectId = reader.ReadSByte();
            TeamId = reader.ReadSByte();
            NbTurnBeforeNextWave = reader.ReadShort();
        }

    }
}
