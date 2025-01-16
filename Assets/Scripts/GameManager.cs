using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject[] playerList;
    public List<GameObject> penaltyList;
    public List<GameObject> deadList;
    GameObject leaderPlayer;
    ScoreManager scoreManager;

    public int maxPlayerCnt = 4;

    public int curRound = 1;
    public int maxRound = 3;
    public int curTurn;
    public int maxCardCnt = 5;

    public int selectedBomb = -1;

    List<bool> cardCheck; //이미 나온 카드인지 여부
    List<bool> isPenalty; //벌칙 대상자인지 여부

    public CardManager cardManager;

    void Start()
    {
        playerList = GameObject.FindGameObjectsWithTag("Player");

        //리더 플레이어(맨 처음 시작할 사람) 정하기
        curTurn = Random.Range(0, playerList.Length);
        leaderPlayer = playerList[curTurn];

        //라운드 시작
        StartRound();
    }
    // 

    void StartRound()
    {
        //리스트 초기화
        ResetLists();

        Debug.Log("=========Round " + curRound + " =========");

        //플레이어들에게 카드 나눠주기
        cardManager.DoCardShuffle();
        cardManager.TestUserCard(playerList.Length);

        //승수 결정하기;
        DecideWinCnt();

        //4번의 턴 시작
        for (int i = 1; i <= 4; i++)
        {
            Debug.Log("=========Turn " + i + " =========");

            //카드 제출하기
            SubmitCard();

            //제출한 카드 보고 승자 결정하기(추가 해야 함)
        }

        //라운드가 끝날 때마다 승수 맞췄는지 판단, 벌칙 결정(미완성)
        if (curRound > maxRound)
        {
            for (int i = 0; i < playerList.Length; i++)
            {
                if (isPenalty[i] == true)
                {
                    penaltyList.Add(playerList[i]);
                    scoreManager.CheckBomb(i);
                }
            }

        }

        //라운드 종료
        curRound++;
        if (curRound > maxRound)
        {
            Debug.Log("최대 라운드 초과");
        }
        else
        {
            StartRound();
        }
    }

    void DecideWinCnt()
    {
        Debug.Log("=========승수 선언 단계=========");

        //리더 플레이어부터 차례로 승수 선언. 임시로 랜덤숫자로 승리선언 처리해둠
        for (int i = 0; i < playerList.Length; i++)
        {
            //여기에서 플레이어 리스트[현재 차례]의 승수 선언 UI 활성화

            Debug.Log(playerList[curTurn] + "의 승수 선언 : " + Random.Range(0, 4) + "승");

            curTurn++;
            if (curTurn >= playerList.Length) curTurn = 0;
        }
    }

    void SubmitCard() // SubmitCard(int subitCard)
    {
        Debug.Log("=========카드 제출 단계=========");


        //리더 플레이어부터 차례로 카드 제출. 임시로 랜덤숫자로 카드제출 처리해둠
        for (int i = 0; i < playerList.Length; i++)
        {
            List<int> curCardList = playerList[curTurn].GetComponent<PlayerController>().cardList;
            // tempCard was init for testing
            int tempCard = Random.Range(0, curCardList.Count);
            // 실제 구현 단계에서는, 이거를 레이케이스트로 해서 받아온 카드의 정보가 되겠죠?
            int selectedCard = curCardList[tempCard];

            Debug.Log(playerList[curTurn] + "의 카드 제출 : " + selectedCard);

            playerList[curTurn].GetComponent<PlayerController>().cardList.RemoveAt(tempCard);

            curTurn++;
            if (curTurn >= playerList.Length) curTurn = 0;
        }

    }
    void ResetLists()
    {
        cardManager.GetComponent<CardManager>().ResetCardSet();
        for (int i = 0; i < playerList.Length; i++)
        {
            playerList[i].GetComponent<PlayerController>().cardList.Clear();
        }
    }
}
