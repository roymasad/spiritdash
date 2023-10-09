using UnityEngine;
using System.Collections;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;
#if UNITY_ANDROID || UNITY_IOS
using UnityEngine.Advertisements;
#endif

public class menu1 : MonoBehaviour {


    public GameObject camera;

    public GameObject maingame;

    public GameObject scoreIn;
    public GameObject scoreOut;

    public GameObject doublecoinsButton;

    public GameObject bonusCoinsScore;

    public GameObject creditPanel;


	public GameObject toggleMusicObj;
	public GameObject toggleSFXObj;

    public GameObject chakra;

    public GameObject lifeCheckSprite;
    public GameObject lifeParticle;
    public GameObject lifeObject;
    public GameObject lifeObjectRef;

    public GameObject chakraCheckSprite;
    public GameObject chakraParticle;

    public GameObject tapCheckSprite;
    public GameObject tapParticle;

    public GameObject coinsParticle;

    public GameObject storeMessage;

    public GameObject powerupStar;

    public AudioClip coinSplah;

    public GameObject loginToggle;

    Color backupmessageboxcolor;
    Color backupmessageoutlinecolor;

    int lifecost = 300;
    int chakracost = 600;
    int tapcost = 900;

    public string gameIdAndroid;
    public string gameIdIphone;
    public string gameId; // Set this value from the inspector.
    public bool enableTestMode = true;

    // Use this for initialization
    void Start () {

		if (!PlayerPrefs.HasKey("Music")) { PlayerPrefs.SetInt("Music", 1); PlayerPrefs.Save(); }
		if (!PlayerPrefs.HasKey("SFX")) { PlayerPrefs.SetInt("SFX", 1); PlayerPrefs.Save(); }
        if (!PlayerPrefs.HasKey("Social")) { PlayerPrefs.SetInt("Social", 1); PlayerPrefs.Save(); }

        if (PlayerPrefs.GetInt("Music") == 1) {toggleMusicObj.GetComponent<Toggle>().isOn = true; game.musicON = true;}
		else {toggleMusicObj.GetComponent<Toggle>().isOn = false; ; game.musicON = false;}
		
		if (PlayerPrefs.GetInt("SFX") == 1) {toggleSFXObj.GetComponent<Toggle>().isOn = true; ; game.sfxON = true;}
		else {toggleSFXObj.GetComponent<Toggle>().isOn = false; game.sfxON = false;}


        if (PlayerPrefs.GetInt("Social") == 1) { loginToggle.GetComponent<Toggle>().isOn = true; SocialLogin0(); }
        else loginToggle.GetComponent<Toggle>().isOn = false;


#if UNITY_ANDROID
        gameId = gameIdAndroid;
#elif UNITY_IOS
        gameId = gameIdIphone;
#endif

#if UNITY_ANDROID || UNITY_IOS
        if (Advertisement.isSupported) { // If runtime platform is supported...
            Advertisement.Initialize(gameId, enableTestMode); // ...initialize.
        }
#endif


        

        //((PlayGamesPlatform)Social.Active).SignOut();

        //closeCreditPanel();
        //closeOptionPanel();


    }

    void Awake ()
    {
        backupmessageboxcolor = storeMessage.GetComponent<Text>().color;
        backupmessageoutlinecolor = storeMessage.GetComponent<Outline>().effectColor;


    }
	// Update is called once per frame
	void Update () {

        //if (Input.GetKeyDown("joystick button 7")) LoadLevel1Button();
        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();

    }

	public void LoadLevel1Button()
	{
		Time.timeScale = 1.0f;

        Application.LoadLevel("main");


    }


    public void rateUs()
    {

#if UNITY_ANDROID 
        Application.OpenURL("market://details?id=com.crankypixel.spiritdash");
#elif UNITY_IOS
        Application.OpenURL("itms-apps://itunes.apple.com/app/id1209659096?ls=1&mt=8");
#endif



    }


    public void SocialLogin0()
    {
#if UNITY_ANDROID

        if (!Social.localUser.authenticated)
        {
            PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder()
            // enables saving game progress.
            //.EnableSavedGames()
            .Build();

            PlayGamesPlatform.InitializeInstance(config);
            // recommended for debugging:
            PlayGamesPlatform.DebugLogEnabled = true;


            PlayGamesPlatform.Activate();


            Social.localUser.Authenticate((bool success) =>
            {
                    PlayerPrefs.SetInt("Social", 1);

                    loginToggle.GetComponent<Toggle>().isOn = true;

            });

        }

#endif
    }

