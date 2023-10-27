namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using System.Collections.Generic;
    using Stump.Core.IO;

    [Serializable]
    public class ObjectFeedMessage : Message
    {
        public const uint Id = 6290;
        public override uint MessageId
        {
            get { return Id; }
        }
        public uint ObjectUID { get; set; }
        public ObjectItemQuantity[] Meal { get; set; }

        public ObjectFeedMessage(uint objectUID, ObjectItemQuantity[] meal)
        {
            this.ObjectUID = objectUID;
            this.Meal = meal;
        }

        public ObjectFeedMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteVarUInt(ObjectUID);
            writer.WriteShort((short)Meal.Count());
            for (var mealIndex = 0; mealIndex < Meal.Count(); mealIndex++)
            {
                var objectToSend = Meal[mealIndex];
                objectToSend.Serialize(writer);
            }
        }

        public override void Deserialize(IDataReader reader)
        {
            ObjectUID = reader.ReadVarUInt();
            var mealCount = reader.ReadUShort();
            Meal = new ObjectItemQuantity[mealCount];
            for (var mealIndex = 0; mealIndex < mealCount; mealIndex++)
            {
                var objectToAdd = new ObjectItemQuantity();
                objectToAdd.Deserialize(reader);
                Meal[mealIndex] = objectToAdd;
            }
        }

    }
}
