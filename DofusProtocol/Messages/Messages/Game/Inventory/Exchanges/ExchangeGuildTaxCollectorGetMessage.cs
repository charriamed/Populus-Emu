namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using System.Collections.Generic;
    using Stump.Core.IO;

    [Serializable]
    public class ExchangeGuildTaxCollectorGetMessage : Message
    {
        public const uint Id = 5762;
        public override uint MessageId
        {
            get { return Id; }
        }
        public string CollectorName { get; set; }
        public short WorldX { get; set; }
        public short WorldY { get; set; }
        public double MapId { get; set; }
        public ushort SubAreaId { get; set; }
        public string UserName { get; set; }
        public ulong CallerId { get; set; }
        public string CallerName { get; set; }
        public double Experience { get; set; }
        public ushort Pods { get; set; }
        public ObjectItemGenericQuantity[] ObjectsInfos { get; set; }

        public ExchangeGuildTaxCollectorGetMessage(string collectorName, short worldX, short worldY, double mapId, ushort subAreaId, string userName, ulong callerId, string callerName, double experience, ushort pods, ObjectItemGenericQuantity[] objectsInfos)
        {
            this.CollectorName = collectorName;
            this.WorldX = worldX;
            this.WorldY = worldY;
            this.MapId = mapId;
            this.SubAreaId = subAreaId;
            this.UserName = userName;
            this.CallerId = callerId;
            this.CallerName = callerName;
            this.Experience = experience;
            this.Pods = pods;
            this.ObjectsInfos = objectsInfos;
        }

        public ExchangeGuildTaxCollectorGetMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteUTF(CollectorName);
            writer.WriteShort(WorldX);
            writer.WriteShort(WorldY);
            writer.WriteDouble(MapId);
            writer.WriteVarUShort(SubAreaId);
            writer.WriteUTF(UserName);
            writer.WriteVarULong(CallerId);
            writer.WriteUTF(CallerName);
            writer.WriteDouble(Experience);
            writer.WriteVarUShort(Pods);
            writer.WriteShort((short)ObjectsInfos.Count());
            for (var objectsInfosIndex = 0; objectsInfosIndex < ObjectsInfos.Count(); objectsInfosIndex++)
            {
                var objectToSend = ObjectsInfos[objectsInfosIndex];
                objectToSend.Serialize(writer);
            }
        }

        public override void Deserialize(IDataReader reader)
        {
            CollectorName = reader.ReadUTF();
            WorldX = reader.ReadShort();
            WorldY = reader.ReadShort();
            MapId = reader.ReadDouble();
            SubAreaId = reader.ReadVarUShort();
            UserName = reader.ReadUTF();
            CallerId = reader.ReadVarULong();
            CallerName = reader.ReadUTF();
            Experience = reader.ReadDouble();
            Pods = reader.ReadVarUShort();
            var objectsInfosCount = reader.ReadUShort();
            ObjectsInfos = new ObjectItemGenericQuantity[objectsInfosCount];
            for (var objectsInfosIndex = 0; objectsInfosIndex < objectsInfosCount; objectsInfosIndex++)
            {
                var objectToAdd = new ObjectItemGenericQuantity();
                objectToAdd.Deserialize(reader);
                ObjectsInfos[objectsInfosIndex] = objectToAdd;
            }
        }

    }
}
