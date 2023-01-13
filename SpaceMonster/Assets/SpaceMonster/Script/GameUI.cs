using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    
    public Text txtScore;
    public int totScore = 0;
   
   
    
    // Start is called before the first frame update
    void Start()
    {
        //처음 실행 후 저장된 스코어 정보로드
        // GetInt("TOT_SCORE") 이렇게 하면 되지만
        // 최초 저장 값이 없으니 기본값을 지정할 수 있다.
        totScore = PlayerPrefs.GetInt("TOT_SCORE", 0);
        DispScore(0);
        
      
    }

    public void DispScore(int score)
    {
       
        totScore += score;
        txtScore.text = "SCORE<color=##ff0000>" + "</color>" + totScore.ToString() ;

        PlayerPrefs.SetInt("TOT_SCORE", totScore);
    }

   


    // Update is called once per frame
    void Update()
    {
       
    }
}
