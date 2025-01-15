//using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    //[변수]
    public bool AIPlayer = false;
    public List<int> cardList;

    public List<bool> bombList = new List<bool>() {
        false, false, true };
    public GameObject[] bombPrefab;
    public int remainingBomb = 3; //  int 남은 폭탄심지 수
    //public int playerOrder = 0; //    플레이어 순서별 가중치 0 to 3;
    public int submitTime = 0; // 만약에, 유저가 레이케이스트로 카드를 선택하고 제출했을 때, 그때의 유저의 순서가 몇 번인가를 기준으로 초기화
    public bool bombVisible = false;
    public bool isDead = false;//플레이어의 사망
    public GameManager gm;

    //[함수]
    public void DeclareWins() //함수 승수 선언()
    {
        //    0~4 승수 선택용 숫자 등장();
        Invoke("TimeOut", 10f);
        //타임아웃 함수는 여기 캔슬은 버튼매니저에서

        // 버튼에서 vr object 찍을 수 있는 상태로 만들어 준 카드를 선택 only

    }//함수 끝

    public void AppearBomb(int remainingBomb) //함수 심지 등장(int 심지 수)
    {
        for (int i = 0; i < remainingBomb; i++)
        {
            gameObject.GetComponentInChildren<GameObject>().SetActive(true);
        }

        //  플레이어 선택 존중하려면 랜덤 배제해야 하나 편의상 선택과
        //  무관하게 랜덤으로 터지는 폭탄 생성



    }//함수 끝

    public void DrawBomb()//함수 폭탄 결정(파라메타 없음)
    {
        int rnd = Random.Range(0, bombList.Count);
        bool istrueBomb = bombList[rnd]; // 딕셔너리가 필요한가? 필요없는것으로 판단

        if (istrueBomb)
        {
            gm.deadList.Add(this.gameObject);
            gm.penaltyList.Remove(this.gameObject);
            // 사망처리
            bool isDead = true;
        }
        else
        {
            remainingBomb--;
            gm.penaltyList.Remove(this.gameObject);
            bombList.RemoveAt(rnd);
            // false가 지워지겠죠.
            // 생존처리
        }
    } //    함수 끝
}
