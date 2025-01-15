//using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

//[����]
    public bool AIPlayer = false;
    public List<int> cardList;

    public List<bool> bombList = new List<bool>() {
        false, false, true };
    // ��Ʈ���� ���� ���� 0~2�� �ƴ� 1~3�� �ƴ�. �� Ű���� ���ϴ�.
    // 1�� ��ź, 2�� ��ź, 3�� ��ź
    // bool -> ��¥�� �ƴϳ�.
    // 1�� ��ź<- string, false <- bool = 1�� ��ź�� ¥��.
    // 2�� ��ź <- string, true <- bool = 2�� ��ź�� ��¥��.

    //public List<string, string> bombList = new List<string, string>();
    public GameObject[] bombPrefab;
    public int remainingBomb = 3; //  int ���� ��ź���� ��
    public int playerOrder =0; //    �÷��̾� ������ ����ġ 0 to 3;
    public bool bombVisible =false;   
    public bool isDead =false;//�÷��̾��� ���

    //[�Լ�]
    public void DeclareWins() //�Լ� �¼� ����()
    {
        //    0~4 �¼� ���ÿ� ���� ����();
        Invoke("TimeOut", 10f);
        //Ÿ�Ӿƿ� �Լ��� ���� ĵ���� ��ư�Ŵ�������
        
        // ��ư���� vr object ���� �� �ִ� ���·� ����� �� ī�带 ���� only
        
    }//�Լ� ��

    /// <summary>
    /// ���࿡, �÷��̾� 1�� ī�� 4�� �´�.
    /// �׸��� �÷��̾� 1�� ������ 2���̴�.
    /// �׷��� � ������ �� ���� �ʿ��ұ�
    /// ��� �ǹ��������� ������ �Ǿ�ߵȴ�.
    /// �÷��̾ �� ī�忡 ���� ������ �ϳ� �־�� �Ѵ�.
    /// �÷��̾� 1�� ������ 2���̴�. <- �÷��̾ �� �� ° ���������� ���� ������ �� �ϳ� �־�� �Ѵ�.
    /// �׷� �̰� ��𼭺��� �޾ƿ;� �ұ�
    /// ����, �Ǵ� ����� ����� �Լ��� �״�� �Լ� ������ �����Ѵ�.
    /// �Ǵ� �Ķ��Ÿ ������ �����´�.
    /// </summary>
    public int SubmitCard(int cardValue, int cardOrder)//�Լ� ī�� ����(int �÷��̾� ������ ����ġ 0 to 3)
    {
        // cardvalue�� 40 30 20 10 �̷����̴�.
        // realCardValue = cardvalue  + cardOrder;
        //ī�� 
        // �� playerOrder�� ��� ������ �ʿ��Ѱ�?
        // ���� �ƴ�. cardOrder <- �� �Ķ���Ϳ� ���� �������� �ʿ��ϴ�.
        // �׷��� ������, Start()��, Update()��, ����ؼ� ��ȭ�� �� �ֵ��� ������ִ�
        // �Լ����� �� ���� ���������� ��ȭ�ϸ鼭 �� �Լ��� Ŀ�ؼ��� �־�� �Ѵ�.
        // playerOrder ������ cardOrder�� ���� ��.
        // return 

        int realCardValue = cardValue + cardOrder;
        return realCardValue;
        
      
        //	���� AIPlayer��� AI ī�� ���� ����
       Invoke("TimeOut", 30f);
    }//    �Լ� ��

    public void SelectBomb()//�Լ� ��ź ����(�Ķ��Ÿ ����)

    {
        bool bombVisible = true;

        AppearBomb(remainingBomb);

        // ���� ��� �̷��� �ǰ���.
        // �Լ� 1�� �Ķ��Ÿ ���� float�Դϴ�.
        // �ٵ� ���� �ְ� ���� ���� int����
        // �׷� ��, (float) ���� �̷������� ���� ����ȯ�� �� �� �ְ���

        // �Լ��� �ڷ����� �ְ� �Ǵ� ���� ������ �ִ°�?
        // �̰� �ܼ��� ������ �ڷ����� �ٲܶ���.
        //    ���� ����(���� ���� ��);//player���� reamainedBomb ����������
        //	���� AIPlayer��� int rnd = 3 to 10; Invoke("�Լ� ��ź ����", rnd��);
        Invoke("DrawBomb", 30f);         //		Invoke("�Լ� ��ź ����",30��);
        
    }   //�Լ� ��

    public void AppearBomb(int remainingBomb) //�Լ� ���� ����(int ���� ��)
    {

        //    if������ � player �� ���� �������� ��ġ ����
        //  �÷��̾� ���� �����Ϸ��� ���� �����ؾ� �ϳ� ���ǻ� ���ð�
        //  �����ϰ� �������� ������ ��ź ����
        
       
        
    }//�Լ� ��

    public void DrawBomb()//�Լ� ��ź ����(�Ķ��Ÿ ����)

    {

        // �� ������ ��� �ɱ�.
        // ������ �ؿ�. �ٵ� �츮�� �����ϰ� �ϳ� �����ϴ� ������� �ϱ�� �ߴ�.
        // �̷��� ��� ������?
        // �׷�, ����Ʈ�� ī��Ʈ ��ŭ �����ϰ� ���� ��.
        // Range(0, ����Ʈ.ī��Ʈ)
        // �׷� �� ������ ������ ��ź ����Ʈ�� �ҷ��ɴϴ�.
        // �̶� ������ Ʈ������ �罺����.
        // ���� Ʈ���� ���.

        int rnd = Random.Range(0, bombList.Count);

        bool istrueBomb = bombList[rnd]; // ��ųʸ��� �ʿ��Ѱ�? �ʿ���°����� �Ǵ�
        // ������ �ƴ��� ��������?

        if(istrueBomb)
        {
            // ���ó��
            bool isDead = true;
        }
        else
        {
            bombList.RemoveAt(rnd);
            // false�� ����������.
            // ����ó��
        }
        // ���ο� ���尡 ������ �ȴ�.
        // �׷��� �Ǹ�, ��ź�� ����Ʈ�� �ٽ� �ʱ�ȭ�ϸ� �ǰ���?
        // ���? true �ϳ��� false 2����.


        // ��ź�� 3���ε�. ���ǹ��� �ϳ���.
        // �׷��ٸ�, �̰Ÿ� �ִ��� ���귮�� ���̴� ����� �����ؾ� �Ѵ�.
        // ���� �����ϱ⿡ ���� ���귮�� ���̴� �����
        // ó������. ��¥ ��ź �ϳ�, ������ �ϰ�
        // ī�带 ���� ����� �״�� ���ϴ�.

       
        //    GameManager.��ź ��(this.player, ����);
    } //    �Լ� ��

    public void Timeout()//�Լ� Ÿ�� �ƿ�(�Ķ��Ÿ ����)

    {        //    GameManager.��Ģ ��� ����Ʈ.Clear();
             //   ��Ģ��� ����Ʈ�� ���ھ� �Ŵ���, ��ź������ �� ���ӸŴ���
             //    GameManager.��ź ��(this.player, ����);
    }   //    �Լ� ��


 }
