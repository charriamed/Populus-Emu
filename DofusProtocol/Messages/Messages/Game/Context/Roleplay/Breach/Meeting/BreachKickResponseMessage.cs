namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class BreachKickResponseMessage : Message
    {
        public const uint Id = 6789;
        public override uint MessageId
        {
            get { return Id; }
        }
        public CharacterMinimalInformations Target { get; set; }
        public bool Kicked { get; set; }

        public BreachKickResponseMessage(CharacterMinimalInformations target, bool kicked)
        {
            this.Target = target;
            this.Kicked = kicked;
        }

        public BreachKickResponseMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            Target.Serialize(writer);
            writer.WriteBoolean(Kicked);
        }

        public override void Deserialize(IDataReader reader)
        {
            Target = new CharacterMinimalInformations();
            Target.Deserialize(reader);
            Kicked = reader.ReadBoolean();
        }

    }
}
