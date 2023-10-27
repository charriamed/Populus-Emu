namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using System.Collections.Generic;
    using Stump.Core.IO;

    [Serializable]
    public class AnomalySubareaInformationResponseMessage : Message
    {
        public const uint Id = 6836;
        public override uint MessageId
        {
            get { return Id; }
        }
        public AnomalySubareaInformation[] Subareas { get; set; }

        public AnomalySubareaInformationResponseMessage(AnomalySubareaInformation[] subareas)
        {
            this.Subareas = subareas;
        }

        public AnomalySubareaInformationResponseMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort((short)Subareas.Count());
            for (var subareasIndex = 0; subareasIndex < Subareas.Count(); subareasIndex++)
            {
                var objectToSend = Subareas[subareasIndex];
                objectToSend.Serialize(writer);
            }
        }

        public override void Deserialize(IDataReader reader)
        {
            var subareasCount = reader.ReadUShort();
            Subareas = new AnomalySubareaInformation[subareasCount];
            for (var subareasIndex = 0; subareasIndex < subareasCount; subareasIndex++)
            {
                var objectToAdd = new AnomalySubareaInformation();
                objectToAdd.Deserialize(reader);
                Subareas[subareasIndex] = objectToAdd;
            }
        }

    }
}
