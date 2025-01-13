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
        //[important]totalPlayer 변수는 게임매니저에서 가져올 예정

        // 만약에 이 테스트 유저 카드함수를 쓰고 싶다.
        // 그러면 게임 메니저는 토탈 플레이어의 수에 대한 변수를 가지고 있지 않을까요?
        // 그렇다면, 게임 메니저 코드라인에서
        // CardManager.TestUserCard(totalPlayers)
        // 이런식으로 쓰고, 왜 제가 함수별로 나누고, 또 클래스별로 나눠야 하는가에 대한 강조가
        // 여기서 이런식으로 재사용하고 서로 커낵션하기 쉬워지기 때문에, 이를 강조한 것이다.
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

        // 카드셋이 시작은 16.
        // 근데 하나가 빠지게 되면 카운트는 15
        // 또 하나 또 빠지게 되면 카운트 14가 되겠죠
        // 사실 i < cardset.Count 이렇게 해도 문제가 안되는데
        // 직관적으로 알려드리고 적기 위해서 max 변수에다가 넣어서 보여준거.
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
        // 이쪽 아래 포문이 유저 한명을 지목하는 코드라인.
        // 그렇다면, 만약에 플레이어가 3명 밖에 없다면?
        for (int i = 0; i < totalPlayers; i++)
        {
            Debug.Log($"Player{i} --------------------------");
            // 이쪽 아래 포문이, 유저 한 명에게 카드를 주는 코드라인.
            for (int j = 0; j < 4; j++)
            {
                Debug.Log(GiveACardToUsers());
            }
        }
        Debug.Log($"Remained Card :{card.Count}");
    }
}
