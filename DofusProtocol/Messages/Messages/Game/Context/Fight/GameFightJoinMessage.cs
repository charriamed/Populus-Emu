namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class GameFightJoinMessage : Message
    {
        public const uint Id = 702;
        public override uint MessageId
        {
            get { return Id; }
        }
        public bool IsTeamPhase { get; set; }
        public bool CanBeCancelled { get; set; }
        public bool CanSayReady { get; set; }
        public bool IsFightStarted { get; set; }
        public short TimeMaxBeforeFightStart { get; set; }
        public sbyte FightType { get; set; }

        public GameFightJoinMessage(bool isTeamPhase, bool canBeCancelled, bool canSayReady, bool isFightStarted, short timeMaxBeforeFightStart, sbyte fightType)
        {
            this.IsTeamPhase = isTeamPhase;
            this.CanBeCancelled = canBeCancelled;
            this.CanSayReady = canSayReady;
            this.IsFightStarted = isFightStarted;
            this.TimeMaxBeforeFightStart = timeMaxBeforeFightStart;
            this.FightType = fightType;
        }

        public GameFightJoinMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            var flag = new byte();
            flag = BooleanByteWrapper.SetFlag(flag, 0, IsTeamPhase);
            flag = BooleanByteWrapper.SetFlag(flag, 1, CanBeCancelled);
            flag = BooleanByteWrapper.SetFlag(flag, 2, CanSayReady);
            flag = BooleanByteWrapper.SetFlag(flag, 3, IsFightStarted);
            writer.WriteByte(flag);
            writer.WriteShort(TimeMaxBeforeFightStart);
            writer.WriteSByte(FightType);
        }

        public override void Deserialize(IDataReader reader)
        {
            var flag = reader.ReadByte();
            IsTeamPhase = BooleanByteWrapper.GetFlag(flag, 0);
            CanBeCancelled = BooleanByteWrapper.GetFlag(flag, 1);
            CanSayReady = BooleanByteWrapper.GetFlag(flag, 2);
            IsFightStarted = BooleanByteWrapper.GetFlag(flag, 3);
            TimeMaxBeforeFightStart = reader.ReadShort();
            FightType = reader.ReadSByte();
        }

    }
}
