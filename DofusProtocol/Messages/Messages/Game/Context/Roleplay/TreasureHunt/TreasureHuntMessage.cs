namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using System.Collections.Generic;
    using Stump.Core.IO;

    [Serializable]
    public class TreasureHuntMessage : Message
    {
        public const uint Id = 6486;
        public override uint MessageId
        {
            get { return Id; }
        }
        public sbyte QuestType { get; set; }
        public double StartMapId { get; set; }
        public TreasureHuntStep[] KnownStepsList { get; set; }
        public sbyte TotalStepCount { get; set; }
        public uint CheckPointCurrent { get; set; }
        public uint CheckPointTotal { get; set; }
        public int AvailableRetryCount { get; set; }
        public TreasureHuntFlag[] Flags { get; set; }

        public TreasureHuntMessage(sbyte questType, double startMapId, TreasureHuntStep[] knownStepsList, sbyte totalStepCount, uint checkPointCurrent, uint checkPointTotal, int availableRetryCount, TreasureHuntFlag[] flags)
        {
            this.QuestType = questType;
            this.StartMapId = startMapId;
            this.KnownStepsList = knownStepsList;
            this.TotalStepCount = totalStepCount;
            this.CheckPointCurrent = checkPointCurrent;
            this.CheckPointTotal = checkPointTotal;
            this.AvailableRetryCount = availableRetryCount;
            this.Flags = flags;
        }

        public TreasureHuntMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteSByte(QuestType);
            writer.WriteDouble(StartMapId);
            writer.WriteShort((short)KnownStepsList.Count());
            for (var knownStepsListIndex = 0; knownStepsListIndex < KnownStepsList.Count(); knownStepsListIndex++)
            {
                var objectToSend = KnownStepsList[knownStepsListIndex];
                writer.WriteShort(objectToSend.TypeId);
                objectToSend.Serialize(writer);
            }
            writer.WriteSByte(TotalStepCount);
            writer.WriteVarUInt(CheckPointCurrent);
            writer.WriteVarUInt(CheckPointTotal);
            writer.WriteInt(AvailableRetryCount);
            writer.WriteShort((short)Flags.Count());
            for (var flagsIndex = 0; flagsIndex < Flags.Count(); flagsIndex++)
            {
                var objectToSend = Flags[flagsIndex];
                objectToSend.Serialize(writer);
            }
        }

        public override void Deserialize(IDataReader reader)
        {
            QuestType = reader.ReadSByte();
            StartMapId = reader.ReadDouble();
            var knownStepsListCount = reader.ReadUShort();
            KnownStepsList = new TreasureHuntStep[knownStepsListCount];
            for (var knownStepsListIndex = 0; knownStepsListIndex < knownStepsListCount; knownStepsListIndex++)
            {
                var objectToAdd = ProtocolTypeManager.GetInstance<TreasureHuntStep>(reader.ReadShort());
                objectToAdd.Deserialize(reader);
                KnownStepsList[knownStepsListIndex] = objectToAdd;
            }
            TotalStepCount = reader.ReadSByte();
            CheckPointCurrent = reader.ReadVarUInt();
            CheckPointTotal = reader.ReadVarUInt();
            AvailableRetryCount = reader.ReadInt();
            var flagsCount = reader.ReadUShort();
            Flags = new TreasureHuntFlag[flagsCount];
            for (var flagsIndex = 0; flagsIndex < flagsCount; flagsIndex++)
            {
                var objectToAdd = new TreasureHuntFlag();
                objectToAdd.Deserialize(reader);
                Flags[flagsIndex] = objectToAdd;
            }
        }

    }
}
