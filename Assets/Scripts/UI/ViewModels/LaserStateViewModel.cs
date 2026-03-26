using System;
using GamePlay.Combat.Weapons;
using MVVM;
using UniRx;
using Zenject;

namespace UI.ViewModels
{
    public class LaserStateViewModel : IInitializable, IDisposable
    {
        private readonly Laser _laser;
        
        [Data("Charges")]
        public ReactiveProperty<string> CurrentCharges = new ReactiveProperty<string>();
        
        [Data("CooldownPercent")]
        public ReactiveProperty<float> CooldownPercent = new ReactiveProperty<float>();

        public LaserStateViewModel(Laser laser)
        {
           _laser = laser; 
        }
        public void Initialize()
        {
            _laser.ChargesChanged += OnChargesChanged;
            _laser.CooldownPercentageChanged += OnCooldownPercentChanged;
        }

        public void Dispose()
        {
            _laser.ChargesChanged-= OnChargesChanged;
            _laser.CooldownPercentageChanged -= OnCooldownPercentChanged;
        }

        private void OnChargesChanged(int charges)
        {
            CurrentCharges.Value = charges.ToString();
        }

        private void OnCooldownPercentChanged(float percent)
        {
            CooldownPercent.Value = percent;
        }
    }
}