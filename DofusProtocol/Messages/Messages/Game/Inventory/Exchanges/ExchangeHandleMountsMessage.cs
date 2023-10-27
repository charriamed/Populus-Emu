namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using System.Collections.Generic;
    using Stump.Core.IO;

    [Serializable]
    public class ExchangeHandleMountsMessage : Message
    {
        public const uint Id = 6752;
        public override uint MessageId
        {
            get { return Id; }
        }
        public sbyte ActionType { get; set; }
        public uint[] RidesId { get; set; }

        public ExchangeHandleMountsMessage(sbyte actionType, uint[] ridesId)
        {
            this.ActionType = actionType;
            this.RidesId = ridesId;
        }

        public ExchangeHandleMountsMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteSByte(ActionType);
            writer.WriteShort((short)RidesId.Count());
            for (var ridesIdIndex = 0; ridesIdIndex < RidesId.Count(); ridesIdIndex++)
            {
                writer.WriteVarUInt(RidesId[ridesIdIndex]);
            }
        }

        public override void Deserialize(IDataReader reader)
        {
            ActionType = reader.ReadSByte();
            var ridesIdCount = reader.ReadUShort();
            RidesId = new uint[ridesIdCount];
            for (var ridesIdIndex = 0; ridesIdIndex < ridesIdCount; ridesIdIndex++)
            {
                RidesId[ridesIdIndex] = reader.ReadVarUInt();
            }
        }

    }
}
