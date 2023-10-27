using System;
using System.Collections.Generic;
using System.Linq;
using Stump.Server.BaseServer.Database;
using Stump.Server.BaseServer.Initialization;
using Stump.Server.WorldServer.Database;
using Stump.Server.WorldServer.Database.Accounts;

namespace Stump.Server.WorldServer.Game.Actors.RolePlay.Characters
{
    public class MiniGamesManager : DataManager<MiniGamesManager>, ISaveable
    {
        readonly Dictionary<int, CharactersMiniGames> m_records = new Dictionary<int, CharactersMiniGames>();

        [Initialization(InitializationPass.Ninth)]
        public override void Initialize()
        {
            foreach (
                var record in Database.Query<CharactersMiniGames>(CharactersMiniGamesRelator.FetchQuery))
            {

                m_records.Add((ushort)record.CharacterId, record);
            }
            World.Instance.RegisterSaveableInstance(this);
        }

        public CharactersMiniGames GetCharacterMiniGames(Character character)
        {
            return m_records.FirstOrDefault(x => x.Key == character.Id).Value;
        }

        public void RecordHammerGame(Character character)
        {
            var record = GetCharacterMiniGames(character);

            if(record != null)
            {
                record.Hammer = DateTime.Now;
            }
            else
            {
                var miniGames = new CharactersMiniGames()
                {
                    CharacterId = character.Id,
                    Hammer = DateTime.Now,
                };
                m_records.Add(character.Id, miniGames);
            }
        }

        public CharactersMiniGames GetMiniGamesRecordByCharacterId(int id)
        {
            WorldServer.Instance.IOTaskPool.EnsureContext();
            return Database.Query<CharactersMiniGames>(string.Format(CharactersMiniGamesRelator.FetchById, id)).FirstOrDefault();
        }

        public void Save()
        {
            Database.BeginTransaction();
            var dbIds = m_records.Values;

            foreach (var id in dbIds.Distinct())
            {
                CharactersMiniGames record = GetMiniGamesRecordByCharacterId(id.CharacterId);
                if (record != null)
                {
                    Database.Update(id);
                }
                else
                {
                    Database.Insert(id);
                }
            }

            Database.CompleteTransaction();
        }
    }
}