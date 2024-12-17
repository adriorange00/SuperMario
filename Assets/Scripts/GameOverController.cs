using UnityEngine;

public class GameOverController : MonoBehaviour
{
    private void Update() {
        if (Input.anyKeyDown) {
            SCManager.instance.LoadScene("Menu");
        }
    }
}
