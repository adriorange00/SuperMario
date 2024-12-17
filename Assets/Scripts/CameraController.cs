using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] GameObject player;
    void Start()
    {

    }

    void Update()
    {
        //transform.position = new Vector3(player.transform.position.x, player.transform.position.y, transform.position.z);
        transform.position = new Vector3(Mathf.Clamp(player.transform.position.x,0,8), transform.position.y, transform.position.z);
    }
}
