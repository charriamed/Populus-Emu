
using Stump.DofusProtocol.Enums;
using Stump.Server.BaseServer.Commands;
using Stump.Server.WorldServer.Commands.Commands.Patterns;

public class LevelUp : TargetCommand
{
    public LevelUp()
    {
        Aliases = new[] { "levelup" };
        RequiredRole = RoleEnum.GameMaster_Padawan;
        Description = "Augmente le niveau d'un personnage.";
        AddParameter("amount", "amount", "Quantité de niveau a ajouté.", (short)1);
        AddTargetParameter(true, "Character who will level up");
    }

    public override void Execute(TriggerBase trigger)
    {
        foreach (var target in GetTargets(trigger))
        {
            byte delta;

            var amount = trigger.Get<short>("amount");
            if (amount > 0 && amount <= byte.MaxValue)
            {
                delta = (byte)(amount);
                target.LevelUp(delta);
                trigger.Reply("Agregados " + trigger.Bold("{0}") + " niveles a '{1}'.", delta, target.Name);

            }
            else if (amount < 0 && -amount <= byte.MaxValue)
            {
                trigger.ReplyError("Level invalide.");
            }
            else
            {
                trigger.ReplyError("Level invalide.");
            }
        }
    }
}
