using System;
using System.Collections.Generic;
using System.Linq;
using Stump.Core.IO;
using Stump.DofusProtocol.Enums;
using Stump.Server.BaseServer.Database;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Characters;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Npcs;
using Stump.Server.WorldServer.Game.Items;
using Stump.Server.WorldServer.Game.Items.Player;
using Stump.Server.WorldServer.Game.Maps;
using Stump.Server.WorldServer.Game.Maps.Cells;
using Stump.Server.WorldServer.Game.Spells;

namespace Stump.Server.WorldServer.Database.Npcs.Replies
{
    [Discriminator("Incarnation", typeof(NpcReply), typeof(NpcReplyRecord))]
    public class Jesaispasquoimettre : NpcReply
    {
        private bool m_mustRefreshPosition;
        private ObjectPosition m_position;

        public Jesaispasquoimettre()
        {
        }

        public Jesaispasquoimettre(NpcReplyRecord record)
            : base(record)
        {
        }

        public int chracter
        {
            get;
            set;
        }

        public Inventory Inventory
        {
            get;
            private set;
        }

        /// <summary>
        /// Parameter 0
        /// </summary>
        public int MapId
        {
            get
            {
                return Record.GetParameter<int>(0);
            }
            set
            {
                Record.SetParameter(0, value);
                m_mustRefreshPosition = true;
            }
        }

        /// <summary>
        /// Parameter 1
        /// </summary>
        public int CellId
        {
            get
            {
                return Record.GetParameter<int>(1);
            }
            set
            {
                Record.SetParameter(1, value);
                m_mustRefreshPosition = true;
            }
        }

        /// <summary>
        /// Parameter 2
        /// </summary>
        public DirectionsEnum Direction
        {
            get
            {
                return (DirectionsEnum)Record.GetParameter<int>(2);
            }
            set
            {
                Record.SetParameter(2, (int)value);
                m_mustRefreshPosition = true;
            }
        }

        /// <summary>
        /// Parameter 3
        /// </summary>
        public int IncarnationId
        {
            get
            {
                return Record.GetParameter<int>(3, true);
            }
            set
            {
                Record.SetParameter(3, value);
            }
        }

        public string SubAreasCSV
        {
            get
            {
                return Record.GetParameter<string>(4, true);
            }
            set
            {
                Record.SetParameter(4, value);
            }
        }

        private void RefreshPosition()
        {
            var map = Game.World.Instance.GetMap(MapId);

            if (map == null)
                throw new Exception(string.Format("Cannot load SkillTeleport id={0}, map {1} isn't found", Id, MapId));

            var cell = map.Cells[CellId];

            m_position = new ObjectPosition(map, cell, Direction);
        }

        public ObjectPosition GetPosition()
        {
            if (m_position == null || m_mustRefreshPosition)
                RefreshPosition();

            m_mustRefreshPosition = false;

            return m_position;
        }

        public SubArea[] areas
        {
            get
            {
                List<SubArea> list = new List<SubArea>();
                foreach (var id in SubAreasCSV.FromCSV<int>(";"))
                {
                    var subarea = Game.World.Instance.GetSubArea(id);
                    if (subarea != null) list.Add(subarea);
                }
                return list.ToArray();
            }
        }

        public override bool CanShow(Npc npc, Character character) => base.CanShow(npc, character) && MapId != character.Map.Id;

        public override bool Execute(Npc npc, Character character)
        {
            if (!base.Execute(npc, character))
                return false;

            var record = IncarnationManager.Instance.GetCustomIncarnationRecord(IncarnationId);
            IncarnationManager.Instance.ApplyCustomIncarnation(character, record);

            chracter = character.Id;       
            IncarnationManager.Instance.handlers.Add(this);
            return character.Teleport(GetPosition());         
        }
    }
}