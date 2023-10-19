using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private Rigidbody rigidbody;
    public float speed = 10f;
    public float jumpHeight = 3f;
    public float dash = 5f;
    public float rotSpeed = 8; // 캐릭터 회전 속도

    private Vector3 dir = Vector3.zero;
    private bool isJumping = false; // 플레이어가 현재 점프하고 있는지

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = this.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        dir.x = Input.GetAxis("Horizontal");
        dir.z = Input.GetAxis("Vertical");
        dir.Normalize(); // 대각선으로 이동시 속도가 빨라지는 것을 방지하기 위해 정규화를 시켜줌

        if (Input.GetKeyDown(KeyCode.Space) && !isJumping) // Space 누를때 점프하고 있지 않은지 확인
        {
            // Set isJumping to true to prevent multiple jumps
            isJumping = true;

            // rigidbody에 AddForce힘을 가하고
            // AddForce(방향, 힘을 어떻게 사용하는지)
            // ForceMode.Impulse 순간적인 힘으로 무게를 적용할 때 사용
            Vector3 jumpForce = Vector3.up * jumpHeight;
            rigidbody.AddForce(jumpForce, ForceMode.Impulse);
        }

        if (Input.GetButtonDown("Dash"))
        {
            Vector3 dashPower = transform.forward * -Mathf.Log(1 / rigidbody.drag) * dash;
            rigidbody.AddForce(dashPower, ForceMode.VelocityChange);
        }
    }

    private void FixedUpdate()
    {
        if (dir != Vector3.zero)
        {
            // 지금 바라보는 방향의 부호 != 나아갈 방향 부호
            // Mathf.sign은 ()안에 들어간 값이 음수, 0 양수인지 구분
            if (Mathf.Sign(dir.x) != Mathf.Sign(transform.position.x) || Mathf.Sign(dir.z) != Mathf.Sign(transform.position.z))
            {
                transform.Rotate(0, 1, 0);
            }
            // 캐릭터 회전
            transform.forward = Vector3.Lerp(transform.forward, dir, Time.deltaTime * rotSpeed);
        }

        // 캐릭터 이동(이대로 사용하면 대각선으로 움직이는 속도가 일반적인 이동보다 빠르다.)
        rigidbody.MovePosition(this.gameObject.transform.position + dir * speed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Reset isJumping to false when the player touches the ground
        isJumping = false;
    }
}