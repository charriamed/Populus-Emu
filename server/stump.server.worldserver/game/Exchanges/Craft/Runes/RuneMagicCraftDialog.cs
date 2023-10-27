using System;
using System.Collections.Generic;
using System.Linq;
using Stump.Core.Extensions;
using Stump.Core.Mathematics;
using Stump.Core.Reflection;
using Stump.Core.Timers;
using Stump.DofusProtocol.Enums;
using Stump.Server.WorldServer.Game.Effects;
using Stump.Server.WorldServer.Game.Effects.Instances;
using Stump.Server.WorldServer.Game.Exchanges.Trades;
using Stump.Server.WorldServer.Game.Exchanges.Trades.Players;
using Stump.Server.WorldServer.Game.Interactives;
using Stump.Server.WorldServer.Game.Interactives.Skills;
using Stump.Server.WorldServer.Game.Items;
using Stump.Server.WorldServer.Game.Items.Player;
using Stump.Server.WorldServer.Game.Jobs;
using Stump.Server.WorldServer.Handlers.Basic;

namespace Stump.Server.WorldServer.Game.Exchanges.Craft.Runes
{
    public abstract class RuneMagicCraftDialog : BaseCraftDialog
    {
        public const int MAX_STAT_POWER_25 = 25;
        public const int MAX_STAT_POWER_50 = 50;
        public const int MAX_STAT_POWER_75 = 75;
        public const int MAX_STAT_POWER_100 = 100;
        public const int AUTOCRAFT_INTERVAL = 1000;

        private TimedTimerEntry m_autoCraftTimer;

        public RuneMagicCraftDialog(InteractiveObject interactive, Skill skill, Job job)
            : base(interactive, skill, job)
        {
        }

        public RuneCrafter RuneCrafter => Crafter as RuneCrafter;

        public PlayerTradeItem ItemToImprove
        {
            get;
            private set;
        }

        public IEnumerable<EffectInteger> ItemEffects => ItemToImprove.Effects.OfType<EffectInteger>();

        public PlayerTradeItem Rune
        {
            get;
            private set;
        }
        public PlayerTradeItem SpecialRune
        {
            get;
            private set;
        }

        public PlayerTradeItem SignatureRune
        {
            get;
            private set;
        }

        public PlayerTradeItem Potion
        {
            get; private set;
        }

        public PlayerTradeItem Orbe
        {
            get; private set;
        }

        public virtual void Open()
        {
            FirstTrader.ItemMoved += OnItemMoved;
            SecondTrader.ItemMoved += OnItemMoved;
        }

        public override void Close()
        {
            StopAutoCraft();
        }

        public void StopAutoCraft(ExchangeReplayStopReasonEnum reason = ExchangeReplayStopReasonEnum.STOPPED_REASON_USER)
        {
            if (m_autoCraftTimer != null)
            {
                m_autoCraftTimer.Stop();
                m_autoCraftTimer = null;

                OnAutoCraftStopped(reason);
                ChangeAmount(1);
            }
        }

        protected virtual void OnAutoCraftStopped(ExchangeReplayStopReasonEnum reason)
        {

        }

        protected virtual void OnItemMoved(Trader trader, TradeItem item, bool modified, int difference)
        {
            var playerItem = item as PlayerTradeItem;

            if (playerItem == null)
                return;

            if (item.Template.Type.ItemType == ItemTypeEnum.RUNE_DE_FORGEMAGIE && (playerItem != Rune || playerItem.Stack == 0))
            {
                Rune = playerItem.Stack > 0 ? playerItem : null;
            }
            else if ((item.Template.Type.ItemType == ItemTypeEnum.RUNE_DE_TRANSCENDANCE || item.Template.Type.ItemType == ItemTypeEnum.RUNE_DE_CORUPTION) && (playerItem != SpecialRune || playerItem.Stack == 0))
            {
                SpecialRune = playerItem.Stack > 0 ? playerItem : null;
            }
            else if (item.Template.Id == 7508 && (playerItem != SignatureRune || playerItem.Stack == 0))
            {
                SignatureRune = playerItem.Stack > 0 ? playerItem : null;
            }
            else if (item.Template.Type.ItemType == ItemTypeEnum.POTION_DE_FORGEMAGIE && (playerItem != Potion || playerItem.Stack == 0))
            {
                Potion = playerItem.Stack > 0 ? playerItem : null;
            }
            else if (item.Template.Type.ItemType == ItemTypeEnum.ORBE_DE_FORGEMAGIE && (playerItem != Orbe || playerItem.Stack == 0))
            {
                Orbe = playerItem.Stack > 0 ? playerItem : null;
            }
            else if (IsItemEditable(item) && (playerItem != ItemToImprove || playerItem.Stack == 0))
            {
                ItemToImprove = playerItem.Stack > 0 ? playerItem : null;
            }
        }

