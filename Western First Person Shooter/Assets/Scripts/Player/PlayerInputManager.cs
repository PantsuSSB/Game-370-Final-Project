using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputManager : MonoBehaviour
{
    [SerializeField]
    Vector2 directionalInput;
    public Vector2 DirectionalInput { get { return directionalInput; } }

    [SerializeField]
    Vector2 mouseMovement;
    public Vector2 MouseMovement { get { return mouseMovement; } }

    [SerializeField]
    bool jumpPressed;
    public bool JumpPressed { get { return jumpPressed; } }

    [SerializeField]
    bool crouchPressed;
    public bool CrouchPressed { get { return crouchPressed; } }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GetAxisInput();
        GetMouseMovement();
        GetButtonInputs();
    }

    void GetAxisInput()
    {
        directionalInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }

    void GetMouseMovement()
    {
        mouseMovement = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
    }

    void GetButtonInputs()
    {
        jumpPressed = Input.GetButton("Jump");

        crouchPressed = Input.GetButton("Crouch");
    }
}
