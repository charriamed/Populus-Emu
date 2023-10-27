namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class GameFightOptionToggleMessage : Message
    {
        public const uint Id = 707;
        public override uint MessageId
        {
            get { return Id; }
        }
        public sbyte Option { get; set; }

        public GameFightOptionToggleMessage(sbyte option)
        {
            this.Option = option;
        }

        public GameFightOptionToggleMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteSByte(Option);
        }

        public override void Deserialize(IDataReader reader)
        {
            Option = reader.ReadSByte();
        }

    }
}
