using UnityEngine;
using UnityEngine.Rendering;

public class PlayerMovement : MonoBehaviour
{
    public float walkSpeed = 4;
    public float sprintSpeed = 14;
    public float maxVelocityChange = 10f;
    [Space]
    public float jumpHeight = 30;
    public Transform sensorGround;

    public LayerMask jumpLayer;
    
    private Vector2 input;
    private Rigidbody rb;

    private bool sprinting;
    private bool jumping;
    private bool grounded = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        input.Normalize();

        sprinting = Input.GetButton("Sprint");
        
        if(Input.GetKeyDown(KeyCode.Space) && grounded){
            rb.AddForce(new Vector3(rb.linearVelocity.x, jumpHeight, rb.linearVelocity.z), ForceMode.Impulse);
        }


    }
    
    void FixedUpdate()
    {
        grounded = Physics.CheckSphere(sensorGround.transform.position, 0.2f, jumpLayer);

        if(input.magnitude > 0.5f){
            rb.AddForce(CalculateMovement(sprinting ? sprintSpeed : walkSpeed), ForceMode.VelocityChange);
        }
        else{
            var velocity1 = rb.linearVelocity;
            velocity1 = new Vector3(velocity1.x * 0.2f * Time.fixedDeltaTime, velocity1.y, velocity1.z * 0.2f * Time.fixedDeltaTime);
            rb.linearVelocity = velocity1; 
        }
    }


    Vector3 CalculateMovement(float _speed){
        
        Vector3 targetVelocity = new Vector3(input.x, 0, input.y);
        targetVelocity = transform.TransformDirection(targetVelocity);

        targetVelocity *= _speed;

        Vector3 velocity = rb.linearVelocity;

        if(input.magnitude > 0.5f){
            Vector3 velocityChange = targetVelocity - velocity;

            velocityChange.x = Mathf.Clamp(velocityChange.x, -maxVelocityChange, maxVelocityChange);
            velocityChange.z = Mathf.Clamp(velocityChange.z, -maxVelocityChange, maxVelocityChange);

            velocityChange.y = 0;

            return velocityChange;
        }
        else{
            return new Vector3();
        }
    }
}
