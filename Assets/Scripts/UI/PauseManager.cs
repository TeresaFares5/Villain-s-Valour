using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public GameObject pausePanel;
    public bool paused;

    [SerializeField] GameObject joystick;
    [SerializeField] Joystick mobileJoystick;
    [SerializeField] GameObject pauseIcon;
    [SerializeField] GameObject daggerIcon;

    private bool daggerWasActive;

    private void Start()
    {
        if (pausePanel != null)
            pausePanel.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (paused)
                UnPauseGame();
            else
                PauseGame();
        }
    }

    public void PauseGame()
    {
        paused = true;

        if (daggerIcon != null)
            daggerWasActive = daggerIcon.activeSelf;

        Time.timeScale = 0;

        if (pausePanel != null)
            pausePanel.SetActive(true);

        if (joystick != null)
            joystick.SetActive(false);

        if (pauseIcon != null)
            pauseIcon.SetActive(false);

        if (daggerIcon != null)
            daggerIcon.SetActive(false);
    }

    public void UnPauseGame()
    {
        paused = false;

        if (mobileJoystick != null)
            mobileJoystick.ResetJoystick();

        Time.timeScale = 1;

        if (pausePanel != null)
            pausePanel.SetActive(false);

        if (joystick != null)
            joystick.SetActive(true);

        if (pauseIcon != null)
            pauseIcon.SetActive(true);

        if (daggerIcon != null)
            daggerIcon.SetActive(daggerWasActive);
    }
}