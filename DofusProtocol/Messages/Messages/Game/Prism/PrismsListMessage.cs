namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using System.Collections.Generic;
    using Stump.Core.IO;

    [Serializable]
    public class PrismsListMessage : Message
    {
        public const uint Id = 6440;
        public override uint MessageId
        {
            get { return Id; }
        }
        public PrismSubareaEmptyInfo[] Prisms { get; set; }

        public PrismsListMessage(PrismSubareaEmptyInfo[] prisms)
        {
            this.Prisms = prisms;
        }

        public PrismsListMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort((short)Prisms.Count());
            for (var prismsIndex = 0; prismsIndex < Prisms.Count(); prismsIndex++)
            {
                var objectToSend = Prisms[prismsIndex];
                writer.WriteShort(objectToSend.TypeId);
                objectToSend.Serialize(writer);
            }
        }

        public override void Deserialize(IDataReader reader)
        {
            var prismsCount = reader.ReadUShort();
            Prisms = new PrismSubareaEmptyInfo[prismsCount];
            for (var prismsIndex = 0; prismsIndex < prismsCount; prismsIndex++)
            {
                var objectToAdd = ProtocolTypeManager.GetInstance<PrismSubareaEmptyInfo>(reader.ReadShort());
                objectToAdd.Deserialize(reader);
                Prisms[prismsIndex] = objectToAdd;
            }
        }

    }
}
