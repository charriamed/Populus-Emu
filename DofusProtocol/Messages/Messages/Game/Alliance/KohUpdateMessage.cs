namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using System.Collections.Generic;
    using Stump.Core.IO;

    [Serializable]
    public class KohUpdateMessage : Message
    {
        public const uint Id = 6439;
        public override uint MessageId
        {
            get { return Id; }
        }
        public AllianceInformations[] Alliances { get; set; }
        public ushort[] AllianceNbMembers { get; set; }
        public uint[] AllianceRoundWeigth { get; set; }
        public byte[] AllianceMatchScore { get; set; }
        public BasicAllianceInformations[] AllianceMapWinners { get; set; }
        public uint AllianceMapWinnerScore { get; set; }
        public uint AllianceMapMyAllianceScore { get; set; }
        public double NextTickTime { get; set; }

        public KohUpdateMessage(AllianceInformations[] alliances, ushort[] allianceNbMembers, uint[] allianceRoundWeigth, byte[] allianceMatchScore, BasicAllianceInformations[] allianceMapWinners, uint allianceMapWinnerScore, uint allianceMapMyAllianceScore, double nextTickTime)
        {
            this.Alliances = alliances;
            this.AllianceNbMembers = allianceNbMembers;
            this.AllianceRoundWeigth = allianceRoundWeigth;
            this.AllianceMatchScore = allianceMatchScore;
            this.AllianceMapWinners = allianceMapWinners;
            this.AllianceMapWinnerScore = allianceMapWinnerScore;
            this.AllianceMapMyAllianceScore = allianceMapMyAllianceScore;
            this.NextTickTime = nextTickTime;
        }

        public KohUpdateMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort((short)Alliances.Count());
            for (var alliancesIndex = 0; alliancesIndex < Alliances.Count(); alliancesIndex++)
            {
                var objectToSend = Alliances[alliancesIndex];
                objectToSend.Serialize(writer);
            }
            writer.WriteShort((short)AllianceNbMembers.Count());
            for (var allianceNbMembersIndex = 0; allianceNbMembersIndex < AllianceNbMembers.Count(); allianceNbMembersIndex++)
            {
                writer.WriteVarUShort(AllianceNbMembers[allianceNbMembersIndex]);
            }
            writer.WriteShort((short)AllianceRoundWeigth.Count());
            for (var allianceRoundWeigthIndex = 0; allianceRoundWeigthIndex < AllianceRoundWeigth.Count(); allianceRoundWeigthIndex++)
            {
                writer.WriteVarUInt(AllianceRoundWeigth[allianceRoundWeigthIndex]);
            }
            writer.WriteShort((short)AllianceMatchScore.Count());
            for (var allianceMatchScoreIndex = 0; allianceMatchScoreIndex < AllianceMatchScore.Count(); allianceMatchScoreIndex++)
            {
                writer.WriteByte(AllianceMatchScore[allianceMatchScoreIndex]);
            }
            writer.WriteShort((short)AllianceMapWinners.Count());
            for (var allianceMapWinnersIndex = 0; allianceMapWinnersIndex < AllianceMapWinners.Count(); allianceMapWinnersIndex++)
            {
                var objectToSend = AllianceMapWinners[allianceMapWinnersIndex];
                objectToSend.Serialize(writer);
            }
            writer.WriteVarUInt(AllianceMapWinnerScore);
            writer.WriteVarUInt(AllianceMapMyAllianceScore);
            writer.WriteDouble(NextTickTime);
        }

        public override void Deserialize(IDataReader reader)
        {
            var alliancesCount = reader.ReadUShort();
            Alliances = new AllianceInformations[alliancesCount];
            for (var alliancesIndex = 0; alliancesIndex < alliancesCount; alliancesIndex++)
            {
                var objectToAdd = new AllianceInformations();
                objectToAdd.Deserialize(reader);
                Alliances[alliancesIndex] = objectToAdd;
            }
            var allianceNbMembersCount = reader.ReadUShort();
            AllianceNbMembers = new ushort[allianceNbMembersCount];
            for (var allianceNbMembersIndex = 0; allianceNbMembersIndex < allianceNbMembersCount; allianceNbMembersIndex++)
            {
                AllianceNbMembers[allianceNbMembersIndex] = reader.ReadVarUShort();
            }
            var allianceRoundWeigthCount = reader.ReadUShort();
            AllianceRoundWeigth = new uint[allianceRoundWeigthCount];
            for (var allianceRoundWeigthIndex = 0; allianceRoundWeigthIndex < allianceRoundWeigthCount; allianceRoundWeigthIndex++)
            {
                AllianceRoundWeigth[allianceRoundWeigthIndex] = reader.ReadVarUInt();
            }
            var allianceMatchScoreCount = reader.ReadUShort();
            AllianceMatchScore = new byte[allianceMatchScoreCount];
            for (var allianceMatchScoreIndex = 0; allianceMatchScoreIndex < allianceMatchScoreCount; allianceMatchScoreIndex++)
            {
                AllianceMatchScore[allianceMatchScoreIndex] = reader.ReadByte();
            }
            var allianceMapWinnersCount = reader.ReadUShort();
            AllianceMapWinners = new BasicAllianceInformations[allianceMapWinnersCount];
            for (var allianceMapWinnersIndex = 0; allianceMapWinnersIndex < allianceMapWinnersCount; allianceMapWinnersIndex++)
            {
                var objectToAdd = new BasicAllianceInformations();
                objectToAdd.Deserialize(reader);
                AllianceMapWinners[allianceMapWinnersIndex] = objectToAdd;
            }
            AllianceMapWinnerScore = reader.ReadVarUInt();
            AllianceMapMyAllianceScore = reader.ReadVarUInt();
            NextTickTime = reader.ReadDouble();
        }

    }
}
