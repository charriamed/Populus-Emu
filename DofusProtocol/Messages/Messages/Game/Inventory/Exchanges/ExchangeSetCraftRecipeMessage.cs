namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class ExchangeSetCraftRecipeMessage : Message
    {
        public const uint Id = 6389;
        public override uint MessageId
        {
            get { return Id; }
        }
        public ushort ObjectGID { get; set; }

        public ExchangeSetCraftRecipeMessage(ushort objectGID)
        {
            this.ObjectGID = objectGID;
        }

        public ExchangeSetCraftRecipeMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteVarUShort(ObjectGID);
        }

        public override void Deserialize(IDataReader reader)
        {
            ObjectGID = reader.ReadVarUShort();
        }

    }
}
