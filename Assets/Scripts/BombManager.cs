using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombManager : MonoBehaviour
{
    //�� �༮�� �� ��° ��ź����
    public int bombNum;
    GameManager gameManager;

    void Start()
    {
        gameManager = GameObject.Find("GameObject").GetComponent<GameManager>();

        //���� �÷��̾ �� �༮�� �����Ѵٸ� gameManager�� selectedBomb�� �� �༮ ��ȣ �־��ֱ�.
        //�ӽ÷� start�� ����־��
        gameManager.selectedBomb = bombNum;
    }

    void Update()
    {
        
    }
}