        public bool IsItemEditable(IItem item)
        {
            return Skill.SkillTemplate.ModifiableItemTypes.Contains((int)item.Template.TypeId);
        }


        public override bool CanMoveItem(BasePlayerItem item)
        {
            return item.Template.TypeId == (int)ItemTypeEnum.RUNE_DE_FORGEMAGIE || Skill.SkillTemplate.ModifiableItemTypes.Contains((int)item.Template.TypeId);
        }

        protected virtual void OnRuneApplied(CraftResultEnum result, MagicPoolStatus poolStatus)
        {
        }

        public void ApplyAllRunes()
        {
            if (m_autoCraftTimer != null)
                StopAutoCraft();

            if (Amount == 1 || Amount == 0)
                ApplyRune();
            else
                AutoCraft();
        }

        private void AutoCraft()
        {
            ApplyRune();
            if (ItemToImprove != null && Rune != null && Amount == -1)
                m_autoCraftTimer = Crafter.Character.Area.CallDelayed(AUTOCRAFT_INTERVAL, AutoCraft);
            else
                StopAutoCraft(ExchangeReplayStopReasonEnum.STOPPED_REASON_OK);
        }

        private void ApliedEffectsPotion(PlayerTradeItem potion)
        {
            var effect = ItemToImprove.Effects.FirstOrDefault(x => x.EffectId == EffectsEnum.Effect_DamageNeutral) as EffectInteger;
            if (effect != null)
            {
                var existantEffect = GetEffectToImprove(effect);



                double criticalSuccess, neutralSuccess, criticalFailure;
                GetChances(existantEffect, effect, out criticalSuccess, out neutralSuccess, out criticalFailure);

                var rand = new CryptoRandom();
                var randNumber = (int)(rand.NextDouble() * 100);

                if (randNumber <= criticalSuccess)
                {
                    BoostEffectPotion(effect);

                    OnRuneApplied(CraftResultEnum.CRAFT_SUCCESS, MagicPoolStatus.UNMODIFIED);
                }
                //else if (randNumber <= criticalSuccess + neutralSuccess)
                //{
                //    BoostEffectPotion(effect);
                //    int residual = DeBoostEffect(effect);

                //    OnRuneApplied(CraftResultEnum.CRAFT_SUCCESS, GetMagicPoolStatus(residual));
                //}
                else
                {
                    //int residual = DeBoostEffect(effect);

                    OnRuneApplied(CraftResultEnum.CRAFT_FAILED, GetMagicPoolStatus(0/* residual */));
                }
            }
        }

        private void BoostEffectPotion(EffectInteger potionEffect)
        {
            var effect = ItemToImprove.Effects.Find(x => x.EffectId == EffectsEnum.Effect_DamageNeutral);
            var effectCac = (EffectsEnum)GetWeaponEffect(Potion.PlayerItem.Template.Id);

            if (effect != null)
            {
                ItemToImprove.Effects.Remove(effect);
                var effect_ = (EffectDice)effect;
                ItemToImprove.Effects.Add(new EffectDice((short)effectCac, 0, effect_.DiceNum, effect_.DiceFace, new EffectBase()));
            }
        }

