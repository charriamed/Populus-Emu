namespace Stump.DofusProtocol.Types
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class GameContextActorInformations
    {
        public const short Id  = 150;
        public virtual short TypeId
        {
            get { return Id; }
        }
        public double ContextualId { get; set; }
        public EntityLook Look { get; set; }
        public EntityDispositionInformations Disposition { get; set; }

        public GameContextActorInformations(double contextualId, EntityLook look, EntityDispositionInformations disposition)
        {
            this.ContextualId = contextualId;
            this.Look = look;
            this.Disposition = disposition;
        }

        public GameContextActorInformations() { }

        public virtual void Serialize(IDataWriter writer)
        {
            writer.WriteDouble(ContextualId);
            Look.Serialize(writer);
            writer.WriteShort(Disposition.TypeId);
            Disposition.Serialize(writer);
        }

        public virtual void Deserialize(IDataReader reader)
        {
            ContextualId = reader.ReadDouble();
            Look = new EntityLook();
            Look.Deserialize(reader);
            Disposition = ProtocolTypeManager.GetInstance<EntityDispositionInformations>(reader.ReadShort());
            Disposition.Deserialize(reader);
        }

    }
}
