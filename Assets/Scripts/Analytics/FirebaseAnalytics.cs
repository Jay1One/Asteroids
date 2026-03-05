using Firebase;
using Firebase.Extensions;
using Zenject;

namespace Analytics
{
    public class FirebaseAnalytics : IInitializable
    {
        private FirebaseApp _app;
        private bool _isInitialized;
    
        public void Initialize()
        {
            Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task => {
                var dependencyStatus = task.Result;
                if (dependencyStatus == DependencyStatus.Available) {
                    _app = FirebaseApp.DefaultInstance;
                    _isInitialized = true;
                } else {
                    UnityEngine.Debug.LogError(System.String.Format(
                        "Could not resolve all Firebase dependencies: {0}", dependencyStatus));
                }
            });
        }
    }
}
