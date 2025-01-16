//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEngine.Rendering;
using static UnityEngine.UIElements.UxmlAttributeDescription;

public class AIPlayer : PlayerController
{
    List<float> eachOfSubmitedCardWeightList = new List<float>();
    Dictionary<int, float> eachOfAICardWeightList = new Dictionary<int, float>();
    //    public List<Card> handCards;// 자신의 카드 패
    //    public int expectedWin;// 예상 승리 설정


    //    public void DecideAction() //행동 결정
    //    {
    //        AnalyzeCard(); // 1. 카드 분석   
    //        SetExpectedWin(); // 2. 예상 승리 설정      
    //        ChooseCardForTurn(); // 3. 현재 턴에 낼 카드
    //    }
    //    public void Start()   
    //    {
    //        AnalyzeCard();   
    //    }

    /// <summary>
    /// #Critical: This function must be used after the DetermineWinningCard() function has been called. 
    /// If DetermineWinningCard() returns a value of 0, the AI has determined that it has no winning hand, and the AI should discard the least valuable card.
    /// </summary>
    /// <returns>The lowest-scoring card in AI hands</returns>
    public int ThrowGarbageCard()
    {
        int throwCard = cardList.Min();
        cardList.Remove(throwCard);

        return throwCard;
    }

    /// <summary>
    /// This function finds the weight of each previously submitted card, 
    /// determines the weight of the cards in the AI's own hand, 
    /// and compares them against those weights to come up with the smallest number of cards that it can play while still winning.
    /// The parameters submitOrder, alpha, beta, and gamma contained in the parameters below are the parameters that will go into the CalculateWeightsForCard_I() function, 
    /// and their descriptions are detailed in the CalculateWeightsForCard_I() function.
    /// So, if you've used the CalculateWeightsForCard_I() function before, 
    /// please use the same alpha, beta, and gamma weights that you used in CalculateWeightsForCard_I().
    /// </summary>
    /// <param name="submitCardList">
    /// #Critical: 
    /// List parameters for cards submitted to date. 
    /// This list must come from the Game Manager script, 
    /// and the corresponding list maintained by the Game Manager must be Add() to the list in the order of cards submitted,
    /// without sorting such as Sort(), etc.
    /// </param>
    /// <returns>
    /// In general, if the AI is in a situation where it can submit a winning card, it will return a value for that card. 
    /// However, if the AI is in a situation where it cannot win, it returns 0.
    /// </returns>
    public int DetermineWinningCard(List<int>submitCardList, float alpha, float beta, float gamma, int submitOrder)
    {
        eachOfAICardWeightList.Clear();
        eachOfSubmitedCardWeightList.Clear();

        List<int> target = new List<int>();

        for (int i = 0; i < submitCardList.Count; i++)
        {
            eachOfSubmitedCardWeightList.Add(CalculateWeightsForCard_I(submitCardList[i], alpha, beta, gamma, i));
        }
        
        float highWeight = eachOfSubmitedCardWeightList.Max();

        for (int j = 0; j < cardList.Count; j++)
        {
            eachOfAICardWeightList.Add(cardList[j], CalculateWeightsForCard_I(cardList[j], alpha, beta, gamma, submitOrder));
        }

        foreach(var dicItem in eachOfAICardWeightList)
        {
            if (dicItem.Value > highWeight)
                target.Add(dicItem.Key);
        }

        if (target.Count == 0)
        {
            UnityEngine.Debug.Log("현재 AI가 이길 수 있는 경우의 수가 없습니다.");
            return 0;
        }
        else
        {
            int chooseCard = target.Min();
            cardList.Remove(chooseCard);
            return chooseCard;
        }

    }

