using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    GameManager gm;
    PlayerController pc;
    bool isSelect = false;
    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public IEnumerator CheckBomb(PlayerController player)
    {
        //��Ģ ������� �����ִ� ��ź�� Ȱ��ȭ��Ű��
        for (int i = 0; i < player.remainingBomb; i++)
        {
            player.bombPrefab[i].SetActive(true);
        }

        // player.���� �ִ� ��ź�� Ȱ��ȭ ��Ű�� �Լ�();
        // -> �̷��� �Ǹ� �Ѹ� ���� �Լ��� �ٲ�� ��.
        // -> ���� �޴������� for�� ������ �ش��ϴ� ��� �÷��̾�� �� �Լ� ����Ų��.

        //�����ִ� ���� ����ŭ ���� ������ ��÷ ���� �����ϱ�
        int realBomb = Random.Range(0, player.remainingBomb);

        //������ ����������� ��ٸ���(�⺻���� -1)
        yield return new WaitUntil(() => gm.selectedBomb >= 0);

        // pc.DrawBomb()�� ��ź�� �������� ��, ����Ǿ�� ��.
        player.DrawBomb(gm.selectedBomb);

    }
    public IEnumerator Timeout(PlayerController player)//�Լ� Ÿ�� �ƿ�(�Ķ��Ÿ ����)
    {        //    GameManager.��Ģ ��� ����Ʈ.Clear();
             //   ��Ģ��� ����Ʈ�� ���ھ� �Ŵ���, ��ź������ �� ���ӸŴ���
             //    GameManager.��ź ��(this.player, ����);

        yield return new WaitForSeconds(10);

        if (!isSelect)
        {
            player.DrawBomb(0);//������ �������� ���� ���� �ʿ�
        }
        else
        {
            yield return null;
        }
    }   //    �Լ� ��

    public void DistinguishScore(PlayerController player)
    {
            player.remainingBomb--; // This is a temporary line put in for the current structure.
                                   // If a bomb selection line is ever created,
                                   // it should run the function, subtract the bomb, and so on.
    }
}
