using UnityEngine;
using Zenject;

namespace GamePlay.Input.Mobile_controls
{
    public class MobileInput : IInitializable
    {
        private GameObject _mobileInputPrefab;
        private DiContainer _container;

        public MobileInput(GameObject mobileInputPrefab, DiContainer container)
        {
            _mobileInputPrefab = mobileInputPrefab;
            _container = container;
        }
        public void Initialize()
        {
            _container.InstantiatePrefab(_mobileInputPrefab);
        }
    }
}