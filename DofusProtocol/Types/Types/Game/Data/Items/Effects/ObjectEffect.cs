namespace Stump.DofusProtocol.Types
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class ObjectEffect
    {
        public const short Id  = 76;
        public virtual short TypeId
        {
            get { return Id; }
        }
        public ushort ActionId { get; set; }

        public ObjectEffect(ushort actionId)
        {
            this.ActionId = actionId;
        }

        public ObjectEffect() { }

        public virtual void Serialize(IDataWriter writer)
        {
            writer.WriteVarUShort(ActionId);
        }

        public virtual void Deserialize(IDataReader reader)
        {
            ActionId = reader.ReadVarUShort();
        }

    }
}
