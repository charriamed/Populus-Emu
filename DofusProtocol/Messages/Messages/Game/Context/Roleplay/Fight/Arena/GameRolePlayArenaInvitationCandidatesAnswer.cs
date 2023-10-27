namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using System.Collections.Generic;
    using Stump.Core.IO;

    [Serializable]
    public class GameRolePlayArenaInvitationCandidatesAnswer : Message
    {
        public const uint Id = 6783;
        public override uint MessageId
        {
            get { return Id; }
        }
        public LeagueFriendInformations[] Candidates { get; set; }

        public GameRolePlayArenaInvitationCandidatesAnswer(LeagueFriendInformations[] candidates)
        {
            this.Candidates = candidates;
        }

        public GameRolePlayArenaInvitationCandidatesAnswer() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort((short)Candidates.Count());
            for (var candidatesIndex = 0; candidatesIndex < Candidates.Count(); candidatesIndex++)
            {
                var objectToSend = Candidates[candidatesIndex];
                objectToSend.Serialize(writer);
            }
        }

        public override void Deserialize(IDataReader reader)
        {
            var candidatesCount = reader.ReadUShort();
            Candidates = new LeagueFriendInformations[candidatesCount];
            for (var candidatesIndex = 0; candidatesIndex < candidatesCount; candidatesIndex++)
            {
                var objectToAdd = new LeagueFriendInformations();
                objectToAdd.Deserialize(reader);
                Candidates[candidatesIndex] = objectToAdd;
            }
        }

    }
}
