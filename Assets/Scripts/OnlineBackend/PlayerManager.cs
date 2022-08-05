using Photon.Pun;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManager : MonoBehaviourPun
{
    public Behaviour[] componentsToDisable;
    public GameObject[] gunComponents;
    public GameObject userNameText;
    public UI ui;
    public Camera cam;

    public void Awake()
    {
        if (photonView.AmOwner || PhotonNetwork.OfflineMode)
        {
            foreach (var component in gunComponents)
            {
                component.layer = 3;
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

    public void Pause(InputAction.CallbackContext context)
    {
        if (context.performed && ui != null)
        {
            ui.Pause();
        }
    }
}