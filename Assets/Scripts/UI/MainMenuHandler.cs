using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuHandler : MonoBehaviour
{
    [Header("Buttons")]
    public Button playBtn;
    public Button optionsBtn;
    public Button exitBtn;
    [Space]
    [Header("OptionsScreen")]
    public GameObject optionsScreen;

    private void Start()
    {
        playBtn.onClick.AddListener(delegate { HostOrJoinGame(); });
        optionsBtn.onClick.AddListener(delegate { OpenOptions(); });
        exitBtn.onClick.AddListener(delegate { ExitGame(); });

    }

    public void HostOrJoinGame()
    {
        // Show UI to choose between hosting or joining a game
        // For example, display buttons for "Host" and "Join"

        // When "Host" is clicked:
        NetworkManager.Singleton.StartHost();

        // When "Join" is clicked:
        NetworkManager.Singleton.StartClient();
    }

    public void OpenOptions()
    {
        optionsScreen.SetActive(true);
        Debug.Log("Options menu opened.");

    }

    public void CloseOptions()
    {
        optionsScreen.SetActive(false);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
