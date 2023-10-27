using System;
using System.Collections.Generic;
using System.Linq;
using NLog;
using Stump.Core.Attributes;
using Stump.Core.Extensions;
using Stump.DofusProtocol.D2oClasses;
using Stump.DofusProtocol.Enums;
using Stump.Server.WorldServer.Database.Items;
using Stump.Server.WorldServer.Database.Items.Pets;
using Stump.Server.WorldServer.Database.Items.Templates;
using Stump.Server.WorldServer.Database.Monsters;
using Stump.Server.WorldServer.Game.Actors.Fight;
using Stump.Server.WorldServer.Game.Actors.Look;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Characters;
using Stump.Server.WorldServer.Game.Effects;
using Stump.Server.WorldServer.Game.Effects.Handlers.Items;
using Stump.Server.WorldServer.Game.Effects.Instances;
using Stump.Server.WorldServer.Game.Fights;
using System.Drawing;

namespace Stump.Server.WorldServer.Game.Items.Player.Custom
{
    [ItemType(ItemTypeEnum.MONTILIER)]
    [ItemType(ItemTypeEnum.FAMILIER)]
    public sealed class PetItem : BasePlayerItem
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        public PetItem(Character owner, PlayerItemRecord record)
            : base(owner, record)
        {
            PetTemplate = PetManager.Instance.GetPetTemplate(Template.Id);

            if (PetTemplate == null)
                return;

            InitializeEffects();
        }

        private void InitializeEffects()
        {
            // new item
            if (Effects.OfType<EffectInteger>().All(x => x.EffectId != EffectsEnum.Effect_PetLevel))
            {
                Effects.Add(new EffectDice(EffectsEnum.Effect_PetLevel, 1, 18, 0));
                Effects.Add(new EffectDice(EffectsEnum.Effect_PetExp, 0, 0, (int)ExperienceManager.Instance.GetPetNextLevelExperience(1)));
                ChangeEffectsSquallingOnLevel(0);
            }
        }

        public override bool CanFeed(BasePlayerItem item)
        {
            return true;
        }

        public PetTemplate PetTemplate
        {
            get;
        }

        public int Experience
        {
            get { return ((Effects.Where(x => x.EffectId == EffectsEnum.Effect_PetExp).FirstOrDefault()) as EffectDice).Value; }
            set { ((Effects.Where(x => x.EffectId == EffectsEnum.Effect_PetExp).FirstOrDefault()) as EffectDice).Value = value; }
        }

        public int MaxExperience
        {
            get { return ((Effects.Where(x => x.EffectId == EffectsEnum.Effect_PetExp).FirstOrDefault()) as EffectDice).DiceFace; }
            set { ((Effects.Where(x => x.EffectId == EffectsEnum.Effect_PetExp).FirstOrDefault()) as EffectDice).DiceFace = value; }
        }

        public int Level
        {
            get { return ((Effects.Where(x => x.EffectId == EffectsEnum.Effect_PetLevel).FirstOrDefault()) as EffectDice).Value; }
            set { ((Effects.Where(x => x.EffectId == EffectsEnum.Effect_PetLevel).FirstOrDefault()) as EffectDice).Value = value; }
        }

        public override bool OnRemoveItem()
        {
            return base.OnRemoveItem();
        }

