using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    List<GameObject> playerList = new List<GameObject>();
    List<GameObject> penaltyList;
    List<GameObject> deadList;
    GameObject leaderPlayer;
    int curRound = 0;
    int maxRound;
    int curTurn;
    int maxCardCnt = 5;

    public int selectedBomb = -1;

    List<bool> cardCheck; //이미 나온 카드인지 여부
    List<bool> isPenalty; //벌칙 대상자인지 여부

    PlayerController playerController;

    void Start()
    {
        playerList = new List<GameObject>();


        StartRound();
    }

    void StartRound()
    {
        //라운드가 끝날 때마다 벌칙 대상 확인
        if (curRound > maxRound)
        {
            for (int i = 0; i < playerList.Count; i++)
            {
                if (isPenalty[i] == true)
                {
                    penaltyList.Add(playerList[i]);
                    CheckBomb(i);
                }
            }

        }

        //리더 플레이어(맨 처음 시작할 사람) 정하기
        curTurn = Random.Range(0, playerList.Count);
        leaderPlayer = playerList[curTurn];

        //플레이어들에게 카드 나눠주기
        for (int i = 0; i < playerList.Count; i++)
        {
            for (int j = 0; j < maxCardCnt; j++)
            {
                playerList[i].GetComponent<PlayerController>().cardList[j] = SelectRandomCard();
            }
        }

        //승수 결정하기;
        DecideWinCnt();
    }

    int SelectRandomCard()
    {
        //카드매니저의 덱 리스트의 길이만큼 랜덤 돌리기, 지금은 임시로 20까지만!
        //int selectedCardNum = Random.Range(0, CardManager.DeckList.Count);
        int selectedCardNum = Random.Range(0, 20);

        //모든 카드덱이 한 번씩 다 나왔으면
        if (cardCheck.All(b => b))
        {
            // 모든 값을 false로 초기화
            for (int i = 0; i < cardCheck.Count; i++)
            {
                cardCheck[i] = false;
            }
        }

        //이미 나온 카드면 재귀함수 사용해서 다시 고르기 
        if (cardCheck[selectedCardNum] == true)
        {
            return SelectRandomCard();
        }

        //나왔던 카드라고 표시해두고 리턴
        cardCheck[selectedCardNum] = true;
        return selectedCardNum;
    }

    void DecideWinCnt()
    {
        if (playerList[curTurn] != leaderPlayer)
        {
            //플레이어 리스트[현재 차례]의 승수 선언 UI 활성화
        }
        else
        {
            //플레이어 리스트[현재 차례].카드 제출(플레이어 순서별 가중치 = 3);
        }
    }

    void MoveNextTurn()
    {
        curTurn++;
        if (curTurn >= playerList.Count)
        {
            curTurn = 0;
        }

    }

    IEnumerator CheckBomb(int player)
    {
        //벌칙 대상자의 남아있는 폭탄들 활성화시키기
        for (int i = 0; i < playerList[player].GetComponent<PlayerController>().remainedBomb; i++)
        {
            playerList[player].GetComponent<PlayerController>().bombPrefab[i].SetActive(true);
        }

        //남아있는 심지 수만큼 랜덤 돌려서 당첨 심지 결정하기
        int realBomb = Random.Range(0, playerList[player].GetComponent<PlayerController>().remainedBomb);

        //심지가 골라질때까지 기다리기(기본값은 -1)
        yield return new WaitUntil(() => selectedBomb >= 0);

        //받아온 폭탄 번호가 당첨 폭탄 번호랑 똑같으면 사망시키기
        if (selectedBomb == realBomb)
        {
            playerList.Remove(playerList[player]);
            deadList.Add(playerList[player]);
        }
        else
        {
            playerList[player].GetComponent<PlayerController>().remainedBomb--;
            penaltyList.Clear();
        }
    }

}