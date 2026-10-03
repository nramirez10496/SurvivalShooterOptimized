using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour 
{ 
    public float speed = 6f; 

    private Vector3 movement; 
    private Animator anim; 
    private Rigidbody playerRigidbody; 
    private int floorMask; 
    private float camRayLength = 100f;

    //new 
    Vector2 moveInput;
    Vector2 aimInput;
    
    void Awake() 
    { 
        floorMask = LayerMask.GetMask("Floor"); 
        anim = GetComponent<Animator>(); 
        playerRigidbody = GetComponent<Rigidbody>(); 
    } 

    void FixedUpdate() 
    { 
        float h = moveInput.x; 
        float v = moveInput.y;
        
        Move(h, v); 
        Turning(); 
        Animating(h, v); 
    } 
    
    void Move(float h, float v) 
    { 
        movement.Set(h, 0f, v); 
        movement = movement.normalized * speed * Time.fixedDeltaTime; 
        
        playerRigidbody.MovePosition(transform.position + movement); 
    } 

    void Turning() 
    { 
        Ray camRay = Camera.main.ScreenPointToRay(aimInput); 
        RaycastHit floorHit; 
        
        if (Physics.Raycast(camRay, out floorHit, camRayLength, floorMask)) 
        { 
            Vector3 playerToMouse = floorHit.point - transform.position; 
            playerToMouse.y = 0f;

            if (playerToMouse.sqrMagnitude > 0.001f)
            {
                Quaternion newRotation = Quaternion.LookRotation(playerToMouse);
                playerRigidbody.MoveRotation(newRotation);
            }
        } 
    } 

    void Animating(float h, float v) 
    { 
        bool walking = h != 0f || v != 0f; 

        anim.SetBool("IsWalking", walking); 
    } 

    public void OnMove(InputAction.CallbackContext ctx)
    {
        moveInput = ctx.ReadValue<Vector2>();
    }

    public void OnAim(InputAction.CallbackContext ctx)
    {
        aimInput = ctx.ReadValue<Vector2>();
    }
}
