using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputManager : MonoBehaviour
{
    
    Vector2 directionalInput;
    public Vector2 DirectionalInput { get { return directionalInput; } }

    Vector2 mouseMovement;
    public Vector2 MouseMovement { get { return mouseMovement; } }

    bool jumpPressed;
    public bool JumpPressed { get { return jumpPressed; } }

    bool crouchPressed;
    public bool CrouchPressed { get { return crouchPressed; } }

    bool interactPressed;
    public bool InteractPressed { get { return interactPressed; } }

    bool aimPressed;
    public bool AimPressed { get { return aimPressed; } }

    bool firePressed;
    public bool FirePressed { get { return firePressed; } }

    bool reloadPressed;
    public bool ReloadPressed { get { return reloadPressed; } }


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

        interactPressed = Input.GetButton("Interact");

        reloadPressed = Input.GetButton("Reload");

        aimPressed = Input.GetButton("Fire2");

        firePressed = Input.GetButton("Fire1");
    }
}
