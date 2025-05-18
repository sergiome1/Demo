using Assets.Scripts.GameProcessors;
using Assets.Scripts.HTTPs.Requests;
using Assets.Scripts.Managers;
using Assets.Scripts.MatchMessages.Requests;
using Newtonsoft.Json;

namespace Assets.Scripts.HeroAbilities.GameProcessorHeroAbilities
{
    public class OnApplyHeroeAbility : GameProcessor
    {
        public override void Process(string data)
        {
            ApplyHeroeAbilityRequest request = JsonConvert.DeserializeObject<ApplyHeroeAbilityRequest>(data);

            HTTPManager.Instance.HellmasterData(new ApplyHeroeAbilityMessage()
            {
                roomId = request.roomId,
                sourceActorNumber = request.sourceActorNumber,
                targetActorNumber = request.targetActorNumber,
                heroeType = request.heroeType,
                turnStep = request.turnStep,
                powerData = JsonConvert.SerializeObject(request.powerData)
            });
        }
    }
}
