namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using System.Collections.Generic;
    using Stump.Core.IO;

    [Serializable]
    public class IdolFightPreparationUpdateMessage : Message
    {
        public const uint Id = 6586;
        public override uint MessageId
        {
            get { return Id; }
        }
        public sbyte IdolSource { get; set; }
        public Idol[] Idols { get; set; }

        public IdolFightPreparationUpdateMessage(sbyte idolSource, Idol[] idols)
        {
            this.IdolSource = idolSource;
            this.Idols = idols;
        }

        public IdolFightPreparationUpdateMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteSByte(IdolSource);
            writer.WriteShort((short)Idols.Count());
            for (var idolsIndex = 0; idolsIndex < Idols.Count(); idolsIndex++)
            {
                var objectToSend = Idols[idolsIndex];
                writer.WriteShort(objectToSend.TypeId);
                objectToSend.Serialize(writer);
            }
        }

        public override void Deserialize(IDataReader reader)
        {
            IdolSource = reader.ReadSByte();
            var idolsCount = reader.ReadUShort();
            Idols = new Idol[idolsCount];
            for (var idolsIndex = 0; idolsIndex < idolsCount; idolsIndex++)
            {
                var objectToAdd = ProtocolTypeManager.GetInstance<Idol>(reader.ReadShort());
                objectToAdd.Deserialize(reader);
                Idols[idolsIndex] = objectToAdd;
            }
        }

    }
}
