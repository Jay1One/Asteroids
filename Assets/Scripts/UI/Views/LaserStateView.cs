using MVVM;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Views
{
    public class LaserStateView : MonoBehaviour
    {
        [SerializeField] private Image _fillImage;

        [Data("Charges")]
        public TMP_Text CurrentChargesText;
        
        [Setter("CooldownPercent")]
        public float CooldownPercent
        {
            set => _fillImage.fillAmount = value;
        }
        
    }
}