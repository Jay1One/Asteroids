using System;
using GamePlay.Combat.Systems;
using MVVM;
using UniRx;
using Zenject;

namespace UI.ViewModels
{
    public class ScoreViewModel :IInitializable, IDisposable
    {
        private readonly ScoreCalculator _scoreCalculator;
        
        [Data ("Score")]
        public ReactiveProperty<string> Score = new ReactiveProperty<string>();
        
        public ScoreViewModel(ScoreCalculator scoreCalculator)
        {
            _scoreCalculator = scoreCalculator;
        }

        public void Dispose()
        {
            _scoreCalculator.ScoreChanged -= OnScoreChanged;
        }

        public void Initialize()
        {
            _scoreCalculator.ScoreChanged += OnScoreChanged;
            OnScoreChanged(_scoreCalculator.Score);
        }

        private void OnScoreChanged(int score)
        {
            Score.Value = score.ToString();
        }
    }
}