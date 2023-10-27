namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using System.Collections.Generic;
    using Stump.Core.IO;

    [Serializable]
    public class ExchangeStartOkMountMessage : ExchangeStartOkMountWithOutPaddockMessage
    {
        public new const uint Id = 5979;
        public override uint MessageId
        {
            get { return Id; }
        }
        public MountClientData[] PaddockedMountsDescription { get; set; }

        public ExchangeStartOkMountMessage(MountClientData[] stabledMountsDescription, MountClientData[] paddockedMountsDescription)
        {
            this.StabledMountsDescription = stabledMountsDescription;
            this.PaddockedMountsDescription = paddockedMountsDescription;
        }

        public ExchangeStartOkMountMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteShort((short)PaddockedMountsDescription.Count());
            for (var paddockedMountsDescriptionIndex = 0; paddockedMountsDescriptionIndex < PaddockedMountsDescription.Count(); paddockedMountsDescriptionIndex++)
            {
                var objectToSend = PaddockedMountsDescription[paddockedMountsDescriptionIndex];
                objectToSend.Serialize(writer);
            }
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            var paddockedMountsDescriptionCount = reader.ReadUShort();
            PaddockedMountsDescription = new MountClientData[paddockedMountsDescriptionCount];
            for (var paddockedMountsDescriptionIndex = 0; paddockedMountsDescriptionIndex < paddockedMountsDescriptionCount; paddockedMountsDescriptionIndex++)
            {
                var objectToAdd = new MountClientData();
                objectToAdd.Deserialize(reader);
                PaddockedMountsDescription[paddockedMountsDescriptionIndex] = objectToAdd;
            }
        }

    }
}