        public void AddLegendaryEffect(int legendaryFoodId)
        {
            EffectBase legendary = new EffectBase();
            foreach (var effect in Effects)
            {
                var eff = effect as EffectDice;

                if (eff is EffectDice && (eff.DiceNum == 12485 || eff.DiceNum == 12483 || eff.DiceNum == 12486 || eff.DiceNum == 12482 || eff.DiceNum == 12484 || eff.DiceNum == 12488 || eff.DiceNum == 12492 || eff.DiceNum == 12487 || eff.DiceNum == 12497 || eff.DiceNum == 12490 || eff.DiceNum == 12489 || eff.DiceNum == 12498 || eff.DiceNum == 12475 || eff.DiceNum == 12480 || eff.DiceNum == 12481 || eff.DiceNum == 12479 || eff.DiceNum == 12477))
                {
                    Effects.Remove(effect);
                    break;
                }
                else if(eff is EffectDice && (eff.DiceNum == 12505 || eff.DiceNum == 12501 || eff.DiceNum == 12499 || eff.DiceNum == 12503 || eff.DiceNum == 12500 || eff.DiceNum == 12506))
                {
                    legendary = effect;
                }
            }

            var random = new Random();

            if (legendaryFoodId == 20974 && legendary == new EffectBase())
            {
                var rnd = random.Next(1, 5);
                switch (rnd)
                {
                    case 1:
                        Effects.Add(new EffectDice(EffectsEnum.Effect_CastSpell_1175, 0, 12485, 1));
                        break;

                    case 2:
                        Effects.Add(new EffectDice(EffectsEnum.Effect_CastSpell_1175, 0, 12483, 1));
                        break;

                    case 3:
                        Effects.Add(new EffectDice(EffectsEnum.Effect_CastSpell_1175, 0, 12486, 1));
                        break;

                    case 4:
                        Effects.Add(new EffectDice(EffectsEnum.Effect_CastSpell_1175, 0, 12482, 1));
                        break;

                    case 5:
                        Effects.Add(new EffectDice(EffectsEnum.Effect_CastSpell_1175, 0, 12484, 1));
                        break;
                }
            }
            else if (legendaryFoodId == 20975 && legendary == new EffectBase())
            {
                var rnd2 = random.Next(1, 7);
                switch (rnd2)
                {
                    case 1:
                        Effects.Add(new EffectDice(EffectsEnum.Effect_CastSpell_1175, 0, 12488, 1));
                        break;

                    case 2:
                        Effects.Add(new EffectDice(EffectsEnum.Effect_CastSpell_1175, 0, 12492, 1));
                        break;

                    case 3:
                        Effects.Add(new EffectDice(EffectsEnum.Effect_CastSpell_1175, 0, 12487, 1));
                        break;

                    case 4:
                        Effects.Add(new EffectDice(EffectsEnum.Effect_CastSpell_1175, 0, 12497, 1));
                        break;

                    case 5:
                        Effects.Add(new EffectDice(EffectsEnum.Effect_CastSpell_1175, 0, 12490, 1));
                        break;

                    case 6:
                        Effects.Add(new EffectDice(EffectsEnum.Effect_CastSpell_1175, 0, 12489, 1));
                        break;

                    case 7:
                        Effects.Add(new EffectDice(EffectsEnum.Effect_CastSpell_1175, 0, 12498, 1));
                        break;
                }
            }
            else if (legendaryFoodId == 20973 && legendary == new EffectBase())
            {
                var rnd3 = random.Next(1, 5);
                switch (rnd3)
                {
                    case 1:
                        Effects.Add(new EffectDice(EffectsEnum.Effect_CastSpell_1175, 0, 12475, 1));
                        break;

                    case 2:
                        Effects.Add(new EffectDice(EffectsEnum.Effect_CastSpell_1175, 0, 12480, 1));
                        break;

                    case 3:
                        Effects.Add(new EffectDice(EffectsEnum.Effect_CastSpell_1175, 0, 12481, 1));
                        break;

                    case 4:
                        Effects.Add(new EffectDice(EffectsEnum.Effect_CastSpell_1175, 0, 12479, 1));
                        break;

                    case 5:
                        Effects.Add(new EffectDice(EffectsEnum.Effect_CastSpell_1175, 0, 12477, 1));
                        break;
                }
            }
            else if (legendaryFoodId == 20976 && legendary == new EffectBase())
            {
                var rnd3 = random.Next(1, 6);
                switch (rnd3)
                {
                    case 1:
                        Effects.Add(new EffectDice(EffectsEnum.Effect_CastSpell_1175, 0, 12505, 1));
                        break;

                    case 2:
                        Effects.Add(new EffectDice(EffectsEnum.Effect_CastSpell_1175, 0, 12501, 1));
                        break;

                    case 3:
                        Effects.Add(new EffectDice(EffectsEnum.Effect_CastSpell_1175, 0, 12499, 1));
                        break;

                    case 4:
                        Effects.Add(new EffectDice(EffectsEnum.Effect_CastSpell_1175, 0, 12503, 1));
                        break;

                    case 5:
                        Effects.Add(new EffectDice(EffectsEnum.Effect_CastSpell_1175, 0, 12500, 1));
                        break;

                    case 6:
                        Effects.Add(new EffectDice(EffectsEnum.Effect_CastSpell_1175, 0, 12506, 1));
                        break;
                }
            }
            else
            {
                Owner.SendServerMessage("Votre familier possède un bonus spécial qui l'empeche de changer d'effet.", Color.OrangeRed);
            }
        }

        public override bool Feed(BasePlayerItem food)
        {
            if (IsDeleted || food.AppearanceId != 0)
                return false;

            if (food.Template.TypeId == (int)ItemTypeEnum.FAMILIER || food.Template.TypeId == (int)ItemTypeEnum.MONTILIER)
                return false;

            var xpToAdd = 0;
            var exp = food.Effects.Where(x => x.EffectId == EffectsEnum.Effect_Exp).FirstOrDefault();

            if (exp != null)
            {
                xpToAdd += (exp as EffectInteger).Value;
            }
            else if(food.Template.Id == 20968 && Level > 100)
            {
                Effects.Add(new EffectInteger(EffectsEnum.Effect_LegendaryState, 1));
            }
            else if(food.Template.Id == 20974 || food.Template.Id == 20975 || food.Template.Id == 20973 || food.Template.Id == 20976)
            {
                AddLegendaryEffect(food.Template.Id);
            }
            else
            {
                xpToAdd = 1;
            }

            if (Level <= 100)
                Experience += xpToAdd;

            while(Experience >= ExperienceManager.Instance.GetPetNextLevelExperience((ushort)Level) && Level <= 100)
            {
                Level++;
            }

            MaxExperience = (int)ExperienceManager.Instance.GetPetNextLevelExperience((ushort)Level);
            ChangeEffectsSquallingOnLevel(Level);

            Invalidate();
            Owner.Inventory.RefreshItem(this);

            return true;
        }

