namespace Stump.DofusProtocol.Types
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using System.Collections.Generic;
    using Stump.Core.IO;

    [Serializable]
    public class GameRolePlayGroupMonsterWaveInformations : GameRolePlayGroupMonsterInformations
    {
        public new const short Id = 464;
        public override short TypeId
        {
            get { return Id; }
        }
        public sbyte NbWaves { get; set; }
        public GroupMonsterStaticInformations[] Alternatives { get; set; }

        public GameRolePlayGroupMonsterWaveInformations(double contextualId, EntityLook look, EntityDispositionInformations disposition, bool keyRingBonus, bool hasHardcoreDrop, bool hasAVARewardToken, GroupMonsterStaticInformations staticInfos, sbyte lootShare, sbyte alignmentSide, sbyte nbWaves, GroupMonsterStaticInformations[] alternatives)
        {
            this.ContextualId = contextualId;
            this.Look = look;
            this.Disposition = disposition;
            this.KeyRingBonus = keyRingBonus;
            this.HasHardcoreDrop = hasHardcoreDrop;
            this.HasAVARewardToken = hasAVARewardToken;
            this.StaticInfos = staticInfos;
            this.LootShare = lootShare;
            this.AlignmentSide = alignmentSide;
            this.NbWaves = nbWaves;
            this.Alternatives = alternatives;
        }

        public GameRolePlayGroupMonsterWaveInformations() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteSByte(NbWaves);
            writer.WriteShort((short)Alternatives.Count());
            for (var alternativesIndex = 0; alternativesIndex < Alternatives.Count(); alternativesIndex++)
            {
                var objectToSend = Alternatives[alternativesIndex];
                writer.WriteShort(objectToSend.TypeId);
                objectToSend.Serialize(writer);
            }
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            NbWaves = reader.ReadSByte();
            var alternativesCount = reader.ReadUShort();
            Alternatives = new GroupMonsterStaticInformations[alternativesCount];
            for (var alternativesIndex = 0; alternativesIndex < alternativesCount; alternativesIndex++)
            {
                var objectToAdd = ProtocolTypeManager.GetInstance<GroupMonsterStaticInformations>(reader.ReadShort());
                objectToAdd.Deserialize(reader);
                Alternatives[alternativesIndex] = objectToAdd;
            }
        }

    }
}
