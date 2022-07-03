using Photon.Realtime;
using UnityEngine;
using TMPro;

public class JoinGameButton : MonoBehaviour
{
    public TextMeshProUGUI text;

    public RoomInfo roomInfo;

    public void SetRoomInfo(RoomInfo roomInfo)
    {
        this.roomInfo = roomInfo;
        text.text = roomInfo.Name + " (" + roomInfo.PlayerCount + "/" + roomInfo.MaxPlayers + ")";
    }

    private void Enable()
    {
        //give this button one of the rooms
    }

    public void Join()
    {
        //join the room
    }
}