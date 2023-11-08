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
            // StartCoroutine() 코루틴 실행 함수
            //Stop을 앞에 쓰는 이유는 같은 동작을 새로 시작하기 위해 로직이 꼬이지 않게 하기 위함
            StopCoroutine("Swing"); 

            StartCoroutine("Swing");
        }
    }
    //코루틴 사용
    IEnumerator Swing()
    {
        // yield 결과를 전달하는 키워드 코루틴에서는 1개 이상 필요
        // yield 키워드를 여러개 사용해서 시간차 로직 작성 가능

        // 1
        yield return new WaitForSeconds(0.1f); // 0.1초 대기
        meleeArea.enabled = true;
        trailEffect.enabled = true;

        //2
        yield return new WaitForSeconds(0.3f);
        meleeArea.enabled = false;

        //3
        yield return new WaitForSeconds(0.3f);
        meleeArea.enabled = false;


        // yield break; 로 코루틴 탈출 가능

    }
    // 일반함수일때
    //Use() 메인루틴 -> Swing() 서브루틴 -> Use()메인루틴

    //코루틴일때
    //Use() 메인루틴 + Swing() 코루틴(co-op)
}
