namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using System.Collections.Generic;
    using Stump.Core.IO;

    [Serializable]
    public class TaxCollectorMovementsOfflineMessage : Message
    {
        public const uint Id = 6611;
        public override uint MessageId
        {
            get { return Id; }
        }
        public TaxCollectorMovement[] Movements { get; set; }

        public TaxCollectorMovementsOfflineMessage(TaxCollectorMovement[] movements)
        {
            this.Movements = movements;
        }

        public TaxCollectorMovementsOfflineMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort((short)Movements.Count());
            for (var movementsIndex = 0; movementsIndex < Movements.Count(); movementsIndex++)
            {
                var objectToSend = Movements[movementsIndex];
                objectToSend.Serialize(writer);
            }
        }

        public override void Deserialize(IDataReader reader)
        {
            var movementsCount = reader.ReadUShort();
            Movements = new TaxCollectorMovement[movementsCount];
            for (var movementsIndex = 0; movementsIndex < movementsCount; movementsIndex++)
            {
                var objectToAdd = new TaxCollectorMovement();
                objectToAdd.Deserialize(reader);
                Movements[movementsIndex] = objectToAdd;
            }
        }

    }
}
