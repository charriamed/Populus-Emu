namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using System.Collections.Generic;
    using Stump.Core.IO;

    [Serializable]
    public class UpdateMountCharacteristicsMessage : Message
    {
        public const uint Id = 6753;
        public override uint MessageId
        {
            get { return Id; }
        }
        public int RideId { get; set; }
        public UpdateMountCharacteristic[] BoostToUpdateList { get; set; }

        public UpdateMountCharacteristicsMessage(int rideId, UpdateMountCharacteristic[] boostToUpdateList)
        {
            this.RideId = rideId;
            this.BoostToUpdateList = boostToUpdateList;
        }

        public UpdateMountCharacteristicsMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteVarInt(RideId);
            writer.WriteShort((short)BoostToUpdateList.Count());
            for (var boostToUpdateListIndex = 0; boostToUpdateListIndex < BoostToUpdateList.Count(); boostToUpdateListIndex++)
            {
                var objectToSend = BoostToUpdateList[boostToUpdateListIndex];
                writer.WriteShort(objectToSend.TypeId);
                objectToSend.Serialize(writer);
            }
        }

        public override void Deserialize(IDataReader reader)
        {
            RideId = reader.ReadVarInt();
            var boostToUpdateListCount = reader.ReadUShort();
            BoostToUpdateList = new UpdateMountCharacteristic[boostToUpdateListCount];
            for (var boostToUpdateListIndex = 0; boostToUpdateListIndex < boostToUpdateListCount; boostToUpdateListIndex++)
            {
                var objectToAdd = ProtocolTypeManager.GetInstance<UpdateMountCharacteristic>(reader.ReadShort());
                objectToAdd.Deserialize(reader);
                BoostToUpdateList[boostToUpdateListIndex] = objectToAdd;
            }
        }

    }
}
