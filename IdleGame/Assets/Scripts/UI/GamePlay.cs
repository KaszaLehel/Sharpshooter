using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GamePlay : MonoBehaviour
{
    [Header("Upgrade Buttons")]
    [SerializeField] private Button ShotUpdateButton;
    [SerializeField] private Button TargetUpdateButton;
    [SerializeField] private Button CrossUpdateButton;

    [Header("Upgrade Panels")]
    [SerializeField]private GameObject ShotUpdatePanel;
    [SerializeField]private GameObject TargetUpdatePanel;
    [SerializeField]private GameObject CrossUpdatePanel;
    //[SerializeField]private GameObject MainPanel;

    [Header("Settings Panels")]
    [SerializeField]private GameObject SettingsPanel;
    [SerializeField]private GameObject SoundPanel;
    [SerializeField]private GameObject TextPanel;


    void Start()
    {
        if(GameManager.Instance.isFirstShot)
        {
            ShotUpdateButton.gameObject.SetActive(true);
        }
        if(GameManager.Instance.isFirstShot)
        {
            TargetUpdateButton.gameObject.SetActive(true);
        }
        if(GameManager.Instance.isTargetOpen)
        {
            CrossUpdateButton.gameObject.SetActive(true);
        }

        UpdateButtons();

        GameManager.Instance.OnFirstShotChanged += UpdateButtons;
        GameManager.Instance.OnTargetOpenChanged += UpdateButtons;

        ShotUpdateButton.onClick.AddListener(() => TogglePanel(ShotUpdatePanel, TargetUpdatePanel, CrossUpdatePanel, SettingsPanel, SoundPanel, TextPanel));
        TargetUpdateButton.onClick.AddListener(() => TogglePanel(TargetUpdatePanel, ShotUpdatePanel, CrossUpdatePanel, SettingsPanel, SoundPanel, TextPanel)); 
        CrossUpdateButton.onClick.AddListener(() => TogglePanel(CrossUpdatePanel, ShotUpdatePanel, TargetUpdatePanel, SettingsPanel, SoundPanel, TextPanel)); 
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
        {
            if (!IsPointerOverUIButton()) 
            {
                ShotUpdatePanel.SetActive(false);
                TargetUpdatePanel.SetActive(false);
                CrossUpdatePanel.SetActive(false);
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
            if (result.gameObject.GetComponent<Selectable>() != null) 
            {
                return true; 
            }
        }

        return false; 
    }

    private void OnDestroy()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.OnFirstShotChanged -= UpdateButtons;
            GameManager.Instance.OnTargetOpenChanged -= UpdateButtons;
        }
    }

    private void UpdateButtons()
    {
        ShotUpdateButton.gameObject.SetActive(GameManager.Instance.isFirstShot);
        TargetUpdateButton.gameObject.SetActive(GameManager.Instance.isFirstShot);
        CrossUpdateButton.gameObject.SetActive(GameManager.Instance.isTargetOpen);
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
}
