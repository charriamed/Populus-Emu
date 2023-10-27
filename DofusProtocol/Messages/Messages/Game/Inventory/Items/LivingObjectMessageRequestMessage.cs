namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using System.Collections.Generic;
    using Stump.Core.IO;

    [Serializable]
    public class LivingObjectMessageRequestMessage : Message
    {
        public const uint Id = 6066;
        public override uint MessageId
        {
            get { return Id; }
        }
        public ushort MsgId { get; set; }
        public string[] Parameters { get; set; }
        public uint LivingObject { get; set; }

        public LivingObjectMessageRequestMessage(ushort msgId, string[] parameters, uint livingObject)
        {
            this.MsgId = msgId;
            this.Parameters = parameters;
            this.LivingObject = livingObject;
        }

        public LivingObjectMessageRequestMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteVarUShort(MsgId);
            writer.WriteShort((short)Parameters.Count());
            for (var parametersIndex = 0; parametersIndex < Parameters.Count(); parametersIndex++)
            {
                writer.WriteUTF(Parameters[parametersIndex]);
            }
            writer.WriteVarUInt(LivingObject);
        }

        public override void Deserialize(IDataReader reader)
        {
            MsgId = reader.ReadVarUShort();
            var parametersCount = reader.ReadUShort();
            Parameters = new string[parametersCount];
            for (var parametersIndex = 0; parametersIndex < parametersCount; parametersIndex++)
            {
                Parameters[parametersIndex] = reader.ReadUTF();
            }
            LivingObject = reader.ReadVarUInt();
        }

    }
}
