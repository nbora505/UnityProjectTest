using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public bool AIPlayer = false;
    public List<int> cardList;
    public GameObject[] bombPrefab;
    public int remainedBomb = 3;
    public int weighting = 0;
    public bool bombVisible = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}