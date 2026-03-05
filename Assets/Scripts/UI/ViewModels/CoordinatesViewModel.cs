using System;
using GamePlay.Combat.Units.Player_mechanics;
using MVVM;
using UniRx;
using UnityEngine;
using Zenject;

namespace UI.ViewModels
{
    public class CoordinatesViewModel :IInitializable, IDisposable
    {
        private readonly PlayerMovement _playerMovement;
        
        [Data("Coordinates")]
        public ReactiveProperty<string> Coordinates = new ReactiveProperty<string>();

        public CoordinatesViewModel(PlayerMovement playerMovement)
        {
            _playerMovement = playerMovement;
        }

        private void OnCoordinatesChanged(Vector2 coordinates)
        {
            Coordinates.Value =$"x: {coordinates.x :F1} \ny: {coordinates.y :F1}";
        }

        public void Initialize()
        {
            _playerMovement.CoordinatesChanged += OnCoordinatesChanged;
        }

        public void Dispose()
        {
            _playerMovement.CoordinatesChanged -= OnCoordinatesChanged;
        }
    }
}