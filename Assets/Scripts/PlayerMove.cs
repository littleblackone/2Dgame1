using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [Header("�ƶ�����")]
    public float Speed;
    public float JumpForce;
    float xVelocity;

    [Header("ͼ��")]
    public LayerMask Groundlayer;

    BoxCollider2D box; 
    Rigidbody2D rb;

    [Header("״̬�ж�")]
    public  bool isonGround;

    [Header("�����жϲ���")]
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
        Gizmos.DrawLine(transform.position + rayWidth, transform.position + rayWidth + Vector3.down * rayDistance);//��һ����������㣬�ڶ������յ�
        Gizmos.DrawLine(transform.position - rayWidth, transform.position - rayWidth + Vector3.down * rayDistance);     
    }
}
