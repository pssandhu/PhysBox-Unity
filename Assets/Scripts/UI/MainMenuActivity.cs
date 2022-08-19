using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuActivity : MonoBehaviour {

    [SerializeField]
    public string ActivityName;

    [SerializeField]
    public string ActivityDescription;

    public void Start() {
        gameObject.GetComponent<Button>().onClick.AddListener(PreviewActivity);
    }

    public void PreviewActivity() {
        if (MainMenuController.Instance.Sidebar.activeInHierarchy == false) {
            MainMenuController.Instance.Sidebar.SetActive(true);
        }

        MainMenuController.Instance.DescriptionBox.GetComponent<TMP_Text>().text = ActivityDescription;
        MainMenuController.Instance.SelectedActivity = ActivityName;
    }
}
