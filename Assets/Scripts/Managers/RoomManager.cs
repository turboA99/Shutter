using UnityEngine;

public class RoomManager : MonoBehaviour
{
    [SerializeField] GameObject startRoom;
    GameObject _currentRoom;

    void Start()
    {
        startRoom.SetActive(true);
        _currentRoom = startRoom;
    }

    // Update is called once per frame
    public void GoToRoom(GameObject room)
    {
        if (_currentRoom) _currentRoom.SetActive(false);
        room.SetActive(true);
        _currentRoom = room;
    }
}
