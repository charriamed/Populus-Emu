namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using System.Collections.Generic;
    using Stump.Core.IO;

    [Serializable]
    public class ExchangeTypesExchangerDescriptionForUserMessage : Message
    {
        public const uint Id = 5765;
        public override uint MessageId
        {
            get { return Id; }
        }
        public uint[] TypeDescription { get; set; }

        public ExchangeTypesExchangerDescriptionForUserMessage(uint[] typeDescription)
        {
            this.TypeDescription = typeDescription;
        }

        public ExchangeTypesExchangerDescriptionForUserMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort((short)TypeDescription.Count());
            for (var typeDescriptionIndex = 0; typeDescriptionIndex < TypeDescription.Count(); typeDescriptionIndex++)
            {
                writer.WriteVarUInt(TypeDescription[typeDescriptionIndex]);
            }
        }

        public override void Deserialize(IDataReader reader)
        {
            var typeDescriptionCount = reader.ReadUShort();
            TypeDescription = new uint[typeDescriptionCount];
            for (var typeDescriptionIndex = 0; typeDescriptionIndex < typeDescriptionCount; typeDescriptionIndex++)
            {
                TypeDescription[typeDescriptionIndex] = reader.ReadVarUInt();
            }
        }

    }
}
