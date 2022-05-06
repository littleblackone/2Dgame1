using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [Header("移动参数")]
    public float Speed;
    public float JumpForce;
    float xVelocity;

    [Header("图层")]
    public LayerMask Groundlayer;

    BoxCollider2D box; 
    Rigidbody2D rb;

    [Header("状态判断")]
    public  bool isonGround;

    [Header("射线判断参数")]
    public float footOffset = 0.49f;
    public float rayDistance;
    public Vector3 rayWidth;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        box = GetComponent<BoxCollider2D>();
    }


    void Update()
    {
        rayCheck();
        jump();       
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
            rb.transform.localScale = new Vector2(0.47f, 1);
        }
        if (xVelocity<0)
        {
            rb.transform.localScale = new Vector2(-0.47f, 1);
        }
    }
    private void rayCheck()
    {
        isonGround = (Physics2D.Raycast(transform.position + rayWidth, Vector2.down, rayDistance, Groundlayer) || Physics2D.Raycast(transform.position - rayWidth, Vector2.down, rayDistance, Groundlayer));
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position + rayWidth, transform.position + rayWidth + Vector3.down * rayDistance);//第一个参数是起点，第二个是终点
        Gizmos.DrawLine(transform.position - rayWidth, transform.position - rayWidth + Vector3.down * rayDistance);     
    }
}
