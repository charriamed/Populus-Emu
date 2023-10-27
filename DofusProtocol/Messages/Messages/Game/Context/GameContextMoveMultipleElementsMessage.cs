namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using System.Collections.Generic;
    using Stump.Core.IO;

    [Serializable]
    public class GameContextMoveMultipleElementsMessage : Message
    {
        public const uint Id = 254;
        public override uint MessageId
        {
            get { return Id; }
        }
        public EntityMovementInformations[] Movements { get; set; }

        public GameContextMoveMultipleElementsMessage(EntityMovementInformations[] movements)
        {
            this.Movements = movements;
        }

        public GameContextMoveMultipleElementsMessage() { }

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
            Movements = new EntityMovementInformations[movementsCount];
            for (var movementsIndex = 0; movementsIndex < movementsCount; movementsIndex++)
            {
                var objectToAdd = new EntityMovementInformations();
                objectToAdd.Deserialize(reader);
                Movements[movementsIndex] = objectToAdd;
            }
        }

    }
}