    public void SocialLogin()
    {
        if (loginToggle.GetComponent<Toggle>().isOn) { loginToggle.GetComponent<Toggle>().isOn = false; PlayerPrefs.SetInt("Social", 0); }
        else { loginToggle.GetComponent<Toggle>().isOn = true; PlayerPrefs.SetInt("Social", 1); }

        PlayerPrefs.Save();

        if (loginToggle.GetComponent<Toggle>().isOn)
        {
            SocialLogin0();

        }

    }


    public void QuitMe () {

#if UNITY_ANDROID 
                     //((PlayGamesPlatform)Social.Active).SignOut();

#endif


        Application.Quit();

	}

	public void showLeaderboards(){
        //Debug.Log("leaderboards - " + Social.localUser.authenticated);
        //((PlayGamesPlatform)Social.Active).ShowLeaderboardUI(SpiritRun.GPGSIds.leaderboard_topscore);
#if UNITY_ANDROID 
        if (!Social.localUser.authenticated) SocialLogin0();

        if (Social.localUser.authenticated) ((PlayGamesPlatform)Social.Active).ShowLeaderboardUI(SpiritDash.GPGSIds.leaderboard_topscore);   
    
#endif

    }

    public void showAchievements()
    {

#if UNITY_ANDROID || UNITY_IOS
        if (!Social.localUser.authenticated) SocialLogin0();

        if (Social.localUser.authenticated) ((PlayGamesPlatform)Social.Active).ShowAchievementsUI();
#endif

    }




    public void closeCreditPanel() {


		//Vector3 reset = new Vector3(0.01f,0.01f,1.0f);

		//if (creditPanel.activeSelf) creditPanel.transform.localScale = reset;

		creditPanel.SetActive(false);

	}

	public void openCreditPanel() {
		
		//closeOptionPanel();
		creditPanel.SetActive(true);
        /*
		Hashtable ht = new Hashtable();

		ht.Add("y",0.94f);
		ht.Add("x",0.8f);
		ht.Add("time",0.5f);
		//ht.Add("delay",0);
		//ht.Add("onupdate","myUpdateFunction");
		ht.Add("easetype",iTween.EaseType.spring);
		ht.Add("looptype",iTween.LoopType.none);

		iTween.ScaleTo(creditPanel,ht);
        */
		
	}


	
	public void toggleMusic() {


		if (toggleMusicObj.GetComponent<Toggle>().isOn) {
		
			game.musicON = true;

            camera.GetComponent<AudioSource>().Play();

            PlayerPrefs.SetInt("Music", 1); PlayerPrefs.Save();
		}
		else 
		{

			game.musicON = false;

			PlayerPrefs.SetInt("Music", 0); PlayerPrefs.Save();

            camera.GetComponent<AudioSource>().Pause();


        }



	}


	public void toggleSFX() {
		
		if (toggleSFXObj.GetComponent<Toggle>().isOn) {
			
			game.sfxON = true;
			
			PlayerPrefs.SetInt("SFX", 1); PlayerPrefs.Save();
		}
		else 
		{
			
			game.sfxON = false;
			
			PlayerPrefs.SetInt("SFX", 0); PlayerPrefs.Save();
		}

		
	}


    public void lifeButton()
    {
        if (game.paused == true) return;

        //if (true) {
        if (game.allcoins >= lifecost && maingame.GetComponent<game>().extraHeart == 0)
        {
            if (game.sfxON) AudioSource.PlayClipAtPoint(coinSplah, camera.transform.position, 0.5f);

            lifeObject.SetActive(false);
            lifeCheckSprite.SetActive(true);
            lifeParticle.GetComponent<ParticleSystem>().Play();
            coinsParticle.GetComponent<ParticleSystem>().Play();

            powerupStar.SetActive(true);

            storeMessage.SetActive(true);
            storeMessage.GetComponent<Text>().CrossFadeAlpha(0.0f, 0.0f, false);
            storeMessage.GetComponent<Text>().CrossFadeAlpha(1.0f, 0.3f, false);
            storeMessage.GetComponent<Text>().color = backupmessageboxcolor;
            storeMessage.GetComponent<Outline>().effectColor = backupmessageoutlinecolor;
            storeMessage.GetComponent<Text>().text = "MAX LIFE BOOST !!";

            maingame.GetComponent<game>().extraHeart = 1;
            maingame.GetComponent<game>().lives = 3;

            game.allcoins -= lifecost;

            if (game.allcoins < 0) game.allcoins = 0;

            maingame.GetComponent<game>().coinsbox.GetComponent<Text>().text = game.allcoins.ToString();

            maingame.GetComponent<game>().heart3.SetActive(true);

            maingame.GetComponent<game>().heart3.transform.localRotation = lifeObjectRef.transform.localRotation;

            if (PlayerPrefs.HasKey("Coins")) { PlayerPrefs.SetInt("Coins", game.allcoins); PlayerPrefs.Save(); }

            StartCoroutine("hideMessage");

            checkAll3Powerups();

        }
        else
        {

            storeMessage.SetActive(true);
            storeMessage.GetComponent<Text>().CrossFadeAlpha(0.0f, 0.0f, false);
            storeMessage.GetComponent<Text>().CrossFadeAlpha(1.0f, 0.3f, false);

            storeMessage.GetComponent<Text>().color = Color.red;
            storeMessage.GetComponent<Outline>().effectColor = Color.white;
            storeMessage.GetComponent<Text>().text = "NOT ENOUGH COINS!!";

            StartCoroutine("hideMessage");


        }


    }


