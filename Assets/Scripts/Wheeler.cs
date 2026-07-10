
using UnityEngine;
using UnityEngine.InputSystem;
public class Wheeler : MonoBehaviour
{
    public float moveSpeed = 10f;
    public float reverseSpeed = 5f;
    public float turnSpeed = 175; 
    public float brakeDrag = 5f;
    



    

    private float steeringInput = 0f;
    private Vector3 velocity = Vector3.zero;

    private InputAction joystickThrottle;
    private InputAction keyboardThrottle;
    private InputAction joystickBrake;
    private InputAction keyboardBrake;
    private InputAction steeringAction;
 
    private void OnEnable()
    {

        joystickThrottle = new InputAction(type: InputActionType.Value, binding: "<Joystick>/z");
        keyboardThrottle = new InputAction(type: InputActionType.Button, binding: "<Keyboard>/w");

      



        joystickBrake = new InputAction(type: InputActionType.Value, binding: "<Joystick>/rz");
        keyboardBrake = new InputAction(type: InputActionType.Button, binding: "<Keyboard>/s");

        steeringAction = new InputAction(type: InputActionType.Value);
        steeringAction.AddBinding("<Joystick>/stick/x");
        steeringAction.AddCompositeBinding("1DAxis")
        .With("negative", "<Keyboard>/a")
        .With("positive", "<keyboard>/d");

        joystickThrottle.Enable();
        keyboardThrottle.Enable();
        joystickBrake.Enable();
        keyboardBrake.Enable();
        steeringAction.Enable();
       

    }

    void OnDisable()
    {
        joystickThrottle.Disable();
        keyboardThrottle.Disable();
        joystickBrake.Disable();
        keyboardBrake.Disable();
        steeringAction.Disable();
        

    }

    void Update()
    {
        // Joystick pedals: return 1 when idle, 0 when fully pressed
        float rawJoyThrottle = joystickThrottle.ReadValue<float>();
        float rawJoyBrake = joystickBrake.ReadValue<float>();


        //Invert joystick values if the pedal is actually pressed
        float joyThrottle = rawJoyThrottle < 0.99f ? 0f - rawJoyThrottle : 1f;
        float joyBrake = rawJoyBrake < 0.99f ? 0f - rawJoyBrake : 1f;

        // keyboard returns 0 or 1
        float keyThrottle = keyboardThrottle.ReadValue<float>();
        float keyBrake = keyboardBrake.ReadValue<float>();

        // Combine both - use whichever input is stronger
        float throttleInput = Mathf.Max(joyThrottle, keyThrottle);
        float brakeInput = Mathf.Max(joyBrake, keyBrake);
        steeringInput = steeringAction.ReadValue<float>();   

        Vector3 moveDirection = Vector3.zero;

        if (throttleInput > 0.1f)
        {
            moveDirection = transform.forward * throttleInput * moveSpeed;

        }
        else if (brakeInput > 0.1f)
        {
            moveDirection = -transform.forward * brakeInput * reverseSpeed;
        }
        else
        {
            // Slow down when no input
            velocity = Vector3.Lerp(velocity, Vector3.zero, brakeDrag * Time.deltaTime);
        }


        if (moveDirection != Vector3.zero)
        {
            velocity = moveDirection;
        }

        transform.Translate(velocity * Time.deltaTime, Space.World);

        float turn = steeringInput * turnSpeed * Time.deltaTime;
        transform.Rotate(0f, turn, 0f);

       

    }

    

  







}
