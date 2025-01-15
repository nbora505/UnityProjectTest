using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    GameManager gm;
    PlayerController pc;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public IEnumerator CheckBomb(int player)
    {
        //벌칙 대상자의 남아있는 폭탄들 활성화시키기
        for (int i = 0; i < gm.playerList[player].GetComponent<PlayerController>().remainingBomb; i++)
        {
            gm.playerList[player].GetComponent<PlayerController>().bombPrefab[i].SetActive(true);
        }

        //남아있는 심지 수만큼 랜덤 돌려서 당첨 심지 결정하기
        //pc.DrawBomb();


        //int realBomb = Random.Range(0, gm.playerList[player].GetComponent<PlayerController>().remainingBomb);

        //심지가 골라질때까지 기다리기(기본값은 -1)
        yield return new WaitUntil(() => gm.selectedBomb >= 0);

        pc.DrawBomb();

    }
    public void Timeout()//함수 타임 아웃(파라메타 없음)
    {        //    GameManager.벌칙 대상 리스트.Clear();
             //   벌칙대상 리스트는 스코어 매니저, 폭탄터지는 건 게임매니저
             //    GameManager.폭탄 비교(this.player, 랜덤);
    }   //    함수 끝

    public void DistinguishScore(PlayerController player)
    {
            player.remainingBomb--; // This is a temporary line put in for the current structure.
                                   // If a bomb selection line is ever created,
                                   // it should run the function, subtract the bomb, and so on.
    }
}
