using Photon.Pun;
using TMPro;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

public class PlayerManager : MonoBehaviourPun
{
    public Behaviour[] componentsToDisable;

    public GameObject[] gunStuff;

    public GameObject userNameText;

    public UI UI;

    public Camera cam;

    public void Start()
    {
        if (photonView.AmOwner || PhotonNetwork.OfflineMode)
        {
            for (int i = 0; i < gunStuff.Length; i++)
            {
                gunStuff[i].layer = 3;
            }
            cam.tag = "MainCamera";
            gameObject.tag = "Player";
            userNameText.SetActive(false);
            gameObject.layer = 7;
            UI = FindObjectOfType<UI>();
        }
        else
        {
            for (int i = 0; i < componentsToDisable.Length; i++)
            {
                componentsToDisable[i].enabled = false;
            }
            userNameText.GetComponentInChildren<TMP_Text>().text = photonView.Owner.NickName;
        }
    }
    public void Pause(CallbackContext context)
    {
        if (context.performed && UI != null)
        {
            UI.Pause();
        }
    }
}