namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using System.Collections.Generic;
    using Stump.Core.IO;

    [Serializable]
    public class GameFightTurnListMessage : Message
    {
        public const uint Id = 713;
        public override uint MessageId
        {
            get { return Id; }
        }
        public double[] Ids { get; set; }
        public double[] DeadsIds { get; set; }

        public GameFightTurnListMessage(double[] ids, double[] deadsIds)
        {
            this.Ids = ids;
            this.DeadsIds = deadsIds;
        }

        public GameFightTurnListMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort((short)Ids.Count());
            for (var idsIndex = 0; idsIndex < Ids.Count(); idsIndex++)
            {
                writer.WriteDouble(Ids[idsIndex]);
            }
            writer.WriteShort((short)DeadsIds.Count());
            for (var deadsIdsIndex = 0; deadsIdsIndex < DeadsIds.Count(); deadsIdsIndex++)
            {
                writer.WriteDouble(DeadsIds[deadsIdsIndex]);
            }
        }

        public override void Deserialize(IDataReader reader)
        {
            var idsCount = reader.ReadUShort();
            Ids = new double[idsCount];
            for (var idsIndex = 0; idsIndex < idsCount; idsIndex++)
            {
                Ids[idsIndex] = reader.ReadDouble();
            }
            var deadsIdsCount = reader.ReadUShort();
            DeadsIds = new double[deadsIdsCount];
            for (var deadsIdsIndex = 0; deadsIdsIndex < deadsIdsCount; deadsIdsIndex++)
            {
                DeadsIds[deadsIdsIndex] = reader.ReadDouble();
            }
        }

    }
}
