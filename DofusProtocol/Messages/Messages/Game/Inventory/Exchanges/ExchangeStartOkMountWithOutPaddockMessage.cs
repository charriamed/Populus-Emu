namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using System.Collections.Generic;
    using Stump.Core.IO;

    [Serializable]
    public class ExchangeStartOkMountWithOutPaddockMessage : Message
    {
        public const uint Id = 5991;
        public override uint MessageId
        {
            get { return Id; }
        }
        public MountClientData[] StabledMountsDescription { get; set; }

        public ExchangeStartOkMountWithOutPaddockMessage(MountClientData[] stabledMountsDescription)
        {
            this.StabledMountsDescription = stabledMountsDescription;
        }

        public ExchangeStartOkMountWithOutPaddockMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort((short)StabledMountsDescription.Count());
            for (var stabledMountsDescriptionIndex = 0; stabledMountsDescriptionIndex < StabledMountsDescription.Count(); stabledMountsDescriptionIndex++)
            {
                var objectToSend = StabledMountsDescription[stabledMountsDescriptionIndex];
                objectToSend.Serialize(writer);
            }
        }

        public override void Deserialize(IDataReader reader)
        {
            var stabledMountsDescriptionCount = reader.ReadUShort();
            StabledMountsDescription = new MountClientData[stabledMountsDescriptionCount];
            for (var stabledMountsDescriptionIndex = 0; stabledMountsDescriptionIndex < stabledMountsDescriptionCount; stabledMountsDescriptionIndex++)
            {
                var objectToAdd = new MountClientData();
                objectToAdd.Deserialize(reader);
                StabledMountsDescription[stabledMountsDescriptionIndex] = objectToAdd;
            }
        }

    }
}
