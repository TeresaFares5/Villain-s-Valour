using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    [SerializeField] private string sceneName = "NextScene";

    public GameObject spaceToSkipUI;
    private bool canSkip;

    private void Start()
    {
        spaceToSkipUI.SetActive(false);
        canSkip = false;
    }
    private void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            
            if (canSkip == true)
            {
                SceneManager.LoadScene(sceneName);
            }
            else if(canSkip == false)
            {
                spaceToSkipUI.SetActive(true);
                canSkip = true;
            }
        }
      
    }
     
}
