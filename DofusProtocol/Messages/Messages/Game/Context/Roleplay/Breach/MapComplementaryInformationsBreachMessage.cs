namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using System.Collections.Generic;
    using Stump.Core.IO;

    [Serializable]
    public class MapComplementaryInformationsBreachMessage : MapComplementaryInformationsDataMessage
    {
        public new const uint Id = 6791;
        public override uint MessageId
        {
            get { return Id; }
        }
        public uint Floor { get; set; }
        public sbyte Room { get; set; }
        public BreachBranch[] Branches { get; set; }

        public MapComplementaryInformationsBreachMessage(ushort subAreaId, double mapId, HouseInformations[] houses, GameRolePlayActorInformations[] actors, InteractiveElement[] interactiveElements, StatedElement[] statedElements, MapObstacle[] obstacles, FightCommonInformations[] fights, bool hasAggressiveMonsters, FightStartingPositions fightStartPositions, uint floor, sbyte room, BreachBranch[] branches)
        {
            this.SubAreaId = subAreaId;
            this.MapId = mapId;
            this.Houses = houses;
            this.Actors = actors;
            this.InteractiveElements = interactiveElements;
            this.StatedElements = statedElements;
            this.Obstacles = obstacles;
            this.Fights = fights;
            this.HasAggressiveMonsters = hasAggressiveMonsters;
            this.FightStartPositions = fightStartPositions;
            this.Floor = floor;
            this.Room = room;
            this.Branches = branches;
        }

        public MapComplementaryInformationsBreachMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteVarUInt(Floor);
            writer.WriteSByte(Room);
            writer.WriteShort((short)Branches.Count());
            for (var branchesIndex = 0; branchesIndex < Branches.Count(); branchesIndex++)
            {
                var objectToSend = Branches[branchesIndex];
                objectToSend.Serialize(writer);
            }
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            Floor = reader.ReadVarUInt();
            Room = reader.ReadSByte();
            var branchesCount = reader.ReadUShort();
            Branches = new BreachBranch[branchesCount];
            for (var branchesIndex = 0; branchesIndex < branchesCount; branchesIndex++)
            {
                var objectToAdd = new BreachBranch();
                objectToAdd.Deserialize(reader);
                Branches[branchesIndex] = objectToAdd;
            }
        }

    }
}
