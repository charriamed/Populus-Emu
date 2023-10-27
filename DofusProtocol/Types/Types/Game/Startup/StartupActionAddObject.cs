namespace Stump.DofusProtocol.Types
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using System.Collections.Generic;
    using Stump.Core.IO;

    [Serializable]
    public class StartupActionAddObject
    {
        public const short Id  = 52;
        public virtual short TypeId
        {
            get { return Id; }
        }
        public int Uid { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public string DescUrl { get; set; }
        public string PictureUrl { get; set; }
        public ObjectItemInformationWithQuantity[] Items { get; set; }

        public StartupActionAddObject(int uid, string title, string text, string descUrl, string pictureUrl, ObjectItemInformationWithQuantity[] items)
        {
            this.Uid = uid;
            this.Title = title;
            this.Text = text;
            this.DescUrl = descUrl;
            this.PictureUrl = pictureUrl;
            this.Items = items;
        }

        public StartupActionAddObject() { }

        public virtual void Serialize(IDataWriter writer)
        {
            writer.WriteInt(Uid);
            writer.WriteUTF(Title);
            writer.WriteUTF(Text);
            writer.WriteUTF(DescUrl);
            writer.WriteUTF(PictureUrl);
            writer.WriteShort((short)Items.Count());
            for (var itemsIndex = 0; itemsIndex < Items.Count(); itemsIndex++)
            {
                var objectToSend = Items[itemsIndex];
                objectToSend.Serialize(writer);
            }
        }

        public virtual void Deserialize(IDataReader reader)
        {
            Uid = reader.ReadInt();
            Title = reader.ReadUTF();
            Text = reader.ReadUTF();
            DescUrl = reader.ReadUTF();
            PictureUrl = reader.ReadUTF();
            var itemsCount = reader.ReadUShort();
            Items = new ObjectItemInformationWithQuantity[itemsCount];
            for (var itemsIndex = 0; itemsIndex < itemsCount; itemsIndex++)
            {
                var objectToAdd = new ObjectItemInformationWithQuantity();
                objectToAdd.Deserialize(reader);
                Items[itemsIndex] = objectToAdd;
            }
        }

    }
}
