﻿namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using System.Collections.Generic;
    using Stump.Core.IO;

    [Serializable]
    public class ExchangeMountsPaddockRemoveMessage : Message
    {
        public const uint Id = 6559;
        public override uint MessageId
        {
            get { return Id; }
        }
        public int[] MountsId { get; set; }

        public ExchangeMountsPaddockRemoveMessage(int[] mountsId)
        {
            this.MountsId = mountsId;
        }

        public ExchangeMountsPaddockRemoveMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort((short)MountsId.Count());
            for (var mountsIdIndex = 0; mountsIdIndex < MountsId.Count(); mountsIdIndex++)
            {
                writer.WriteVarInt(MountsId[mountsIdIndex]);
            }
        }

        public override void Deserialize(IDataReader reader)
        {
            var mountsIdCount = reader.ReadUShort();
            MountsId = new int[mountsIdCount];
            for (var mountsIdIndex = 0; mountsIdIndex < mountsIdCount; mountsIdIndex++)
            {
                MountsId[mountsIdIndex] = reader.ReadVarInt();
            }
        }

    }
}
