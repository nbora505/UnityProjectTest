using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject[] playerList;
    List<GameObject> penaltyList;
    List<GameObject> deadList;
    GameObject leaderPlayer;

    public int maxPlayerCnt = 4;

    public int curRound = 1;
    public int maxRound = 3;
    public int curTurn;
    public int maxCardCnt = 5;

    public int selectedBomb = -1;

    List<bool> cardCheck; //�̹� ���� ī������ ����
    List<bool> isPenalty; //��Ģ ��������� ����

    public CardManager cardManager;

    void Start()
    {
        playerList = GameObject.FindGameObjectsWithTag("Player");

        //���� �÷��̾�(�� ó�� ������ ���) ���ϱ�
        curTurn = Random.Range(0, playerList.Length);
        leaderPlayer = playerList[curTurn];

        //���� ����
        StartRound();
    }

    void StartRound()
    {
        //����Ʈ �ʱ�ȭ
        ResetLists();

        Debug.Log("=========Round " + curRound + " =========");

        //�÷��̾�鿡�� ī�� �����ֱ�
        cardManager.DoCardShuffle();
        cardManager.TestUserCard(playerList.Length);

        //�¼� �����ϱ�;
        DecideWinCnt();

        //4���� �� ����
        for (int i = 1; i <= 4; i++)
        {
            Debug.Log("=========Turn " + i + " =========");

            //ī�� �����ϱ�
            SubmitCard();

            //������ ī�� ���� ���� �����ϱ�(�߰� �ؾ� ��)
        }

        //���尡 ���� ������ �¼� ������� �Ǵ�, ��Ģ ����(�̿ϼ�)
        if (curRound > maxRound)
        {
            for (int i = 0; i < playerList.Length; i++)
            {
                if (isPenalty[i] == true)
                {
                    penaltyList.Add(playerList[i]);
                    CheckBomb(i);
                }
            }

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

            Debug.Log(playerList[curTurn] + "�� �¼� ���� : " + Random.Range(0, 4) + "��");

            curTurn++;
            if (curTurn >= playerList.Length) curTurn = 0;
        }
    }

    void SubmitCard()
    {
        Debug.Log("=========ī�� ���� �ܰ�=========");


        //���� �÷��̾���� ���ʷ� ī�� ����. �ӽ÷� �������ڷ� ī������ ó���ص�
        for (int i = 0; i < playerList.Length; i++)
        {
            List<int> curCardList = playerList[curTurn].GetComponent<PlayerController>().cardList;
            int yimsiCard = Random.Range(0, curCardList.Count);
            int selectedCard = curCardList[yimsiCard];

            Debug.Log(playerList[curTurn] + "�� ī�� ���� : " + selectedCard);

            playerList[curTurn].GetComponent<PlayerController>().cardList.RemoveAt(yimsiCard);

            curTurn++;
            if (curTurn >= playerList.Length) curTurn = 0;
        }

    }

    public IEnumerator CheckBomb(int player)
    {
        //��Ģ ������� �����ִ� ��ź�� Ȱ��ȭ��Ű��
        for (int i = 0; i < playerList[player].GetComponent<PlayerController>().remainedBomb; i++)
        {
            playerList[player].GetComponent<PlayerController>().bombPrefab[i].SetActive(true);
        }

        //�����ִ� ���� ����ŭ ���� ������ ��÷ ���� �����ϱ�
        int realBomb = Random.Range(0, playerList[player].GetComponent<PlayerController>().remainedBomb);

        //������ ����������� ��ٸ���(�⺻���� -1)
        yield return new WaitUntil(() => selectedBomb >= 0);

        //�޾ƿ� ��ź ��ȣ�� ��÷ ��ź ��ȣ�� �Ȱ����� �����Ű��
        if (selectedBomb == realBomb)
        {
            //playerList.Remove(playerList[player]);
            deadList.Add(playerList[player]);
        }
        else
        {
            playerList[player].GetComponent<PlayerController>().remainedBomb--;
            penaltyList.Clear();
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
