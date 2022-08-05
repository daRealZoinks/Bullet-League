using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;
using UnityEngine;

public class JoinGame : MonoBehaviourPunCallbacks
{
    public Transform content;
    public JoinGameButton button;

    [Space] private readonly List<JoinGameButton> _buttonList = new();

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        foreach (var joinGameButton in _buttonList)
        {
            Destroy(joinGameButton.gameObject);
        }

        foreach (var info in roomList)
        {
            if (info.RemovedFromList)
            {
                RemoveRoom(info.Name);
            }
            else
            {
                AddRoom(info);
            }
        }
    }

    private void AddRoom(RoomInfo info)
    {
        var joinGameButton = Instantiate(button, content);

        if (joinGameButton == null) return;

        joinGameButton.RoomInfo = info;
        _buttonList.Add(joinGameButton);
    }

    private void RemoveRoom(string roomName)
    {
        var index = _buttonList.FindIndex(x => x.RoomInfo.Name == roomName);

        if (index == -1) return;

        Destroy(_buttonList[index].gameObject);
        _buttonList.RemoveAt(index);
    }
}