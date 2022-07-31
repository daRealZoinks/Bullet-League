using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;
using UnityEngine;

public class JoinGame : MonoBehaviourPunCallbacks
{
    public Transform content;
    public JoinGameButton button;
    [Space]
    private readonly List<JoinGameButton> buttonList = new();
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        foreach (var button in buttonList)
        {
            Destroy(button.gameObject);
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
        JoinGameButton joinGameButton = Instantiate(button, content);
        if (joinGameButton != null)
        {
            joinGameButton.SetRoomInfo(info);
            buttonList.Add(joinGameButton);
        }
    }
    private void RemoveRoom(string roomName)
    {
        int index = buttonList.FindIndex(x => x.roomInfo.Name == roomName);
        if (index != -1)
        {
            Destroy(buttonList[index].gameObject);
            buttonList.RemoveAt(index);
        }
    }
}