    public void chakraButton()
    {
        if (game.paused == true) return;

        //if (true){
        if (game.allcoins >= chakracost && maingame.GetComponent<game>().extraCharkatime == 0)
        {

            if (game.sfxON) AudioSource.PlayClipAtPoint(coinSplah, camera.transform.position, 0.5f);


            chakraCheckSprite.SetActive(true);
            chakraParticle.GetComponent<ParticleSystem>().Play();
            coinsParticle.GetComponent<ParticleSystem>().Play();

            powerupStar.SetActive(true);


            storeMessage.SetActive(true);
            storeMessage.GetComponent<Text>().CrossFadeAlpha(0.0f, 0.0f, false);
            storeMessage.GetComponent<Text>().CrossFadeAlpha(1.0f, 0.3f, false);
            storeMessage.GetComponent<Text>().color = backupmessageboxcolor;
            storeMessage.GetComponent<Outline>().effectColor = backupmessageoutlinecolor;
            storeMessage.GetComponent<Text>().text = "EXTRA MAGIC TIME !!";

            maingame.GetComponent<game>().extraCharkatime = 6;

            game.allcoins -= chakracost;

            if (game.allcoins < 0) game.allcoins = 0;

            maingame.GetComponent<game>().coinsbox.GetComponent<Text>().text = game.allcoins.ToString();

            if (PlayerPrefs.HasKey("Coins")) { PlayerPrefs.SetInt("Coins", game.allcoins); PlayerPrefs.Save(); }

            StartCoroutine("hideMessage");

            checkAll3Powerups();

        }
        else
        {

            storeMessage.SetActive(true);
            storeMessage.GetComponent<Text>().CrossFadeAlpha(0.0f, 0.0f, false);
            storeMessage.GetComponent<Text>().CrossFadeAlpha(1.0f, 0.3f, false);

            storeMessage.GetComponent<Text>().color = Color.red;
            storeMessage.GetComponent<Outline>().effectColor = Color.white;
            storeMessage.GetComponent<Text>().text = "NOT ENOUGH COINS!!";

            StartCoroutine("hideMessage");


        }



    }


    public void tapButton()
    {

        if (game.paused == true) return;

        ///if (true){
        if (game.allcoins >= tapcost && maingame.GetComponent<game>().triplejumpallowed == false)
        {

            if (game.sfxON) AudioSource.PlayClipAtPoint(coinSplah, camera.transform.position, 0.5f);

            tapCheckSprite.SetActive(true);
            tapParticle.GetComponent<ParticleSystem>().Play();
            coinsParticle.GetComponent<ParticleSystem>().Play();

            powerupStar.SetActive(true);


            storeMessage.SetActive(true);
            storeMessage.GetComponent<Text>().CrossFadeAlpha(0.0f, 0.0f, false);
            storeMessage.GetComponent<Text>().CrossFadeAlpha(1.0f, 0.3f, false);
            storeMessage.GetComponent<Text>().color = backupmessageboxcolor;
            storeMessage.GetComponent<Outline>().effectColor = backupmessageoutlinecolor;
            storeMessage.GetComponent<Text>().text = "TRIPLE JUMP ENABLED !!";

            maingame.GetComponent<game>().triplejumpallowed = true;

            game.allcoins -= tapcost;

            if (game.allcoins < 0) game.allcoins = 0;

            maingame.GetComponent<game>().coinsbox.GetComponent<Text>().text = game.allcoins.ToString();

            if (PlayerPrefs.HasKey("Coins")) { PlayerPrefs.SetInt("Coins", game.allcoins); PlayerPrefs.Save(); }

            StartCoroutine("hideMessage");

            checkAll3Powerups();
        }
        else
        {

            storeMessage.SetActive(true);
            storeMessage.GetComponent<Text>().CrossFadeAlpha(0.0f, 0.0f, false);
            storeMessage.GetComponent<Text>().CrossFadeAlpha(1.0f, 0.3f, false);

            storeMessage.GetComponent<Text>().color = Color.red;
            storeMessage.GetComponent<Outline>().effectColor = Color.white;
            storeMessage.GetComponent<Text>().text = "NOT ENOUGH COINS!!";

            StartCoroutine("hideMessage");


        }



    }


