namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using System.Collections.Generic;
    using Stump.Core.IO;

    [Serializable]
    public class GameActionFightTackledMessage : AbstractGameActionMessage
    {
        public new const uint Id = 1004;
        public override uint MessageId
        {
            get { return Id; }
        }
        public double[] TacklersIds { get; set; }

        public GameActionFightTackledMessage(ushort actionId, double sourceId, double[] tacklersIds)
        {
            this.ActionId = actionId;
            this.SourceId = sourceId;
            this.TacklersIds = tacklersIds;
        }

        public GameActionFightTackledMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteShort((short)TacklersIds.Count());
            for (var tacklersIdsIndex = 0; tacklersIdsIndex < TacklersIds.Count(); tacklersIdsIndex++)
            {
                writer.WriteDouble(TacklersIds[tacklersIdsIndex]);
            }
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            var tacklersIdsCount = reader.ReadUShort();
            TacklersIds = new double[tacklersIdsCount];
            for (var tacklersIdsIndex = 0; tacklersIdsIndex < tacklersIdsCount; tacklersIdsIndex++)
            {
                TacklersIds[tacklersIdsIndex] = reader.ReadDouble();
            }
        }

    }
}
