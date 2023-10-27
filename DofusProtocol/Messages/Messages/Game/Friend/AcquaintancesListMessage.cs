namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using System.Collections.Generic;
    using Stump.Core.IO;

    [Serializable]
    public class AcquaintancesListMessage : Message
    {
        public const uint Id = 6820;
        public override uint MessageId
        {
            get { return Id; }
        }
        public AcquaintanceInformation[] AcquaintanceList { get; set; }

        public AcquaintancesListMessage(AcquaintanceInformation[] acquaintanceList)
        {
            this.AcquaintanceList = acquaintanceList;
        }

        public AcquaintancesListMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort((short)AcquaintanceList.Count());
            for (var acquaintanceListIndex = 0; acquaintanceListIndex < AcquaintanceList.Count(); acquaintanceListIndex++)
            {
                var objectToSend = AcquaintanceList[acquaintanceListIndex];
                writer.WriteShort(objectToSend.TypeId);
                objectToSend.Serialize(writer);
            }
        }

        public override void Deserialize(IDataReader reader)
        {
            var acquaintanceListCount = reader.ReadUShort();
            AcquaintanceList = new AcquaintanceInformation[acquaintanceListCount];
            for (var acquaintanceListIndex = 0; acquaintanceListIndex < acquaintanceListCount; acquaintanceListIndex++)
            {
                var objectToAdd = ProtocolTypeManager.GetInstance<AcquaintanceInformation>(reader.ReadShort());
                objectToAdd.Deserialize(reader);
                AcquaintanceList[acquaintanceListIndex] = objectToAdd;
            }
        }

    }
}
