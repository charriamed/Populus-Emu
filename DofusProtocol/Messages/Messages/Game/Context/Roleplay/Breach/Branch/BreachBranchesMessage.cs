namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using System.Collections.Generic;
    using Stump.Core.IO;

    [Serializable]
    public class BreachBranchesMessage : Message
    {
        public const uint Id = 6812;
        public override uint MessageId
        {
            get { return Id; }
        }
        public ExtendedBreachBranch[] Branches { get; set; }

        public BreachBranchesMessage(ExtendedBreachBranch[] branches)
        {
            this.Branches = branches;
        }

        public BreachBranchesMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort((short)Branches.Count());
            for (var branchesIndex = 0; branchesIndex < Branches.Count(); branchesIndex++)
            {
                var objectToSend = Branches[branchesIndex];
                objectToSend.Serialize(writer);
            }
        }

        public override void Deserialize(IDataReader reader)
        {
            var branchesCount = reader.ReadUShort();
            Branches = new ExtendedBreachBranch[branchesCount];
            for (var branchesIndex = 0; branchesIndex < branchesCount; branchesIndex++)
            {
                var objectToAdd = new ExtendedBreachBranch();
                objectToAdd.Deserialize(reader);
                Branches[branchesIndex] = objectToAdd;
            }
        }

    }
}
