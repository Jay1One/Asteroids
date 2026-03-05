using MVVM;
using UI.Binders;
using Zenject;

namespace Infrastructure.Installers
{
    public class BindersInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BinderFactory.RegisterBinder<TextBinder>();
            BinderFactory.RegisterBinder<ButtonBinder>();
            BinderFactory.RegisterBinder<ViewSetterBinder<int>>();
            BinderFactory.RegisterBinder<ViewSetterBinder<float>>();
            BinderFactory.RegisterBinder<ViewSetterBinder<bool>>();
        }
    }
}