using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private Rigidbody rigidbody;
    public float speed = 10f;
    public float jumpHeight = 3f;
    public float dash = 5f;
    public float rotSpeed = 8; // ĳ���� ȸ�� �ӵ�

    private Vector3 dir = Vector3.zero;
    private bool isJumping = false; // �÷��̾ ���� �����ϰ� �ִ���

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
        dir.Normalize(); // �밢������ �̵��� �ӵ��� �������� ���� �����ϱ� ���� ����ȭ�� ������

        if (Input.GetKeyDown(KeyCode.Space) && !isJumping) // Space ������ �����ϰ� ���� ������ Ȯ��
        {
            // Set isJumping to true to prevent multiple jumps
            isJumping = true;

            // rigidbody�� AddForce���� ���ϰ�
            // AddForce(����, ���� ��� ����ϴ���)
            // ForceMode.Impulse �������� ������ ���Ը� ������ �� ���
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
            // ���� �ٶ󺸴� ������ ��ȣ != ���ư� ���� ��ȣ
            // Mathf.sign�� ()�ȿ� �� ���� ����, 0 ������� ����
            if (Mathf.Sign(dir.x) != Mathf.Sign(transform.position.x) || Mathf.Sign(dir.z) != Mathf.Sign(transform.position.z))
            {
                transform.Rotate(0, 1, 0);
            }
            // ĳ���� ȸ��
            transform.forward = Vector3.Lerp(transform.forward, dir, Time.deltaTime * rotSpeed);
        }

        // ĳ���� �̵�(�̴�� ����ϸ� �밢������ �����̴� �ӵ��� �Ϲ����� �̵����� ������.)
        rigidbody.MovePosition(this.gameObject.transform.position + dir * speed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Reset isJumping to false when the player touches the ground
        isJumping = false;
    }
}