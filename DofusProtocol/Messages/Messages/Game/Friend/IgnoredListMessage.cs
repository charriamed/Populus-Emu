namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using System.Collections.Generic;
    using Stump.Core.IO;

    [Serializable]
    public class IgnoredListMessage : Message
    {
        public const uint Id = 5674;
        public override uint MessageId
        {
            get { return Id; }
        }
        public IgnoredInformations[] IgnoredList { get; set; }

        public IgnoredListMessage(IgnoredInformations[] ignoredList)
        {
            this.IgnoredList = ignoredList;
        }

        public IgnoredListMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort((short)IgnoredList.Count());
            for (var ignoredListIndex = 0; ignoredListIndex < IgnoredList.Count(); ignoredListIndex++)
            {
                var objectToSend = IgnoredList[ignoredListIndex];
                writer.WriteShort(objectToSend.TypeId);
                objectToSend.Serialize(writer);
            }
        }

        public override void Deserialize(IDataReader reader)
        {
            var ignoredListCount = reader.ReadUShort();
            IgnoredList = new IgnoredInformations[ignoredListCount];
            for (var ignoredListIndex = 0; ignoredListIndex < ignoredListCount; ignoredListIndex++)
            {
                var objectToAdd = ProtocolTypeManager.GetInstance<IgnoredInformations>(reader.ReadShort());
                objectToAdd.Deserialize(reader);
                IgnoredList[ignoredListIndex] = objectToAdd;
            }
        }

    }
}
