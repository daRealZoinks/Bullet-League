using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class HostGame : MonoBehaviour
{
    private int _roomSize = 2;
    private string _roomName;

    public void SetNumberOfPlayers(int numberOfPlayers)
    {
        _roomSize = (numberOfPlayers + 1) * 2;
    }

    public void SetRoomName(string roomName)
    {
        _roomName = roomName;
    }

    public void CreateRoom()
    {
        RoomOptions roomOptions = new()
        {
            MaxPlayers = (byte)_roomSize
        };

        if (!PhotonNetwork.IsConnected) return;

        if (!string.IsNullOrEmpty(_roomName) && PhotonNetwork.NickName != null)
        {
            PhotonNetwork.JoinOrCreateRoom(_roomName, roomOptions, TypedLobby.Default);
        }
    }
}