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
        }
        else
        {
            foreach (var component in componentsToDisable)
            {
                component.enabled = false;
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