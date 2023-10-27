namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class GameFightTurnFinishMessage : Message
    {
        public const uint Id = 718;
        public override uint MessageId
        {
            get { return Id; }
        }
        public bool IsAfk { get; set; }

        public GameFightTurnFinishMessage(bool isAfk)
        {
            this.IsAfk = isAfk;
        }

        public GameFightTurnFinishMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteBoolean(IsAfk);
        }

        public override void Deserialize(IDataReader reader)
        {
            IsAfk = reader.ReadBoolean();
        }

    }
}
