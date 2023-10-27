using System;
using System.Collections.Generic;
using System.Linq;
using Stump.Server.BaseServer.Database;
using Stump.Server.BaseServer.Initialization;
using Stump.Server.WorldServer.Database.Characters;

namespace Stump.Server.WorldServer.Game.Actors.RolePlay.Characters
{
    public class ExperienceManager : DataManager<ExperienceManager>
    {
        readonly Dictionary<ushort, ExperienceTableEntry> m_records = new Dictionary<ushort, ExperienceTableEntry>();
        KeyValuePair<ushort, ExperienceTableEntry> m_highestCharacterLevel;
        KeyValuePair<ushort, ExperienceTableEntry> m_highestGrade;
        KeyValuePair<ushort, ExperienceTableEntry> m_highestGuildLevel;
        KeyValuePair<ushort, ExperienceTableEntry> m_highestMountLevel;
        KeyValuePair<ushort, ExperienceTableEntry> m_highestPetLevel;
        KeyValuePair<ushort, ExperienceTableEntry> m_highestJobLevel;

        public ushort HighestCharacterLevel => m_highestCharacterLevel.Key;

        public long HighestCharacterExperience => m_highestCharacterLevel.Value.CharacterExp.Value;

        public ushort HighestGuildLevel => m_highestGuildLevel.Key;

        public ushort HighestGrade => m_highestGrade.Key;

        public ushort HighestGradeHonor => m_highestGrade.Value.AlignmentHonor.Value;

        public ushort HighestJobLevel => m_highestJobLevel.Key;

        #region Character

        /// <summary>
        ///     Get the experience requiered to access the given character level
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        public long GetCharacterLevelExperience(ushort level)
        {
            if (!m_records.ContainsKey(level))
                throw new Exception("Level " + level + " not found");

            if (m_records[level].CharacterExp == null)
                throw new Exception("Level " + level + " not found");

            var exp = m_records[level].CharacterExp;

            if (!exp.HasValue)
                throw new Exception("Character level " + level + " is not defined");

            return exp.Value;
        }

        /// <summary>
        ///     Get the experience to reach the next character level
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        public long GetCharacterNextLevelExperience(ushort level)
        {
            if (!m_records.ContainsKey((ushort) (level + 1)))
                level--;

            if (m_records[(ushort)(level + 1)].CharacterExp == null)
                return long.MaxValue;

            var exp = m_records[(ushort) (level + 1)].CharacterExp;

            if (!exp.HasValue)
                throw new Exception("Character level " + level + " is not defined");

            return exp.Value;
        }
        public ushort GetCharacterLevel(long experience, int prestigeRank)
        {
            return GetCharacterLevel(experience - prestigeRank*HighestCharacterExperience);
        }

        public ushort GetCharacterLevel(long experience)
        {
            try
            {
                if (experience >= m_highestCharacterLevel.Value.CharacterExp)
                    return m_highestCharacterLevel.Key;

                return (ushort) (m_records.First(entry => entry.Value.CharacterExp > experience).Key - 1);
            }
            catch (InvalidOperationException ex)
            {
                throw new Exception(string.Format("Experience {0} isn't bind to a character level", experience), ex);
            }
        }

        #endregion

        #region Alignement

        /// <summary>
        ///     Get the honor requiered to access the given grade
        /// </summary>
        /// <returns></returns>
        public ushort GetAlignementGradeHonor(ushort grade)
        {
            if (!m_records.ContainsKey(grade))
                throw new Exception("Grade " + grade + " not found");

            if (m_records[grade].AlignmentHonor == null)
                throw new Exception("Grade " + grade + " not found");

            var honor = m_records[grade].AlignmentHonor;

            if (!honor.HasValue)
                throw new Exception("Grade " + grade + " is not defined");

            return honor.Value;
        }

        /// <summary>
        ///     Get the honor to reach the next grade
        /// </summary>
        /// <returns></returns>
        public ushort GetAlignementNextGradeHonor(ushort grade)
        {
            if (!m_records.ContainsKey((ushort)(grade + 1)))
                return Character.HonorLimit;

            if (m_records[(ushort) (grade + 1)].AlignmentHonor == null)
                return Character.HonorLimit;

            var honor = m_records[(ushort) (grade + 1)].AlignmentHonor;

            return !honor.HasValue ? Character.HonorLimit : honor.Value;
        }

        public ushort GetAlignementGrade(ushort honor)
        {
            try
            {
                if (honor >= m_highestGrade.Value.AlignmentHonor)
                    return m_highestGrade.Key;

                return (ushort) (m_records.First(entry => entry.Value.AlignmentHonor > honor).Key - 1);
            }
            catch (InvalidOperationException ex)
            {
                throw new Exception(string.Format("Honor {0} isn't bind to a grade", honor), ex);
            }
        }

        #endregion

        #region Guild

        /// <summary>
        ///     Get the experience requiered to access the given guild level
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        public long GetGuildLevelExperience(ushort level)
        {
            if (!m_records.ContainsKey(level))
                throw new Exception("Level " + level + " not found");

            if (m_records[level].GuildExp == null)
                throw new Exception("Level " + level + " not found");

            var exp = m_records[level].GuildExp;

            if (!exp.HasValue)
                throw new Exception("Guild level " + level + " is not defined");

            return exp.Value;
        }

        /// <summary>
        ///     Get the experience to reach the next guild level
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        public long GetGuildNextLevelExperience(ushort level)
        {
            if (!m_records.ContainsKey((ushort)(level + 1)))
                return -1;

            if (m_records[(ushort) (level + 1)].GuildExp == null)
                return -1;

            var exp = m_records[(ushort) (level + 1)].GuildExp;

            if (!exp.HasValue)
                throw new Exception("Guild level " + level + " is not defined");

            return exp.Value;
        }

