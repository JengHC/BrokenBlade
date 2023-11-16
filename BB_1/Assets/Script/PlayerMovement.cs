using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Animator anim;

    public float walkSpeed = 10.0f;

    [SerializeField]
    private float runSpeed;

    private float applySpeed;

    public float rotateSpeed = 10.0f;

    public float jumpForce = 10.0f;          // ���� ��

    private bool isGround = true;           // ĳ���Ͱ� ���� �ִ��� Ȯ���� ����

    private bool isRun = false;

    //private bool isFireReady = true;

    //public GameObject[] weapons;

    //Weapon weapon;

    Rigidbody rb;                           // ������ �ٵ� ����

    float h, v;

    //float fireDelay;

    // ����Ƽ ����� ���ÿ� �ѹ� ����Ǵ� �Լ�
    void Start()
    {
        rb = GetComponent<Rigidbody>();   // Component�� Ȱ���� Rigidbody���
        applySpeed = walkSpeed;
        anim = GetComponent<Animator>();
        //weapon = GetComponent<Weapon>();

    }

    void Update()
    {
        tryrun();
        Jump();
        //wea();
        Attack();
        Move();
        
    }
    // �̵� ���� �Լ��� Update���� FixedUpdate�� �� ȿ���� ���ٰ� ��.


    void tryrun()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && isGround)
        {
            Running();
            anim.SetBool("isRun", true);
            Debug.Log("Run");
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            RunningCancel();
            Debug.Log("Run Over");
            anim.SetBool("isRun", false);
        }
    }

    private void Running()
    {
        isRun = true;
        applySpeed = runSpeed;
    }
    private void RunningCancel()
    {
        isRun = false;
        applySpeed = walkSpeed;
    }

   
    void Move()
    {
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");

        Vector3 dir = new Vector3(h, 0, v).normalized;

        if (!(h == 0 && v == 0))
        {
            transform.position += dir * applySpeed * Time.deltaTime;
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(dir), Time.deltaTime * rotateSpeed);
            anim.SetBool("isWalk", true);
        }
        else
        {
            anim.SetBool("isWalk", false);
        }
    }

    void Jump()
    {
        // �����̽��ٸ� ������
        if (Input.GetKey(KeyCode.Space) && isGround)
        {
            // rigidbody�� AddForce���� ���ϰ�
            // AddForce(����, ���� ��� ����ϴ���)
            // ForceMode.Impulse �������� ������ ���Ը� ������ �� ���
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

            anim.SetBool("isJump", true);

            // ���� �浹������ isGround�� false�� �ٲ�
            isGround = false;
            
        }
        else
        {
            anim.SetBool("isJump", false);
        }
    }

    //void wea()
    //{
    //    if(weapon != null)
    //    {
    //        weapon.gameObject.SetActive(!weapon.gameObject.activeSelf);
    //    }

    //}

    void Attack()
    {

        //if (weapon == null)
        //{
        //    return;
        //}
        //fireDelay += Time.deltaTime;
        //isFireReady = weapon.rate < fireDelay;

        if (Input.GetMouseButtonDown(0) /*&& isFireReady*/)
        {
            //weapon.Use();
            anim.SetTrigger("isAttack");
            //fireDelay = 0;
            Debug.Log("Click");
        }

    }

    // �浹 �Լ�
    void OnCollisionEnter(Collision collision)
    {
        // �ε��� ��ü�� �±װ� "Ground"���
        if (collision.gameObject.CompareTag("Ground"))
        {
            // isGround�� true�� ����
            isGround = true;
        }
    }
}
