using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [FormerlySerializedAs("startButton")]
    [SerializeField] Button playButton;

    [SerializeField] Button quitButton;
    [SerializeField] InputActionReference navigateActionReference;

    EventSystem _eventSystem;
    InputSystemUIInputModule _inputModule;
    InputAction _navigateAction;

    void Awake()
    {
        _eventSystem = FindAnyObjectByType<EventSystem>();
        _inputModule = FindAnyObjectByType<InputSystemUIInputModule>();
        if (!_eventSystem || !_inputModule) throw new UnityException("No UI InputModule or Event System found!");

        _navigateAction = navigateActionReference.action;
        _navigateAction.performed += OnNavigate;

        playButton.onClick.AddListener(Play);
        quitButton.onClick.AddListener(Quit);
    }

    void Play()
    {
        SceneManager.LoadScene("Tutorial");
    }

    void OnNavigate(InputAction.CallbackContext context)
    {
        if (!_eventSystem.currentSelectedGameObject)
        {
            _eventSystem.SetSelectedGameObject(_eventSystem.firstSelectedGameObject);
        }
    }

    void Quit()
    {
        Application.Quit();
    }

    IEnumerator Start()
    {
        yield return new WaitForSeconds(.1f);
        _eventSystem.SetSelectedGameObject(playButton.gameObject);
        _eventSystem.firstSelectedGameObject = playButton.gameObject;
    }
}
