using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Flows
{
    public class GameOverFlow : MonoBehaviour
    {
        [FormerlySerializedAs("gameOverEvenets")]
        [SerializeField] UnityEvent gameOverEvents;
        [SerializeField] Button buttonRestart;
        [SerializeField] Button buttonMainMenu;
        
        bool _wasTriggered = false;

        void Awake()
        {
            buttonRestart.onClick.AddListener(() => SceneManager.LoadScene(SceneManager.GetActiveScene().name));
            buttonMainMenu.onClick.AddListener(() => SceneManager.LoadScene("MainMenu"));
        }

        public void TriggerGameOver()
        {
            if (_wasTriggered) gameOverEvents?.Invoke();
            _wasTriggered = true;
        }

        public void ResetTrigger()
        {
            _wasTriggered = false;
        }
    }
}
