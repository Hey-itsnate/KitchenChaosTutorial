using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
    private const string PLAYER_PREFS_BINDINGS = "InputBindings";

    public static GameInput instance { get; private set; }

    private NewControls inputActions;
    public event EventHandler OnInteractPeformed;
    public event EventHandler OnInteractAltPerformed;
    public event EventHandler OnPauseAction;

    public enum Binding 
    {
        Move_Up,
        Move_Down,
        Move_Left,
        Move_Right,
        Interact,
        Interact_Alternate,
        Pause,
        GamePad_Interact,
        GamePad_Interact_Alternate,
        GamePad_Pause
    }

    private void Awake()
    {
        //Setup Instance
        instance = this;

        //SetUp Controls
        inputActions = new NewControls();

        //Load PlayerPrefs for inputActions
        if (PlayerPrefs.HasKey(PLAYER_PREFS_BINDINGS))
        {
            inputActions.LoadBindingOverridesFromJson(PlayerPrefs.GetString(PLAYER_PREFS_BINDINGS));
        }

        //Enable controls
        inputActions.Player.Enable();

        //Subscribe to input Events
        inputActions.Player.Interact.performed += Interact_Performed;
        inputActions.Player.InteractAlt.performed += InteractAlt_performed;
        inputActions.Player.Pause.performed += Pause_Performed;

    }

    private void OnDestroy()
    {
        //Unsubscribe from control Events
        inputActions.Player.Interact.performed += Interact_Performed;
        inputActions.Player.InteractAlt.performed += InteractAlt_performed;
        inputActions.Player.Pause.performed += Pause_Performed;

        //Dispose of controls
        inputActions.Dispose();
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

    private void Pause_Performed(UnityEngine.InputSystem.InputAction.CallbackContext obj) 
    {
        OnPauseAction?.Invoke(this, EventArgs.Empty);
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

    public string GetBindingText(Binding binding) 
    {
        switch (binding) 
        {
            default:
            case Binding.Move_Up:
                return inputActions.Player.Move.bindings[1].ToDisplayString();
            case Binding.Move_Down:
                return inputActions.Player.Move.bindings[2].ToDisplayString();
            case Binding.Move_Left:
                return inputActions.Player.Move.bindings[3].ToDisplayString();
            case Binding.Move_Right:
                return inputActions.Player.Move.bindings[4].ToDisplayString();
            case Binding.Interact:
                return inputActions.Player.Interact.bindings[0].ToDisplayString();
            case Binding.Interact_Alternate:
                return inputActions.Player.InteractAlt.bindings[0].ToDisplayString();
            case Binding.Pause:
                return inputActions.Player.Pause.bindings[0].ToDisplayString();
            case Binding.GamePad_Interact:
                return inputActions.Player.Interact.bindings[1].ToDisplayString();
            case Binding.GamePad_Interact_Alternate:
                return inputActions.Player.InteractAlt.bindings[1].ToDisplayString();
            case Binding.GamePad_Pause:
                return inputActions.Player.Pause.bindings[1].ToDisplayString();

        }
    }

    public void RebindBinding(Binding binding, Action onActionRebound) 
    {
        //Disable Input Actions
        inputActions.Player.Disable();

        //Gathger inputAction and binding index for recieved binding
        InputAction inputAction;
        int bindingIndex;
        switch (binding) 
        {
            default:
            case Binding.Move_Up:
                inputAction = inputActions.Player.Move;
                bindingIndex = 1;
                break;
            case Binding.Move_Down:
                inputAction = inputActions.Player.Move;
                bindingIndex = 2;
                break;
            case Binding.Move_Left:
                inputAction = inputActions.Player.Move;
                bindingIndex = 3;
                break;
            case Binding.Move_Right:
                inputAction = inputActions.Player.Move;
                bindingIndex = 4;
                break;
            case Binding.Interact:
                inputAction = inputActions.Player.Interact;
                bindingIndex = 0;
                break;
            case Binding.Interact_Alternate:
                inputAction = inputActions.Player.InteractAlt;
                bindingIndex = 0;
                break;
            case Binding.Pause:
                inputAction = inputActions.Player.Pause;
                bindingIndex = 0;
                break;
            case Binding.GamePad_Interact:
                inputAction = inputActions.Player.Interact;
                bindingIndex = 1;
                break;
            case Binding.GamePad_Interact_Alternate:
                inputAction = inputActions.Player.InteractAlt;
                bindingIndex = 1;
                break;
            case Binding.GamePad_Pause:
                inputAction = inputActions.Player.Pause;
                bindingIndex = 1;
                break;
        }

        //Clear existing binding override for specified index
        inputAction.ApplyBindingOverride(bindingIndex, string.Empty);

        //Rebind inputAction with InteractiveRebinding
        inputAction.PerformInteractiveRebinding(bindingIndex).OnComplete(callback => 
        {
            Debug.Log(callback.action.bindings[bindingIndex].path);
            Debug.Log(callback.action.bindings[bindingIndex].overridePath);
            callback.Dispose();
            inputActions.Player.Enable();
            onActionRebound();

            PlayerPrefs.SetString(PLAYER_PREFS_BINDINGS, inputActions.SaveBindingOverridesAsJson());
            PlayerPrefs.Save();
        }).Start(); 
    }
}