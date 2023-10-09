using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Reflection;
//using UnityEditor;

public class game : MonoBehaviour {

	public GameObject[] spawners;

	public GameObject horse;

	public GameObject sun;

	public GameObject Panel;

	public GameObject inGamePanel;

	public GameObject maxscore;

	public GameObject score;

	public GameObject newtopscore;

	public GameObject currentscore;

	public bool doublejump;


	public GameObject background1;
	public GameObject ground;

	public GameObject pointlight;

	public GameObject sky;

    public Texture SkyTexture1;
    public Texture SkyTexture2;
    public Texture SkyTexture3;

    public Texture dunebackground;
	public Texture duneground;

    public Texture waterbackground;
    public Texture waterground;

    public Texture dunebackgroundnight;
	public Texture dunegroundnight;

	public Texture darkbackground;
	public Texture darkground;

	public string backgroundname;

	public ParticleSystem doublejumpParticle;

	public ParticleSystem transitionParticles;

	private float updatetimer;

	public GameObject tapicon;
    public GameObject intromsg1;
    public GameObject intromsg2;
    public GameObject intromsg3;

    public GameObject guiCoin1;
    public GameObject guiCoin2;

    public static bool paused;

	public static bool ended;

	public static bool musicON;

	public static bool sfxON;

	public static bool demomode = true;

    public bool matchStarted = false;


    public GameObject pausebutton;
    public GameObject messagebox;
    public GameObject messageboxParticles;

    public int lives;

    public GameObject heart1;
    public GameObject heart2;
    public GameObject heart3;

    public GameObject chakrabutton;
    public GameObject chakrabuttonClone;
    public GameObject chakra2button;

    public Sprite chakraTex1;
    public Sprite chakraTex2;
    public Sprite chakraTex3;
    public Sprite chakraTex4;
    public Sprite chakraTex5;

    public GameObject tap2;

    public string currentChakra;

    public bool triplejumpallowed;
    public bool triplejump;

    public GameObject multiplierGUI;
    public bool  multiplier;

    public Material coinMat;

    public GameObject progressbar1;

    public int level = 1;

    public int levelsize = 33;

    public bool lamp2ready = false;

    public int scoreSwitchAt =0;

    public GameObject lamppickup;

    public GameObject arrowicon;

    public GameObject boomParticles;

    public GameObject rain;

    public Mesh rainmesh;

    public GameObject starrain;

    public GameObject coinsbox;

    public static int allcoins = 0;

    public float extraCharkatime = 0;
    public int extraHeart = 0;


    // 0.01666666

    // Use this for initialization
    void Start () {


		Screen.sleepTimeout = SleepTimeout.NeverSleep;
        //0.01666
        Application.targetFrameRate = 60;

        //0.03333
        //Application.targetFrameRate = 30;

        if (!PlayerPrefs.HasKey("Score")) { PlayerPrefs.SetInt("Score", 0); PlayerPrefs.Save(); }

        if (!PlayerPrefs.HasKey("Coins")) { PlayerPrefs.SetInt("Coins", 0); PlayerPrefs.Save(); game.allcoins = 0; }
        else {


            game.allcoins = PlayerPrefs.GetInt("Coins");

            coinsbox.GetComponent<Text>().text = game.allcoins.ToString();

        }




        newtopscore.SetActive(false);

		Panel.SetActive(false);

		inGamePanel.SetActive(false);

		updatetimer = 1;

		paused = false;

		ended = false;

		int selection = Random.Range(1,3);

		if (selection == 1) setbackgroundDuneDay();
		//if (selection == 2) setbackgroundDuneNight();
		//if (selection == 3) setDarkground();
        if (selection == 2) setWaterground();
        //setWaterground();

        //AudioSettings.SetDSPBufferSize(256, 4);

        


		if (PlayerPrefs.GetInt("Music") == 1) game.musicON = true;
		else game.musicON = false;
		
		if (PlayerPrefs.GetInt("SFX") == 1) game.sfxON = true;
		else game.sfxON = false;

		mover.movingspeed = 0.85f; scroll.movingspeed = 0.85f;

        intromsg1.SetActive(true);
        intromsg2.SetActive(false);
        intromsg3.SetActive(false);
        //guiCoin1.SetActive(true);
        guiCoin2.SetActive(true);

        messagebox.SetActive(false);

        Hashtable ht = new Hashtable();


        ht.Add("y", 1.05f);
        ht.Add("x", 1.05f);
        ht.Add("time", 0.7f);

        ht.Add("easetype", iTween.EaseType.linear);
        ht.Add("looptype", iTween.LoopType.pingPong);
        iTween.ScaleFrom(pausebutton, ht);

        chakra2button.SetActive(false);

        currentChakra = "";

        tap2.SetActive(false);

        coinMat.color = new Color(1, 1, 1);

        progressbar1.SetActive(false);

        lamppickup.SetActive(false);

        //rain.GetComponent<ParticleSystem>().emissionRate = 20;
        //rain.GetComponent<ParticleSystemRenderer>().renderMode = ParticleSystemRenderMode.Mesh;

        //rain.GetComponent<ParticleSystemRenderer>().mesh = rainmesh;

        //if (demomode) horse.SetActive(false);
        //else tapicon.SetActive(false);

        //chakrabutton.SetActive(true);
        //chakrabuttonClone.SetActive(true);
        //PlayerPrefs.SetInt("Coins", 0);





    }

