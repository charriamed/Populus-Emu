namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using System.Collections.Generic;
    using Stump.Core.IO;

    [Serializable]
    public class TextInformationMessage : Message
    {
        public const uint Id = 780;
        public override uint MessageId
        {
            get { return Id; }
        }
        public sbyte MsgType { get; set; }
        public ushort MsgId { get; set; }
        public string[] Parameters { get; set; }

        public TextInformationMessage(sbyte msgType, ushort msgId, string[] parameters)
        {
            this.MsgType = msgType;
            this.MsgId = msgId;
            this.Parameters = parameters;
        }

        public TextInformationMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteSByte(MsgType);
            writer.WriteVarUShort(MsgId);
            writer.WriteShort((short)Parameters.Count());
            for (var parametersIndex = 0; parametersIndex < Parameters.Count(); parametersIndex++)
            {
                writer.WriteUTF(Parameters[parametersIndex]);
            }
        }

        public override void Deserialize(IDataReader reader)
        {
            MsgType = reader.ReadSByte();
            MsgId = reader.ReadVarUShort();
            var parametersCount = reader.ReadUShort();
            Parameters = new string[parametersCount];
            for (var parametersIndex = 0; parametersIndex < parametersCount; parametersIndex++)
            {
                Parameters[parametersIndex] = reader.ReadUTF();
            }
        }

    }
}