        private List<EffectBase> EffectsMax()
        {
            var effects = Template.Effects.Where(x => x.EffectId != EffectsEnum.Effect_PetLevel && x.EffectId != EffectsEnum.Effect_PetExp).ToList();
            return effects;
        }

        private void ChangeEffectsSquallingOnLevel(int level)
        {
            foreach(var effect in EffectsMax())
            {
                var effectToChange = Effects.OfType<EffectInteger>().Where(x => x.EffectId == effect.EffectId).FirstOrDefault();
                if(effectToChange != null)
                {
                    if (IsEquiped())
                    {
                        var handler = EffectManager.Instance.GetItemEffectHandler(effectToChange, Owner, this);
                        handler.Operation = ItemEffectHandler.HandlerOperation.UNAPPLY;
                        handler.Apply();
                        effectToChange.Value = (int)Math.Floor((effect as EffectDice).Max / 101f * level);
                        handler.Operation = ItemEffectHandler.HandlerOperation.APPLY;
                        handler.Apply();
                        Owner.RefreshStats();
                    }
                    else
                    {
                        effectToChange.Value = (int)Math.Floor(((effect as EffectDice).Max / 101f * level));
                    }
                }
            }
        }

        public override bool OnEquipItem(bool unequip)
        {
            if (unequip)
                return base.OnEquipItem(true);

            if (Owner.IsRiding)
                Owner.ForceDismount();

            return base.OnEquipItem(false);
        }

        public override ActorLook UpdateItemSkin(ActorLook characterLook)
        {
            var petLook = PetTemplate?.Look?.Clone();

            if (petLook == null)
            {
                if (Template.Type.ItemType != ItemTypeEnum.FAMILIER && Template.Type.ItemType != ItemTypeEnum.MONTILIER)
                    return characterLook;

                if (Template.Type.ItemType == ItemTypeEnum.MONTILIER)
                {
                    goto PETMOUNT;
                }
                if (IsEquiped())
                {
                    var appareanceId = Template.AppearanceId;

                    if (AppearanceId != 0)
                        appareanceId = AppearanceId;

                    characterLook.SetPetSkin((short)appareanceId, new short[] { 65 });
                    Color color1;
                    Color color2;
                    Color color3;
                    Color color4;
                    Color color5;
                    if (characterLook.Colors.TryGetValue(1, out color1) &&
                        characterLook.Colors.TryGetValue(2, out color2) &&
                        characterLook.Colors.TryGetValue(3, out color3) &&
                        characterLook.Colors.TryGetValue(4, out color4) &&
                        characterLook.Colors.TryGetValue(5, out color5))
                    {
                        if (characterLook.PetLook != null)
                        {
                            characterLook.PetLook.AddColor(1, color1);
                            characterLook.PetLook.AddColor(2, color2);
                            characterLook.PetLook.AddColor(3, color3);
                            characterLook.PetLook.AddColor(4, color4);
                            characterLook.PetLook.AddColor(5, color5);
                        }
                    }
                }
                else
                    characterLook.RemovePets();

                return characterLook;
            }
            PETMOUNT:
            switch (Template.Type.ItemType)
            {
                case ItemTypeEnum.FAMILIER:
                    if (IsEquiped())
                    {
                        if (AppearanceId != 0)
                            petLook.BonesID = (short)AppearanceId;

                        characterLook.SetPetSkin(petLook.BonesID, petLook.DefaultScales.ToArray());
                    }
                    else
                        characterLook.RemovePets();
                    break;
                case ItemTypeEnum.MONTILIER:
                    if (IsEquiped())
                    {
                        characterLook = characterLook.GetRiderLook() ?? characterLook;
                        petLook = ActorLook.Parse("{" + AppearanceId + "}");

                        Color color1;
                        Color color2;
                        if (characterLook.Colors.TryGetValue(3, out color1) &&
                            characterLook.Colors.TryGetValue(4, out color2))
                        {
                            petLook.AddColor(1, color1);
                            petLook.AddColor(2, color2);
                            petLook.AddColor(3, color1);
                            petLook.AddColor(4, color2);
                            petLook.AddColor(5, color2);
                        }

                        if (AppearanceId != 0)
                            petLook.BonesID = (short)AppearanceId;

                        characterLook.BonesID = 2;
                        petLook.SetRiderLook(characterLook);

                        return petLook;
                    }
                    else
                    {
                        var look = characterLook.GetRiderLook();

                        if (look != null)
                        {
                            characterLook = look;
                            characterLook.BonesID = 1;
                        }
                        return characterLook;
                    }
            }

            return characterLook;
        }
    }
}
