namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class GameRolePlayArenaRegisterMessage : Message
    {
        public const uint Id = 6280;
        public override uint MessageId
        {
            get { return Id; }
        }
        public int BattleMode { get; set; }

        public GameRolePlayArenaRegisterMessage(int battleMode)
        {
            this.BattleMode = battleMode;
        }

        public GameRolePlayArenaRegisterMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteInt(BattleMode);
        }

        public override void Deserialize(IDataReader reader)
        {
            BattleMode = reader.ReadInt();
        }

    }
}
