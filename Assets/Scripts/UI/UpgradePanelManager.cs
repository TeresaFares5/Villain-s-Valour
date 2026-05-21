using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradePanelManager : MonoBehaviour
{
    [SerializeField] GameObject pauseIcon;
    [SerializeField] GameObject joystick;
    [SerializeField] GameObject daggerIcon;

    private bool daggerWasActive;

    [SerializeField] Joystick mobileJoystick;
    [SerializeField] GameObject panel;

    PauseManager pauseManager;

    public AudioSource lvlUp;

    [SerializeField] List<UpgradeButton> upgradeButtons;

    private void Awake()
    {
        pauseManager = GetComponent<PauseManager>();
    }

    private void Start()
    {
        HideButtons();
    }

    private void Update()
    {
        if (panel.activeInHierarchy)
        {
            Time.timeScale = 0;
        }
    }

    public void OpenPanel(List<UpgradeData> upgradeDatas)
    {
        Debug.Log("OPEN PANEL CALLED");
        Debug.Log("Upgrade options received: " + upgradeDatas.Count);

        Clean();

        Time.timeScale = 0;

        if (lvlUp != null)
            lvlUp.Play();

        if (panel != null)
            panel.SetActive(true);

        if (pauseIcon != null)
            pauseIcon.SetActive(false);

        if (joystick != null)
            joystick.SetActive(false);

        if (daggerIcon != null)
        {
            daggerWasActive = daggerIcon.activeSelf;
            daggerIcon.SetActive(false);
        }

        for (int i = 0; i < upgradeDatas.Count; i++)
        {
            upgradeButtons[i].gameObject.SetActive(true);
            upgradeButtons[i].Set(upgradeDatas[i]);
        }
    }

    public void Clean()
    {
        for (int i = 0; i < upgradeButtons.Count; i++)
        {
            upgradeButtons[i].Clean();
        }
    }

    public void Upgrade(int pressedButtonID)
    {
        GameManager.instance.playerTransform.GetComponent<Level>().Upgrade(pressedButtonID);

        ClosePanel();

        if (lvlUp != null)
            lvlUp.Play();
    }

    public void ClosePanel()
    {
        HideButtons();

        Time.timeScale = 1;

        if (panel != null)
            panel.SetActive(false);

        if (pauseIcon != null)
            pauseIcon.SetActive(true);

        if (mobileJoystick != null)
            mobileJoystick.ResetJoystick();

        if (joystick != null)
            joystick.SetActive(true);

        if (daggerIcon != null)
            daggerIcon.SetActive(daggerWasActive);
    }

    private void HideButtons()
    {
        for (int i = 0; i < upgradeButtons.Count; i++)
        {
            upgradeButtons[i].gameObject.SetActive(false);
        }
    }
}