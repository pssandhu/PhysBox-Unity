using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour {

    public static MainMenuController Instance;

    [SerializeField]
    public GameObject DescriptionBox;

    [SerializeField]
    public GameObject Sidebar;

    public string SelectedActivity;

    void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(gameObject);
        } else {
            Instance = this;
        }
    }

    public void LoadSelectedActivity() {
        SceneManager.LoadScene(SelectedActivity);
    }

    public void QuitApplication() {
        Debug.Log("Quit!");
        Application.Quit();
    }
}
