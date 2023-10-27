namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using System.Collections.Generic;
    using Stump.Core.IO;

    [Serializable]
    public class DareListMessage : Message
    {
        public const uint Id = 6661;
        public override uint MessageId
        {
            get { return Id; }
        }
        public DareInformations[] Dares { get; set; }

        public DareListMessage(DareInformations[] dares)
        {
            this.Dares = dares;
        }

        public DareListMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort((short)Dares.Count());
            for (var daresIndex = 0; daresIndex < Dares.Count(); daresIndex++)
            {
                var objectToSend = Dares[daresIndex];
                objectToSend.Serialize(writer);
            }
        }

        public override void Deserialize(IDataReader reader)
        {
            var daresCount = reader.ReadUShort();
            Dares = new DareInformations[daresCount];
            for (var daresIndex = 0; daresIndex < daresCount; daresIndex++)
            {
                var objectToAdd = new DareInformations();
                objectToAdd.Deserialize(reader);
                Dares[daresIndex] = objectToAdd;
            }
        }

    }
}