        public void ApplyRune()
        {
            if (ItemToImprove == null)
                return;

            if (ItemToImprove.Effects.Exists(x => x.EffectId == EffectsEnum.Effect_LivingObjectId))
            {
                Crafter.Character.SendServerMessage("Vous ne pouvez pas FM un item avec un Obvijevan équipé. Déliez le et réessayez.");
                return;
            }

            if (ItemToImprove.Effects.Exists(x => x.EffectId == EffectsEnum.Effect_Appearance || x.EffectId == EffectsEnum.Effect_Apparence_Wrapper))
            {
                Crafter.Character.SendServerMessage("Vous ne pouvez pas FM un item mimibioté ou possédant un objet d'apparat. Libérez le du mimibiote ou de son objet d'apparat et réessayez.");
                return;
            }

            if (ItemToImprove.Template.Level > Job.Level)
            {
                BasicHandler.SendSystemMessageDisplayMessage(Rune.Owner.Client, false, 22, ItemToImprove.Template.Level, ItemToImprove.Template.Level);
                return;
            }

            if (ItemToImprove.Effects.FirstOrDefault(x => x.EffectId == EffectsEnum.Effect_CantFM) != null)
            {
                Crafter.Character.OpenPopup("L'objet que vous essayez de modifier possède un enchantement empêchant la forgemagie.");
            }

            var rune = Rune;
            var potion = Potion;
            var orbe = Orbe;
            var specialrune = SpecialRune;
            var signature = SignatureRune;

            if (signature != null)
            {
                //SIGNATURE BY AERMYHN AND MODIFIED A BIT BY OXIZONE
                if (signature != null)
                {
                    signature.Owner.Inventory.RemoveItem(signature.PlayerItem, 1);
                    if (signature.Owner.Id == Crafter.Id)
                        Crafter.MoveItem((uint)signature.Guid, -1);
                    else
                    {
                        if (signature.Stack <= 1)
                        {
                            signature.Stack = 0;
                            signature.Owner.Inventory.RemoveItem(signature.PlayerItem);
                        }
                        else
                        {
                            signature.Stack -= 1;
                        }
                        (this as MultiRuneMagicCraftDialog)?.OnItemMoved(Crafter, signature, true, 1);
                    }

                    if (ItemToImprove.Effects.Exists(x => x.EffectId == EffectsEnum.Effect_985))
                    {
                        ItemToImprove.Effects.RemoveAll(x => x.EffectId == EffectsEnum.Effect_985);
                        ItemToImprove.Effects.Add(new EffectString(EffectsEnum.Effect_985, Crafter.Character.Name));
                    }
                    else
                    {
                        ItemToImprove.Effects.Add(new EffectString(EffectsEnum.Effect_985, Crafter.Character.Name));
                    }
                    Crafter.Character.SendServerMessage("Grâce à votre Rune de Signature, vous avez laissé votre nom sur l'objet que vous venez de FM.");
                }
            }

            if (orbe != null)
            {
                if (orbe.Template.Level < ItemToImprove.Template.Level)
                {
                    Crafter.Character.OpenPopup("Le niveau de cette orbe est trop faible pour être utiliser sur cet objet.");
                    OnRuneApplied(CraftResultEnum.CRAFT_SUCCESS, MagicPoolStatus.UNMODIFIED);
                }
                else
                {
                    //delete rune
                    orbe.Owner.Inventory.RemoveItem(orbe.PlayerItem, 1);
                    if (orbe.Owner.Id == Crafter.Id)
                        Crafter.MoveItem((uint)orbe.Guid, -1);
                    else
                    {
                        if (orbe.Stack <= 1)
                        {
                            orbe.Stack = 0;
                            orbe.Owner.Inventory.RemoveItem(orbe.PlayerItem);
                        }
                        else
                        {
                            orbe.Stack -= 1;
                        }
                        (this as MultiRuneMagicCraftDialog)?.OnItemMoved(Crafter, orbe, true, 1);
                    }
                    //final
                    var effects = Singleton<ItemManager>.Instance.GenerateItemEffects(ItemToImprove.Template);
                    ItemToImprove.PlayerItem.Effects = effects;
                    OnRuneApplied(CraftResultEnum.CRAFT_SUCCESS, MagicPoolStatus.UNMODIFIED);
                }
            }
            else if (potion != null)
            {
                ApliedEffectsPotion(potion);
                potion.Owner.Inventory.RemoveItem(potion.PlayerItem, 1);
                if (potion.Owner.Id == Crafter.Id)
                    Crafter.MoveItem((uint)potion.Guid, -1);
                else
                {
                    if (potion.Stack <= 1)
                    {
                        potion.Stack = 0;
                        potion.Owner.Inventory.RemoveItem(potion.PlayerItem);
                    }
                    else
                    {
                        potion.Stack -= 1;
                    }
                    (this as MultiRuneMagicCraftDialog)?.OnItemMoved(Crafter, potion, true, 1);
                }
            }
            else if (specialrune != null)
            {
                if (ItemToImprove.Effects.Where(x => x.EffectId != EffectsEnum.Effect_PowerSink).Any(x => IsExotic(x) || IsOverMax(x as EffectInteger)))
                {
                    Crafter.Character.OpenPopup("Vous ne pouvez pas appliquer cette rune sur un objet possédant un over ou un exo.");
                    return;
                }

                specialrune.Owner.Inventory.RemoveItem(specialrune.PlayerItem, 1);
                if (specialrune.Owner.Id == Crafter.Id)
                    Crafter.MoveItem((uint)specialrune.Guid, -1);
                else
                {
                    if (specialrune.Stack <= 1)
                    {
                        specialrune.Stack = 0;
                        specialrune.Owner.Inventory.RemoveItem(specialrune.PlayerItem);
                    }
                    else
                    {
                        specialrune.Stack -= 1;
                    }
                    (this as MultiRuneMagicCraftDialog)?.OnItemMoved(Crafter, specialrune, true, 1);
                }

                foreach (var effect in specialrune.Effects.OfType<EffectInteger>().Where(x => x.EffectId != EffectsEnum.Effect_FMPercentOfChance))
                {
                    var percent = (specialrune.Effects.FirstOrDefault(x => x.EffectId == EffectsEnum.Effect_FMPercentOfChance) as EffectInteger);
                    if (percent == null)
                        return;

                    var existantEffect = GetEffectToImprove(effect);

                    var rand = new CryptoRandom();
                    var randNumber = (int)(rand.NextDouble() * 100);

                    if (randNumber <= percent.Value)
                    {
                        if (effect.Template.Operator != "-")
                            BoostEffect(effect);
                        else
                            BoostEffect(effect, true);

                        OnRuneApplied(CraftResultEnum.CRAFT_SUCCESS, MagicPoolStatus.UNMODIFIED);
                    }
                }
            }
            else if (rune != null)
            {
                rune.Owner.Inventory.RemoveItem(rune.PlayerItem, 1);
                if (rune.Owner.Id == Crafter.Id)
                    Crafter.MoveItem((uint)rune.Guid, -1);
                else
                {
                    if (rune.Stack <= 1)
                    {
                        rune.Stack = 0;
                        rune.Owner.Inventory.RemoveItem(rune.PlayerItem);
                    }
                    else
                    {
                        rune.Stack -= 1;
                    }
                    (this as MultiRuneMagicCraftDialog)?.OnItemMoved(Crafter, rune, true, 1);
                }

                foreach (var effect in rune.Effects.OfType<EffectInteger>())
                {
                    var existantEffect = GetEffectToImprove(effect);

                    double criticalSuccess, neutralSuccess, criticalFailure;
                    GetChances(existantEffect, effect, out criticalSuccess, out neutralSuccess, out criticalFailure);

                    var rand = new CryptoRandom();
                    var randNumber = (int)(rand.NextDouble() * 100);

                    if (randNumber == 0 && criticalSuccess == 0 && neutralSuccess == 0 && criticalFailure == 100)
                        randNumber = 1;

                    var percent = (20 * EffectManager.Instance.GetEffectPower(effect)) * (Job.Level - ItemToImprove.Template.Level) * 7 / 100;
                    var xp = 20 * EffectManager.Instance.GetEffectPower(effect) - percent;

                    if (randNumber <= criticalSuccess)
                    {
                        BoostEffect(effect);

                        if (xp <= 0)
                            Job.Experience += 1;
                        else
                            Job.Experience += (long)xp;

                        OnRuneApplied(CraftResultEnum.CRAFT_SUCCESS, MagicPoolStatus.UNMODIFIED);
                    }
                    else if (randNumber <= criticalSuccess + neutralSuccess)
                    {
                        BoostEffect(effect);

                        if (xp <= 0)
                            Job.Experience += 1;
                        else
                            Job.Experience += (long)xp;
                        int residual = DeBoostEffect(effect);

                        OnRuneApplied(CraftResultEnum.CRAFT_SUCCESS, GetMagicPoolStatus(residual));
                    }
                    else
                    {
                        int residual = DeBoostEffect(effect);

                        OnRuneApplied(CraftResultEnum.CRAFT_FAILED, GetMagicPoolStatus(residual));
                    }
                }
            }

            ItemToImprove.PlayerItem.Invalidate();
            ItemToImprove.Owner.Inventory.RefreshItem(ItemToImprove.PlayerItem);

        }
        private MagicPoolStatus GetMagicPoolStatus(int residual)
        {
            return residual == 0 ? MagicPoolStatus.UNMODIFIED : (residual > 0 ? MagicPoolStatus.INCREASED : MagicPoolStatus.DECREASED);
        }

