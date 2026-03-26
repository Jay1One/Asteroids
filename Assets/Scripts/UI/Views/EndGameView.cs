using MVVM;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Views
{
    public class EndGameView : MonoBehaviour
    {
        [SerializeField] private GameObject _window;
        
        [Data("RestartButton")]
        public Button RestartButton;
        
       [Setter("IsVisible")]
        public bool IsVisible
        {
            set => _window.SetActive(value);
        }
    }
}