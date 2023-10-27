namespace Stump.DofusProtocol.Types
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using System.Collections.Generic;
    using Stump.Core.IO;

    [Serializable]
    public class PrismFightersInformation
    {
        public const short Id  = 443;
        public virtual short TypeId
        {
            get { return Id; }
        }
        public ushort SubAreaId { get; set; }
        public ProtectedEntityWaitingForHelpInfo WaitingForHelpInfo { get; set; }
        public CharacterMinimalPlusLookInformations[] AllyCharactersInformations { get; set; }
        public CharacterMinimalPlusLookInformations[] EnemyCharactersInformations { get; set; }

        public PrismFightersInformation(ushort subAreaId, ProtectedEntityWaitingForHelpInfo waitingForHelpInfo, CharacterMinimalPlusLookInformations[] allyCharactersInformations, CharacterMinimalPlusLookInformations[] enemyCharactersInformations)
        {
            this.SubAreaId = subAreaId;
            this.WaitingForHelpInfo = waitingForHelpInfo;
            this.AllyCharactersInformations = allyCharactersInformations;
            this.EnemyCharactersInformations = enemyCharactersInformations;
        }

        public PrismFightersInformation() { }

        public virtual void Serialize(IDataWriter writer)
        {
            writer.WriteVarUShort(SubAreaId);
            WaitingForHelpInfo.Serialize(writer);
            writer.WriteShort((short)AllyCharactersInformations.Count());
            for (var allyCharactersInformationsIndex = 0; allyCharactersInformationsIndex < AllyCharactersInformations.Count(); allyCharactersInformationsIndex++)
            {
                var objectToSend = AllyCharactersInformations[allyCharactersInformationsIndex];
                writer.WriteShort(objectToSend.TypeId);
                objectToSend.Serialize(writer);
            }
            writer.WriteShort((short)EnemyCharactersInformations.Count());
            for (var enemyCharactersInformationsIndex = 0; enemyCharactersInformationsIndex < EnemyCharactersInformations.Count(); enemyCharactersInformationsIndex++)
            {
                var objectToSend = EnemyCharactersInformations[enemyCharactersInformationsIndex];
                writer.WriteShort(objectToSend.TypeId);
                objectToSend.Serialize(writer);
            }
        }

        public virtual void Deserialize(IDataReader reader)
        {
            SubAreaId = reader.ReadVarUShort();
            WaitingForHelpInfo = new ProtectedEntityWaitingForHelpInfo();
            WaitingForHelpInfo.Deserialize(reader);
            var allyCharactersInformationsCount = reader.ReadUShort();
            AllyCharactersInformations = new CharacterMinimalPlusLookInformations[allyCharactersInformationsCount];
            for (var allyCharactersInformationsIndex = 0; allyCharactersInformationsIndex < allyCharactersInformationsCount; allyCharactersInformationsIndex++)
            {
                var objectToAdd = ProtocolTypeManager.GetInstance<CharacterMinimalPlusLookInformations>(reader.ReadShort());
                objectToAdd.Deserialize(reader);
                AllyCharactersInformations[allyCharactersInformationsIndex] = objectToAdd;
            }
            var enemyCharactersInformationsCount = reader.ReadUShort();
            EnemyCharactersInformations = new CharacterMinimalPlusLookInformations[enemyCharactersInformationsCount];
            for (var enemyCharactersInformationsIndex = 0; enemyCharactersInformationsIndex < enemyCharactersInformationsCount; enemyCharactersInformationsIndex++)
            {
                var objectToAdd = ProtocolTypeManager.GetInstance<CharacterMinimalPlusLookInformations>(reader.ReadShort());
                objectToAdd.Deserialize(reader);
                EnemyCharactersInformations[enemyCharactersInformationsIndex] = objectToAdd;
            }
        }

    }
}