        private void BoostEffect(EffectInteger runeEffect, bool decrease = false)
        {
            var effect = GetEffectToImprove(runeEffect);


            if (effect != null)
            {
                if (decrease)
                {
                    effect.Value -= (short)((effect.Template.BonusType == -1 ? -1 : 1) * runeEffect.Value);

                    if (effect.Value == 0)
                        ItemToImprove.Effects.Remove(effect);
                    else if (effect.Value < 0 && effect.Value >= -runeEffect.Value && effect.Template.OppositeId > 0) // from negativ to positiv
                    {
                        ItemToImprove.Effects.Remove(effect);
                        ItemToImprove.Effects.Add(new EffectInteger((EffectsEnum)effect.Template.OppositeId, Math.Abs(effect.Value)));
                    }
                }
                else
                {
                    effect.Value += (short)((effect.Template.BonusType == -1 ? -1 : 1) * runeEffect.Value);

                    if (effect.Value == 0)
                        ItemToImprove.Effects.Remove(effect);
                    else if (effect.Value > 0 && effect.Value <= runeEffect.Value && effect.Template.OppositeId > 0) // from negativ to positiv
                    {
                        ItemToImprove.Effects.Remove(effect);
                        ItemToImprove.Effects.Add(new EffectInteger((EffectsEnum)effect.Template.OppositeId, effect.Value));
                    }
                }
            }
            else
            {
                ItemToImprove.Effects.Add(new EffectInteger(runeEffect.EffectId, runeEffect.Value));
            }
        }

