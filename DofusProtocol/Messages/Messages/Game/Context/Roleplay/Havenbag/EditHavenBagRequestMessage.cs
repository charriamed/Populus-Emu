namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class EditHavenBagRequestMessage : Message
    {
        public const uint Id = 6626;
        public override uint MessageId
        {
            get { return Id; }
        }

        public EditHavenBagRequestMessage() { }

        public override void Serialize(IDataWriter writer)
        {
        }

        public override void Deserialize(IDataReader reader)
        {
        }

    }
}
