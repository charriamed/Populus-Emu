namespace Stump.DofusProtocol.Types
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class FightOptionsInformations
    {
        public const short Id  = 20;
        public virtual short TypeId
        {
            get { return Id; }
        }
        public bool IsSecret { get; set; }
        public bool IsRestrictedToPartyOnly { get; set; }
        public bool IsClosed { get; set; }
        public bool IsAskingForHelp { get; set; }

        public FightOptionsInformations(bool isSecret, bool isRestrictedToPartyOnly, bool isClosed, bool isAskingForHelp)
        {
            this.IsSecret = isSecret;
            this.IsRestrictedToPartyOnly = isRestrictedToPartyOnly;
            this.IsClosed = isClosed;
            this.IsAskingForHelp = isAskingForHelp;
        }

        public FightOptionsInformations() { }

        public virtual void Serialize(IDataWriter writer)
        {
            var flag = new byte();
            flag = BooleanByteWrapper.SetFlag(flag, 0, IsSecret);
            flag = BooleanByteWrapper.SetFlag(flag, 1, IsRestrictedToPartyOnly);
            flag = BooleanByteWrapper.SetFlag(flag, 2, IsClosed);
            flag = BooleanByteWrapper.SetFlag(flag, 3, IsAskingForHelp);
            writer.WriteByte(flag);
        }

        public virtual void Deserialize(IDataReader reader)
        {
            var flag = reader.ReadByte();
            IsSecret = BooleanByteWrapper.GetFlag(flag, 0);
            IsRestrictedToPartyOnly = BooleanByteWrapper.GetFlag(flag, 1);
            IsClosed = BooleanByteWrapper.GetFlag(flag, 2);
            IsAskingForHelp = BooleanByteWrapper.GetFlag(flag, 3);
        }

    }
}
