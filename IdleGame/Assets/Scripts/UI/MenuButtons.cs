using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuButtons : MonoBehaviour
{
    [Header("Menu Buttons")]
    [SerializeField]private Button SettingButton;
    [SerializeField]private Button SoundButton;
    [SerializeField]private Button TextButton;

    [Header("FullScreen/Windowed")]
    [SerializeField] private Button WindowButton;

    [Header("Pause/Resume")]
    [SerializeField]private Button PauseButton;

    [Header("Restart")]
    [SerializeField] private Button RestartButton;

    [Header("Quit")]
    [SerializeField] private Button QuitButton;

    [Header("Menu Panels")]
    [SerializeField]private GameObject SettingsPanel;
    [SerializeField]private GameObject SoundPanel;
    [SerializeField]private GameObject TextPanel;
    [SerializeField]private GameObject MainPanel;

    [Header("Update Buttons")]
    [SerializeField] private GameObject ShotUpdate;
    [SerializeField] private GameObject TargetUpdate;
    [SerializeField] private GameObject CrossUpdate;

    private bool isPaused = false;
    private bool isWindowed = false;

    void Start()
    {
        SettingButton.onClick.AddListener(() => TogglePanel(SettingsPanel, SoundPanel, TextPanel, ShotUpdate, TargetUpdate, CrossUpdate));
        SoundButton.onClick.AddListener(() => TogglePanel(SoundPanel, SettingsPanel, TextPanel, ShotUpdate, TargetUpdate, CrossUpdate));
        TextButton.onClick.AddListener(() => TogglePanel(TextPanel, SettingsPanel, SoundPanel, ShotUpdate, TargetUpdate, CrossUpdate));

        WindowButton.onClick.AddListener(() => ChangeWindowSize());
        PauseButton.onClick.AddListener(() => TogglePause());
        RestartButton.onClick.AddListener(() => RestartGame());
        QuitButton.onClick.AddListener(() => QuitGame());
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
        {
            if (!IsPointerOverUIButton())
            {
                SettingsPanel.SetActive(false);
                SoundPanel.SetActive(false);
                TextPanel.SetActive(false);
            }
        }
    }

    private bool IsPointerOverUIButton()
    {
        PointerEventData eventData = new PointerEventData(EventSystem.current)
        {
            position = Input.mousePosition
        };

        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);

        foreach (RaycastResult result in results)
        {
            GameObject hitObject = result.gameObject;

            if (hitObject.GetComponent<Button>() != null || hitObject.GetComponent<Slider>() != null)
            {
                return true;
            }

            if (hitObject.transform.parent != null && hitObject.transform.parent.GetComponent<Slider>() != null)
            {
                return true;
            }
        }

        return false; 
    }

    private void TogglePanel(GameObject panel, params GameObject[] otherPanels)
    {
        bool isActive = panel.activeSelf;

        foreach (GameObject otherPanel in otherPanels)
        {
            otherPanel.SetActive(false);
        }

        panel.SetActive(!isActive);
    }

    private void TogglePause()
    {
        isPaused = !isPaused;
        Time.timeScale = isPaused ? 0f : 1f;
    }

    private void ChangeWindowSize()
    {
        isWindowed = !isWindowed;
        Screen.fullScreen = isWindowed;
        Debug.Log("Windowed: " + isWindowed);
    }

    private void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        print("Restart");
    }

    private void QuitGame()
    {
        Debug.Log("Quit Game");
        Application.Quit();
    }
}
