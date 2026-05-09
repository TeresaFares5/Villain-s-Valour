using UnityEngine;
using TMPro;

public class SetUsername : MonoBehaviour
{
    public TMP_InputField inputField;

    private void Start()
    {
        string playerName = PlayerPrefs.GetString("Player Name", "");
        inputField.text = playerName;
        inputField.characterLimit = 10;

        inputField.onValueChanged.AddListener(SaveName);
    }

    private void SaveName(string value)
    {
        PlayerPrefs.SetString("Player Name", value);
        PlayerPrefs.Save();
    }
}