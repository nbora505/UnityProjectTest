//using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    //[����]
    public bool AIPlayer = false;
    public List<int> cardList;

    public List<bool> bombList = new List<bool>() {
        false, false, true };
    public GameObject[] bombPrefab;
    public int remainingBomb = 3; //  int ���� ��ź���� ��
    //public int playerOrder = 0; //    �÷��̾� ������ ����ġ 0 to 3;
    public int submitTime = 0; // ���࿡, ������ �������̽�Ʈ�� ī�带 �����ϰ� �������� ��, �׶��� ������ ������ �� ���ΰ��� �������� �ʱ�ȭ
    public bool bombVisible = false;
    public bool isDead = false;//�÷��̾��� ���
    public GameManager gm;

    private void Update()
    {
        if(isDead) return;

        // �����̳�, �ٸ� ���� Ʈ��ŷ�� ���ϰ� �Ϸ���
        // �츮 ������ ���� Ʈ��ŷ ���� ������Ʈ�� ����
        // �װ� disable �ϸ� �ǵ����.
        // ��θ� ���� �� �Ͼ.

        // -> ���� �޴������� ���� üũ�ϰ� ���� ��Ʈ�� �ϴ�.
        // -> ���� �޴������� �� ������ �����ϱ�, 
        // -> Enum�� �Ἥ ����ġ���� ���� ���� ��.


        // ������ Ʈ���Ÿ� ������ ��, ī�带 ������ �� �־�� �ϰ�.
        // �޼� Ʈ���Ÿ� ������ ��, �� �и� �� �� �־�� ����?
    }
    // ī�� ����� ī�� Ȯ���� �ʿ��ϴ�.

    //[�Լ�]
    public void DeclareWins() //�Լ� �¼� ����()
    {
        //    0~4 �¼� ���ÿ� ���� ����();
        Invoke("TimeOut", 10f);
        //Ÿ�Ӿƿ� �Լ��� ���� ĵ���� ��ư�Ŵ�������

        // ��ư���� vr object ���� �� �ִ� ���·� ����� �� ī�带 ���� only

    }//�Լ� ��

    public void AppearBomb(int remainingBomb) //�Լ� ���� ����(int ���� ��)
    {
        for (int i = 0; i < remainingBomb; i++)
        {
            gameObject.GetComponentInChildren<GameObject>().SetActive(true);
        }

        //  �÷��̾� ���� �����Ϸ��� ���� �����ؾ� �ϳ� ���ǻ� ���ð�
        //  �����ϰ� �������� ������ ��ź ����



    }//�Լ� ��

    public void DrawBomb()//�Լ� ��ź ����(�Ķ��Ÿ ����)
    {
        int rnd = Random.Range(0, bombList.Count);
        bool istrueBomb = bombList[rnd]; // ��ųʸ��� �ʿ��Ѱ�? �ʿ���°����� �Ǵ�

        if (istrueBomb)
        {
            gm.deadList.Add(this.gameObject);
            gm.penaltyList.Remove(this.gameObject);
            // ���ó��
            isDead = true;
        }
        else
        {
            remainingBomb--;
            gm.penaltyList.Remove(this.gameObject);
            bombList.RemoveAt(rnd);
            // false�� ����������.
            // ����ó��
        }
    } //    �Լ� ��
}
