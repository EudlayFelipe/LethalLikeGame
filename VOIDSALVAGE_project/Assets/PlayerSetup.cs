using UnityEngine;

public class PlayerSetup : MonoBehaviour
{
    public PlayerMovement movement;
    public GameObject _camera;

    public void IsLocalPlayer(){
        movement.enabled = true;
        _camera.SetActive(true);
    }
}
