namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class GameFightHumanReadyStateMessage : Message
    {
        public const uint Id = 740;
        public override uint MessageId
        {
            get { return Id; }
        }
        public ulong CharacterId { get; set; }
        public bool IsReady { get; set; }

        public GameFightHumanReadyStateMessage(ulong characterId, bool isReady)
        {
            this.CharacterId = characterId;
            this.IsReady = isReady;
        }

        public GameFightHumanReadyStateMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteVarULong(CharacterId);
            writer.WriteBoolean(IsReady);
        }

        public override void Deserialize(IDataReader reader)
        {
            CharacterId = reader.ReadVarULong();
            IsReady = reader.ReadBoolean();
        }

    }
}
