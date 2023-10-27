namespace Stump.DofusProtocol.Types
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using System.Collections.Generic;
    using Stump.Core.IO;

    [Serializable]
    public class GameRolePlayMerchantInformations : GameRolePlayNamedActorInformations
    {
        public new const short Id = 129;
        public override short TypeId
        {
            get { return Id; }
        }
        public sbyte SellType { get; set; }
        public HumanOption[] Options { get; set; }

        public GameRolePlayMerchantInformations(double contextualId, EntityLook look, EntityDispositionInformations disposition, string name, sbyte sellType, HumanOption[] options)
        {
            this.ContextualId = contextualId;
            this.Look = look;
            this.Disposition = disposition;
            this.Name = name;
            this.SellType = sellType;
            this.Options = options;
        }

        public GameRolePlayMerchantInformations() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteSByte(SellType);
            writer.WriteShort((short)Options.Count());
            for (var optionsIndex = 0; optionsIndex < Options.Count(); optionsIndex++)
            {
                var objectToSend = Options[optionsIndex];
                writer.WriteShort(objectToSend.TypeId);
                objectToSend.Serialize(writer);
            }
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            SellType = reader.ReadSByte();
            var optionsCount = reader.ReadUShort();
            Options = new HumanOption[optionsCount];
            for (var optionsIndex = 0; optionsIndex < optionsCount; optionsIndex++)
            {
                var objectToAdd = ProtocolTypeManager.GetInstance<HumanOption>(reader.ReadShort());
                objectToAdd.Deserialize(reader);
                Options[optionsIndex] = objectToAdd;
            }
        }

    }
}
