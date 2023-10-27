namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class MimicryObjectFeedAndAssociateRequestMessage : SymbioticObjectAssociateRequestMessage
    {
        public new const uint Id = 6460;
        public override uint MessageId
        {
            get { return Id; }
        }
        public uint FoodUID { get; set; }
        public byte FoodPos { get; set; }
        public bool Preview { get; set; }

        public MimicryObjectFeedAndAssociateRequestMessage(uint symbioteUID, byte symbiotePos, uint hostUID, byte hostPos, uint foodUID, byte foodPos, bool preview)
        {
            this.SymbioteUID = symbioteUID;
            this.SymbiotePos = symbiotePos;
            this.HostUID = hostUID;
            this.HostPos = hostPos;
            this.FoodUID = foodUID;
            this.FoodPos = foodPos;
            this.Preview = preview;
        }

        public MimicryObjectFeedAndAssociateRequestMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteVarUInt(FoodUID);
            writer.WriteByte(FoodPos);
            writer.WriteBoolean(Preview);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            FoodUID = reader.ReadVarUInt();
            FoodPos = reader.ReadByte();
            Preview = reader.ReadBoolean();
        }

    }
}
