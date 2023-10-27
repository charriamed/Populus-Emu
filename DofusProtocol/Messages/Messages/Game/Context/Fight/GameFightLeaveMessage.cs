namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class GameFightLeaveMessage : Message
    {
        public const uint Id = 721;
        public override uint MessageId
        {
            get { return Id; }
        }
        public double CharId { get; set; }

        public GameFightLeaveMessage(double charId)
        {
            this.CharId = charId;
        }

        public GameFightLeaveMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteDouble(CharId);
        }

        public override void Deserialize(IDataReader reader)
        {
            CharId = reader.ReadDouble();
        }

    }
}
