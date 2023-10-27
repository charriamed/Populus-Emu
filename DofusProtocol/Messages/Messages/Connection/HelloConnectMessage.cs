namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using System.Collections.Generic;
    using Stump.Core.IO;

    [Serializable]
    public class HelloConnectMessage : Message
    {
        public const uint Id = 3;
        public override uint MessageId
        {
            get { return Id; }
        }
        public string Salt { get; set; }
        public sbyte[] Key { get; set; }

        public HelloConnectMessage(string salt, sbyte[] key)
        {
            this.Salt = salt;
            this.Key = key;
        }

        public HelloConnectMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteUTF(Salt);
            writer.WriteVarInt(Key.Count());
            for (var keyIndex = 0; keyIndex < Key.Count(); keyIndex++)
            {
                writer.WriteSByte(Key[keyIndex]);
            }
        }

        public override void Deserialize(IDataReader reader)
        {
            Salt = reader.ReadUTF();
            var keyCount = reader.ReadVarInt();
            Key = new sbyte[keyCount];
            for (var keyIndex = 0; keyIndex < keyCount; keyIndex++)
            {
                Key[keyIndex] = reader.ReadSByte();
            }
        }

    }
}
