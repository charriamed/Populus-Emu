namespace Stump.DofusProtocol.Types
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using System.Collections.Generic;
    using Stump.Core.IO;

    [Serializable]
    public class TaxCollectorFightersInformation
    {
        public const short Id  = 169;
        public virtual short TypeId
        {
            get { return Id; }
        }
        public double CollectorId { get; set; }
        public CharacterMinimalPlusLookInformations[] AllyCharactersInformations { get; set; }
        public CharacterMinimalPlusLookInformations[] EnemyCharactersInformations { get; set; }

        public TaxCollectorFightersInformation(double collectorId, CharacterMinimalPlusLookInformations[] allyCharactersInformations, CharacterMinimalPlusLookInformations[] enemyCharactersInformations)
        {
            this.CollectorId = collectorId;
            this.AllyCharactersInformations = allyCharactersInformations;
            this.EnemyCharactersInformations = enemyCharactersInformations;
        }

        public TaxCollectorFightersInformation() { }

        public virtual void Serialize(IDataWriter writer)
        {
            writer.WriteDouble(CollectorId);
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
            CollectorId = reader.ReadDouble();
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
