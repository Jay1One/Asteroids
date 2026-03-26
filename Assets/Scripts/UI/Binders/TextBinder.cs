using System;
using MVVM;
using TMPro;
using UniRx;

namespace UI.Binders
{
    public class TextBinder :IBinder, IObserver<string>
    {
        private readonly TMP_Text _view;
        private readonly IReadOnlyReactiveProperty<string> _property;
        private IDisposable _handle;

        public TextBinder(TMP_Text view, IReadOnlyReactiveProperty<string> property)
        {
            _view = view;
            _property = property;
        }
        public void Bind()
        {
            _handle = _property.Subscribe(this);
            OnNext(_property.Value);
        }

        public void Unbind()
        {
            _handle?.Dispose();
            _handle = null;
        }

        public void OnCompleted()
        {
        }

        public void OnError(Exception error)
        {
        }

        public void OnNext(string value)
        {
            _view.text = value;
        }
    }
}