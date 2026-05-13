using UnityEngine;
using UnityEngine.UI;

public class ManualKnifeButton : MonoBehaviour
{
    public static ManualKnifeButton Instance;

    private KnifeWeapon knifeWeapon;

    [Header("Cooldown UI")]
    public Image cooldownFill;
    public Button button;

    private void Awake()
    {
        Instance = this;

        if (button == null)
        {
            button = GetComponent<Button>();
        }

        if (cooldownFill != null)
        {
            cooldownFill.fillAmount = 0;
            cooldownFill.gameObject.SetActive(false);
        }

        gameObject.SetActive(false);
    }

    public void SetKnifeWeapon(KnifeWeapon weapon)
    {
        knifeWeapon = weapon;
        gameObject.SetActive(true);
    }

    public void PressKnifeButton()
    {
        if (knifeWeapon != null)
        {
            knifeWeapon.ManualAttackButton();
        }
    }

    public void UpdateCooldownUI(float remaining, float total)
    {
        if (cooldownFill != null)
        {
            cooldownFill.gameObject.SetActive(remaining > 0);
            cooldownFill.fillAmount = remaining / total;
        }

        if (button != null)
        {
            button.interactable = remaining <= 0;
        }
    }
}