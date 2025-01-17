using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject[] playerList;
    public List<GameObject> penaltyList;
    public List<GameObject> deadList;
    GameObject leaderPlayer;

    public int maxPlayerCnt = 4;

    public int curRound = 1;
    public int maxRound = 3;
    public int curTurn;
    public int maxCardCnt = 5;

    public int selectedBomb = -1;

    public List<int> submitCardList; //����� ī�帮��Ʈ
    public int[] predictedWinCnt; //������ ���帶�� �÷��̾���� ������ �¸� Ƚ��
    public int[] winCntOfEachTurn; //������ �ϸ��� �÷��̾���� ����� �¸� Ƚ��

    public CardManager cardManager;
    public ScoreManager scoreManager;

    void Start()
    {
        playerList = GameObject.FindGameObjectsWithTag("Player");

        //���� �÷��̾�(�� ó�� ������ ���) ���ϱ�
        curTurn = Random.Range(0, playerList.Length);
        leaderPlayer = playerList[curTurn];

        //���� ����
        StartRound();
    }
    // 

    void StartRound()
    {
        //����Ʈ �ʱ�ȭ
        ResetLists();

        Debug.Log("=========Round " + curRound + " =========");

        //�÷��̾�鿡�� ī�� �����ֱ�
        cardManager.DoCardShuffle();
        cardManager.TestUserCard(playerList.Length);

        //�¼� �����ޱ�;
        DecideWinCnt();


        //4���� �� ����
        for (int i = 1; i <= 4; i++)
        {
            Debug.Log("=========Turn " + i + " =========");
            //�¼� ���ؼ� ���ڸ� ������ �Լ��� �ʿ��� '������� ī�� ����Ʈ'�� �ϸ��� �ʱ�ȭ
            submitCardList.Clear();

            //ī�� ����ޱ�
            Debug.Log("=========ī�� ���� �ܰ�=========");
            for (int j = 0; j < playerList.Length; j++)
            {
                SubmitCard();
            }

            //������ ī�� ���� ���� �����ϱ�(ī��Ŵ����� �� �ִ� �Լ� ȣ��)
            Debug.Log("=========�̹� �� ���� ����=========");
            for (int j = 0; j < playerList.Length; j++)
            {
                bool checker = cardManager.CardCompare(submitCardList, j);

                if (checker) //checker�� ��ȯ���� true��...
                {
                    //playerList[curTurn]�� �̹� �� ���ڶ�� ��!
                    winCntOfEachTurn[curTurn]++;
                    Debug.Log("�̹� ���� ���ڴ� " + playerList[curTurn] + "! (���� " + winCntOfEachTurn[curTurn] + "��)");
                }
                curTurn++;
                if (curTurn >= playerList.Length) curTurn = 0;
            }

        }

        //���尡 ���� ������ �¼� ������� �Ǵ�, ��Ģ ����(�̿ϼ�)
        for (int i = 0; i < playerList.Length; i++)
        {
            GameObject curPlayer = playerList[curTurn];

            if (predictedWinCnt[curTurn] == winCntOfEachTurn[curTurn])
            {
                Debug.Log(curPlayer + " ���� ����!");
            }
            else
            {
                Debug.Log(curPlayer + " ���� ����...");

                //������ �÷��̾����� ��ź ���� �����Ű�� �ϱ�
                scoreManager.CheckBomb(curPlayer.GetComponent<PlayerController>());
            }

            curTurn++;
            if (curTurn >= playerList.Length) curTurn = 0;
        }


        //���� ����
        curRound++;
        if (curRound > maxRound)
        {
            Debug.Log("�ִ� ���� �ʰ�");
        }
        else
        {
            StartRound();
        }
    }

    void DecideWinCnt()
    {
        Debug.Log("=========�¼� ���� �ܰ�=========");

        //���� �÷��̾���� ���ʷ� �¼� ����. �ӽ÷� �������ڷ� �¸����� ó���ص�
        for (int i = 0; i < playerList.Length; i++)
        {
            //���⿡�� �÷��̾� ����Ʈ[���� ����]�� �¼� ���� UI Ȱ��ȭ
            predictedWinCnt[curTurn] = Random.Range(0, 4);
            Debug.Log(playerList[curTurn] + "�� �¼� ���� : " + predictedWinCnt[curTurn] + "��");

            curTurn++;
            if (curTurn >= playerList.Length) curTurn = 0;
        }
    }

    void SubmitCard() // SubmitCard(int subitCard)
    {
        //���� �÷��̾���� ���ʷ� ī�� ����. �ӽ÷� �������ڷ� ī������ ó���ص�
        List<int> curCardList = playerList[curTurn].GetComponent<PlayerController>().cardList;
        // tempCard was init for testing
        int tempCard = Random.Range(0, curCardList.Count);
        
        // ���� ���� �ܰ迡����, �̰Ÿ� �������̽�Ʈ�� �ؼ� �޾ƿ� ī���� ������ �ǰ���?
        int selectedCard = curCardList[tempCard];

            Debug.Log(playerList[curTurn] + "�� ī�� ���� : " + selectedCard);

            curCardList.RemoveAt(tempCard);
            submitCardList.Add(selectedCard);//����� ī�峢�� ���ϱ� ���� ����ī�帮��Ʈ�� �ֱ�

            curTurn++;
            if (curTurn >= playerList.Length) curTurn = 0;
        

    }
    void ResetLists()
    {
        predictedWinCnt = new int[playerList.Length];
        winCntOfEachTurn = new int[playerList.Length];

        cardManager.GetComponent<CardManager>().ResetCardSet();
        for (int i = 0; i < playerList.Length; i++)
        {
            playerList[i].GetComponent<PlayerController>().cardList.Clear();
        }
    }
}
