using Assets.MirrorApp;
using Assets.Scripts.GameProcessors;
using Assets.Scripts.HTTPs.Requests;
using Assets.Scripts.Managers;
using Assets.Scripts.MatchMessages.Requests;
using Newtonsoft.Json;

namespace Assets.Scripts.SpellBooks.GameProcessorSpellBooks
{
    public class OnApplySpell : GameProcessor
    {
        public override void Process(string data)
        {
            ApplySpellRequest request = new ApplySpellRequest(
                MirrorAppClient.Instance.LocalActorNumber,
                MirrorAppClient.Instance.RemoteActorNumber,
                data);

            HTTPManager.Instance.HellmasterData(new ApplySpellMessage()
            {
                applySpellRequest = JsonConvert.SerializeObject(request)
            });
        }
    }
}
