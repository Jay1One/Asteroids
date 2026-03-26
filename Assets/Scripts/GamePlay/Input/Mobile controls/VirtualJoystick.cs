using GamePlay.Combat.Units;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace GamePlay.Input.Mobile_controls
{
    public class VirtualJoystick : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
    {
        [SerializeField] private RectTransform _handle;
        [SerializeField] private RectTransform _base;
        
        private Vector2 _handleStartPosition;
        private float _baseRadius;
        private float _handleRadius;
        private Vector2 _inputVector;
        private bool _isDragging;

        private Player _player;
        
        [Inject]
        private void Construct(Player player)
        {
            _player = player;
        }

        private void Start()
        {
            _handleStartPosition = _handle.anchoredPosition;
            _handleRadius = _handle.rect.width / 2f;
            _baseRadius = _base.rect.width / 2f - _handleRadius;
        }
        

        public void OnDrag(PointerEventData eventData)
        {
            PlaceHandle(eventData.position); 
            CalculateInputVector(); 
        }

        private void Update()
        {
            if (_isDragging)
            {
                _player.TryRotate(_inputVector);
            }
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            _isDragging = false;
            _handle.anchoredPosition = _handleStartPosition;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            _isDragging = true;
        }
        
        private void CalculateInputVector()
        {
            _inputVector = _handle.anchoredPosition / _baseRadius;
        }
        
        private void PlaceHandle(Vector2 inputPosition)
        {
            Vector2 directPosition = inputPosition - (Vector2)_base.position;

            if (directPosition.magnitude > _baseRadius)
            {
                directPosition = directPosition.normalized * _baseRadius;
            }
            
            _handle.anchoredPosition = directPosition;
        }
    }
}