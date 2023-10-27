namespace Stump.DofusProtocol.Types
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class AdditionalTaxCollectorInformations
    {
        public const short Id  = 165;
        public virtual short TypeId
        {
            get { return Id; }
        }
        public string CollectorCallerName { get; set; }
        public int Date { get; set; }

        public AdditionalTaxCollectorInformations(string collectorCallerName, int date)
        {
            this.CollectorCallerName = collectorCallerName;
            this.Date = date;
        }

        public AdditionalTaxCollectorInformations() { }

        public virtual void Serialize(IDataWriter writer)
        {
            writer.WriteUTF(CollectorCallerName);
            writer.WriteInt(Date);
        }

        public virtual void Deserialize(IDataReader reader)
        {
            CollectorCallerName = reader.ReadUTF();
            Date = reader.ReadInt();
        }

    }
}