        public ushort GetGuildLevel(long experience)
        {
            try
            {
                if (experience >= m_highestGuildLevel.Value.GuildExp)
                    return m_highestGuildLevel.Key;

                return (ushort) (m_records.First(entry => entry.Value.GuildExp > experience).Key - 1);
            }
            catch (InvalidOperationException ex)
            {
                throw new Exception(string.Format("Experience {0} isn't bind to a guild level", experience), ex);
            }
        }

        #endregion

        #region Mount
        /// <summary>
        ///     Get the experience requiered to access the given mount level
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        public long GetMountLevelExperience(ushort level)
        {
            if (!m_records.ContainsKey(level))
                throw new Exception("Level " + level + " not found");

            if (m_records[level].MountExp == null)
                throw new Exception("Level " + level + " not found");

            var exp = m_records[level].MountExp;

            if (!exp.HasValue)
                throw new Exception("Mount level " + level + " is not defined");

            return exp.Value;
        }

        /// <summary>
        ///     Get the experience to reach the next mount level
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        public long GetMountNextLevelExperience(ushort level)
        {
            if (!m_records.ContainsKey((ushort)(level + 1)))
                return -1;

            if (m_records[(ushort)(level + 1)].MountExp == null)
                return -1;

            var exp = m_records[(ushort)(level + 1)].MountExp;

            if (!exp.HasValue)
                throw new Exception("Mount level " + level + " is not defined");

            return exp.Value;
        }

        public ushort GetMountLevel(long experience)
        {
            try
            {
                if (experience >= m_highestMountLevel.Value.MountExp)
                    return m_highestMountLevel.Key;

                return (ushort)(m_records.First(entry => entry.Value.MountExp > experience).Key - 1);
            }
            catch (InvalidOperationException ex)
            {
                throw new Exception(string.Format("Experience {0} isn't bind to a mount level", experience), ex);
            }
        }

        #endregion

        #region Pets
        /// <summary>
        ///     Get the experience requiered to access the given pet level
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        public long GetPetLevelExperience(ushort level)
        {
            if (!m_records.ContainsKey(level))
                throw new Exception("Level " + level + " not found");

            if (m_records[level].MountExp == null)
                throw new Exception("Level " + level + " not found");

            var exp = m_records[level].PetExp;

            if (!exp.HasValue)
                throw new Exception("Pet level " + level + " is not defined");

            return exp.Value;
        }

        /// <summary>
        ///     Get the experience to reach the next pet level
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        public long GetPetNextLevelExperience(ushort level)
        {
            if (!m_records.ContainsKey((ushort)(level + 1)))
                return -1;

            if (m_records[(ushort)(level + 1)].PetExp == null)
                return -1;

            var exp = m_records[(ushort)(level + 1)].PetExp;

            if (!exp.HasValue)
                throw new Exception("Pet level " + level + " is not defined");

            return exp.Value;
        }

        public ushort GetPetLevel(long experience)
        {
            try
            {
                if (experience >= m_highestPetLevel.Value.PetExp)
                    return m_highestPetLevel.Key;

                return (ushort)(m_records.First(entry => entry.Value.PetExp > experience).Key - 1);
            }
            catch (InvalidOperationException ex)
            {
                throw new Exception(string.Format("Experience {0} isn't bind to a pet level", experience), ex);
            }
        }

        #endregion

        #region Job

        /// <summary>
        ///     Get the experience requiered to access the given Job level
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        public long GetJobLevelExperience(ushort level)
        {
            if (!m_records.ContainsKey(level))
                throw new Exception("Level " + level + " not found");

            if (m_records[level].JobExp == null)
                throw new Exception("Level " + level + " not found");

            var exp = m_records[level].JobExp;

            if (!exp.HasValue)
                throw new Exception("Job level " + level + " is not defined");

            return exp.Value;
        }

        /// <summary>
        ///     Get the experience to reach the next Job level
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        public long GetJobNextLevelExperience(ushort level)
        {
            if (!m_records.ContainsKey((ushort)(level + 1)))
                return 0;

            if (m_records[(ushort)(level + 1)].JobExp == null)
                return 0;

            var exp = m_records[(ushort)(level + 1)].JobExp;

            if (!exp.HasValue)
                throw new Exception("Job level " + level + " is not defined");

            return exp.Value;
        }

        public ushort GetJobLevel(long experience)
        {
            try
            {
                if (experience >= m_highestJobLevel.Value.JobExp)
                    return m_highestJobLevel.Key;

                return (ushort)(m_records.First(entry => entry.Value.JobExp > experience).Key - 1);
            }
            catch (InvalidOperationException ex)
            {
                throw new Exception(string.Format("Experience {0} isn't bind to a Job level", experience), ex);
            }
        }

        #endregion

        [Initialization(InitializationPass.Fourth)]
        public override void Initialize()
        {
            foreach (
                var record in Database.Query<ExperienceTableEntry>(ExperienceTableRelator.FetchQuery))
            {

                m_records.Add((ushort) record.Level, record);
            }

            m_highestCharacterLevel = m_records.OrderByDescending(entry => entry.Value.CharacterExp).FirstOrDefault();
            m_highestGrade = m_records.OrderByDescending(entry => entry.Value.AlignmentHonor).FirstOrDefault();
            m_highestGuildLevel = m_records.OrderByDescending(entry => entry.Value.GuildExp).FirstOrDefault();
            m_highestMountLevel = m_records.OrderByDescending(entry => entry.Value.MountExp).FirstOrDefault();
            m_highestPetLevel = m_records.OrderByDescending(entry => entry.Value.PetExp).FirstOrDefault();
            m_highestJobLevel = m_records.OrderByDescending(entry => entry.Value.JobExp).FirstOrDefault();
        }
    }
}