namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class MimicryObjectPreviewMessage : Message
    {
        public const uint Id = 6458;
        public override uint MessageId
        {
            get { return Id; }
        }
        public ObjectItem Result { get; set; }

        public MimicryObjectPreviewMessage(ObjectItem result)
        {
            this.Result = result;
        }

        public MimicryObjectPreviewMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            Result.Serialize(writer);
        }

        public override void Deserialize(IDataReader reader)
        {
            Result = new ObjectItem();
            Result.Deserialize(reader);
        }

    }
}