    public void PushMessage(string msg, Color col)
    {

        iTween.Stop(messagebox);

        messagebox.SetActive(true);
        messageboxParticles.GetComponent<ParticleSystem>().Play();

        messagebox.GetComponent<Text>().color = new Color(col.r, col.g, col.b, 0.8f);
        
        messagebox.GetComponent<Text>().text = msg;
        Hashtable ht = new Hashtable();


        if (iTween.Count(messagebox) == 0)
        {
            ht.Add("y", 0.4f);
            ht.Add("x", 0.4f);
            ht.Add("time", 0.3f);

            ht.Add("easetype", iTween.EaseType.spring);
            ht.Add("looptype", iTween.LoopType.none);
            iTween.ScaleFrom(messagebox, ht);
        }
        messagebox.GetComponent<Text>().CrossFadeAlpha(0.0f, 0.0f, false);
        messagebox.GetComponent<Text>().CrossFadeAlpha(1.0f, 0.3f, false);

        StartCoroutine("PopMessage", 0.8f);


    }

    IEnumerator PopMessage(float delay)
    {

        
        yield return new WaitForSeconds(delay);

        messagebox.GetComponent<Text>().CrossFadeAlpha(0.0f, 0.5f, false);

        StopCoroutine("PopMessage");

        StartCoroutine("PopMessage2", 0.5f);
    }

    IEnumerator PopMessage2(float delay)
    {
        yield return new WaitForSeconds(delay);

        messagebox.GetComponent<Text>().color = new Color(1f, 1f, 1f);
        messagebox.GetComponent<Text>().transform.localScale = new Vector3(1, 1, 1);
        messagebox.SetActive(false);

        StopCoroutine("PopMessage2");
    }

    public void pause(){

		paused = true;
		Time.timeScale = 0;
        //messagebox.SetActive(false);
    }


	public void unpause(){
		
		paused = false;
		Time.timeScale = 1;
	}

	public void ShowInGameMenu(){
		
		if (game.ended == true) return;
		pause();
		inGamePanel.SetActive(true);

        //tapicon.SetActive(false);
        //intromsg1.SetActive(false);
        //intromsg2.SetActive(false);
        //intromsg3.SetActive(false);

        //guiCoin2.SetActive(false);
        //messagebox.SetActive(false);

        //matchStarted = true;

    }

	public void HideInGameMenu(){

        unpause();
		inGamePanel.SetActive(false);
	}




