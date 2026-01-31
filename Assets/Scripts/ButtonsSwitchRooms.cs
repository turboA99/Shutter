using UnityEngine;
using UnityEngine.UI;

public class ButtonsSwitchRooms : MonoBehaviour
{
    [Header("Buttons to switch rooms")]
    [SerializeField]
    Button Room1ToRoom2Button;
    [SerializeField]
    Button Room2ToRoom1Button;
    [SerializeField]
    Button Room2ToRoom3Button;
    [SerializeField]
    Button Room3ToRoom2Button;

    [Header("Rooms to switch")]
    [SerializeField]
    GameObject Room1;
    [SerializeField]
    GameObject Room2;
    [SerializeField]
    GameObject Room3;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Room1ToRoom2Button.onClick.AddListener(SwitchToRoom2);
        Room2ToRoom1Button.onClick.AddListener(SwitchToRoom1);
        Room2ToRoom3Button.onClick.AddListener(SwitchToRoom3);
        Room3ToRoom2Button.onClick.AddListener(SwitchToRoom2);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SwitchToRoom2()
    {
        Room1.SetActive(false);
        Room2.SetActive(true);
        Room3.SetActive(false);
        Debug.Log("Switching from Room 1 to Room 2");
    }

    public void SwitchToRoom1()
    {
        Room1.SetActive(true);
        Room2.SetActive(false);
        Room3.SetActive(false);
        Debug.Log("Switching from Room 2 to Room 1");
    }

    public void SwitchToRoom3()
    {
        Room1.SetActive(false);
        Room2.SetActive(false);
        Room3.SetActive(true);
        Debug.Log("Switching from Room 2 to Room 3");
    }
}