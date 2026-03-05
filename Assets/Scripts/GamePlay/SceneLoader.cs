using Core.Signals;
using UnityEngine.SceneManagement;
using Zenject;

namespace GamePlay
{
    public class SceneLoader
    {
        private const string BattleScene = "BattleScene";
        
        private SignalBus _signalBus;

        public SceneLoader(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }

        public void LoadBattleScene()
        {
            SceneManager.LoadScene(BattleScene, LoadSceneMode.Single);
            _signalBus.Fire(new SceneLoadedSignal());
        }
    }
}