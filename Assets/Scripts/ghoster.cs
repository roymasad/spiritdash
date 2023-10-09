using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ghoster : MonoBehaviour {

    public GameObject spriteBox;

	// Use this for initialization
	void Start () {

        switchFade2();

    }

    void switchFade1()
    {

        Hashtable ht = new Hashtable();
        ht.Add("alpha", 1.0f);
        ht.Add("time", 1.0f);
        ht.Add("oncomplete", "switchFade2");
        ht.Add("onCompleteTarget", this.gameObject);
        ht.Add("easetype", iTween.EaseType.spring);
        ht.Add("looptype", iTween.LoopType.none);
        iTween.FadeTo(spriteBox, ht);



    }


    void switchFade2()
    {


        Hashtable ht = new Hashtable();
        ht.Add("alpha", 0.3f);
        ht.Add("time", 1.0f);
        ht.Add("oncomplete", "switchFade1");
        ht.Add("onCompleteTarget", this.gameObject);
        ht.Add("easetype", iTween.EaseType.spring);
        ht.Add("looptype", iTween.LoopType.none);
        iTween.FadeTo(spriteBox, ht);


    }

    // Update is called once per frame
    //void Update () {

        //Debug.Log(spriteBox.GetComponent<Renderer>().material.color.a);

	//}
}
