namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class GameContextCreateMessage : Message
    {
        public const uint Id = 200;
        public override uint MessageId
        {
            get { return Id; }
        }
        public sbyte Context { get; set; }

        public GameContextCreateMessage(sbyte context)
        {
            this.Context = context;
        }

        public GameContextCreateMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteSByte(Context);
        }

        public override void Deserialize(IDataReader reader)
        {
            Context = reader.ReadSByte();
        }

    }
}
