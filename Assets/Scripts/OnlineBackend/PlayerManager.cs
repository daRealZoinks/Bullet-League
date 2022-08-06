using System;
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

    public GunManager gunManager;

    [Space]

    private Team _team;
    public GameObject graphics;

    [Space]

    public Material blueMaterial;
    public Material orangeMaterial;

    public Team Team
    {
        get => _team;
        set
        {
            _team = value;

            ColorPlayer();
        }
    }

    public void ColorPlayer()
    {
        switch (_team)
        {
            default:
            case Team.Blue:
                graphics.GetComponent<MeshRenderer>().sharedMaterial = blueMaterial;
                foreach (var weapon in gunManager.weapons)
                {
                    weapon.GetComponent<Gun>().Color(blueMaterial);
                }

                break;

            case Team.Orange:
                graphics.GetComponent<MeshRenderer>().sharedMaterial = orangeMaterial;
                foreach (var weapon in gunManager.weapons)
                {
                    weapon.GetComponent<Gun>().Color(orangeMaterial);
                }

                break;
        }
    }

    public void Awake()
    {
        if (photonView.AmOwner || PhotonNetwork.OfflineMode)
        {
            foreach (var component in gunComponents)
            {
                component.layer = 3;
            }

            cam.tag = "MainCamera";
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

            ColorPlayer();
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