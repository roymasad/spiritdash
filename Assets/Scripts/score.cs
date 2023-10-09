using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class score : MonoBehaviour {

	public int currentscore;

	private int oldscore;

	public Color scorecolor;

    public GameObject scoreshadower;

    public GameObject scorefloodmask;

    public GameObject maingame;

	// Use this for initialization
	void Start () {

		currentscore = 0;

		GetComponent<Text>().color = scorecolor;
        GetComponent<Text>().text = "0";
        scoreshadower.GetComponent<Text>().text = "0";

        //Debug.Log(PlayerPrefs.GetInt("Score").ToString());

        if (PlayerPrefs.HasKey("Score")) {

            GetComponent<Text>().text = PlayerPrefs.GetInt("Score").ToString();

            scoreshadower.GetComponent<Text>().text = PlayerPrefs.GetInt("Score").ToString();

        }
        

    }
	
	// Update is called once per frame
	void Update () {


        if (currentscore == oldscore) return;

		if (currentscore < oldscore) GetComponent<Text>().color = new Color(0.9f,0.3f,0.3f);
		else if (currentscore > oldscore) GetComponent<Text>().color = scorecolor;

        //GetComponent<Text>().text = ArabicFixer.Fix(currentscore.ToString(), true, true);

        Hashtable ht = new Hashtable();

        GetComponent<Text>().text = currentscore.ToString();

        ht.Add("y", 1.1f);
        ht.Add("x", 1.1f);
        ht.Add("time", 0.2f);
        //ht.Add("oncomplete", "WindupCallback");
        //ht.Add("delay",0);
        //ht.Add("onupdate","myUpdateFunction");
        ht.Add("easetype", iTween.EaseType.spring);
        ht.Add("looptype", iTween.LoopType.none);
        
        oldscore = currentscore;

        scoreshadower.GetComponent<Text>().text = GetComponent<Text>().text;

        if (iTween.Count(this.gameObject) > 0) return;

        iTween.ScaleFrom(this.gameObject, ht);
        iTween.ScaleFrom(scoreshadower, ht);

        if (currentscore > maingame.GetComponent<game>().scoreSwitchAt + maingame.GetComponent<game>().levelsize) return;

        Vector3 pos = new Vector3(scorefloodmask.transform.localPosition.x, (85 * (currentscore - maingame.GetComponent<game>().scoreSwitchAt) / maingame.GetComponent<game>().levelsize) - 73 , scorefloodmask.transform.localPosition.z);

        //Debug.Log(maingame.GetComponent<game>().levelsize);

        scorefloodmask.transform.localPosition = pos;
        

        

    }





    public void SetColor (Color col) {

		GetComponent<Text>().color = col;

		scorecolor = col;
		
	}

}
