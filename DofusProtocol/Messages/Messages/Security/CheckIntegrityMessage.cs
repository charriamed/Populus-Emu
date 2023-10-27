namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using System.Collections.Generic;
    using Stump.Core.IO;

    [Serializable]
    public class CheckIntegrityMessage : Message
    {
        public const uint Id = 6372;
        public override uint MessageId
        {
            get { return Id; }
        }
        public sbyte[] Data { get; set; }

        public CheckIntegrityMessage(sbyte[] data)
        {
            this.Data = data;
        }

        public CheckIntegrityMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteVarInt(Data.Count());
            for (var dataIndex = 0; dataIndex < Data.Count(); dataIndex++)
            {
                writer.WriteSByte(Data[dataIndex]);
            }
        }

        public override void Deserialize(IDataReader reader)
        {
            var dataCount = reader.ReadVarInt();
            Data = new sbyte[dataCount];
            for (var dataIndex = 0; dataIndex < dataCount; dataIndex++)
            {
                Data[dataIndex] = reader.ReadSByte();
            }
        }

    }
}
