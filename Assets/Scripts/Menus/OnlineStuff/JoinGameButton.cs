using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;

public class JoinGameButton : MonoBehaviour
{
    public TextMeshProUGUI text;
    private RoomInfo _roomInfo;

    public RoomInfo RoomInfo
    {
        get => _roomInfo;
        set
        {
            _roomInfo = value;
            text.text = _roomInfo.Name + " - (" + _roomInfo.PlayerCount + "/" + _roomInfo.MaxPlayers + ")";
        }
    }

    public void Join()
    {
        PhotonNetwork.JoinRoom(RoomInfo.Name);
    }
}