    /// <summary>
    /// A computational function for declaring its own victory that the AI should call before the game starts.
    /// Alpha and beta must never exceed 1.
    /// </summary>
    /// <param name="alpha">
    /// The parameter alpha is a weighting factor for whether the AI should focus more on the probability of losing for a single card in its hand.
    /// If you want the AI to focus more on the probability of losing than the value of the card, you can increase the value of the alpha.
    /// </param>
    /// <param name="beta">
    /// The parameter beta is a weighting factor to make the AI focus more on the value of the card. 
    /// If you want the AI to focus more on the value of the card than the chance of losing, you can increase this number.
    /// </param>
    public int CalculateOddsOfWinning(float alpha, float beta)
    {
        if (alpha + beta > 1)
        {
            UnityEngine.Debug.LogError($"현재 알파와 베타의 합이 1을 초과 했습니다. 알파와 베타의 합을 다시 확인해보시오.{alpha + beta}");
            return 0;
        }

        //// 1안
        //float calTotalWinningRate = 0;
        //for (int i = 0; i < cardList.Count; i++)
        //{
        //    calTotalWinningRate += CalculateWeightsForCard_I(cardList[i],alpha, beta, 0);
        //}

        //int finalTotalWin = (int)Math.Round(calTotalWinningRate); 

        //return finalTotalWin;

        // 2안

        int finalTotalWin = 0;

        for (int i = 0; i < cardList.Count; i++)
        {
            finalTotalWin += (int)Math.Round(CalculateWeightsForCard_I(cardList[i], alpha, beta, 0, submitTime));
        }

        return finalTotalWin;
    }

    /// <summary>
    /// Functions related to the AI's behavioral weight formula for Card I.
    /// The sum of alpha, beta, and gamma must not exceed 1. 
    /// Therefore, each number must be distributed within the number 1.
    /// </summary>
    /// <param name="card_I">
    /// The card_I parameter specifies the card i for which you want to calculate the AI's one of the card weight.
    /// The value of this parameter must contain only values from 1 to 4.
    /// </param>
    /// <param name="alpha">
    /// alpha is the behavioral weighting of the AI's win probability. 
    /// If you want the AI to focus more on win probability, you can set a higher parameter value for alpha.
    /// </param>
    /// <param name="beta">
    /// beta is the weighting for the value of the card. 
    /// If you want the AI to focus more on the value of the card, you can increase the beta value.
    /// </param>
    /// <param name="gamma">
    /// gamma is the AI's behavioral weighting of AI own card submission order. 
    /// If you want the AI to focus more on your card submission sequence, you can increase this number.
    /// </param>   
    /// <param name="submitOrder">
    /// This parameter is used to tell the function what the current AI's turn is. 
    /// Different turns will have different values for the turn weight, so it's important to be sure and accurate.
    /// The value should be in the range of 0 to 3.
    /// </param>
    public float CalculateWeightsForCard_I(int card_I, float alpha, float beta, float gamma, int submitOrder)
    {
        if (card_I > 4 || card_I < 1)
        {
            UnityEngine.Debug.LogError($"현재 파라메타로 들어온 카드의 값이 4를 초과하거나, 1 미만입니다. 카드의 값을 확인해보시오. {card_I}");
            return 0;
        }

        if (alpha + beta + gamma > 1)
        {
            UnityEngine.Debug.LogError($"알파, 베타, 감마의 총 합이 1을 넘어 섰습니다. 알파 베타 감마의 현재 총합 {alpha + beta + gamma}");
            return 0;
        }


        // #Critical: Specify that the base is the denominator.
        // The numerator is calculated by subtracting card_I after the denominator sigma calculation.
        float sigmaPlusMeterForCards = 0;

        if (card_I != 4)
        {
            for (int i = card_I; i <= 4; i++)
            {
                sigmaPlusMeterForCards += i;
            }
        }
        float losingProbability = (sigmaPlusMeterForCards - card_I) / sigmaPlusMeterForCards;

        float cardValueFormulasResult = card_I / 4;

        float orderWeight = 0;

        switch (submitOrder)
        {
            case 0: orderWeight = 1.0f;
                break;
            case 1: orderWeight = 0.75f; 
                break;
            case 2:
                orderWeight = 0.5f;
                break;
            case 3:
                orderWeight = 0.25f;
                break;
            default:
                UnityEngine.Debug.LogError($"순서 계산에 있어, 문제가 생겼습니다.{submitTime}을 확인해보십시오. submitTime은 반드시 0~3 사이의 숫자여야 합니다.");
                return 0;
        }

        // (1 - losingProbability) is the probability of winning.
        // cardValueFormulasResult means the result of the calculation for the value of the card.
        // orderWeight is the computational weight of the AI's own ordering.
        float result = alpha * (1 - losingProbability) +
            beta * cardValueFormulasResult +
            gamma * orderWeight;

        // return card_i's weight
        return result;
    }
}