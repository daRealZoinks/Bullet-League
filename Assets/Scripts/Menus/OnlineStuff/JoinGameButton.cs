using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;

public class JoinGameButton : MonoBehaviour
{
    public TextMeshProUGUI text;

    public RoomInfo roomInfo;

    public void SetRoomInfo(RoomInfo roomInfo)
    {
        this.roomInfo = roomInfo;
        text.text = this.roomInfo.Name + " - (" + this.roomInfo.PlayerCount + "/" + this.roomInfo.MaxPlayers + ")";
    }

    public void Join()
    {
        PhotonNetwork.JoinRoom(roomInfo.Name);
    }
}