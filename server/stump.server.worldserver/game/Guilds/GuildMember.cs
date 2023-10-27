﻿using Stump.DofusProtocol.Enums;
using Stump.DofusProtocol.Types;
using Stump.Server.WorldServer.Database.Guilds;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Characters;
using System;
using NetworkGuildMember = Stump.DofusProtocol.Types.GuildMember;

namespace Stump.Server.WorldServer.Game.Guilds
{
    public class GuildMember
    {
        public GuildMember(GuildMemberRecord record)
        {
            Record = record;
        }

        public GuildMember(Guild guild, Character character)
        {
            Record = new GuildMemberRecord
            {
                CharacterId = character.Id,
                AccountId = character.Account.Id,
                Character = character.Record,
                GivenExperience = 0,
                GivenPercent = 0,
                RankId = 0,
                GuildId = guild.Id,
                Rights = GuildRightsBitEnum.GUILD_RIGHT_NONE,
            };

            Guild = guild;
            Character = character;
            IsDirty = true;
            IsNew = true;
        }

        public GuildMemberRecord Record
        {
            get;
        }

        public int Id => Record.CharacterId;

        /// <summary>
        ///     Null if the character isn't connected.
        /// </summary>
        public Character Character
        {
            get;
            private set;
        }

        public bool IsConnected => Character != null;

        public Guild Guild
        {
            get;
            private set;
        }

        public long GivenExperience
        {
            get { return Record.GivenExperience; }
            set
            {
                Record.GivenExperience = value;
                IsDirty = true;
            }
        }

        public byte GivenPercent
        {
            get { return Record.GivenPercent; }
            set
            {
                Record.GivenPercent = value;
                IsDirty = true;
            }
        }

        public GuildRightsBitEnum Rights
        {
            get { return Record.Rights; }
            set
            {
                Record.Rights = value;
                IsDirty = true;
            }
        }

        public short RankId
        {
            get { return Record.RankId >= 0 && Record.RankId <= 35 ? Record.RankId : (short)0; }
            set
            {
                Record.RankId = value;
                IsDirty = true;
            }
        }

        public bool IsBoss => RankId == 1;

        public string Name => Record.Name;

        public long Experience => Record.Experience;

        public int PrestigeRank => Record.PrestigeRank;

        public PlayableBreedEnum Breed => Record.Breed;

        public SexTypeEnum Sex => Record.Sex;

        public AlignmentSideEnum AlignementSide => Record.AlignementSide;

        public DateTime? LastConnection => Record.LastConnection;

        /// <summary>
        /// True if must be saved
        /// </summary>
        public bool IsDirty
        {
            get;
            protected set;
        }

        public bool IsNew
        {
            get;
            protected set;
        }

        public CharacterMinimalInformations GetCharacterMinimalInformations() => new CharacterMinimalInformations((ulong)Id, Name, (ushort)ExperienceManager.Instance.GetCharacterLevel(Experience, PrestigeRank));

        public CharacterMinimalGuildPublicInformations GetCharacterMinimalGuildPublicInformations() => new CharacterMinimalGuildPublicInformations((ulong)Id, Name, (ushort)ExperienceManager.Instance.GetCharacterLevel(Experience), (uint) RankId);

        public NetworkGuildMember GetNetworkGuildMember()
        {
            var hvb = HavenBags.HavenBagManager.Instance.GetHavenBagByOwner(Character);
            if (hvb == null && this.IsConnected)
            {
                return new NetworkGuildMember((ulong)Id, Character.Name, (ushort)Character.Level, Character.Sex == SexTypeEnum.SEX_FEMALE, false, (sbyte)Character.Breed.Id, (ushort)RankId,
                                               (ulong)GivenExperience, (sbyte)GivenPercent, (uint)Rights, Character.IsInFight() ? (sbyte)2 : (sbyte)1,
                                               (sbyte)Character.AlignmentSide, (ushort)DateTime.Now.Hour, (ushort)Character.SmileyMoodId,
                                               Record.AccountId, 0, Character.Status);
            }
            else if (this.IsConnected)
            {
                return new NetworkGuildMember((ulong)Id, Character.Name, (ushort)Character.Level, Character.Sex == SexTypeEnum.SEX_FEMALE, hvb.GuildAllowed, (sbyte)Character.Breed.Id, (ushort)RankId,
                                               (ulong)GivenExperience, (sbyte)GivenPercent, (uint)Rights, Character.IsInFight() ? (sbyte)2 : (sbyte)1,
                                               (sbyte)Character.AlignmentSide, (ushort)DateTime.Now.Hour, (ushort)Character.SmileyMoodId,
                                               Record.AccountId, 0, Character.Status);
            }

            return new NetworkGuildMember((ulong)Id, Name, (ushort)ExperienceManager.Instance.GetCharacterLevel(Experience, PrestigeRank),
                Sex == SexTypeEnum.SEX_FEMALE, false,
                (sbyte)Breed, (ushort)RankId,
                (ulong)GivenExperience, (sbyte)GivenPercent, (uint)Rights, 0,
                (sbyte)AlignementSide, LastConnection != null ? (ushort)(DateTime.Now - LastConnection.Value).TotalHours : (ushort)0, 0,
                Record.AccountId, 0, new PlayerStatus((sbyte)PlayerStatusEnum.PLAYER_STATUS_OFFLINE));
        }

        public bool HasRight(GuildRightsBitEnum right) => Rights.HasFlag(GuildRightsBitEnum.GUILD_RIGHT_BOSS) || (Rights.HasFlag(GuildRightsBitEnum.GUILD_RIGHT_MANAGE_RIGHTS) && right != GuildRightsBitEnum.GUILD_RIGHT_BOSS) || Rights.HasFlag(right);

        public event Action<GuildMember> Connected;

        public event Action<GuildMember, Character> Disconnected;

        public void OnCharacterConnected(Character character)
        {
            if (character.Id != Record.CharacterId)
            {
                throw new Exception(string.Format("GuildMember.CharacterId ({0}) != characterid ({1})",
                                                  Record.CharacterId, character.Id));
            }

            Character = character;

            var evnt = Connected;
            if (evnt != null)
                evnt(this);
        }

        public void OnCharacterDisconnected(Character character)
        {
            IsDirty = true;
            Character = null;

            var evnt = Disconnected;
            if (evnt != null)
                evnt(this, character);
        }

        public void AddXP(long experience)
        {
            GivenExperience += experience;
            Guild.AddXP(experience);
        }

        public void BindGuild(Guild guild)
        {
            if (Guild != null)
                throw new Exception(string.Format("Guild already bound to GuildMember {0}", Id));

            Guild = guild;
        }

        public void Save(ORM.Database database)
        {
            WorldServer.Instance.IOTaskPool.ExecuteInContext(() =>
            {
                if (IsNew)
                    database.Insert(Record);
                else
                    database.Update(Record);

                IsDirty = false;
                IsNew = false;
            });
        }
    }
}