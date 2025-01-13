using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    // It can be changed. this vars inited for test
    List<string> card = new List<string>();
    List<string> cardset = new List<string>(){
        "1", "1", "1", "1",
        "2", "2", "2", "2",
        "3", "3", "3", "3",
        "4", "4", "4", "4",
    };
    // Start is called before the first frame update
    void Start()
    {
        // if player play multi, use this line.
        //if (PhotonNetwork.isMasterClient)
        //{
        //    DoCardShuffle();
        //    TestUserCard();
        //}

        // else, player play single, use this line

        DoCardShuffle();
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
            Debug.Log(card[i]);
        }
    }

    public string GiveACardToUsers()
    {
        string aCard = card[0];
        card.RemoveAt(0);

        return aCard;
    }

    public void TestUserCard(int totalPlayers)
    {
        // ���� �Ʒ� ������ ���� �Ѹ��� �����ϴ� �ڵ����.
        // �׷��ٸ�, ���࿡ �÷��̾ 3�� �ۿ� ���ٸ�?
        for (int i = 0; i < totalPlayers; i++)
        {
            Debug.Log($"Player{i} --------------------------");
            // ���� �Ʒ� ������, ���� �� ���� ī�带 �ִ� �ڵ����.
            for (int j = 0; j < 4; j++)
            {
                Debug.Log(GiveACardToUsers());
            }
        }
        Debug.Log($"Remained Card :{card.Count}");
    }
}
