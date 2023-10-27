namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class GuildMemberWarnOnConnectionStateMessage : Message
    {
        public const uint Id = 6160;
        public override uint MessageId
        {
            get { return Id; }
        }
        public bool Enable { get; set; }

        public GuildMemberWarnOnConnectionStateMessage(bool enable)
        {
            this.Enable = enable;
        }

        public GuildMemberWarnOnConnectionStateMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteBoolean(Enable);
        }

        public override void Deserialize(IDataReader reader)
        {
            Enable = reader.ReadBoolean();
        }

    }
}
