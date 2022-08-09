using Photon.Pun;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour
{
    [Header("InGameUI")] public TextMeshProUGUI ammoCounter;
    public RectTransform reticule;
    public TextMeshProUGUI blueScoreText;
    public TextMeshProUGUI orangeScoreText;

    [Header("Pause")] public GameObject inGameUI;
    public GameObject pauseMenu;

    private GameManager _gameManager;
    private GameObject _player;
    private GunManager _gunManager;
    private PlayerMovement _playerMovement;
    private Rigidbody _playerRb;

    private void Awake()
    {
        _playerMovement = FindObjectOfType<PlayerMovement>();
        _player = _playerMovement.gameObject;
        _player.GetComponent<PlayerManager>().ui = this;
        _playerRb = _player.GetComponent<Rigidbody>();
        _gunManager = _player.GetComponentInChildren<GunManager>();
    }

    private void OnEnable()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        ammoCounter.text = _gunManager.currentAmmo + " | " + _gunManager.activeGun.maxAmmo;
        var size = 40 + _playerRb.velocity.magnitude * 7 + _playerMovement.lookingDirection.magnitude * 2;
        reticule.sizeDelta = Vector2.Lerp(reticule.sizeDelta, new Vector2(size, size), Time.deltaTime * 10);
        UpdateScore();
    }

    public void SetGameManager(GameManager gameManager)
    {
        _gameManager = gameManager;
    }

    public void UpdateScore()
    {
        blueScoreText.text = _gameManager.blueScore.ToString();
        orangeScoreText.text = _gameManager.orangeScore.ToString();
    }

    #region Pause

    public void Pause()
    {
        inGameUI.SetActive(false);
        pauseMenu.SetActive(true);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        _player.GetComponent<PlayerInput>().enabled = false;
    }

    public void UnPause()
    {
        inGameUI.SetActive(true);
        pauseMenu.SetActive(false);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        _player.GetComponent<PlayerInput>().enabled = true;
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