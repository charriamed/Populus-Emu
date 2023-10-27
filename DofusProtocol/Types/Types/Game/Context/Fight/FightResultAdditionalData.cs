namespace Stump.DofusProtocol.Types
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class FightResultAdditionalData
    {
        public const short Id  = 191;
        public virtual short TypeId
        {
            get { return Id; }
        }

        public FightResultAdditionalData() { }

        public virtual void Serialize(IDataWriter writer)
        {
        }

        public virtual void Deserialize(IDataReader reader)
        {
        }

    }
}
