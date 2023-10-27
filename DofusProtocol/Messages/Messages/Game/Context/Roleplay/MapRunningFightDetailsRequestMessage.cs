namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class MapRunningFightDetailsRequestMessage : Message
    {
        public const uint Id = 5750;
        public override uint MessageId
        {
            get { return Id; }
        }
        public ushort FightId { get; set; }

        public MapRunningFightDetailsRequestMessage(ushort fightId)
        {
            this.FightId = fightId;
        }

        public MapRunningFightDetailsRequestMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteVarUShort(FightId);
        }

        public override void Deserialize(IDataReader reader)
        {
            FightId = reader.ReadVarUShort();
        }

    }
}
