namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class GameRolePlayArenaRegistrationStatusMessage : Message
    {
        public const uint Id = 6284;
        public override uint MessageId
        {
            get { return Id; }
        }
        public bool Registered { get; set; }
        public sbyte Step { get; set; }
        public int BattleMode { get; set; }

        public GameRolePlayArenaRegistrationStatusMessage(bool registered, sbyte step, int battleMode)
        {
            this.Registered = registered;
            this.Step = step;
            this.BattleMode = battleMode;
        }

        public GameRolePlayArenaRegistrationStatusMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteBoolean(Registered);
            writer.WriteSByte(Step);
            writer.WriteInt(BattleMode);
        }

        public override void Deserialize(IDataReader reader)
        {
            Registered = reader.ReadBoolean();
            Step = reader.ReadSByte();
            BattleMode = reader.ReadInt();
        }

    }
}
