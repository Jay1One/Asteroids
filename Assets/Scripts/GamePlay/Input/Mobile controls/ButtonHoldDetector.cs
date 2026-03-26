using UnityEngine.Events;
using UnityEngine;
using UnityEngine.EventSystems;

namespace GamePlay.Input.Mobile_controls
{
    public class ButtonHoldDetector : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler
    {
        [SerializeField] private UnityEvent OnButtonHold;
        [SerializeField] private UnityEvent OnButtonRelease;
        private bool _isPressed;

        public void OnPointerDown(PointerEventData eventData)
        {
            _isPressed = true;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            _isPressed = false;
            OnButtonRelease?.Invoke();
        }

        private void Update()
        {
            if (_isPressed)
            {
                OnButtonHold?.Invoke();
            }
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (_isPressed)
            {
                OnPointerUp(eventData);
            }
        }
    }
}