namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using System.Collections.Generic;
    using Stump.Core.IO;

    [Serializable]
    public class EmotePlayMassiveMessage : EmotePlayAbstractMessage
    {
        public new const uint Id = 5691;
        public override uint MessageId
        {
            get { return Id; }
        }
        public double[] ActorIds { get; set; }

        public EmotePlayMassiveMessage(byte emoteId, double emoteStartTime, double[] actorIds)
        {
            this.EmoteId = emoteId;
            this.EmoteStartTime = emoteStartTime;
            this.ActorIds = actorIds;
        }

        public EmotePlayMassiveMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteShort((short)ActorIds.Count());
            for (var actorIdsIndex = 0; actorIdsIndex < ActorIds.Count(); actorIdsIndex++)
            {
                writer.WriteDouble(ActorIds[actorIdsIndex]);
            }
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            var actorIdsCount = reader.ReadUShort();
            ActorIds = new double[actorIdsCount];
            for (var actorIdsIndex = 0; actorIdsIndex < actorIdsCount; actorIdsIndex++)
            {
                ActorIds[actorIdsIndex] = reader.ReadDouble();
            }
        }

    }
}
