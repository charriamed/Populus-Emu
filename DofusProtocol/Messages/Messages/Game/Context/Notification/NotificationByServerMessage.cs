namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using System.Collections.Generic;
    using Stump.Core.IO;

    [Serializable]
    public class NotificationByServerMessage : Message
    {
        public const uint Id = 6103;
        public override uint MessageId
        {
            get { return Id; }
        }
        public ushort ObjectId { get; set; }
        public string[] Parameters { get; set; }
        public bool ForceOpen { get; set; }

        public NotificationByServerMessage(ushort objectId, string[] parameters, bool forceOpen)
        {
            this.ObjectId = objectId;
            this.Parameters = parameters;
            this.ForceOpen = forceOpen;
        }

        public NotificationByServerMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteVarUShort(ObjectId);
            writer.WriteShort((short)Parameters.Count());
            for (var parametersIndex = 0; parametersIndex < Parameters.Count(); parametersIndex++)
            {
                writer.WriteUTF(Parameters[parametersIndex]);
            }
            writer.WriteBoolean(ForceOpen);
        }

        public override void Deserialize(IDataReader reader)
        {
            ObjectId = reader.ReadVarUShort();
            var parametersCount = reader.ReadUShort();
            Parameters = new string[parametersCount];
            for (var parametersIndex = 0; parametersIndex < parametersCount; parametersIndex++)
            {
                Parameters[parametersIndex] = reader.ReadUTF();
            }
            ForceOpen = reader.ReadBoolean();
        }

    }
}
