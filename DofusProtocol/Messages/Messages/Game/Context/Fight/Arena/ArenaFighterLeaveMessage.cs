namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class ArenaFighterLeaveMessage : Message
    {
        public const uint Id = 6700;
        public override uint MessageId
        {
            get { return Id; }
        }
        public CharacterBasicMinimalInformations Leaver { get; set; }

        public ArenaFighterLeaveMessage(CharacterBasicMinimalInformations leaver)
        {
            this.Leaver = leaver;
        }

        public ArenaFighterLeaveMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            Leaver.Serialize(writer);
        }

        public override void Deserialize(IDataReader reader)
        {
            Leaver = new CharacterBasicMinimalInformations();
            Leaver.Deserialize(reader);
        }

    }
}
