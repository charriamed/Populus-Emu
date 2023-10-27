namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class GameFightOptionStateUpdateMessage : Message
    {
        public const uint Id = 5927;
        public override uint MessageId
        {
            get { return Id; }
        }
        public ushort FightId { get; set; }
        public sbyte TeamId { get; set; }
        public sbyte Option { get; set; }
        public bool State { get; set; }

        public GameFightOptionStateUpdateMessage(ushort fightId, sbyte teamId, sbyte option, bool state)
        {
            this.FightId = fightId;
            this.TeamId = teamId;
            this.Option = option;
            this.State = state;
        }

        public GameFightOptionStateUpdateMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteVarUShort(FightId);
            writer.WriteSByte(TeamId);
            writer.WriteSByte(Option);
            writer.WriteBoolean(State);
        }

        public override void Deserialize(IDataReader reader)
        {
            FightId = reader.ReadVarUShort();
            TeamId = reader.ReadSByte();
            Option = reader.ReadSByte();
            State = reader.ReadBoolean();
        }

    }
}
