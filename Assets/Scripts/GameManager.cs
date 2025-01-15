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

    List<bool> cardCheck; //�̹� ���� ī������ ����
    List<bool> isPenalty; //��Ģ ��������� ����

    PlayerController playerController;

    void Start()
    {
        playerList = new List<GameObject>();


        StartRound();
    }

    void StartRound()
    {
        //���尡 ���� ������ ��Ģ ��� Ȯ��
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

        //���� �÷��̾�(�� ó�� ������ ���) ���ϱ�
        curTurn = Random.Range(0, playerList.Count);
        leaderPlayer = playerList[curTurn];

        //�÷��̾�鿡�� ī�� �����ֱ�
        for (int i = 0; i < playerList.Count; i++)
        {
            for (int j = 0; j < maxCardCnt; j++)
            {
                playerList[i].GetComponent<PlayerController>().cardList[j] = SelectRandomCard();
            }
        }

        //�¼� �����ϱ�;
        DecideWinCnt();
    }

    int SelectRandomCard()
    {
        //ī��Ŵ����� �� ����Ʈ�� ���̸�ŭ ���� ������, ������ �ӽ÷� 20������!
        //int selectedCardNum = Random.Range(0, CardManager.DeckList.Count);
        int selectedCardNum = Random.Range(0, 20);

        //��� ī�嵦�� �� ���� �� ��������
        if (cardCheck.All(b => b))
        {
            // ��� ���� false�� �ʱ�ȭ
            for (int i = 0; i < cardCheck.Count; i++)
            {
                cardCheck[i] = false;
            }
        }

        //�̹� ���� ī��� ����Լ� ����ؼ� �ٽ� ���� 
        if (cardCheck[selectedCardNum] == true)
        {
            return SelectRandomCard();
        }

        //���Դ� ī���� ǥ���صΰ� ����
        cardCheck[selectedCardNum] = true;
        return selectedCardNum;
    }

    void DecideWinCnt()
    {
        if (playerList[curTurn] != leaderPlayer)
        {
            //�÷��̾� ����Ʈ[���� ����]�� �¼� ���� UI Ȱ��ȭ
        }
        else
        {
            //�÷��̾� ����Ʈ[���� ����].ī�� ����(�÷��̾� ������ ����ġ = 3);
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