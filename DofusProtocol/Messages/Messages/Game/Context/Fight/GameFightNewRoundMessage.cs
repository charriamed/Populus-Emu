namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class GameFightNewRoundMessage : Message
    {
        public const uint Id = 6239;
        public override uint MessageId
        {
            get { return Id; }
        }
        public uint RoundNumber { get; set; }

        public GameFightNewRoundMessage(uint roundNumber)
        {
            this.RoundNumber = roundNumber;
        }

        public GameFightNewRoundMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteVarUInt(RoundNumber);
        }

        public override void Deserialize(IDataReader reader)
        {
            RoundNumber = reader.ReadVarUInt();
        }

    }
}
