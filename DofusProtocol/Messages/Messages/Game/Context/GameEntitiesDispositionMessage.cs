namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using System.Collections.Generic;
    using Stump.Core.IO;

    [Serializable]
    public class GameEntitiesDispositionMessage : Message
    {
        public const uint Id = 5696;
        public override uint MessageId
        {
            get { return Id; }
        }
        public IdentifiedEntityDispositionInformations[] Dispositions { get; set; }

        public GameEntitiesDispositionMessage(IdentifiedEntityDispositionInformations[] dispositions)
        {
            this.Dispositions = dispositions;
        }

        public GameEntitiesDispositionMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort((short)Dispositions.Count());
            for (var dispositionsIndex = 0; dispositionsIndex < Dispositions.Count(); dispositionsIndex++)
            {
                var objectToSend = Dispositions[dispositionsIndex];
                objectToSend.Serialize(writer);
            }
        }

        public override void Deserialize(IDataReader reader)
        {
            var dispositionsCount = reader.ReadUShort();
            Dispositions = new IdentifiedEntityDispositionInformations[dispositionsCount];
            for (var dispositionsIndex = 0; dispositionsIndex < dispositionsCount; dispositionsIndex++)
            {
                var objectToAdd = new IdentifiedEntityDispositionInformations();
                objectToAdd.Deserialize(reader);
                Dispositions[dispositionsIndex] = objectToAdd;
            }
        }

    }
}
