using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DistinguishScore(bool isWin, PlayerController player)
    {
        if (isWin)
            return;
        else
            player.remainedBomb--; // This is a temporary line put in for the current structure.
                                   // If a bomb selection line is ever created,
                                   // it should run the function, subtract the bomb, and so on.
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
            else if(submitCard == cards[i])
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
