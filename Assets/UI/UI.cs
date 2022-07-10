using Photon.Pun;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour
{
    [Header("InGameUI")]
    public TextMeshProUGUI ammoCounter;
    public RectTransform reticle;
    public TextMeshProUGUI blueScoreText;
    public TextMeshProUGUI orangeScoreText;

    [Header("Pause")]
    public GameObject inGameUI;
    public GameObject pauseMenu;

    private GameManager gameManager;

    //Player related stuff
    private GameObject player;
    private GunManager gunManager;
    private PlayerMovement playerMovement;
    private Rigidbody playerRB;

    private void Start()
    {
        playerMovement = FindObjectOfType<PlayerMovement>();
        player = playerMovement.gameObject;
        playerRB = player.GetComponent<Rigidbody>();
        gunManager = player.GetComponentInChildren<GunManager>();
    }

    private void OnEnable()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        ammoCounter.text = gunManager.currentAmmo + " | " + gunManager.activeGun.maxAmmo;
        float size = (float)(40 + playerRB.velocity.magnitude * 7 + playerMovement.lookingDirection.magnitude * 2);
        reticle.sizeDelta = Vector2.Lerp(reticle.sizeDelta, new Vector2(size, size), Time.deltaTime * 10);
        UpdateScore();
    }

    public void SetGameManager(GameManager gameManager)
    {
        this.gameManager = gameManager;
    }

    public void UpdateScore()
    {
        blueScoreText.text = gameManager.blueScore.ToString();
        orangeScoreText.text = gameManager.orangeScore.ToString();
    }

    #region Pause
    public void Pause()
    {
        inGameUI.SetActive(false);
        pauseMenu.SetActive(true);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        player.GetComponent<PlayerInput>().enabled = false;
    }

    public void UnPause()
    {
        inGameUI.SetActive(true);
        pauseMenu.SetActive(false);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        player.GetComponent<PlayerInput>().enabled = true;
    }

    public void QuitToTheMenu()
    {
        if (PhotonNetwork.IsConnected)
        {
            PhotonNetwork.AutomaticallySyncScene = false;
            PhotonNetwork.Disconnect();
        }
        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }
    #endregion
}
