namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class GameRolePlayPlayerLifeStatusMessage : Message
    {
        public const uint Id = 5996;
        public override uint MessageId
        {
            get { return Id; }
        }
        public sbyte State { get; set; }
        public double PhenixMapId { get; set; }

        public GameRolePlayPlayerLifeStatusMessage(sbyte state, double phenixMapId)
        {
            this.State = state;
            this.PhenixMapId = phenixMapId;
        }

        public GameRolePlayPlayerLifeStatusMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteSByte(State);
            writer.WriteDouble(PhenixMapId);
        }

        public override void Deserialize(IDataReader reader)
        {
            State = reader.ReadSByte();
            PhenixMapId = reader.ReadDouble();
        }

    }
}
