
using Assets.Scripts.DemonAbilities;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.MatchUI
{
    public abstract class UIDeckCard : UICard
    {
        [SerializeField]
        protected List<UIAbility> uIAbilities = null;

    }
}
