namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class IdolsPresetSaveRequestMessage : PresetSaveRequestMessage
    {
        public new const uint Id = 6758;
        public override uint MessageId
        {
            get { return Id; }
        }

        public IdolsPresetSaveRequestMessage(short presetId, sbyte symbolId, bool updateData)
        {
            this.PresetId = presetId;
            this.SymbolId = symbolId;
            this.UpdateData = updateData;
        }

        public IdolsPresetSaveRequestMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
        }

    }
}