	public void setbackgroundDuneNight() {

        //rain.GetComponent<ParticleSystem>().Stop();

        background1.GetComponent<Renderer>().material.mainTexture = dunebackgroundnight;
		
		ground.GetComponent<Renderer>().material.mainTexture = dunegroundnight;
		
		pointlight.SetActive(false);

        int skyBK = Random.Range(1, 3);

        if (skyBK == 1) sky.GetComponent<Renderer>().material.mainTexture = SkyTexture1;
        else if (skyBK == 2) sky.GetComponent<Renderer>().material.mainTexture = SkyTexture2;


        sky.SetActive(true);

		backgroundname = "dunenight";

		//currentscore.GetComponent<score>().SetColor(Color.white);

		transitionParticles.Play ();

		ground.GetComponent<Renderer>().material.SetFloat("_Shininess",0.2453521f);
	}


	public void setbackgroundDuneDay() {

        //rain.GetComponent<ParticleSystem>().Stop();

        background1.GetComponent<Renderer>().material.mainTexture = dunebackground;
		
		ground.GetComponent<Renderer>().material.mainTexture = duneground;
		
		pointlight.SetActive(true);

		pointlight.GetComponent<Light>().intensity = 0.85f;

		pointlight.GetComponent<Light>().color = Color.white;

		sky.SetActive(true);

		backgroundname = "duneday";

		//currentscore.GetComponent<score>().SetColor(new Color32(255, 255, 255, 255));

		transitionParticles.Play ();

		ground.GetComponent<Renderer>().material.SetFloat("_Shininess",0.2453521f);
		
		
	}

    public void setWaterground()
    {

        //rain.GetComponent<ParticleSystem>().Play();

        background1.GetComponent<Renderer>().material.mainTexture = waterbackground;

        ground.GetComponent<Renderer>().material.mainTexture = waterground;

        pointlight.SetActive(true);

        pointlight.GetComponent<Light>().intensity = 0.85f;

        pointlight.GetComponent<Light>().color = Color.white;

        int skyBK = Random.Range(1, 4);

        if (skyBK == 1) sky.GetComponent<Renderer>().material.mainTexture = SkyTexture1;
        else if (skyBK == 2) sky.GetComponent<Renderer>().material.mainTexture = SkyTexture2;
        else if (skyBK == 3) sky.GetComponent<Renderer>().material.mainTexture = SkyTexture3;

        sky.SetActive(true);

        backgroundname = "waterday";

        //currentscore.GetComponent<score>().SetColor(Color.white);

        transitionParticles.Play();

        ground.GetComponent<Renderer>().material.SetFloat("_Shininess", 0.4453521f);


    }


    public void setDarkground() {
		
		
		background1.GetComponent<Renderer>().material.mainTexture = darkbackground;
		
		ground.GetComponent<Renderer>().material.mainTexture = darkground;

        //rain.GetComponent<ParticleSystem>().Play();

        pointlight.SetActive(true);

        pointlight.GetComponent<Light>().intensity = 0.85f;

        pointlight.GetComponent<Light>().color = Color.white;

        //pointlight.light.intensity = 0.45f;

        //pointlight.light.color = Color.blue;

        pointlight.SetActive(false);

        int skyBK = Random.Range(1, 4);

        if (skyBK == 1) sky.GetComponent<Renderer>().material.mainTexture = SkyTexture1;
        else if (skyBK == 2) sky.GetComponent<Renderer>().material.mainTexture = SkyTexture2;
        else if (skyBK == 3) sky.GetComponent<Renderer>().material.mainTexture = SkyTexture3;

        sky.SetActive(true);
		
		backgroundname = "dark";
		
		//currentscore.GetComponent<score>().SetColor(Color.white);
		
		transitionParticles.Play ();		

		ground.GetComponent<Renderer>().material.SetFloat("_Shininess", 0.09f);

		
	}
	
	// Update is called once per frame
	void Update () {


        //Debug.Log(mover.movingspeed);



        if (Input.GetKeyDown("joystick button 0") && !(Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer))
        {

            chakraBtn();

        }


        if (matchStarted == false) return;


        if (Input.GetKeyDown("joystick button 7") && !(Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer))
        {

            //Debug.Log("tick");
            if (ended == true) return;
            if (paused == true) HideInGameMenu();
            else ShowInGameMenu();

        }

        if (paused == true) return;

        if (Input.GetKeyDown("joystick button 1") && !(Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer))
        {

            chakra2Btn();

        }


        StartCoroutine("Spawn", updatetimer);  
		
		StartCoroutine("Ticktock", 1.5); 


	
	}

