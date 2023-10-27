namespace Stump.DofusProtocol.Types
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using System.Collections.Generic;
    using Stump.Core.IO;

    [Serializable]
    public class AllianceInsiderPrismInformation : PrismInformation
    {
        public new const short Id = 431;
        public override short TypeId
        {
            get { return Id; }
        }
        public int LastTimeSlotModificationDate { get; set; }
        public uint LastTimeSlotModificationAuthorGuildId { get; set; }
        public ulong LastTimeSlotModificationAuthorId { get; set; }
        public string LastTimeSlotModificationAuthorName { get; set; }
        public ObjectItem[] ModulesObjects { get; set; }

        public AllianceInsiderPrismInformation(sbyte @typeId, sbyte state, int nextVulnerabilityDate, int placementDate, uint rewardTokenCount, int lastTimeSlotModificationDate, uint lastTimeSlotModificationAuthorGuildId, ulong lastTimeSlotModificationAuthorId, string lastTimeSlotModificationAuthorName, ObjectItem[] modulesObjects)
        {
            this.@typeId = @typeId;
            this.State = state;
            this.NextVulnerabilityDate = nextVulnerabilityDate;
            this.PlacementDate = placementDate;
            this.RewardTokenCount = rewardTokenCount;
            this.LastTimeSlotModificationDate = lastTimeSlotModificationDate;
            this.LastTimeSlotModificationAuthorGuildId = lastTimeSlotModificationAuthorGuildId;
            this.LastTimeSlotModificationAuthorId = lastTimeSlotModificationAuthorId;
            this.LastTimeSlotModificationAuthorName = lastTimeSlotModificationAuthorName;
            this.ModulesObjects = modulesObjects;
        }

        public AllianceInsiderPrismInformation() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteInt(LastTimeSlotModificationDate);
            writer.WriteVarUInt(LastTimeSlotModificationAuthorGuildId);
            writer.WriteVarULong(LastTimeSlotModificationAuthorId);
            writer.WriteUTF(LastTimeSlotModificationAuthorName);
            writer.WriteShort((short)ModulesObjects.Count());
            for (var modulesObjectsIndex = 0; modulesObjectsIndex < ModulesObjects.Count(); modulesObjectsIndex++)
            {
                var objectToSend = ModulesObjects[modulesObjectsIndex];
                objectToSend.Serialize(writer);
            }
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            LastTimeSlotModificationDate = reader.ReadInt();
            LastTimeSlotModificationAuthorGuildId = reader.ReadVarUInt();
            LastTimeSlotModificationAuthorId = reader.ReadVarULong();
            LastTimeSlotModificationAuthorName = reader.ReadUTF();
            var modulesObjectsCount = reader.ReadUShort();
            ModulesObjects = new ObjectItem[modulesObjectsCount];
            for (var modulesObjectsIndex = 0; modulesObjectsIndex < modulesObjectsCount; modulesObjectsIndex++)
            {
                var objectToAdd = new ObjectItem();
                objectToAdd.Deserialize(reader);
                ModulesObjects[modulesObjectsIndex] = objectToAdd;
            }
        }

    }
}
