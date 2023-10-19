using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float walkSpeed = 10.0f;

    [SerializeField]
    private float runSpeed;

    private float applySpeed;

    public float rotateSpeed = 10.0f;

    public float jumpForce = 10.0f;          // ���� ��

    private bool isGround = true;           // ĳ���Ͱ� ���� �ִ��� Ȯ���� ����

    private bool isRun = false;

    Rigidbody rb;                           // ������ �ٵ� ����

    float h, v;

    // ����Ƽ ����� ���ÿ� �ѹ� ����Ǵ� �Լ�
    void Start()
    {
        rb = GetComponent<Rigidbody>();   // Component�� Ȱ���� Rigidbody���
        applySpeed = walkSpeed;
    }

    // Update is called once per frame
    void Update()
    {

    }

    // �̵� ���� �Լ��� Update���� FixedUpdate�� �� ȿ���� ���ٰ� ��.
    void FixedUpdate()
    {
        Move();
        Jump();
        tryrun();
    }

    void Move()
    {
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");

        Vector3 dir = new Vector3(h, 0, v);

        if (!(h == 0 && v == 0))
        {
            transform.position += dir * walkSpeed * Time.deltaTime;
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(dir), Time.deltaTime * rotateSpeed);
        }
    }
    void tryrun()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            Running();
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            RunningCancel();
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

    void Jump()
    {
        // �����̽��ٸ� ������
        if (Input.GetKey(KeyCode.Space) && isGround)
        {
            // rigidbody�� AddForce���� ���ϰ�
            // AddForce(����, ���� ��� ����ϴ���)
            // ForceMode.Impulse �������� ������ ���Ը� ������ �� ���
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

            // ���� �浹������ isGround�� false�� �ٲ�
            isGround = false;
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
