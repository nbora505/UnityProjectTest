//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class AIPlayer : PlayerController
//{
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
//}