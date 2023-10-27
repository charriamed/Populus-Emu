namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class ResetCharacterStatsRequestMessage : Message
    {
        public const uint Id = 6739;
        public override uint MessageId
        {
            get { return Id; }
        }

        public ResetCharacterStatsRequestMessage() { }

        public override void Serialize(IDataWriter writer)
        {
        }

        public override void Deserialize(IDataReader reader)
        {
        }

    }
}
