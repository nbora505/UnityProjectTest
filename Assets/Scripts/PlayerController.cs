//using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

//[변수]
    public bool AIPlayer = false;
    public List<int> cardList;

    public List<bool> bombList = new List<bool>() {
        false, false, true };
    // 스트링에 고유 값인 0~2가 됐던 1~3이 됐던. 그 키값이 들어갑니다.
    // 1번 폭탄, 2번 폭탄, 3번 폭탄
    // bool -> 진짜냐 아니냐.
    // 1번 폭탄<- string, false <- bool = 1번 폭탄은 짜가.
    // 2번 폭탄 <- string, true <- bool = 2번 폭탄은 진짜다.

    //public List<string, string> bombList = new List<string, string>();
    public GameObject[] bombPrefab;
    public int remainingBomb = 3; //  int 남은 폭탄심지 수
    public int playerOrder =0; //    플레이어 순서별 가중치 0 to 3;
    public bool bombVisible =false;   
    public bool isDead =false;//플레이어의 사망

    //[함수]
    public void DeclareWins() //함수 승수 선언()
    {
        //    0~4 승수 선택용 숫자 등장();
        Invoke("TimeOut", 10f);
        //타임아웃 함수는 여기 캔슬은 버튼매니저에서
        
        // 버튼에서 vr object 찍을 수 있는 상태로 만들어 준 카드를 선택 only
        
    }//함수 끝

    /// <summary>
    /// 만약에, 플레이어 1이 카드 4를 냈다.
    /// 그리고 플레이어 1의 순서가 2번이다.
    /// 그러면 어떤 변수가 몇 개가 필요할까
    /// 라는 의문에서부터 시작이 되어야된다.
    /// 플레이어가 낸 카드에 대한 변수가 하나 있어야 한다.
    /// 플레이어 1의 순서가 2번이다. <- 플레이어가 몇 번 째 순서인지에 대한 변수가 또 하나 있어야 한다.
    /// 그럼 이걸 어디서부터 받아와야 할까
    /// 전역, 또는 멤버로 선언된 함수를 그대로 함수 내에서 차용한다.
    /// 또는 파라메타 값으로 가져온다.
    /// </summary>
    public int SubmitCard(int cardValue, int cardOrder)//함수 카드 제출(int 플레이어 순서별 가중치 0 to 3)
    {
        // cardvalue는 40 30 20 10 이런식이다.
        // realCardValue = cardvalue  + cardOrder;
        //카드 
        // 왜 playerOrder가 멤버 변수로 필요한가?
        // 뭐가 됐던. cardOrder <- 요 파라메터에 넣을 변수값이 필요하다.
        // 그렇기 때문에, Start()던, Update()던, 계속해서 변화할 수 있도록 만들어주는
        // 함수에서 저 값이 유동적으로 변화하면서 이 함수랑 커넥션이 있어야 한다.
        // playerOrder 변수가 cardOrder로 들어가는 것.
        // return 

        int realCardValue = cardValue + cardOrder;
        return realCardValue;
        
      
        //	만일 AIPlayer라면 AI 카드 제출 로직
       Invoke("TimeOut", 30f);
    }//    함수 끝

    public void SelectBomb()//함수 폭탄 선택(파라메타 없음)

    {
        bool bombVisible = true;

        AppearBomb(remainingBomb);

        // 예로 들면 이런게 되겠죠.
        // 함수 1의 파라메타 값이 float입니다.
        // 근데 제가 넣고 싶은 값은 int에요
        // 그럴 땐, (float) 변수 이런식으로 강제 형변환을 할 수 있겠죠

        // 함수에 자료형을 넣게 되는 경우는 무엇이 있는가?
        // 이건 단순히 강제로 자료형을 바꿀때만.
        //    심지 등장(남은 심지 수);//player에서 reamainedBomb 변수선언함
        //	만일 AIPlayer라면 int rnd = 3 to 10; Invoke("함수 폭탄 결정", rnd초);
        Invoke("DrawBomb", 30f);         //		Invoke("함수 폭탄 결정",30초);
        
    }   //함수 끝

    public void AppearBomb(int remainingBomb) //함수 심지 등장(int 심지 수)
    {

        //    if문으로 어떤 player 앞 심지 생성될지 위치 선정
        //  플레이어 선택 존중하려면 랜덤 배제해야 하나 편의상 선택과
        //  무관하게 랜덤으로 터지는 폭탄 생성
        
       
        
    }//함수 끝

    public void DrawBomb()//함수 폭탄 결정(파라메타 없음)

    {

        // 자 구조가 어떻게 될까.
        // 선택을 해요. 근데 우리가 랜덤하게 하나 결정하는 방식으로 하기로 했다.
        // 이렇게 얘기 오갔죠?
        // 그럼, 리스트의 카운트 만큼 랜덤하게 돌린 값.
        // Range(0, 리스트.카운트)
        // 그럼 그 값으로 결정된 폭탄 리스트를 불러옵니다.
        // 이때 벨류가 트루인지 펠스인지.
        // 만약 트루라면 즉사.

        int rnd = Random.Range(0, bombList.Count);

        bool istrueBomb = bombList[rnd]; // 딕셔너리가 필요한가? 필요없는것으로 판단
        // 참인지 아닌지 나오겠죠?

        if(istrueBomb)
        {
            // 사망처리
            bool isDead = true;
        }
        else
        {
            bombList.RemoveAt(rnd);
            // false가 지워지겠죠.
            // 생존처리
        }
        // 새로운 라운드가 진행이 된다.
        // 그렇게 되면, 폭탄의 리스트를 다시 초기화하면 되겠죠?
        // 어떻게? true 하나랑 false 2개로.


        // 폭탄이 3개인데. 진또배기는 하나다.
        // 그렇다면, 이거를 최대한 연산량을 줄이는 방법을 생각해야 한다.
        // 제가 생각하기에 가장 연산량을 줄이는 방법은
        // 처음부터. 진짜 폭탄 하나, 설정을 하고
        // 카드를 섞는 방식을 그대로 씁니다.

       
        //    GameManager.폭탄 비교(this.player, 랜덤);
    } //    함수 끝

    public void Timeout()//함수 타임 아웃(파라메타 없음)

    {        //    GameManager.벌칙 대상 리스트.Clear();
             //   벌칙대상 리스트는 스코어 매니저, 폭탄터지는 건 게임매니저
             //    GameManager.폭탄 비교(this.player, 랜덤);
    }   //    함수 끝


 }
