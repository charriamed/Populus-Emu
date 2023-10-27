namespace Stump.DofusProtocol.Messages
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System;
    using Stump.Core.IO;
    using Stump.DofusProtocol.Types;

    [Serializable]
    public class DareVersatileListMessage : Message
    {
        public const uint Id = 6657;
        public override uint MessageId
        {
            get { return Id; }
        }
        public IEnumerable<DareVersatileInformations> dares;

        public DareVersatileListMessage(IEnumerable<DareVersatileInformations> dares)
        {
            this.dares = dares;
        }

        public DareVersatileListMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort((short)dares.Count());
            foreach (var objectToSend in dares)
            {
                objectToSend.Serialize(writer);
            }
        }

        public override void Deserialize(IDataReader reader)
        {
            var daresCount = reader.ReadUShort();
            var dares_ = new DareVersatileInformations[daresCount];
            for (var daresIndex = 0; daresIndex < daresCount; daresIndex++)
            {
                var objectToAdd = new DareVersatileInformations();
                objectToAdd.Deserialize(reader);
                dares_[daresIndex] = objectToAdd;
            }
            dares = dares_;
        }

    }
}