namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class GuildCreationResultMessage : Message
    {
        public const uint Id = 5554;
        public override uint MessageId
        {
            get { return Id; }
        }
        public sbyte Result { get; set; }

        public GuildCreationResultMessage(sbyte result)
        {
            this.Result = result;
        }

        public GuildCreationResultMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteSByte(Result);
        }

        public override void Deserialize(IDataReader reader)
        {
            Result = reader.ReadSByte();
        }

    }
}
