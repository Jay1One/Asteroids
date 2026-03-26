using System;
using GamePlay.Combat.Units;
using MVVM;
using UniRx;
using UnityEngine;
using Zenject;

namespace UI.ViewModels
{
    public class CoordinatesViewModel :IInitializable, IDisposable
    {
        private readonly Player _player;
        
        [Data("Coordinates")]
        public ReactiveProperty<string> Coordinates = new ReactiveProperty<string>();

        public CoordinatesViewModel(Player player)
        {
            _player = player;
        }

        private void OnCoordinatesChanged(Vector2 coordinates)
        {
            Coordinates.Value =$"x: {coordinates.x :F1} \ny: {coordinates.y :F1}";
        }

        public void Initialize()
        {
            _player.CoordinatesChanged += OnCoordinatesChanged;
        }

        public void Dispose()
        {
            _player.CoordinatesChanged -= OnCoordinatesChanged;
        }
    }
}