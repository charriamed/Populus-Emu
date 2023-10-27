namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class CurrentMapInstanceMessage : CurrentMapMessage
    {
        public new const uint Id = 6738;
        public override uint MessageId
        {
            get { return Id; }
        }
        public double InstantiatedMapId { get; set; }

        public CurrentMapInstanceMessage(double mapId, string mapKey, double instantiatedMapId)
        {
            this.MapId = mapId;
            this.MapKey = mapKey;
            this.InstantiatedMapId = instantiatedMapId;
        }

        public CurrentMapInstanceMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteDouble(InstantiatedMapId);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            InstantiatedMapId = reader.ReadDouble();
        }

    }
}
