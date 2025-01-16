//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

using System.Data.Common;
using System.Diagnostics;

public class AIPlayer : PlayerController
{
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
    //    public void AnalyzeCard() //카드분석 , 자신의 카드 패를 분석하여 가능한 조합과 가치 분석
    //    {
    //        handCards = new List<Card>();

    //        for(int i=0; i < 4; i++)
    //        {
    //            Card card = new Card();
    //            handCards.Add(card);
    //        }
    //    }

    //    public void SetExpectedWin() //예상승리 설정
    //    {
    //        // 자신의 패와 순서를 인원수를 고려하여 예상 승리 설정
    //    }

    //    public void ChooseCardForTurn() // 현재 턴에 낼 카드
    //    {
    //        // 자신의 카드,승수,제출된 카드를 고려
    //    }

    //    public void AnalyzeResult() // 결과 분석, 이 부분은 안쓰일지도
    //    {
    //        // 게임 결과를 평가하고 예상 승리와 비교
    //    }

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
    /// <returns></returns>
    public float CalculateWeightsForCard_I(int card_I, float alpha, float beta, float gamma)
    {
        // #Critical: Specify that the base is the denominator.
        // The numerator is calculated by subtracting card_I after the denominator sigma calculation.
        float sigmaMeterForCards = 0;

        if (card_I != 4)
        {
            for (int i = card_I; i <= 4; i++)
            {
                sigmaMeterForCards += i;
            }
        }
        float lossingProbability = (sigmaMeterForCards - card_I) / sigmaMeterForCards;

        float cardValueFormulasResult = card_I / 4;

        float orderWeight = 0;

        switch (submitTime)
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
                break;
        }

        // (1 - losingProbability) is the probability of winning.
        // cardValueFormulasResult means the result of the calculation for the value of the card.
        // orderWeight is the computational weight of the AI's own ordering.
        float result = alpha * (1 - lossingProbability) +
            beta * cardValueFormulasResult +
            gamma * orderWeight;

        return result;
    }
}