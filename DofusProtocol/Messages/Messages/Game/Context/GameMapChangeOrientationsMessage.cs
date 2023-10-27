namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using System.Collections.Generic;
    using Stump.Core.IO;

    [Serializable]
    public class GameMapChangeOrientationsMessage : Message
    {
        public const uint Id = 6155;
        public override uint MessageId
        {
            get { return Id; }
        }
        public ActorOrientation[] Orientations { get; set; }

        public GameMapChangeOrientationsMessage(ActorOrientation[] orientations)
        {
            this.Orientations = orientations;
        }

        public GameMapChangeOrientationsMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort((short)Orientations.Count());
            for (var orientationsIndex = 0; orientationsIndex < Orientations.Count(); orientationsIndex++)
            {
                var objectToSend = Orientations[orientationsIndex];
                objectToSend.Serialize(writer);
            }
        }

        public override void Deserialize(IDataReader reader)
        {
            var orientationsCount = reader.ReadUShort();
            Orientations = new ActorOrientation[orientationsCount];
            for (var orientationsIndex = 0; orientationsIndex < orientationsCount; orientationsIndex++)
            {
                var objectToAdd = new ActorOrientation();
                objectToAdd.Deserialize(reader);
                Orientations[orientationsIndex] = objectToAdd;
            }
        }

    }
}
