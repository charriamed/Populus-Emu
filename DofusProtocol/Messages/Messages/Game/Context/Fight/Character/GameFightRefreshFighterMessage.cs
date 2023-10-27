namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class GameFightRefreshFighterMessage : Message
    {
        public const uint Id = 6309;
        public override uint MessageId
        {
            get { return Id; }
        }
        public GameContextActorInformations Informations { get; set; }

        public GameFightRefreshFighterMessage(GameContextActorInformations informations)
        {
            this.Informations = informations;
        }

        public GameFightRefreshFighterMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort(Informations.TypeId);
            Informations.Serialize(writer);
        }

        public override void Deserialize(IDataReader reader)
        {
            Informations = ProtocolTypeManager.GetInstance<GameContextActorInformations>(reader.ReadShort());
            Informations.Deserialize(reader);
        }

    }
}
