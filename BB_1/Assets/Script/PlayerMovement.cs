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

    public float jumpForce = 10.0f;          // 점프 힘

    private bool isGround = true;           // 캐릭터가 땅에 있는지 확인할 변수

    private bool isRun = false;

    Rigidbody rb;                           // 리지드 바디 변수

    float h, v;

    // 유니티 실행과 동시에 한번 실행되는 함수
    void Start()
    {
        rb = GetComponent<Rigidbody>();   // Component를 활용해 Rigidbody사용
        applySpeed = walkSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        tryrun();
        Move();
        Jump();
    }

    // 이동 관련 함수는 Update보다 FixedUpdate가 더 효율이 좋다고 함.


    void tryrun()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            Running();
            Debug.Log("Run");
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            RunningCancel();
            Debug.Log("Run Over");
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
        }
    }

    void Jump()
    {
        // 스페이스바를 누르면
        if (Input.GetKey(KeyCode.Space) && isGround)
        {
            // rigidbody에 AddForce힘을 가하고
            // AddForce(방향, 힘을 어떻게 사용하는지)
            // ForceMode.Impulse 순간적인 힘으로 무게를 적용할 때 사용
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

            // 땅과 충돌없으면 isGround를 false로 바꿈
            isGround = false;
        }
    }

    // 충돌 함수
    void OnCollisionEnter(Collision collision)
    {
        // 부딪힌 물체의 태그가 "Ground"라면
        if (collision.gameObject.CompareTag("Ground"))
        {
            // isGround를 true로 변경
            isGround = true;
        }
    }
}

