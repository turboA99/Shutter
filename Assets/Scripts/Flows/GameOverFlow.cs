using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Flows
{
    public class GameOverFlow : MonoBehaviour
    {
        [SerializeField] Button buttonRestart;
        [SerializeField] Button buttonMainMenu;

        void Awake()
        {
            buttonRestart.onClick.AddListener(() => SceneManager.LoadScene(SceneManager.GetActiveScene().name));
            buttonMainMenu.onClick.AddListener(() => SceneManager.LoadScene("MainMenu"));
        }
    }
}
