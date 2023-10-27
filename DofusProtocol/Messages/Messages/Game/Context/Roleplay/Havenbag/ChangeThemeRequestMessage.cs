namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class ChangeThemeRequestMessage : Message
    {
        public const uint Id = 6639;
        public override uint MessageId
        {
            get { return Id; }
        }
        public sbyte Theme { get; set; }

        public ChangeThemeRequestMessage(sbyte theme)
        {
            this.Theme = theme;
        }

        public ChangeThemeRequestMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteSByte(Theme);
        }

        public override void Deserialize(IDataReader reader)
        {
            Theme = reader.ReadSByte();
        }

    }
}
