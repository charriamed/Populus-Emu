namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class GuildCharacsUpgradeRequestMessage : Message
    {
        public const uint Id = 5706;
        public override uint MessageId
        {
            get { return Id; }
        }
        public sbyte CharaTypeTarget { get; set; }

        public GuildCharacsUpgradeRequestMessage(sbyte charaTypeTarget)
        {
            this.CharaTypeTarget = charaTypeTarget;
        }

        public GuildCharacsUpgradeRequestMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteSByte(CharaTypeTarget);
        }

        public override void Deserialize(IDataReader reader)
        {
            CharaTypeTarget = reader.ReadSByte();
        }

    }
}