        private int DeBoostEffect(EffectInteger runeEffect)
        {
            var pwrToLose = (int)Math.Ceiling(EffectManager.Instance.GetEffectPower(runeEffect));
            short residual = 0;

            if (ItemToImprove.PlayerItem.PowerSink > 0)
            {
                residual = (short)-Math.Min(pwrToLose, ItemToImprove.PlayerItem.PowerSink);
                ItemToImprove.PlayerItem.PowerSink += residual;
                pwrToLose += residual;
            }

            if (pwrToLose == 0)
                return residual;

            while (pwrToLose > 0)
            {
                var effect = GetEffectToDown(runeEffect);

                if (effect == null)
                    break;

                var maxLost = (int)Math.Ceiling(EffectManager.Instance.GetEffectBasePower(runeEffect) / Math.Abs(EffectManager.Instance.GetEffectBasePower(effect)));

                var rand = new CryptoRandom();
                var lost = rand.Next(1, maxLost + 1);

                var oldValue = effect.Value;

                if (effect.Template.BonusType == -1 && effect.Value + lost > (ItemToImprove.Template.Effects.FirstOrDefault(x => x.EffectId == effect.EffectId) as EffectDice).Min * 2)
                    return 0;

                effect.Value -= (short)((effect.Template.BonusType == -1 ? -1 : 1) * lost);
                pwrToLose -= (int)Math.Ceiling(lost * Math.Abs(EffectManager.Instance.GetEffectBasePower(effect)));

                if (effect.Value == 0 || (effect.Value < 0 && oldValue > 0))
                {
                    if (effect.Template.Id == 96 || effect.Template.Id == 97 || effect.Template.Id == 98 || effect.Template.Id == 99 ||
                        effect.Template.Id == 100 || effect.Template.Id == 91 || effect.Template.Id == 92 || effect.Template.Id == 93 ||
                        effect.Template.Id == 94 || effect.Template.Id == 95 || effect.Template.Id == 101 || effect.Template.Id == 108)
                    {
                        break;
                    }
                    else
                    {
                        ItemToImprove.Effects.Remove(effect);
                    }
                }
                else if (effect.Value < 0 && effect.Value >= -lost && effect.Template.OppositeId > 0) // from positiv to negativ stat
                {
                    if (effect.Template.Id == 96 || effect.Template.Id == 97 || effect.Template.Id == 98 || effect.Template.Id == 99 ||
                        effect.Template.Id == 100 || effect.Template.Id == 91 || effect.Template.Id == 92 || effect.Template.Id == 93 ||
                        effect.Template.Id == 94 || effect.Template.Id == 95 || effect.Template.Id == 101 || effect.Template.Id == 108)
                    {
                        break;
                    }
                    else
                    {
                        ItemToImprove.Effects.Remove(effect);
                        ItemToImprove.Effects.Add(new EffectInteger((EffectsEnum)effect.Template.OppositeId, (short)-effect.Value));
                    }

                }
            }

            residual = (short)(pwrToLose < 0 ? -pwrToLose : 0);
            ItemToImprove.PlayerItem.PowerSink += residual;

            return residual;
        }

