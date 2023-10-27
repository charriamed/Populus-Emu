namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using System.Collections.Generic;
    using Stump.Core.IO;

    [Serializable]
    public class EntityTalkMessage : Message
    {
        public const uint Id = 6110;
        public override uint MessageId
        {
            get { return Id; }
        }
        public double EntityId { get; set; }
        public ushort TextId { get; set; }
        public string[] Parameters { get; set; }

        public EntityTalkMessage(double entityId, ushort textId, string[] parameters)
        {
            this.EntityId = entityId;
            this.TextId = textId;
            this.Parameters = parameters;
        }

        public EntityTalkMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteDouble(EntityId);
            writer.WriteVarUShort(TextId);
            writer.WriteShort((short)Parameters.Count());
            for (var parametersIndex = 0; parametersIndex < Parameters.Count(); parametersIndex++)
            {
                writer.WriteUTF(Parameters[parametersIndex]);
            }
        }

        public override void Deserialize(IDataReader reader)
        {
            EntityId = reader.ReadDouble();
            TextId = reader.ReadVarUShort();
            var parametersCount = reader.ReadUShort();
            Parameters = new string[parametersCount];
            for (var parametersIndex = 0; parametersIndex < parametersCount; parametersIndex++)
            {
                Parameters[parametersIndex] = reader.ReadUTF();
            }
        }

    }
}
