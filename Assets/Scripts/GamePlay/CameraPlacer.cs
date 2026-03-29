using GamePlay.Combat.Systems;
using UnityEngine;
using Zenject;

namespace GamePlay
{
    public class CameraPlacer : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private float _topViewPortBorder;
        [SerializeField] private float _bottomViewPortBorder;
        private GameField _gameField;

        [Inject]
        public void Construct(GameField gameField)
        {
            _gameField = gameField;
        }
    
        private void Start()
        {
#if !(UNITY_IOS || UNITY_ANDROID) || UNITY_EDITOR
            _bottomViewPortBorder=0f;
#endif
            float minCameraWidth = _gameField.Width;
            float minCameraHeight = _gameField.Height / (_topViewPortBorder - _bottomViewPortBorder);
            float cameraCenterYOffsetViewPort = (1-_topViewPortBorder - _bottomViewPortBorder) / 2f;
            _camera.orthographicSize = minCameraHeight / 2;
            _camera.transform.Translate(new Vector2(0,_camera.orthographicSize*2*cameraCenterYOffsetViewPort));

            float cameraWidth = _camera.orthographicSize * 2f * _camera.aspect;
        
            if (cameraWidth < minCameraWidth)
            {
                float scaleFactor = minCameraWidth / cameraWidth;
                _camera.orthographicSize *= scaleFactor;
            }
        }
    }
}