        private EffectInteger GetEffectToImprove(EffectInteger runeEffect)
        {
            return ItemEffects.FirstOrDefault(x => x.EffectId == runeEffect.EffectId || (x.Template.OppositeId != 0 && x.Template.OppositeId == runeEffect.Id));
        }

        private EffectInteger GetEffectToDown(EffectInteger runeEffect)
        {
            var effectToImprove = GetEffectToImprove(runeEffect);
            // recherche de jet exotique
            var exoticEffect = ItemEffects.Where(x => IsExotic(x) && x != effectToImprove).RandomElementOrDefault();

            if (exoticEffect != null)
                return exoticEffect;

            // recherche de jet overmax
            var overmaxEffect = ItemEffects.Where(x => IsOverMax(x, runeEffect) && x != effectToImprove).RandomElementOrDefault();

            if (overmaxEffect != null)
                return overmaxEffect;

            var rand = new CryptoRandom();

            if (ItemToImprove.Template.Level >= 1 && ItemToImprove.Template.Level <= 50)
            {
                foreach (var effect in ItemEffects.ShuffleLinq().Where(x => x != effectToImprove))
                {
                    if (EffectManager.Instance.GetEffectPower(effect) - EffectManager.Instance.GetEffectPower(runeEffect) < MAX_STAT_POWER_25)
                        continue;

                    if (rand.NextDouble() <= EffectManager.Instance.GetEffectPower(runeEffect) / Math.Abs(EffectManager.Instance.GetEffectBasePower(effect)))
                        return effect;
                }
            }
            else if (ItemToImprove.Template.Level >= 51 && ItemToImprove.Template.Level <= 100)
            {
                foreach (var effect in ItemEffects.ShuffleLinq().Where(x => x != effectToImprove))
                {
                    if (EffectManager.Instance.GetEffectPower(effect) - EffectManager.Instance.GetEffectPower(runeEffect) < MAX_STAT_POWER_50)
                        continue;

                    if (rand.NextDouble() <= EffectManager.Instance.GetEffectPower(runeEffect) / Math.Abs(EffectManager.Instance.GetEffectBasePower(effect)))
                        return effect;
                }
            }
            else if (ItemToImprove.Template.Level >= 101 && ItemToImprove.Template.Level <= 150)
            {
                foreach (var effect in ItemEffects.ShuffleLinq().Where(x => x != effectToImprove))
                {
                    if (EffectManager.Instance.GetEffectPower(effect) - EffectManager.Instance.GetEffectPower(runeEffect) < MAX_STAT_POWER_75)
                        continue;

                    if (rand.NextDouble() <= EffectManager.Instance.GetEffectPower(runeEffect) / Math.Abs(EffectManager.Instance.GetEffectBasePower(effect)))
                        return effect;
                }
            }
            else if (ItemToImprove.Template.Level >= 151)
            {
                foreach (var effect in ItemEffects.ShuffleLinq().Where(x => x != effectToImprove))
                {
                    if (EffectManager.Instance.GetEffectPower(effect) - EffectManager.Instance.GetEffectPower(runeEffect) < MAX_STAT_POWER_100)
                        continue;

                    if (rand.NextDouble() <= EffectManager.Instance.GetEffectPower(runeEffect) / Math.Abs(EffectManager.Instance.GetEffectBasePower(effect)))
                        return effect;
                }
            }

            return ItemEffects.FirstOrDefault(x => x != effectToImprove);
        }

