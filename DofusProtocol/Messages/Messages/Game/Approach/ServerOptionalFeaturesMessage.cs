namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using System.Collections.Generic;
    using Stump.Core.IO;

    [Serializable]
    public class ServerOptionalFeaturesMessage : Message
    {
        public const uint Id = 6305;
        public override uint MessageId
        {
            get { return Id; }
        }
        public byte[] Features { get; set; }

        public ServerOptionalFeaturesMessage(byte[] features)
        {
            this.Features = features;
        }

        public ServerOptionalFeaturesMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort((short)Features.Count());
            for (var featuresIndex = 0; featuresIndex < Features.Count(); featuresIndex++)
            {
                writer.WriteByte(Features[featuresIndex]);
            }
        }

        public override void Deserialize(IDataReader reader)
        {
            var featuresCount = reader.ReadUShort();
            Features = new byte[featuresCount];
            for (var featuresIndex = 0; featuresIndex < featuresCount; featuresIndex++)
            {
                Features[featuresIndex] = reader.ReadByte();
            }
        }

    }
}
