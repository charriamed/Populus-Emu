namespace Stump.DofusProtocol.Types
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using System.Collections.Generic;
    using Stump.Core.IO;

    [Serializable]
    public class HumanOptionFollowers : HumanOption
    {
        public new const short Id = 410;
        public override short TypeId
        {
            get { return Id; }
        }
        public IndexedEntityLook[] FollowingCharactersLook { get; set; }

        public HumanOptionFollowers(IndexedEntityLook[] followingCharactersLook)
        {
            this.FollowingCharactersLook = followingCharactersLook;
        }

        public HumanOptionFollowers() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteShort((short)FollowingCharactersLook.Count());
            for (var followingCharactersLookIndex = 0; followingCharactersLookIndex < FollowingCharactersLook.Count(); followingCharactersLookIndex++)
            {
                var objectToSend = FollowingCharactersLook[followingCharactersLookIndex];
                objectToSend.Serialize(writer);
            }
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            var followingCharactersLookCount = reader.ReadUShort();
            FollowingCharactersLook = new IndexedEntityLook[followingCharactersLookCount];
            for (var followingCharactersLookIndex = 0; followingCharactersLookIndex < followingCharactersLookCount; followingCharactersLookIndex++)
            {
                var objectToAdd = new IndexedEntityLook();
                objectToAdd.Deserialize(reader);
                FollowingCharactersLook[followingCharactersLookIndex] = objectToAdd;
            }
        }

    }
}
