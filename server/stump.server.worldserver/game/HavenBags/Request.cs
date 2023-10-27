using Stump.Server.WorldServer.Game.Actors.RolePlay.Characters;
using Stump.Server.WorldServer.Game.Dialogs;

namespace Stump.Server.WorldServer.Game.HavenBags
{
    public class HavenBagInvitationRequest : RequestBox
    {
        public HavenBagInvitationRequest(Character source, Character target) :
            base(source, target)
        {
        }

        protected override void OnOpen()
        {
            Target.Client.Send(new DofusProtocol.Messages.InviteInHavenBagOfferMessage(Source.GetCharacterMinimalInformations(), 300));
        }

        protected override void OnAccept()
        {
            HavenBagManager.Instance.HandleHavenBagEnter(Target.Client, Source.Id, true);
        }
    }
}