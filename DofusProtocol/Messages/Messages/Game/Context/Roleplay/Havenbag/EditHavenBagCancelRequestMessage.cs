namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class EditHavenBagCancelRequestMessage : Message
    {
        public const uint Id = 6619;
        public override uint MessageId
        {
            get { return Id; }
        }

        public EditHavenBagCancelRequestMessage() { }

        public override void Serialize(IDataWriter writer)
        {
        }

        public override void Deserialize(IDataReader reader)
        {
        }

    }
}
