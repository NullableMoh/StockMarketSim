using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StockPricePlayerCashMiddleMan : MonoBehaviour
{
	StockPrice stockPrice;
	PlayerCash playerCash;
    // Start is called before the first frame update
    void Start()
    {
	    stockPrice = GetComponentInParent<StockPrice>();
	    playerCash = FindObjectOfType<PlayerCash>();
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
