namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class StartupActionsObjetAttributionMessage : Message
    {
        public const uint Id = 1303;
        public override uint MessageId
        {
            get { return Id; }
        }
        public int ActionId { get; set; }
        public ulong CharacterId { get; set; }

        public StartupActionsObjetAttributionMessage(int actionId, ulong characterId)
        {
            this.ActionId = actionId;
            this.CharacterId = characterId;
        }

        public StartupActionsObjetAttributionMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteInt(ActionId);
            writer.WriteVarULong(CharacterId);
        }

        public override void Deserialize(IDataReader reader)
        {
            ActionId = reader.ReadInt();
            CharacterId = reader.ReadVarULong();
        }

    }
}
