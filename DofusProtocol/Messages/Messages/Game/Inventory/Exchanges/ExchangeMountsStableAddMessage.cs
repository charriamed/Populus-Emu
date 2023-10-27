namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using System.Collections.Generic;
    using Stump.Core.IO;

    [Serializable]
    public class ExchangeMountsStableAddMessage : Message
    {
        public const uint Id = 6555;
        public override uint MessageId
        {
            get { return Id; }
        }
        public MountClientData[] MountDescription { get; set; }

        public ExchangeMountsStableAddMessage(MountClientData[] mountDescription)
        {
            this.MountDescription = mountDescription;
        }

        public ExchangeMountsStableAddMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort((short)MountDescription.Count());
            for (var mountDescriptionIndex = 0; mountDescriptionIndex < MountDescription.Count(); mountDescriptionIndex++)
            {
                var objectToSend = MountDescription[mountDescriptionIndex];
                objectToSend.Serialize(writer);
            }
        }

        public override void Deserialize(IDataReader reader)
        {
            var mountDescriptionCount = reader.ReadUShort();
            MountDescription = new MountClientData[mountDescriptionCount];
            for (var mountDescriptionIndex = 0; mountDescriptionIndex < mountDescriptionCount; mountDescriptionIndex++)
            {
                var objectToAdd = new MountClientData();
                objectToAdd.Deserialize(reader);
                MountDescription[mountDescriptionIndex] = objectToAdd;
            }
        }

    }
}