	IEnumerator Spawn(float delay)  
	{

        

        yield return new WaitForSeconds(delay);  

		foreach (GameObject instance in spawners)
		{

			instance.GetComponent<spawner>().Spawn();

		}

		StopCoroutine("Spawn");  
	}

	IEnumerator Ticktock(float delay)  
	{
		
		yield return new WaitForSeconds(delay);

        //if (lamp2ready) currentscore.GetComponent<score>().currentscore -= 1;

        //if (currentscore.GetComponent<score>().currentscore < 0) currentscore.GetComponent<score>().currentscore = 0;

        if (updatetimer> 0.4f) updatetimer -= 0.01f;

		if (mover.movingspeed <= 1.3f) { mover.movingspeed += 0.005f; scroll.movingspeed += 0.005f; }

		//Debug.Log(scroll.movingspeed );
		//Debug.Log(mover.movingspeed + "-");

		//Debug.Log (updatetimer);
		
		StopCoroutine("Ticktock");  
		
	}

    IEnumerator HideTip(float delay)
    {

        
        yield return new WaitForSeconds(delay);


        intromsg1.SetActive(false);
        intromsg2.SetActive(false);
        intromsg3.SetActive(false);
        //guiCoin1.SetActive(false);
        guiCoin2.SetActive(false);

        //rain.GetComponent<ParticleSystemRenderer>().renderMode = ParticleSystemRenderMode.Billboard;

        //rain.GetComponent<ParticleSystem>().Play();


        starrain.GetComponent<ParticleSystem>().Stop();


        StopCoroutine("HideTip");
    }


    void disableChakra()
    {
        



        chakra2button.SetActive(false);


    }

    public void chakra2Btn()
    {

        if (paused == true || ended == true) return;

        if (currentChakra == "") return;

        tap2.SetActive(false);

        /*
        Hashtable ht = new Hashtable();
        ht.Add("y", 1.2f);
        ht.Add("x", 1.2f);
        ht.Add("time", 0.3f);
        ht.Add("oncomplete", "disableChakra");
        ht.Add("onCompleteTarget", this.gameObject);
        ht.Add("easetype", iTween.EaseType.spring);
        ht.Add("looptype", iTween.LoopType.none);
        iTween.ScaleFrom(chakra2button, ht);
        */

        chakra2button.SetActive(false);

        if (game.sfxON) AudioSource.PlayClipAtPoint(horse.GetComponent<horse>().sweep, horse.GetComponent<horse>().audiotarget.position, 0.3f);

        if (currentChakra == "crown")
        {

            horse.GetComponent<horse>().CrownChakra();


        }
        else if (currentChakra == "fire")
        {

            horse.GetComponent<horse>().FireChakra();


        }
        else if (currentChakra == "dark")
        {

            horse.GetComponent<horse>().DarkChakra();


        }
        else if (currentChakra == "ghost")
        {

            horse.GetComponent<horse>().GhostChakra();


        }

        currentChakra = "";

    }




