using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Stump.DofusProtocol.Enums;
using Stump.Server.BaseServer.Database;
using Stump.Server.BaseServer.Initialization;
using Stump.Server.WorldServer.Database.Effects;
using Stump.Server.WorldServer.Database.Items.Pets;
using Stump.Server.WorldServer.Database.Items.Templates;
using Stump.Server.WorldServer.Game.Effects.Instances;

namespace Stump.Server.WorldServer.Game.Items.Player.Custom
{
    public class PetManager : DataManager<PetManager>
    {
        private Dictionary<int, PetTemplate> m_pets;
        public ReadOnlyDictionary<int, PetTemplate> Pets => new ReadOnlyDictionary<int, PetTemplate>(m_pets);

        [Initialization(typeof(ItemManager))]
        public override void Initialize()
        {
            m_pets = Database.Query<PetTemplate, PetFoodRecord, PetTemplate>(new PetTemplateRelator().Map, PetTemplateRelator.FetchQuery).ToDictionary(x => x.Id);
        }

        public PetTemplate GetPetTemplate(int id)
        {
            return m_pets.TryGetValue(id, out var template) ? template : null;
        }
    }
}