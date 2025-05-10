using UnityEngine;
using Photon.Pun;

public class MouseLook : MonoBehaviourPun
{
    [Header("Settings")]
    public float mouseSensitivity = 100f;
    public bool invertY = false;

    [Header("References")]
    public Transform playerBody; // Referência ao corpo (para girar no Y)
    public Transform cameraHolder; // Referência à câmera (para girar no X)

    private float xRotation = 0f;

    void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    void Start()
    {
        if (!photonView.IsMine)
        {
            enabled = false; // Desativa controle em players remotos
            return;
        }

        
    }

    void Update()
    {
        HandleMouseLook();
    }

    void HandleMouseLook()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        if (invertY)
            mouseY = -mouseY;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        cameraHolder.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);
    }
}
