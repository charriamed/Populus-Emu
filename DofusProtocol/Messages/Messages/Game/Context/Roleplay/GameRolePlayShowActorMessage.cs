namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class GameRolePlayShowActorMessage : Message
    {
        public const uint Id = 5632;
        public override uint MessageId
        {
            get { return Id; }
        }
        public GameRolePlayActorInformations Informations { get; set; }

        public GameRolePlayShowActorMessage(GameRolePlayActorInformations informations)
        {
            this.Informations = informations;
        }

        public GameRolePlayShowActorMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort(Informations.TypeId);
            Informations.Serialize(writer);
        }

        public override void Deserialize(IDataReader reader)
        {
            Informations = ProtocolTypeManager.GetInstance<GameRolePlayActorInformations>(reader.ReadShort());
            Informations.Deserialize(reader);
        }

    }
}
