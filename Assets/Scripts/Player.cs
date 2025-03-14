
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] float moveSpeed = 8f;
    [SerializeField] float paddingLeft;
    [SerializeField] float paddingRight;
    [SerializeField] float paddingTop;
    [SerializeField] float paddingBottom;
    Vector2 rawInput;
    Vector2 minBounds;
    Vector2 maxBounds;

    Shooter shooter;

    void Awake()
    {
        shooter = GetComponent<Shooter>();
    }

    void Start()
    {
        InitBounds();
    }
    void Update()
    {
        Move();
    }
    void InitBounds(){
        Camera mainCamera = Camera.main;
        minBounds = mainCamera.ViewportToWorldPoint(new Vector2(0, 0));
        maxBounds = mainCamera.ViewportToWorldPoint(new Vector2(1,1));
        
    }
    void Move()
    {
        Vector2 delta = rawInput * moveSpeed * Time.deltaTime;
        Vector2 newPos = new Vector2();
        newPos.x = Mathf.Clamp(transform.position.x+delta.x,minBounds.x + paddingLeft,maxBounds.x - paddingRight);
        newPos.y = Mathf.Clamp(transform.position.y+delta.y,minBounds.y + paddingBottom,maxBounds.y - paddingTop);
        transform.position = newPos;
    }

    void OnMove(InputValue value){
        rawInput = value.Get<Vector2>();
        // Debug.Log(rawInput);
    }

    void OnAttack(InputValue value){
        //do nothing for now, default is to have player firing all the time
        // if(shooter != null){
        //     shooter.isFiring = value.isPressed;
        // }
    }
}
