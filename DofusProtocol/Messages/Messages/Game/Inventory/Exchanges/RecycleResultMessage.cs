namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class RecycleResultMessage : Message
    {
        public const uint Id = 6601;
        public override uint MessageId
        {
            get { return Id; }
        }
        public uint NuggetsForPrism { get; set; }
        public uint NuggetsForPlayer { get; set; }

        public RecycleResultMessage(uint nuggetsForPrism, uint nuggetsForPlayer)
        {
            this.NuggetsForPrism = nuggetsForPrism;
            this.NuggetsForPlayer = nuggetsForPlayer;
        }

        public RecycleResultMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteVarUInt(NuggetsForPrism);
            writer.WriteVarUInt(NuggetsForPlayer);
        }

        public override void Deserialize(IDataReader reader)
        {
            NuggetsForPrism = reader.ReadVarUInt();
            NuggetsForPlayer = reader.ReadVarUInt();
        }

    }
}
