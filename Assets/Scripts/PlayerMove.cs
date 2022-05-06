using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [Header("移动参数")]
    public float Speed;
    public float JumpForce;
    float xVelocity;

    LayerMask layer;
    BoxCollider2D box; 
    Rigidbody2D rb;

    [Header("状态判断")]
    public  bool isonGround;

    [Header("射线判断参数")]
    public float footoffset = 0.49f;
    public float raydistance;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        box = GetComponent<BoxCollider2D>();
        layer = LayerMask.NameToLayer("Ground");
    }

    
    void Update()
    {
        jump();
        RayCheck();
    }
    private void FixedUpdate()
    {
        xVelocity = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(xVelocity * Speed, rb.velocity.y);
        Facing();
    }
    private void jump()
    {
        if (Input .GetKeyDown(KeyCode.Space)&&isonGround)
        {
            rb.AddForce(Vector2.up * JumpForce, ForceMode2D.Impulse);       
        }
    }
    private void Facing()
    {
        if (xVelocity>0)
        {
            rb.transform.localScale = new Vector2(1, 1);
        }
        if (xVelocity<0)
        {
            rb.transform.localScale = new Vector2(-1, 1);
        }
    }
    private void RayCheck()
    {       
        RaycastHit2D left = Physics2D.Raycast(new Vector2(box.offset.x+footoffset,0), Vector2.down, raydistance, layer);
        Color color1 = left ? Color.red : Color.green;
        Debug.DrawRay(new Vector2(box.offset.x + footoffset, 0), Vector2.down,color1);
        RaycastHit2D right = Physics2D.Raycast(new Vector2(box.offset.x - footoffset, 0), Vector2.down, raydistance, layer);
        Color color2 = right ? Color.red : Color.green;
        Debug.DrawRay(new Vector2(box.offset.x - footoffset, 0), Vector2.down, color2);
        if (left || right)
        {
            isonGround = true;
        }
        else isonGround = false;
    }
}
