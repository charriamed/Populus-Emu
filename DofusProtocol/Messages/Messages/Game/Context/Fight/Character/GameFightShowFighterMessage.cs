namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class GameFightShowFighterMessage : Message
    {
        public const uint Id = 5864;
        public override uint MessageId
        {
            get { return Id; }
        }
        public GameFightFighterInformations Informations { get; set; }

        public GameFightShowFighterMessage(GameFightFighterInformations informations)
        {
            this.Informations = informations;
        }

        public GameFightShowFighterMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort(Informations.TypeId);
            Informations.Serialize(writer);
        }

        public override void Deserialize(IDataReader reader)
        {
            Informations = ProtocolTypeManager.GetInstance<GameFightFighterInformations>(reader.ReadShort());
            Informations.Deserialize(reader);
        }

    }
}
