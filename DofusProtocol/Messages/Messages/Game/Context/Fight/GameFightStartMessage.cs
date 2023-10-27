namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using System.Collections.Generic;
    using Stump.Core.IO;

    [Serializable]
    public class GameFightStartMessage : Message
    {
        public const uint Id = 712;
        public override uint MessageId
        {
            get { return Id; }
        }
        public Idol[] Idols { get; set; }

        public GameFightStartMessage(Idol[] idols)
        {
            this.Idols = idols;
        }

        public GameFightStartMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort((short)Idols.Count());
            for (var idolsIndex = 0; idolsIndex < Idols.Count(); idolsIndex++)
            {
                var objectToSend = Idols[idolsIndex];
                objectToSend.Serialize(writer);
            }
        }

        public override void Deserialize(IDataReader reader)
        {
            var idolsCount = reader.ReadUShort();
            Idols = new Idol[idolsCount];
            for (var idolsIndex = 0; idolsIndex < idolsCount; idolsIndex++)
            {
                var objectToAdd = new Idol();
                objectToAdd.Deserialize(reader);
                Idols[idolsIndex] = objectToAdd;
            }
        }

    }
}