        private bool IsExotic(EffectBase effect)
        {
            return ItemToImprove.Template.Effects.All(x => x.EffectId != effect.EffectId);
        }

        private double GetExoticPower()
        {
            return ItemToImprove.Effects.Where(x => x.EffectId != EffectsEnum.Effect_PowerSink && IsExotic(x)).OfType<EffectInteger>().Sum(x => EffectManager.Instance.GetEffectPower(x));
        }

        private bool IsOverMax(EffectInteger effect)
        {
            var template = GetTemplateEffect(effect);

            return effect.Template.BonusType > -1 && effect.Value > template?.Max;
        }

        private bool IsOverMax(EffectInteger effect, EffectInteger runeEffect)
        {
            var template = GetTemplateEffect(effect);

            return effect.Template.BonusType > -1 && effect.Value + runeEffect.Value > template?.Max;
        }

        private EffectDice GetTemplateEffect(EffectBase effect)
        {
            return ItemToImprove.Template.Effects.OfType<EffectDice>().FirstOrDefault(x => x.EffectId == effect.EffectId || (x.Template.OppositeId > 0 && x.Template.OppositeId == (int)effect.EffectId));
        }

        private void GetChances(EffectInteger effectToImprove, EffectInteger runeEffect, out double criticalSuccess, out double neutralSuccess, out double criticalFailure)
        {
            var minPwr = EffectManager.Instance.GetItemMinPower(ItemToImprove);
            var maxPwr = EffectManager.Instance.GetItemMaxPower(ItemToImprove);
            var pwr = EffectManager.Instance.GetItemPower(ItemToImprove);

            double itemStatus = Math.Max(0, GetProgress(pwr, maxPwr, minPwr) * 100);
            var parentEffect = GetTemplateEffect(runeEffect);

            double max = 0;
            if (effectToImprove != null && !IsExotic(effectToImprove))
                max = EffectManager.Instance.GetEffectPower(runeEffect.Template) * GetTemplateEffect(effectToImprove).Max;

            if (ItemToImprove.Template.Level >= 1 && ItemToImprove.Template.Level <= 50)
            {
                if (effectToImprove != null && (!IsExotic(effectToImprove) ? EffectManager.Instance.GetEffectPower(effectToImprove) + EffectManager.Instance.GetEffectPower(runeEffect) > max + MAX_STAT_POWER_25 : EffectManager.Instance.GetEffectPower(effectToImprove) + EffectManager.Instance.GetEffectPower(runeEffect) > MAX_STAT_POWER_25 && EffectManager.Instance.GetEffectPower(effectToImprove) + EffectManager.Instance.GetEffectPower(runeEffect) > MAX_STAT_POWER_100 || GetExoticPower() > MAX_STAT_POWER_25))
                {
                    neutralSuccess = 0;
                    criticalSuccess = 0;
                    criticalFailure = 100;
                    return;
                }
            }
            else if (ItemToImprove.Template.Level >= 51 && ItemToImprove.Template.Level <= 100)
            {
                if (effectToImprove != null && (!IsExotic(effectToImprove) ? EffectManager.Instance.GetEffectPower(effectToImprove) + EffectManager.Instance.GetEffectPower(runeEffect) > max + MAX_STAT_POWER_50 : EffectManager.Instance.GetEffectPower(effectToImprove) + EffectManager.Instance.GetEffectPower(runeEffect) > MAX_STAT_POWER_50 && EffectManager.Instance.GetEffectPower(effectToImprove) + EffectManager.Instance.GetEffectPower(runeEffect) > MAX_STAT_POWER_100 || GetExoticPower() > MAX_STAT_POWER_50))
                {
                    neutralSuccess = 0;
                    criticalSuccess = 0;
                    criticalFailure = 100;
                    return;
                }
            }
            else if (ItemToImprove.Template.Level >= 101 && ItemToImprove.Template.Level <= 150)
            {
                if (effectToImprove != null && (!IsExotic(effectToImprove) ? EffectManager.Instance.GetEffectPower(effectToImprove) + EffectManager.Instance.GetEffectPower(runeEffect) > max + MAX_STAT_POWER_75 : EffectManager.Instance.GetEffectPower(effectToImprove) + EffectManager.Instance.GetEffectPower(runeEffect) > MAX_STAT_POWER_75 && EffectManager.Instance.GetEffectPower(effectToImprove) + EffectManager.Instance.GetEffectPower(runeEffect) > MAX_STAT_POWER_100 || GetExoticPower() > MAX_STAT_POWER_75))
                {
                    neutralSuccess = 0;
                    criticalSuccess = 0;
                    criticalFailure = 100;
                    return;
                }
            }
            else if (ItemToImprove.Template.Level >= 151)
            {
                if (effectToImprove != null && (EffectManager.Instance.GetEffectPower(effectToImprove) + EffectManager.Instance.GetEffectPower(runeEffect) > MAX_STAT_POWER_100 || GetExoticPower() > MAX_STAT_POWER_100))
                {
                    neutralSuccess = 0;
                    criticalSuccess = 0;
                    criticalFailure = 100;
                    return;
                }
            }

            double effectStatus;
            double diceFactor;
            double itemFactor;
            double effectSuccess;
            double itemSuccess;
            if (parentEffect == null) // exo
            {
                effectStatus = 100;
                itemStatus = 89 + Math.Sqrt(EffectManager.Instance.GetEffectPower(runeEffect)) + Math.Sqrt(itemStatus);
                diceFactor = 40;
                itemFactor = 54;
            }
            else
            {
                effectStatus = Math.Max(0, GetProgress(effectToImprove?.Value ?? 0, parentEffect.Max, parentEffect.Min) * 100);

                if (effectToImprove != null && IsOverMax(effectToImprove, runeEffect))
                {
                    itemStatus = Math.Max(itemStatus, effectStatus / 2);
                    effectStatus += EffectManager.Instance.GetEffectPower(runeEffect);
                }

                diceFactor = 30;
                itemFactor = 50;
            }

            effectStatus = Math.Min(100, effectStatus);
            itemStatus = Math.Min(99, itemStatus);

            if (effectStatus >= 80)
                effectSuccess = diceFactor * effectStatus / 100;
            else
                effectSuccess = effectStatus / 4;

            if (itemStatus >= 50)
                itemSuccess = itemFactor * itemStatus / 100;
            else
                itemSuccess = itemStatus;

            neutralSuccess = 50d;
            criticalSuccess = Math.Max(1, 100 - Math.Ceiling(effectSuccess + itemSuccess));

            if (criticalSuccess > 50)
                neutralSuccess = 100 - criticalSuccess;
            else if (criticalSuccess < 35)
                neutralSuccess = 25 + criticalSuccess;

            criticalFailure = 100 - (criticalSuccess + neutralSuccess);
        }

        private double GetProgress(double value, double max, double min)
        {
            if (min < 0 || max < 0)
            {
                var x = max;
                max = -min;
                min = -x;
            }

            if (max == min && max != 0) return value / max;
            else if (max == 0) return 1d;
            else return (value - min) / (max - min);
        }

        protected int GetWeaponEffect(int integerEffect)
        {
            switch (integerEffect)
            {
                case 1333:
                case 1343:
                case 1345:
                    return 99;
                case 1341:
                case 1325:
                case 1346:
                    return 96;
                case 1337:
                case 1342:
                case 1347:
                    return 98;
                case 1338:
                case 1340:
                case 1348:
                    return 97;
                default:
                    return 2;
            }
        }
    }
}