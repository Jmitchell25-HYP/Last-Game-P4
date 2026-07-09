using UnityEngine;
using UnityEngine.InputSystem;

    

public class Thetest : MonoBehaviour
{
    PlayerInput input;
    InputAction move_action;
    InputAction accelerate_action;
    InputAction brake_action;

    [SerializeField] float forward_speed = 1.1f;
    [SerializeField] float backward_speed = 1.05f;

    float accelerate;
    float brake;

    [SerializeField] float steeringSpeed = 120f;
    float currentSteeringAngle = 0f;

    void Start()
    {
        input = GetComponent<PlayerInput>();
        move_action = input.actions.FindAction("Move");
        accelerate_action = input.actions.FindAction("Accelerate");
        brake_action = input.actions.FindAction("Brake");
    }

   
    void Update()
    {
        float moveInput = move_action.ReadValue<float>();
        accelerate = accelerate_action.ReadValue<float>();
        brake = brake_action.ReadValue<float>();  

        if (accelerate > 0)
        {
            accelerate = 0;
        }

        currentSteeringAngle += moveInput * steeringSpeed * Time.deltaTime;

        print($"accelerate: {accelerate}, brake: {brake}");

        transform.rotation = Quaternion.Euler(0, currentSteeringAngle, 0);


    }

    private void FixedUpdate()
    {
        transform.Translate(new Vector3(accelerate * -forward_speed, 0, 0));
        transform.Translate(new Vector3(brake * backward_speed, 0, 0));

    }




}
