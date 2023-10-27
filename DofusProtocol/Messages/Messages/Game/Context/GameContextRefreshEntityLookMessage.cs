namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class GameContextRefreshEntityLookMessage : Message
    {
        public const uint Id = 5637;
        public override uint MessageId
        {
            get { return Id; }
        }
        public double ObjectId { get; set; }
        public EntityLook Look { get; set; }

        public GameContextRefreshEntityLookMessage(double objectId, EntityLook look)
        {
            this.ObjectId = objectId;
            this.Look = look;
        }

        public GameContextRefreshEntityLookMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteDouble(ObjectId);
            Look.Serialize(writer);
        }

        public override void Deserialize(IDataReader reader)
        {
            ObjectId = reader.ReadDouble();
            Look = new EntityLook();
            Look.Deserialize(reader);
        }

    }
}
