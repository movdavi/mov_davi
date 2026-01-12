using UnityEngine;

public class MainMenuController : MonoBehaviour
{
    public GameObject menuRoot;

    public void StartExperience()
    {
        menuRoot.SetActive(false);
    }
}
