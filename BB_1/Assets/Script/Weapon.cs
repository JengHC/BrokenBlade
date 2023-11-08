using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public enum Type { Melee, Range }
    public Type type;
    public int damage;
    public float rate;
    public BoxCollider meleeArea;
    public TrailRenderer trailEffect;

    public void Use()
    {
        if(type == Type.Melee)
        {
            // StartCoroutine() �ڷ�ƾ ���� �Լ�
            //Stop�� �տ� ���� ������ ���� ������ ���� �����ϱ� ���� ������ ������ �ʰ� �ϱ� ����
            StopCoroutine("Swing"); 

            StartCoroutine("Swing");
        }
    }
    //�ڷ�ƾ ���
    IEnumerator Swing()
    {
        // yield ����� �����ϴ� Ű���� �ڷ�ƾ������ 1�� �̻� �ʿ�
        // yield Ű���带 ������ ����ؼ� �ð��� ���� �ۼ� ����

        // 1
        yield return new WaitForSeconds(0.1f); // 0.1�� ���
        meleeArea.enabled = true;
        trailEffect.enabled = true;

        //2
        yield return new WaitForSeconds(0.3f);
        meleeArea.enabled = false;

        //3
        yield return new WaitForSeconds(0.3f);
        meleeArea.enabled = false;


        // yield break; �� �ڷ�ƾ Ż�� ����

    }
    // �Ϲ��Լ��϶�
    //Use() ���η�ƾ -> Swing() �����ƾ -> Use()���η�ƾ

    //�ڷ�ƾ�϶�
    //Use() ���η�ƾ + Swing() �ڷ�ƾ(co-op)
}
