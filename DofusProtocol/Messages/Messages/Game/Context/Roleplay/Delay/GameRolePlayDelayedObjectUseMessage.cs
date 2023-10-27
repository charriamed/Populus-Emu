namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class GameRolePlayDelayedObjectUseMessage : GameRolePlayDelayedActionMessage
    {
        public new const uint Id = 6425;
        public override uint MessageId
        {
            get { return Id; }
        }
        public ushort ObjectGID { get; set; }

        public GameRolePlayDelayedObjectUseMessage(double delayedCharacterId, sbyte delayTypeId, double delayEndTime, ushort objectGID)
        {
            this.DelayedCharacterId = delayedCharacterId;
            this.DelayTypeId = delayTypeId;
            this.DelayEndTime = delayEndTime;
            this.ObjectGID = objectGID;
        }

        public GameRolePlayDelayedObjectUseMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteVarUShort(ObjectGID);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            ObjectGID = reader.ReadVarUShort();
        }

    }
}
