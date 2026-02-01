using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class VictoryFlow : MonoBehaviour
{
    [SerializeField] UnityEvent onVictory;
    [SerializeField] Button mainMenuButton;
    [SerializeField] Button quitButton;

    void Awake()
    {
        mainMenuButton.onClick.AddListener(() => SceneManager.LoadScene("MainMenu"));
        quitButton.onClick.AddListener(Application.Quit);
    }

    public void TriggerVictory()
    {
        onVictory?.Invoke();
    }
}
