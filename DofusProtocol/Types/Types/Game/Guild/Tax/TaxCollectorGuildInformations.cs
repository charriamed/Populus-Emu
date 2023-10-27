namespace Stump.DofusProtocol.Types
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class TaxCollectorGuildInformations : TaxCollectorComplementaryInformations
    {
        public new const short Id = 446;
        public override short TypeId
        {
            get { return Id; }
        }
        public BasicGuildInformations Guild { get; set; }

        public TaxCollectorGuildInformations(BasicGuildInformations guild)
        {
            this.Guild = guild;
        }

        public TaxCollectorGuildInformations() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            Guild.Serialize(writer);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            Guild = new BasicGuildInformations();
            Guild.Deserialize(reader);
        }

    }
}
