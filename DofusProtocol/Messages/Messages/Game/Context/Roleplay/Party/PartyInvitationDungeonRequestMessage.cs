namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class PartyInvitationDungeonRequestMessage : PartyInvitationRequestMessage
    {
        public new const uint Id = 6245;
        public override uint MessageId
        {
            get { return Id; }
        }
        public ushort DungeonId { get; set; }

        public PartyInvitationDungeonRequestMessage(string name, ushort dungeonId)
        {
            this.Name = name;
            this.DungeonId = dungeonId;
        }

        public PartyInvitationDungeonRequestMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteVarUShort(DungeonId);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            DungeonId = reader.ReadVarUShort();
        }

    }
}
