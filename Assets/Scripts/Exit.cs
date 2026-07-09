using UnityEngine;
using UnityEngine.SceneManagement;

public class Exit : MonoBehaviour
{
    public void OnClickExit()
    {
        Application.Quit(1);
    }
}
