using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{
    #region
    [SerializeField] private float MoveSpeed = 2f;
    [SerializeField] private GameInput gameInput;
    [SerializeField] private LayerMask counterLayerMask;
    private bool isWalking;
    private Vector3 lastInteractDir;

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        gameInput.OnInteractPeformed += GameInput_OnInteractPeformed;
    }

    private void GameInput_OnInteractPeformed(object sender, System.EventArgs e)
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
            if (hit.transform.TryGetComponent(out ClearCounter clearCounter))
            {
                clearCounter.Interact();
            }

        }

    }

    // Update is called once per frame
    private void Update()
    {
        HandleMovement();
        HandleInteractions();
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
            if (hit.transform.TryGetComponent(out ClearCounter clearCounter)) 
            {

            }
                
        }    

    }

    void HandleMovement() 
    {
        //Move Player
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();
        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);
        isWalking = (moveDir != Vector3.zero);
        //Check if player should move 
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
                { //Can't move at all
                }
            }
        }

        if (canMove)
            transform.position += moveDir * MoveSpeed * Time.deltaTime;



        //Rotate Player towards direction
        float rotateSpeed = 10f;
        transform.forward = Vector3.Slerp(transform.forward, moveDir, rotateSpeed * Time.deltaTime);
    }

    public bool IsWalking() 
    {
        return isWalking;
    }
}
