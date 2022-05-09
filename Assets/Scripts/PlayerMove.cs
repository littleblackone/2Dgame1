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
    public float rayDistancefoot;
    public float rayDistancenose;
    public Vector3 rayWidthfoot;
    public Vector3 rayWidthnose;
    public GameObject nose;

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
            rb.transform.localScale = new Vector2(0.34f, 1);
        }
        if (xVelocity<0)
        {
            rb.transform.localScale = new Vector2(-0.34f, 1);
        }
    }
    private void rayCheck()
    {
        isonGround = (Physics2D.Raycast(transform.position + rayWidthfoot, Vector2.down, rayDistancefoot, Groundlayer) || Physics2D.Raycast(transform.position - rayWidthfoot, Vector2.down, rayDistancefoot, Groundlayer)||Physics2D.Raycast(nose.transform.position + rayWidthnose, Vector2.down, rayDistancenose, Groundlayer)|| Physics2D.Raycast(nose.transform.position - rayWidthnose, Vector2.down, rayDistancenose, Groundlayer));     
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position + rayWidthfoot, transform.position + rayWidthfoot + Vector3.down * rayDistancefoot);//��һ����������㣬�ڶ������յ�
        Gizmos.DrawLine(transform.position - rayWidthfoot, transform.position - rayWidthfoot + Vector3.down * rayDistancefoot);
        if (rb.transform.localScale.x > 0)
        {
            Gizmos.DrawLine(nose.transform.position - rayWidthnose, nose.transform.position - rayWidthnose + Vector3.down * rayDistancenose);
        }
        if (rb.transform.localScale.x < 0)
        {
            Gizmos.DrawLine(nose.transform.position + rayWidthnose, nose.transform.position + rayWidthnose + Vector3.down * rayDistancenose);
        }
    }
}
