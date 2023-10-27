namespace Stump.DofusProtocol.Types
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class AbstractSocialGroupInfos
    {
        public const short Id  = 416;
        public virtual short TypeId
        {
            get { return Id; }
        }

        public AbstractSocialGroupInfos() { }

        public virtual void Serialize(IDataWriter writer)
        {
        }

        public virtual void Deserialize(IDataReader reader)
        {
        }

    }
}
