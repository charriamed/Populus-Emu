namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class GameContextMoveElementMessage : Message
    {
        public const uint Id = 253;
        public override uint MessageId
        {
            get { return Id; }
        }
        public EntityMovementInformations Movement { get; set; }

        public GameContextMoveElementMessage(EntityMovementInformations movement)
        {
            this.Movement = movement;
        }

        public GameContextMoveElementMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            Movement.Serialize(writer);
        }

        public override void Deserialize(IDataReader reader)
        {
            Movement = new EntityMovementInformations();
            Movement.Deserialize(reader);
        }

    }
}
