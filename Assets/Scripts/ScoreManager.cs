using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    GameManager gm;
    public PlayerController pc;
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
    public IEnumerator CheckBomb(int player)
    {
        //��Ģ ������� �����ִ� ��ź�� Ȱ��ȭ��Ű��
        for (int i = 0; i < gm.playerList[player].GetComponent<PlayerController>().remainingBomb; i++)
        {
            gm.playerList[player].GetComponent<PlayerController>().bombPrefab[i].SetActive(true);
        }

        //�����ִ� ���� ����ŭ ���� ������ ��÷ ���� �����ϱ�
        // pc.DrawBomb()�� ��ź�� �������� ��, ����Ǿ�� ��.
        //pc.DrawBomb();


        //int realBomb = Random.Range(0, gm.playerList[player].GetComponent<PlayerController>().remainingBomb);

        //������ ����������� ��ٸ���(�⺻���� -1)
        yield return new WaitUntil(() => gm.selectedBomb >= 0);

        //pc.DrawBomb();

    }
    public IEnumerator Timeout()//�Լ� Ÿ�� �ƿ�(�Ķ��Ÿ ����)
    {        //    GameManager.��Ģ ��� ����Ʈ.Clear();
             //   ��Ģ��� ����Ʈ�� ���ھ� �Ŵ���, ��ź������ �� ���ӸŴ���
             //    GameManager.��ź ��(this.player, ����);

        yield return new WaitForSeconds(10);

        if (!isSelect)
        {
            pc.DrawBomb();
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
