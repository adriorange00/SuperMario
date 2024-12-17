using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    private void Update() {
        if (Input.GetKeyDown(KeyCode.Return)) 
        {
            SCManager.instance.LoadScene("Game");
        }
    }
}
