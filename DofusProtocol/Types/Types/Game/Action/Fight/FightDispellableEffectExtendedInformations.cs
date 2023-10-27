namespace Stump.DofusProtocol.Types
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class FightDispellableEffectExtendedInformations
    {
        public const short Id  = 208;
        public virtual short TypeId
        {
            get { return Id; }
        }
        public ushort ActionId { get; set; }
        public double SourceId { get; set; }
        public AbstractFightDispellableEffect Effect { get; set; }

        public FightDispellableEffectExtendedInformations(ushort actionId, double sourceId, AbstractFightDispellableEffect effect)
        {
            this.ActionId = actionId;
            this.SourceId = sourceId;
            this.Effect = effect;
        }

        public FightDispellableEffectExtendedInformations() { }

        public virtual void Serialize(IDataWriter writer)
        {
            writer.WriteVarUShort(ActionId);
            writer.WriteDouble(SourceId);
            writer.WriteShort(Effect.TypeId);
            Effect.Serialize(writer);
        }

        public virtual void Deserialize(IDataReader reader)
        {
            ActionId = reader.ReadVarUShort();
            SourceId = reader.ReadDouble();
            Effect = ProtocolTypeManager.GetInstance<AbstractFightDispellableEffect>(reader.ReadShort());
            Effect.Deserialize(reader);
        }

    }
}