    IEnumerator hideMessage()
    {


        yield return new WaitForSeconds(1f);

        storeMessage.GetComponent<Text>().CrossFadeAlpha(0.0f, 0.3f, false);

        StopCoroutine("hideMessage");
    }


    public void watchAd()
    {
#if UNITY_ANDROID || UNITY_IOS
        Debug.Log("watchAd");
        ShowOptions options = new ShowOptions();
        options.resultCallback = HandleShowResult;

        if (Advertisement.IsReady("rewardedVideo")) {

            Debug.Log("Ready");
            Advertisement.Show("rewardedVideo", options);

        }
        else
        {

            Debug.Log("NOT Ready");

        }
        //Advertisement.Show("rewardedVideo", options);
#endif
    }

    public void watchAd2()
    {
#if UNITY_ANDROID || UNITY_IOS
        Debug.Log("watchAd2");
        ShowOptions options = new ShowOptions();
        options.resultCallback = HandleShowResult2;
        int selection = Random.Range(1, 4);

        if (Advertisement.IsReady("video") && selection == 1) {

            Debug.Log("Ready");
            Advertisement.Show("video", options);

        }
        else
        {

            LoadLevel1Button();

        }

#endif
    }

#if UNITY_ANDROID || UNITY_IOS
    private void HandleShowResult(ShowResult result)
    {

        Debug.Log("HandleShowResult");

        switch (result)
        {
            case ShowResult.Finished:
                Debug.Log("Video completed. User rewarded credits.");

                doCoinMagic();

                break;
            case ShowResult.Skipped:
                Debug.LogWarning("Video was skipped.");
                break;
            case ShowResult.Failed:
                Debug.LogError("Video failed to show.");
                break;
        }

}
#endif



#if UNITY_ANDROID || UNITY_IOS
    private void HandleShowResult2(ShowResult result)
    {

        Debug.Log("HandleShowResult");

        switch (result)
        {
            case ShowResult.Finished:
                LoadLevel1Button();

                break;
            case ShowResult.Skipped:
                LoadLevel1Button();
                break;
            case ShowResult.Failed:
                LoadLevel1Button();
                break;
        }

}
#endif

    public void doCoinMagic()
    {

        //Debug.Log("doCoinMagic");

        game.allcoins += scoreIn.GetComponent<score>().currentscore;

        PlayerPrefs.SetInt("Coins", game.allcoins);

        PlayerPrefs.Save();

        scoreIn.GetComponent<score>().currentscore *= 2;

        scoreOut.GetComponent<Text>().text = scoreIn.GetComponent<score>().currentscore.ToString();

        scoreOut.GetComponent<Text>().color = ToColor(0x6AD893); ;

        doublecoinsButton.SetActive(false);

        bonusCoinsScore.SetActive(true);

        bonusCoinsScore.GetComponent<Text>().text = game.allcoins.ToString();

        if (game.sfxON) AudioSource.PlayClipAtPoint(coinSplah, camera.transform.position, 0.5f);


#if UNITY_ANDROID 
        if (Social.localUser.authenticated)
        { 
            if (PlayerPrefs.GetInt("achv22") == 0) { Social.ReportProgress(SpiritDash.GPGSIds.achievement_coin_doubler, 100.0f, (bool success) => { }); PlayerPrefs.SetInt("achv22", 1); PlayerPrefs.Save();}    

        }
#endif

    }

    void checkAll3Powerups()
    {

#if UNITY_ANDROID 
        if (Social.localUser.authenticated)
        { 
            if (PlayerPrefs.GetInt("achv23") == 0 && maingame.GetComponent<game>().triplejumpallowed == true &&   maingame.GetComponent<game>().extraCharkatime > 0 &&  maingame.GetComponent<game>().extraHeart > 0)
                { Social.ReportProgress(SpiritDash.GPGSIds.achievement_one_punch_man, 100.0f, (bool success) => { }); PlayerPrefs.SetInt("achv23", 1); PlayerPrefs.Save();}

        }
#endif

    }

    public Color32 ToColor(uint HexVal)
    {
        byte R = (byte)((HexVal >> 16) & 0xFF);
        byte G = (byte)((HexVal >> 8) & 0xFF);
        byte B = (byte)((HexVal) & 0xFF);
        return new Color32(R, G, B, 255);
    }


}
