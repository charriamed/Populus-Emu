namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class JobExperienceOtherPlayerUpdateMessage : JobExperienceUpdateMessage
    {
        public new const uint Id = 6599;
        public override uint MessageId
        {
            get { return Id; }
        }
        public ulong PlayerId { get; set; }

        public JobExperienceOtherPlayerUpdateMessage(JobExperience experiencesUpdate, ulong playerId)
        {
            this.ExperiencesUpdate = experiencesUpdate;
            this.PlayerId = playerId;
        }

        public JobExperienceOtherPlayerUpdateMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteVarULong(PlayerId);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            PlayerId = reader.ReadVarULong();
        }

    }
}
