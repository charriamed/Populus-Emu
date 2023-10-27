namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using System.Collections.Generic;
    using Stump.Core.IO;

    [Serializable]
    public class SystemMessageDisplayMessage : Message
    {
        public const uint Id = 189;
        public override uint MessageId
        {
            get { return Id; }
        }
        public bool HangUp { get; set; }
        public ushort MsgId { get; set; }
        public string[] Parameters { get; set; }

        public SystemMessageDisplayMessage(bool hangUp, ushort msgId, string[] parameters)
        {
            this.HangUp = hangUp;
            this.MsgId = msgId;
            this.Parameters = parameters;
        }

        public SystemMessageDisplayMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteBoolean(HangUp);
            writer.WriteVarUShort(MsgId);
            writer.WriteShort((short)Parameters.Count());
            for (var parametersIndex = 0; parametersIndex < Parameters.Count(); parametersIndex++)
            {
                writer.WriteUTF(Parameters[parametersIndex]);
            }
        }

        public override void Deserialize(IDataReader reader)
        {
            HangUp = reader.ReadBoolean();
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
