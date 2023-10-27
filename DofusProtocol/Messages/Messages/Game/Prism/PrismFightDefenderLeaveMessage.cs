namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class PrismFightDefenderLeaveMessage : Message
    {
        public const uint Id = 5892;
        public override uint MessageId
        {
            get { return Id; }
        }
        public ushort SubAreaId { get; set; }
        public ushort FightId { get; set; }
        public ulong FighterToRemoveId { get; set; }

        public PrismFightDefenderLeaveMessage(ushort subAreaId, ushort fightId, ulong fighterToRemoveId)
        {
            this.SubAreaId = subAreaId;
            this.FightId = fightId;
            this.FighterToRemoveId = fighterToRemoveId;
        }

        public PrismFightDefenderLeaveMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteVarUShort(SubAreaId);
            writer.WriteVarUShort(FightId);
            writer.WriteVarULong(FighterToRemoveId);
        }

        public override void Deserialize(IDataReader reader)
        {
            SubAreaId = reader.ReadVarUShort();
            FightId = reader.ReadVarUShort();
            FighterToRemoveId = reader.ReadVarULong();
        }

    }
}
