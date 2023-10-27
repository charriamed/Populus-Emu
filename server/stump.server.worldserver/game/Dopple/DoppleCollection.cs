using System.Collections.Generic;
using Stump.Server.BaseServer.Database;
using Stump.Server.WorldServer.Database.Dopple;

namespace Stump.Server.WorldServer.Game.Dopple
{
    public class DoppleCollection : DataManager<DoppleCollection>
    {
        public List<DoppleRecord> Dopeul { get; set; }
        public List<DoppleRecord> DeleteDopeul { get; set; }

        public void Save(ORM.Database database)
        {
            foreach (var dopeul in DeleteDopeul)
            {
                if (!dopeul.IsNew)
                    database.Delete(dopeul);
            }

            DeleteDopeul.Clear();

            foreach (var dopeul in Dopeul)
            {
                if (dopeul.IsUpdated && !dopeul.IsNew)
                {
                    database.Update(dopeul);
                    dopeul.IsUpdated = false;
                }

                if (dopeul.IsNew)
                {
                    database.Insert(dopeul);
                    dopeul.IsNew = false;
                }
            }
        }

        public void Load(int id)
        {
            Dopeul = FindByOwner(id);
            DeleteDopeul = new List<DoppleRecord>();
        }

        public void Load(string ip)
        {
            Dopeul = FindByOwner(ip);
            DeleteDopeul = new List<DoppleRecord>();
        }

        public List<DoppleRecord> FindByOwner(int ownerId)
        {
            return Database.Fetch<DoppleRecord>($"SELECT * FROM characters_dopple WHERE CharacterId = {ownerId}");
        }

        public List<DoppleRecord> FindByOwner(string Ip)
        {
            return Database.Fetch<DoppleRecord>($"SELECT * FROM characters_dopple WHERE Ip = '{Ip}'");
        }
    }
}