using Stump.Core.Reflection;
using Stump.Server.BaseServer.Initialization;
using System;
using System.Linq;
using System.Threading.Tasks;
using Stump.Core.Threading;
using Stump.Core.Collections;
using Stump.Server.WorldServer.Core.IPC;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Characters;
using Stump.DofusProtocol.Messages;
using Stump.Server.WorldServer.Game;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Monsters;

namespace Game.Vote
{
    public class MonsterStarLoop : Singleton<MonsterStarLoop>
    {
        // FIELDS
        private static Task _queueRefresherTask;
        private static int _queueRefresherElapsedTime = 5400000;//2H 7200000

        // METHODS
        [Initialization(InitializationPass.Any)]
        private static void Initialize()
        {
            MonsterStarLoop._queueRefresherTask = Task.Factory.StartNewDelayed(MonsterStarLoop._queueRefresherElapsedTime, new Action(Singleton<MonsterStarLoop>.Instance.StartLoop));
        }
        private void StartLoop()
        {
            try
            {
                foreach (var monster in World.Instance.GetMaps().SelectMany(x => x.Actors.OfType<MonsterGroup>()))
                {
                    monster.AgeBonus = 200;
                }
                World.Instance.SendAnnounce("<b>Serveur :</b> Les étoiles des monstres ont été fixés au maximum ! Bon jeu sur <b>Aflorys</b>.", System.Drawing.Color.AliceBlue);

            }
            finally
            {
                MonsterStarLoop._queueRefresherTask = Task.Factory.StartNewDelayed(MonsterStarLoop._queueRefresherElapsedTime, new Action(Singleton<MonsterStarLoop>.Instance.StartLoop));
            }
        }
    }
}
