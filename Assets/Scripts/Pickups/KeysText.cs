using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeysText : MonoBehaviour
{
    [SerializeField] TMPro.TextMeshProUGUI keysCountText;

    private void Update()
    {
        int keysAquired = PlayerPrefs.GetInt("keys");
        keysCountText.text = keysAquired.ToString();
    }
}
