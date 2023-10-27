namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using System.Collections.Generic;
    using Stump.Core.IO;

    [Serializable]
    public class IdolListMessage : Message
    {
        public const uint Id = 6585;
        public override uint MessageId
        {
            get { return Id; }
        }
        public ushort[] ChosenIdols { get; set; }
        public ushort[] PartyChosenIdols { get; set; }
        public PartyIdol[] PartyIdols { get; set; }

        public IdolListMessage(ushort[] chosenIdols, ushort[] partyChosenIdols, PartyIdol[] partyIdols)
        {
            this.ChosenIdols = chosenIdols;
            this.PartyChosenIdols = partyChosenIdols;
            this.PartyIdols = partyIdols;
        }

        public IdolListMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort((short)ChosenIdols.Count());
            for (var chosenIdolsIndex = 0; chosenIdolsIndex < ChosenIdols.Count(); chosenIdolsIndex++)
            {
                writer.WriteVarUShort(ChosenIdols[chosenIdolsIndex]);
            }
            writer.WriteShort((short)PartyChosenIdols.Count());
            for (var partyChosenIdolsIndex = 0; partyChosenIdolsIndex < PartyChosenIdols.Count(); partyChosenIdolsIndex++)
            {
                writer.WriteVarUShort(PartyChosenIdols[partyChosenIdolsIndex]);
            }
            writer.WriteShort((short)PartyIdols.Count());
            for (var partyIdolsIndex = 0; partyIdolsIndex < PartyIdols.Count(); partyIdolsIndex++)
            {
                var objectToSend = PartyIdols[partyIdolsIndex];
                writer.WriteShort(objectToSend.TypeId);
                objectToSend.Serialize(writer);
            }
        }

        public override void Deserialize(IDataReader reader)
        {
            var chosenIdolsCount = reader.ReadUShort();
            ChosenIdols = new ushort[chosenIdolsCount];
            for (var chosenIdolsIndex = 0; chosenIdolsIndex < chosenIdolsCount; chosenIdolsIndex++)
            {
                ChosenIdols[chosenIdolsIndex] = reader.ReadVarUShort();
            }
            var partyChosenIdolsCount = reader.ReadUShort();
            PartyChosenIdols = new ushort[partyChosenIdolsCount];
            for (var partyChosenIdolsIndex = 0; partyChosenIdolsIndex < partyChosenIdolsCount; partyChosenIdolsIndex++)
            {
                PartyChosenIdols[partyChosenIdolsIndex] = reader.ReadVarUShort();
            }
            var partyIdolsCount = reader.ReadUShort();
            PartyIdols = new PartyIdol[partyIdolsCount];
            for (var partyIdolsIndex = 0; partyIdolsIndex < partyIdolsCount; partyIdolsIndex++)
            {
                var objectToAdd = ProtocolTypeManager.GetInstance<PartyIdol>(reader.ReadShort());
                objectToAdd.Deserialize(reader);
                PartyIdols[partyIdolsIndex] = objectToAdd;
            }
        }

    }
}