    public void chakraBtn(){

        if (paused == true || ended == true) return;

        Hashtable ht;

        if (matchStarted == false) {
            ht = new Hashtable();

            tapicon.SetActive(false);
            intromsg1.SetActive(false);
            intromsg2.SetActive(true);
            //guiCoin1.SetActive(false);
            guiCoin2.SetActive(false);
            intromsg3.SetActive(true);

            

            ht.Add("y", 1.5f);
            ht.Add("x", 1.5f);
            ht.Add("time", 0.5f);

            ht.Add("easetype", iTween.EaseType.linear);
            ht.Add("looptype", iTween.LoopType.none);
            iTween.ScaleFrom(intromsg3, ht);

            intromsg3.GetComponent<Text>().CrossFadeAlpha(0f, 0f, false);

            intromsg3.GetComponent<Text>().CrossFadeAlpha(1.0f, 0.5f, false);

            intromsg2.GetComponent<Text>().CrossFadeAlpha(0f, 0f, false);

            intromsg2.GetComponent<Text>().CrossFadeAlpha(1.0f, 0.5f, false);

            //rain.GetComponent<ParticleSystemRenderer>().renderMode = ParticleSystemRenderMode.Billboard;

            //rain.GetComponent<ParticleSystem>().Stop();
            //rain.GetComponent<ParticleSystem>().emissionRate = 100;

            currentscore.GetComponent<Text>().text = "0";

            currentscore.GetComponent<score>().scoreshadower.GetComponent<Text>().text = "0";

            currentscore.GetComponent<score>().SetColor(Color.white);

            chakrabutton.SetActive(true);
            chakrabuttonClone.SetActive(true);


            StartCoroutine("HideTip", 3);

            
        }

        matchStarted = true;

        
        if (iTween.Count(chakrabutton) == 0)
        {
            ht = new Hashtable();
            ht.Add("y", 1.2f);
            ht.Add("x", 1.2f);
            ht.Add("time", 0.3f);

            ht.Add("easetype", iTween.EaseType.spring);
            ht.Add("looptype", iTween.LoopType.none);
            iTween.ScaleFrom(chakrabutton, ht);
            iTween.ScaleFrom(chakrabuttonClone, ht);
            
        }
        

        //if (demomode) { demomode = false; Application.LoadLevel("3"); }


        if (horse.GetComponent<horse>().state == "running") {

            //Debug.Log("running2");

            //if (horse.GetComponent<horse>().lastTouchedFloor.transform.position.y > horse.GetComponent<horse>().transform.position.y) return;

			horse.GetComponent<horse>().state = "jumping";

			horse.GetComponent<horse>().dust1.Stop();


			horse.GetComponent<Rigidbody>().velocity = Vector3.zero;
			horse.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;


			horse.GetComponent<ImageSequenceSingleTexture>().jumping = true;

			horse.transform.Rotate(new Vector3(0,-15,0));

			if (game.sfxON) AudioSource.PlayClipAtPoint(horse.GetComponent<horse>().jump, horse.GetComponent<horse>().audiotarget.position, 0.1f);


            //horse.GetComponent<Rigidbody>().AddForce (transform.up * 2000);
            horse.GetComponent<Rigidbody>().AddForce(transform.up * 2000);

            doublejump = false;
            triplejump = false;


        }

		else if (horse.GetComponent<horse>().state == "jumping" && doublejump == false) {


            //horse.GetComponent<Rigidbody>().AddForce (transform.up * 2300);
            horse.GetComponent<Rigidbody>().AddForce(transform.up * 2300);

            doublejump = true;
            triplejump = false;

            doublejumpParticle.Play();

			if (game.sfxON) AudioSource.PlayClipAtPoint(horse.GetComponent<horse>().jump, horse.GetComponent<horse>().audiotarget.position, 0.1f);
		}

        else if (horse.GetComponent<horse>().state == "jumping" && doublejump == true && triplejump == false && triplejumpallowed == true)
        {


            //horse.GetComponent<Rigidbody>().AddForce(transform.up * 1900);
            horse.GetComponent<Rigidbody>().AddForce(transform.up * 1900);

            triplejump = true;

            doublejumpParticle.Play();

            if (game.sfxON) AudioSource.PlayClipAtPoint(horse.GetComponent<horse>().jump, horse.GetComponent<horse>().audiotarget.position, 0.1f);
        }

        //ClearLog();
        //Debug.Log(horse.GetComponent<horse>().state);
        //Debug.Log(doublejump + " - " + triplejump);


    }

    public static void ClearLog()
    {
        /*
        var assembly = Assembly.GetAssembly(typeof(UnityEditor.ActiveEditorTracker));
        var type = assembly.GetType("UnityEditorInternal.LogEntries");
        var method = type.GetMethod("Clear");
        method.Invoke(new object(), null);
        */
    }



}
