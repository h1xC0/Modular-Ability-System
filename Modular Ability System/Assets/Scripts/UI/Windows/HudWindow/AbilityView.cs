using Game.Combat.Abilities;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Windows.HUDWindow
{
    public class AbilityView : RawView, IAbilityView
    {
        [SerializeField] private AbilityConfiguration _abilityConfiguration;
        [SerializeField] private TMP_Text _numberTMP;
        [SerializeField] private Image _icon;

        public void Initialize(int index, AbilityConfiguration abilityConfiguration)
        {
            _abilityConfiguration = abilityConfiguration;
        }
    }
}