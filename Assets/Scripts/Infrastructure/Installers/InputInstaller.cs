using GamePlay.Input;
using GamePlay.Input.Mobile_controls;
using UnityEngine;
using Zenject;

namespace Infrastructure.Installers
{
    public class InputInstaller : MonoInstaller
    {
        [SerializeField] private GameObject _mobileInputPrefab;

        public override void InstallBindings()
        {
#if (UNITY_IOS || UNITY_ANDROID) && !UNITY_EDITOR
            InstallMobileInput(); 
#else
            InstallPCInput();
#endif
        }

        private void InstallMobileInput()
        {
            Container.BindInterfacesAndSelfTo<MobileInput>().AsSingle().
                WithArguments(_mobileInputPrefab, Container).NonLazy();
        }

        private void InstallPCInput()
        {
            Container.BindInterfacesAndSelfTo<MouseAndKeyBoardInput>().AsSingle();
        }
    }
}