using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [FormerlySerializedAs("startButton")]
    [SerializeField] Button playButton;

    [SerializeField] Button quitButton;

    void Awake()
    {
        playButton.onClick.AddListener(Play);
        quitButton.onClick.AddListener(Quit);
        Cursor.visible = true;
    }

    void Play()
    {
        SceneManager.LoadScene("Tutorial");
    }

    void Quit()
    {
        Application.Quit();
    }
}
