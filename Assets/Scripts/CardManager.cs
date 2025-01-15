using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    public GameManager gameManager;

    // It can be changed. this vars inited for test
    public List<int> card = new List<int>();
    public List<int> cardset = new List<int>();

    private List<int> originalCardset = new List<int>(){
        1,1,1,1,
        2,2,2,2,
        3,3,3,3,
        4,4,4,4
    };

    public void ResetCardSet()
    {
        cardset.Clear();
        cardset.AddRange(originalCardset); // ������ cardset�� ����
    }


    // Start is called before the first frame update
    void Start()
    {
        ResetCardSet();
        // if player play multi, use this line.
        //if (PhotonNetwork.isMasterClient)
        //{
        //    DoCardShuffle();
        //    TestUserCard();
        //}

        // else, player play single, use this line

        //DoCardShuffle();


        ///
        /// TestUserCard();
        //[important]totalPlayer ������ ���ӸŴ������� ������ ����

        // ���࿡ �� �׽�Ʈ ���� ī���Լ��� ���� �ʹ�.
        // �׷��� ���� �޴����� ��Ż �÷��̾��� ���� ���� ������ ������ ���� �������?
        // �׷��ٸ�, ���� �޴��� �ڵ���ο���
        // CardManager.TestUserCard(totalPlayers)
        // �̷������� ����, �� ���� �Լ����� ������, �� Ŭ�������� ������ �ϴ°��� ���� ������
        // ���⼭ �̷������� �����ϰ� ���� Ŀ�����ϱ� �������� ������, �̸� ������ ���̴�.
    }

    // Update is called once per frame
    void Update()
    {

    }
    /// <summary>
    /// For Card Suffle Func.
    /// At first time, clear card List.
    /// At Sec, init maximum, and run For code Lines.
    /// </summary>
    public void DoCardShuffle()
    {
        card.Clear();

        int maximum = cardset.Count;

        // ī����� ������ 16.
        // �ٵ� �ϳ��� ������ �Ǹ� ī��Ʈ�� 15
        // �� �ϳ� �� ������ �Ǹ� ī��Ʈ 14�� �ǰ���
        // ��� i < cardset.Count �̷��� �ص� ������ �ȵǴµ�
        // ���������� �˷��帮�� ���� ���ؼ� max �������ٰ� �־ �����ذ�.
        for (int i = 0; i < maximum; i++)
        {
            int rnd = Random.Range(0, cardset.Count);
            card.Add(cardset[rnd]);
            cardset.RemoveAt(rnd);
        }

        Debug.Log(card.Count);
        ShowCardList();
    }

    /// <summary>
    /// For Debugging.
    /// Check CardList
    /// </summary>
    public void ShowCardList()
    {
        for (int i = 0; i < card.Count; i++)
        {
            //Debug.Log(card[i]);
        }
    }

    public int GiveACardToUsers()
    {
        int aCard = card[0];
        Debug.Log(aCard);
        card.RemoveAt(0);

        return aCard;
    }

    public void TestUserCard(int totalPlayers)
    {
        // ���� �Ʒ� ������ ���� �Ѹ��� �����ϴ� �ڵ����.
        // �׷��ٸ�, ���࿡ �÷��̾ 3�� �ۿ� ���ٸ�?
        for (int i = 0; i < totalPlayers; i++)
        {
            Debug.Log($"Player{i}�� ī�� ��");

            // ���� �Ʒ� ������, ���� �� ���� ī�带 �ִ� �ڵ����.
            for (int j = 0; j < 4; j++)
            {
                gameManager.playerList[i].GetComponent<PlayerController>().cardList.Add(GiveACardToUsers());
            }
        }
        Debug.Log($"Remained Card :{card.Count}");
    }


    /// <summary>
    /// This Func exist for what i want to compare the one player with other player
    /// </summary>
    /// <param name="cardList">
    /// The cardList param's role is, get whole of submit cards from in this round.
    /// and, init getCardList as cardList param.
    /// #Critical : The cardList param must be pushed through the .Add() function sequentially, each time in the correct order.
    /// This is because we will be checking the order of the List in the CardCompare function below.
    /// </param>
    /// <param name="submitTime">
    /// The role of the submitTime parameter is to get the order of the player who submitted the card.
    /// The submitTime is compared to the getCardList List, which is sequentially added to the List, to determine who is the winner.
    /// </param>
    public bool CardCompare(List<int> cardList, int submitTime)
    {
        List<int> cards = new List<int>();
        //List<bool> isWin = new List<bool>();

        cards = cardList;
        int submitCard = cardList[submitTime];

        for (int i = 0; i < cards.Count; i++)
        {
            if (submitCard > cards[i]) continue;
            else if (submitCard == cards[i])
            {
                if (submitTime > i) return false;
                else if (submitTime == i) continue;
                else continue;
            }
            else return false;
        }
        return true;
    }
}

