using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameInput : MonoBehaviour
{
    private NewControls inputActions;
    public event EventHandler OnInteractPeformed;
    public event EventHandler OnInteractAltPerformed;

    private void Awake()
    {
        //SetUp Controls
        inputActions = new NewControls();
        inputActions.Player.Enable();

        //Subscribe to Control Events
        inputActions.Player.Interact.performed += Interact_Performed;
        inputActions.Player.InteractAlt.performed += InteractAlt_performed;
    }

    private void InteractAlt_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnInteractAltPerformed?.Invoke(this, EventArgs.Empty);
    }

    private void Interact_Performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        //The Same As: if(OnInteractPerformed != null) OnInteractPerformed.Invoke(this, EventArgs.Empty);
        //The ?. operator (aka null-coalescing Operator) only runs the right side of Code if the left code is not null
        //Not to be mistaken with the ? operator (aka Conditional/Ternary Operator).
        //Ex: int num = 10; String result = (number > 5) ? "Greater than 5" : "Less than or equal to 5"; (Output is the left part in this example)
        OnInteractPeformed?.Invoke(this,EventArgs.Empty);
    }

    public Vector2 GetMovementVectorNormalized() 
    {
        //Gather Input
        if (inputActions == null) Debug.Log("Errlr");
        Vector2 inputVector = inputActions.Player.Move.ReadValue<Vector2> ();


        if (Input.GetKey(KeyCode.W))
        {
            inputVector.y = 1;
        }
        if (Input.GetKey(KeyCode.S))
        {
            inputVector.y = -1;
        }
        if (Input.GetKey(KeyCode.A))
        {
            inputVector.x = -1;
        }
        if (Input.GetKey(KeyCode.D))
        {
            inputVector.x = 1;
        }
        inputVector.Normalize();

        return inputVector;
    }
}