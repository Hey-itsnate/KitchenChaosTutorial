using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneTemplate;
using UnityEngine;


public class Player : MonoBehaviour, IKitchenObjectParent
{
    

    #region Fields

    //Events
    public event EventHandler<OnSelectedCounterChangedEventArgs> OnSelectedCounterChanged;
    public class OnSelectedCounterChangedEventArgs : EventArgs 
    {
        public BaseCounter SelectedCounter;
    }

    [SerializeField] private float MoveSpeed = 2f;
    [SerializeField] private GameInput gameInput;
    [SerializeField] private LayerMask counterLayerMask;
    [SerializeField] private Transform playerHand;
    private KitchenObject kitchenObject;
    private bool isWalking;
    private Vector3 lastInteractDir;
    private BaseCounter selectedCounter;

    #endregion

    #region Properties

    public static Player Instance { get; private set; }

    #endregion

    #region Methods

    #region Unity Methods

    private void Awake()
    {
        if (Instance != null) 
        {
            Debug.LogError("There is moire than one Player Instance.");
            return;
        }
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        gameInput.OnInteractPeformed += GameInput_OnInteractPeformed;
    }

    // Update is called once per frame
    private void Update()
    {
        HandleMovement();
        HandleInteractions();
    }

    #endregion

    #region Public Methods

    /// <returns>True if player is walking.</returns>
    public bool IsWalking()
    {
        return isWalking;
    }

    #endregion

    #region Private Methods

    private void GameInput_OnInteractPeformed(object sender, System.EventArgs e)
    {
        if (selectedCounter != null) 
        {
            selectedCounter. Interact(this);
        }
    }

    void HandleInteractions() 
    {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();
        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);

        if (moveDir != Vector3.zero) 
        {
            lastInteractDir = moveDir;
        }

        float interactDistance = 2f;
        RaycastHit hit;
        //Out sets the given parameter to a value based on the function
        if (Physics.Raycast(transform.position, moveDir, out hit, interactDistance, counterLayerMask))
        {
            //Check if RayCast hit is a clear counter
            if (hit.transform.TryGetComponent(out BaseCounter baseCounter))
            {
                //Has Clear Counter
                if (baseCounter != selectedCounter)
                {
                    SetSelectedCounter(baseCounter);
                }
            }
            else 
            {
                //RayCast Hit isn't a counter
                SetSelectedCounter(null);
            }
        }
        else 
        {
            //RayCat Hit is null
            SetSelectedCounter(null);
        }
    }

    void HandleMovement() 
    {
        //Get Movement Input
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();
        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);

        //Check if player can move 
        float moveDist = MoveSpeed * Time.deltaTime;
        float playerSize = .7f;
        float playerHeight = 2f;
        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerSize, moveDir, moveDist);
        if (!canMove)
        {
            //Cannot Move towards moveDir
            //Attempt only X movement
            Vector3 moveDirX = new Vector3(moveDir.x, 0f, 0f).normalized;
            canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerSize, moveDirX, moveDist);
            if (canMove)
            {
                //Can move only on the x
                moveDir = moveDirX;
            }
            else
            {
                //Attempt only Z movement
                Vector3 moveDirZ = new Vector3(0f, 0f, moveDir.z).normalized;
                canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerSize, moveDirZ, moveDist);

                if (canMove)
                {
                    //Can only move on the Z
                    moveDir = moveDirZ;
                }
                else
                { 
                    //Can't move at all
                }
            }
        }

        //Move Player
        if (canMove) transform.position += moveDir * MoveSpeed * Time.deltaTime;
        isWalking = (moveDir != Vector3.zero && canMove);

        //Rotate Player towards direction
        float rotateSpeed = 10f;
        transform.forward = Vector3.Slerp(transform.forward, moveDir, rotateSpeed * Time.deltaTime);
    }

    private void SetSelectedCounter(BaseCounter selectedCounter) 
    {
        this.selectedCounter = selectedCounter;
        OnSelectedCounterChanged?.Invoke(this, new OnSelectedCounterChangedEventArgs { SelectedCounter = selectedCounter }); 
    }

    #region IKitchenObjectParent Interface
    public Transform GetKitchenObjectFollowTransform()
    {
        return playerHand;
    }

    public KitchenObject GetKitchenObject() { return kitchenObject; }

    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        this.kitchenObject = kitchenObject;
    }

    public void ClearKitchenObject()
    {
        kitchenObject = null;
    }

    public bool HasKitchObject()
    {
        return kitchenObject != null;
    }
    #endregion

    #endregion

    #endregion
}
