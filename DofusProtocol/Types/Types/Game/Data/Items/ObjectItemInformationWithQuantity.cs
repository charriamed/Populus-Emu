namespace Stump.DofusProtocol.Types
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using System.Collections.Generic;
    using Stump.Core.IO;

    [Serializable]
    public class ObjectItemInformationWithQuantity : ObjectItemMinimalInformation
    {
        public new const short Id = 387;
        public override short TypeId
        {
            get { return Id; }
        }
        public uint Quantity { get; set; }

        public ObjectItemInformationWithQuantity(ushort objectGID, ObjectEffect[] effects, uint quantity)
        {
            this.ObjectGID = objectGID;
            this.Effects = effects;
            this.Quantity = quantity;
        }

        public ObjectItemInformationWithQuantity() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteVarUInt(Quantity);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            Quantity = reader.ReadVarUInt();
        }

    }
}
