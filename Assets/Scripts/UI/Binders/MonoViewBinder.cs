using System;
using MVVM;
using UnityEditor;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

namespace UI.Binders
{
    public sealed class MonoViewBinder : MonoBehaviour
    {
        private enum BindingMode
        {
            FromInstance = 0,
            FromResolve = 1,
            FromResolveId = 2
        }

        [SerializeField]
        private BindingMode viewBinding;
        
        [SerializeField]
        private Object _view;
        
        [SerializeField]
        private MonoScript viewType;
        
        [SerializeField]
        private string viewId;

        [Space(8)]
        [SerializeField]
        private BindingMode viewModelBinding;
        
        [SerializeField]
        private Object viewModel;
        
        [SerializeField]
        private MonoScript viewModelType;
        
        [SerializeField]
        private string viewModelId;

        [Inject]
        private DiContainer _diContainer;

        private IBinder _binder;

        private void Awake()
        {
            _binder = CreateBinder();
        }

        private void OnEnable()
        {
            _binder.Bind();
        }

        private void OnDisable()
        {
            _binder.Unbind();
        }

        private IBinder CreateBinder()
        {
            object view = this.viewBinding switch
            {
                BindingMode.FromInstance => _view,
                BindingMode.FromResolve => _diContainer.Resolve(viewType.GetClass()),
                BindingMode.FromResolveId => _diContainer.ResolveId(viewType.GetClass(), viewId),
                _ => throw new Exception($"Binding type of view {viewBinding} is not found!")
            };

            object model = this.viewModelBinding switch
            {
                BindingMode.FromInstance => viewModel,
                BindingMode.FromResolve => _diContainer.Resolve(viewModelType.GetClass()),
                BindingMode.FromResolveId => _diContainer.ResolveId(viewModelType.GetClass(), viewModelId),
                _ => throw new Exception($"Binding type of view {viewBinding} is not found!")
            };

            return BinderFactory.CreateComposite(view, model);
        }
    }
}