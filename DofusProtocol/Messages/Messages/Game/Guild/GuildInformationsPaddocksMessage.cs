namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using System.Collections.Generic;
    using Stump.Core.IO;

    [Serializable]
    public class GuildInformationsPaddocksMessage : Message
    {
        public const uint Id = 5959;
        public override uint MessageId
        {
            get { return Id; }
        }
        public sbyte NbPaddockMax { get; set; }
        public PaddockContentInformations[] PaddocksInformations { get; set; }

        public GuildInformationsPaddocksMessage(sbyte nbPaddockMax, PaddockContentInformations[] paddocksInformations)
        {
            this.NbPaddockMax = nbPaddockMax;
            this.PaddocksInformations = paddocksInformations;
        }

        public GuildInformationsPaddocksMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteSByte(NbPaddockMax);
            writer.WriteShort((short)PaddocksInformations.Count());
            for (var paddocksInformationsIndex = 0; paddocksInformationsIndex < PaddocksInformations.Count(); paddocksInformationsIndex++)
            {
                var objectToSend = PaddocksInformations[paddocksInformationsIndex];
                objectToSend.Serialize(writer);
            }
        }

        public override void Deserialize(IDataReader reader)
        {
            NbPaddockMax = reader.ReadSByte();
            var paddocksInformationsCount = reader.ReadUShort();
            PaddocksInformations = new PaddockContentInformations[paddocksInformationsCount];
            for (var paddocksInformationsIndex = 0; paddocksInformationsIndex < paddocksInformationsCount; paddocksInformationsIndex++)
            {
                var objectToAdd = new PaddockContentInformations();
                objectToAdd.Deserialize(reader);
                PaddocksInformations[paddocksInformationsIndex] = objectToAdd;
            }
        }

    }
}
