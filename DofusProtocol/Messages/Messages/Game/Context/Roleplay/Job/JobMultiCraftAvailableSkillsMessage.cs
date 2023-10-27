namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using System.Collections.Generic;
    using Stump.Core.IO;

    [Serializable]
    public class JobMultiCraftAvailableSkillsMessage : JobAllowMultiCraftRequestMessage
    {
        public new const uint Id = 5747;
        public override uint MessageId
        {
            get { return Id; }
        }
        public ulong PlayerId { get; set; }
        public ushort[] Skills { get; set; }

        public JobMultiCraftAvailableSkillsMessage(bool enabled, ulong playerId, ushort[] skills)
        {
            this.Enabled = enabled;
            this.PlayerId = playerId;
            this.Skills = skills;
        }

        public JobMultiCraftAvailableSkillsMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteVarULong(PlayerId);
            writer.WriteShort((short)Skills.Count());
            for (var skillsIndex = 0; skillsIndex < Skills.Count(); skillsIndex++)
            {
                writer.WriteVarUShort(Skills[skillsIndex]);
            }
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            PlayerId = reader.ReadVarULong();
            var skillsCount = reader.ReadUShort();
            Skills = new ushort[skillsCount];
            for (var skillsIndex = 0; skillsIndex < skillsCount; skillsIndex++)
            {
                Skills[skillsIndex] = reader.ReadVarUShort();
            }
        }

    }
}
