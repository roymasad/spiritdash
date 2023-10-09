using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.SocialPlatforms;
using System.Reflection;

public class horse : MonoBehaviour {

    public GameObject scoreboard;

    public GameObject spawner0;

    public GameObject spawner1;

    public ParticleSystem dust1;

    public ParticleSystem crash;

    public ParticleSystem pickup;

    public ParticleSystem blastinto;

    public ParticleSystem blastinto2;

    public Light flare;

    public AudioClip gold;

    public AudioClip diamond;

    public AudioClip neigh;

    public AudioClip jump;

    public AudioClip penalty;

    public AudioClip lamp;

    public AudioClip thud;

    public AudioClip sweep;

    public Transform audiotarget;

    public string state;

    public bool ghostmode;

    public GameObject maingame;

    public GameObject debugtext;

    public GameObject rotator;

    public ParticleSystem landingParticle;

    public ParticleSystem pickup2;

    public Texture plus;

    public Texture minus;

    public GameObject lastTouchedFloor;

    public GameObject SuperbeamBox;
    public bool superbeamMode;

    public float darkchakratime = 7;

    public float ghostchakratime = 8;

    public float crownchakratime = 5;

    public float firechakratime = 6;


    // Use this for initialization
    void Start() {

        state = "running";

        lastTouchedFloor = new GameObject();

        ghostmode = false;

        GetComponent<Renderer>().material.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);

        StartCoroutine("Fadein", 1);

    }

    // Update is called once per frame
    void Update() {

        if (game.ended == true) StartCoroutine("SlowMo", 0.20f);


    }




    void OnTriggerEnter(Collider col)
    //void OnCollisionEnter (Collision col)
    {


        //Debug.Log (col.gameObject.transform.parent.name);
        string touchedobject = col.gameObject.transform.parent.name;
        GameObject targetobj = col.gameObject.transform.parent.gameObject;

        //return;
        if (game.paused == true || game.ended == true) return;

        //Debug.Log(col.gameObject.name);
        if (touchedobject == "trunkprefab(Clone)" || touchedobject == "barrelprefab(Clone)" || touchedobject == "cactusprefab(Clone)" || touchedobject == "shurikanprefab(Clone)" || touchedobject == "ghostprefab(Clone)")
        {


            if (ghostmode || superbeamMode)
            {

                if (superbeamMode)
                {


                    maingame.GetComponent<game>().boomParticles.transform.position = targetobj.transform.position;

                    maingame.GetComponent<game>().boomParticles.transform.Translate(new Vector3(-3, 0, 0));

                    maingame.GetComponent<game>().boomParticles.GetComponent<ParticleSystem>().Play();

                    spawner0.GetComponent<spawner>().ReturnToPool(targetobj);


                }

                return;
            }

            if (maingame.GetComponent<game>().lives > 1)
            {


                maingame.GetComponent<game>().boomParticles.transform.position = targetobj.transform.position;

                maingame.GetComponent<game>().boomParticles.transform.Translate(new Vector3(-3, 0, 0));

                maingame.GetComponent<game>().boomParticles.GetComponent<ParticleSystem>().Play();

                spawner0.GetComponent<spawner>().ReturnToPool(targetobj);



                //if (superbeamMode == true) return;

                if (game.sfxON) AudioSource.PlayClipAtPoint(neigh, audiotarget.position);

                crash.Play();

                maingame.GetComponent<game>().lives -= 1;

                maingame.GetComponent<game>().PushMessage("CRASH !", new Color(0.9f, 0.3f, 0.3f));


                if (maingame.GetComponent<game>().lives == 1) { maingame.GetComponent<game>().heart1.SetActive(true); maingame.GetComponent<game>().heart2.SetActive(false); maingame.GetComponent<game>().heart3.SetActive(false); }
                if (maingame.GetComponent<game>().lives == 2) { maingame.GetComponent<game>().heart1.SetActive(true); maingame.GetComponent<game>().heart2.SetActive(true); maingame.GetComponent<game>().heart3.SetActive(false); }

                ghostmode = true;

                GetComponent<Renderer>().material.color = new Color(1.0f, 1.0f, 1.0f, 0.6f);

                StartCoroutine("ResetThings2", 1.0f);


                return;
            }

            maingame.GetComponent<game>().heart1.SetActive(false); maingame.GetComponent<game>().heart2.SetActive(false); maingame.GetComponent<game>().heart3.SetActive(false);

            maingame.GetComponent<game>().lamppickup.SetActive(false);

            scroll.ended = true;

            //collider.enabled = false;

            //targetobj.rigidbody.constraints = RigidbodyConstraints.FreezeAll;


            if (game.sfxON) AudioSource.PlayClipAtPoint(neigh, audiotarget.position);

            crash.Play();

            maingame.GetComponent<game>().messagebox.transform.Translate(new Vector3(-2000, -2000, -2000));


            game.ended = true;

            maingame.GetComponent<game>().Panel.SetActive(true);

            maingame.GetComponent<game>().score.GetComponent<Text>().text = scoreboard.GetComponent<score>().currentscore.ToString();

            maingame.GetComponent<game>().maxscore.GetComponent<Text>().text = PlayerPrefs.GetInt("Score").ToString();

            maingame.GetComponent<game>().newtopscore.SetActive(true);



            game.allcoins += scoreboard.GetComponent<score>().currentscore;

            PlayerPrefs.SetInt("Coins", game.allcoins);

            PlayerPrefs.Save();

#if UNITY_ANDROID
            PlayGamesPlatform.Activate();


            if (Social.localUser.authenticated) Social.ReportScore(scoreboard.GetComponent<score>().currentscore, SpiritDash.GPGSIds.leaderboard_topscore, (bool success) =>
            {
                // handle success or failure


            });
#endif


            if (scoreboard.GetComponent<score>().currentscore > PlayerPrefs.GetInt("Score"))
            {

                PlayerPrefs.SetInt("Score", scoreboard.GetComponent<score>().currentscore);

                PlayerPrefs.Save();


                return;


            }

            maingame.GetComponent<game>().newtopscore.GetComponent<Text>().text = "";

#if UNITY_ANDROID 
            if (Social.localUser.authenticated)
            { 
                if (game.allcoins >= 1000 && PlayerPrefs.GetInt("achv7") == 0) { Social.ReportProgress(SpiritDash.GPGSIds.achievement_lepricon_gold, 100.0f, (bool success) => { }); PlayerPrefs.SetInt("achv7", 1); PlayerPrefs.Save();  }
                if (game.allcoins >= 5000 && PlayerPrefs.GetInt("achv9") == 0) { Social.ReportProgress(SpiritDash.GPGSIds.achievement_you_deserve_a_snack, 100.0f, (bool success) => { }); PlayerPrefs.SetInt("achv8", 1); PlayerPrefs.Save(); }
                if (game.allcoins >= 10000 && PlayerPrefs.GetInt("achv9") == 0) { Social.ReportProgress(SpiritDash.GPGSIds.achievement_gold_plate, 100.0f, (bool success) => { }); PlayerPrefs.SetInt("achv9", 1); PlayerPrefs.Save(); }
                if (game.allcoins >= 20000 && PlayerPrefs.GetInt("achv10") == 0) { Social.ReportProgress(SpiritDash.GPGSIds.achievement_business_class_soul, 100.0f, (bool success) => { }); PlayerPrefs.SetInt("achv10", 1); PlayerPrefs.Save(); }
                if (game.allcoins >= 30000 && PlayerPrefs.GetInt("achv11") == 0) { Social.ReportProgress(SpiritDash.GPGSIds.achievement_bank_man, 100.0f, (bool success) => { }); PlayerPrefs.SetInt("achv11", 1); PlayerPrefs.Save(); }
                if (game.allcoins >= 50000 && PlayerPrefs.GetInt("achv12") == 0) { Social.ReportProgress(SpiritDash.GPGSIds.achievement_elon_musk_level, 100.0f, (bool success) => { }); PlayerPrefs.SetInt("achv12", 1); PlayerPrefs.Save(); }
                if (game.allcoins >= 100000 && PlayerPrefs.GetInt("achv13") == 0) { Social.ReportProgress(SpiritDash.GPGSIds.achievement_bruce_wayne_pimpin, 100.0f, (bool success) => { }); PlayerPrefs.SetInt("achv13", 1); PlayerPrefs.Save(); }
                if (game.allcoins >= 1000000 && PlayerPrefs.GetInt("achv14") == 0) { Social.ReportProgress(SpiritDash.GPGSIds.achievement_scroodge_award, 100.0f, (bool success) => { }); PlayerPrefs.SetInt("achv14", 1); PlayerPrefs.Save(); }

                if (scoreboard.GetComponent<score>().currentscore == 0 && PlayerPrefs.GetInt("achv20") == 0) { Social.ReportProgress(SpiritDash.GPGSIds.achievement_starving_artist, 100.0f, (bool success) => { }); PlayerPrefs.SetInt("achv20", 1); PlayerPrefs.Save();}
            }
#endif
        }

        else if (touchedobject == "pumpkinprefab(Clone)")
        {
            maingame.GetComponent<game>().boomParticles.transform.position = targetobj.transform.position;

            maingame.GetComponent<game>().boomParticles.transform.Translate(new Vector3(-3, 0, 0));

            maingame.GetComponent<game>().boomParticles.GetComponent<ParticleSystem>().Play();

            spawner0.GetComponent<spawner>().ReturnToPool(targetobj);

            maingame.GetComponent<game>().PushMessage("MAXXX LIFE!", Color.yellow);

            maingame.GetComponent<game>().heart1.SetActive(true); maingame.GetComponent<game>().heart2.SetActive(true); maingame.GetComponent<game>().heart3.SetActive(true);

            if (game.sfxON) AudioSource.PlayClipAtPoint(sweep, audiotarget.position, 0.5f);

            maingame.GetComponent<game>().lives = 2 + maingame.GetComponent<game>().extraHeart;

#if UNITY_ANDROID 
            if (Social.localUser.authenticated)
            { 
             
               if (PlayerPrefs.GetInt("achv21") == 0) { Social.ReportProgress(SpiritDash.GPGSIds.achievement_pumpkin_pie, 100.0f, (bool success) => { }); PlayerPrefs.SetInt("achv21", 1); PlayerPrefs.Save();}

            }
#endif



            //maingame.GetComponent<game>().rain.GetComponent<ParticleSystemRenderer>().renderMode = ParticleSystemRenderMode.Mesh;

            //maingame.GetComponent<game>().rain.GetComponent<ParticleSystem>().Play();

            //StartCoroutine("switchRain", 2f);
        }


        else if (touchedobject == "coinprefab(Clone)")
        {



            if (maingame.GetComponent<game>().multiplier == true) scoreboard.GetComponent<score>().currentscore += 2;
            else scoreboard.GetComponent<score>().currentscore += 1;

            pickup.Play();

            if (game.sfxON) AudioSource.PlayClipAtPoint(gold, audiotarget.position, 0.4f);


            pickup2.GetComponent<Renderer>().material.mainTexture = plus;
            pickup2.Play();

            if (scoreboard.GetComponent<score>().currentscore >= (maingame.GetComponent<game>().scoreSwitchAt + maingame.GetComponent<game>().levelsize))
            {

                maingame.GetComponent<game>().lamp2ready = true;

                maingame.GetComponent<game>().lamppickup.SetActive(true);

                maingame.GetComponent<game>().arrowicon.transform.localPosition = new Vector3(12.11969f, 6.5f, 0.8363594f);

                //maingame.GetComponent<game>().level++;

                //maingame.GetComponent<game>().PushMessage("LEVEL " + maingame.GetComponent<game>().level, Color.white);
            }

            maingame.GetComponent<game>().boomParticles.transform.position = targetobj.transform.position;

            maingame.GetComponent<game>().boomParticles.transform.Translate(new Vector3(-3, 0, 0));

            maingame.GetComponent<game>().boomParticles.GetComponent<ParticleSystem>().Play();

            spawner0.GetComponent<spawner>().ReturnToPool(targetobj);

#if UNITY_ANDROID 
            if (Social.localUser.authenticated)
            { 

            if (scoreboard.GetComponent<score>().currentscore >= 100 && PlayerPrefs.GetInt("achv1") == 0) { Social.ReportProgress(SpiritDash.GPGSIds.achievement_money_grows_on_spirit_trees, 100.0f, (bool success) => { }); PlayerPrefs.SetInt("achv1", 1); PlayerPrefs.Save(); }
            if (scoreboard.GetComponent<score>().currentscore >= 300 && PlayerPrefs.GetInt("achv2") == 0) { Social.ReportProgress(SpiritDash.GPGSIds.achievement_money_money_money, 100.0f, (bool success) => { }); PlayerPrefs.SetInt("achv2", 1); PlayerPrefs.Save(); }
            if (scoreboard.GetComponent<score>().currentscore >= 500 && PlayerPrefs.GetInt("achv3") == 0) { Social.ReportProgress(SpiritDash.GPGSIds.achievement_young_blood, 100.0f, (bool success) => { }); PlayerPrefs.SetInt("achv3", 1); PlayerPrefs.Save(); }
            if (scoreboard.GetComponent<score>().currentscore >= 800 && PlayerPrefs.GetInt("achv4") == 0) { Social.ReportProgress(SpiritDash.GPGSIds.achievement_youll_probably_get_fired_from_work, 100.0f, (bool success) => { }); PlayerPrefs.SetInt("achv4", 1); PlayerPrefs.Save(); }
            if (scoreboard.GetComponent<score>().currentscore >= 1000 && PlayerPrefs.GetInt("achv5") == 0) { Social.ReportProgress(SpiritDash.GPGSIds.achievement_dont_drive_while_playing_this_award, 100.0f, (bool success) => { }); PlayerPrefs.SetInt("achv5", 1); PlayerPrefs.Save(); }
            if (scoreboard.GetComponent<score>().currentscore >= 10000 && PlayerPrefs.GetInt("achv6") == 0) { Social.ReportProgress(SpiritDash.GPGSIds.achievement_mobile_master_race_award, 100.0f, (bool success) => { }); PlayerPrefs.SetInt("achv6", 1); PlayerPrefs.Save(); }
            }
#endif

        }

        else if (touchedobject == "badcoinprefab(Clone)")
        {

            maingame.GetComponent<game>().boomParticles.transform.position = targetobj.transform.position;

            maingame.GetComponent<game>().boomParticles.transform.Translate(new Vector3(-3, 0, 0));

            maingame.GetComponent<game>().boomParticles.GetComponent<ParticleSystem>().Play();

            spawner0.GetComponent<spawner>().ReturnToPool(targetobj);

            if (superbeamMode == true || ghostmode == true) return;

            scoreboard.GetComponent<score>().currentscore -= 2;

            if (scoreboard.GetComponent<score>().currentscore < 0) scoreboard.GetComponent<score>().currentscore = 0;

            if (game.sfxON) AudioSource.PlayClipAtPoint(penalty, audiotarget.position, 0.5f);

            pickup2.GetComponent<Renderer>().material.mainTexture = minus;
            pickup2.Play();



            //maingame.GetComponent<game>().PushMessage("COIN -5", new Color(0.9f, 0.3f, 0.3f));


        }
        else if (touchedobject == "heartprefab(Clone)")
        {


            if (maingame.GetComponent<game>().lives == 2 + maingame.GetComponent<game>().extraHeart) maingame.GetComponent<game>().PushMessage("MAX LIFE!", Color.white);
            else maingame.GetComponent<game>().PushMessage("EXTRA LIFE!", Color.white);

            if (game.sfxON) AudioSource.PlayClipAtPoint(diamond, audiotarget.position, 0.5f);

            if (maingame.GetComponent<game>().lives < 2 + maingame.GetComponent<game>().extraHeart) maingame.GetComponent<game>().lives++;

            if (maingame.GetComponent<game>().lives == 1) { maingame.GetComponent<game>().heart1.SetActive(true); maingame.GetComponent<game>().heart2.SetActive(false); maingame.GetComponent<game>().heart3.SetActive(false); }
            if (maingame.GetComponent<game>().lives == 2) { maingame.GetComponent<game>().heart1.SetActive(true); maingame.GetComponent<game>().heart2.SetActive(true); maingame.GetComponent<game>().heart3.SetActive(false); }
            if (maingame.GetComponent<game>().lives == 3) { maingame.GetComponent<game>().heart1.SetActive(true); maingame.GetComponent<game>().heart2.SetActive(true); maingame.GetComponent<game>().heart3.SetActive(true); }

            maingame.GetComponent<game>().boomParticles.transform.position = targetobj.transform.position;

            maingame.GetComponent<game>().boomParticles.transform.Translate(new Vector3(-3, 0, 0));

            maingame.GetComponent<game>().boomParticles.GetComponent<ParticleSystem>().Play();

            spawner0.GetComponent<spawner>().ReturnToPool(targetobj);

        }

        else if (touchedobject == "diamondprefab(Clone)")
        {

            scoreboard.GetComponent<score>().currentscore += 5;

            pickup.Play();

            if (game.sfxON) AudioSource.PlayClipAtPoint(diamond, audiotarget.position, 0.5f);

            pickup2.GetComponent<Renderer>().material.mainTexture = plus;
            pickup2.Play();

            //maingame.GetComponent<game>().PushMessage("Diamond +5", Color.white);

            if (scoreboard.GetComponent<score>().currentscore >= (maingame.GetComponent<game>().scoreSwitchAt + maingame.GetComponent<game>().levelsize))
            {

                maingame.GetComponent<game>().lamp2ready = true;
                maingame.GetComponent<game>().lamppickup.SetActive(true);

            }

            maingame.GetComponent<game>().boomParticles.transform.position = targetobj.transform.position;

            maingame.GetComponent<game>().boomParticles.transform.Translate(new Vector3(-3, 0, 0));

            maingame.GetComponent<game>().boomParticles.GetComponent<ParticleSystem>().Play();

            spawner0.GetComponent<spawner>().ReturnToPool(targetobj);

        }

        else if (touchedobject == "lampprefab(Clone)")
        {



            if (game.sfxON) AudioSource.PlayClipAtPoint(lamp, audiotarget.position, 0.5f);


            int effect = Random.Range(1, 5);

            maingame.GetComponent<game>().chakra2button.SetActive(true);
            Hashtable ht = new Hashtable();
            ht.Add("y", 1.2f);
            ht.Add("x", 1.2f);
            ht.Add("time", 0.5f);
            ht.Add("oncomplete", "disableChakra");
            ht.Add("onCompleteTarget", this.gameObject);
            ht.Add("easetype", iTween.EaseType.spring);
            ht.Add("looptype", iTween.LoopType.none);
            iTween.ScaleFrom(maingame.GetComponent<game>().chakra2button, ht);

            maingame.GetComponent<game>().tap2.SetActive(true);

            if (effect == 1)
            {
                maingame.GetComponent<game>().PushMessage("X2 CHAKRA !", Color.yellow);
                maingame.GetComponent<game>().currentChakra = "dark";
                maingame.GetComponent<game>().chakra2button.GetComponent<Button>().image.sprite = maingame.GetComponent<game>().chakraTex4;

            }

            else if (effect == 2)
            {
                maingame.GetComponent<game>().PushMessage("GOD CHAKRA !", Color.yellow);
                maingame.GetComponent<game>().currentChakra = "ghost";
                maingame.GetComponent<game>().chakra2button.GetComponent<Button>().image.sprite = maingame.GetComponent<game>().chakraTex3;

            }

            else if (effect == 3)
            {
                maingame.GetComponent<game>().PushMessage("GOLD CHAKRA !", Color.yellow);
                maingame.GetComponent<game>().currentChakra = "crown";
                maingame.GetComponent<game>().chakra2button.GetComponent<Button>().image.sprite = maingame.GetComponent<game>().chakraTex2;

            }

            else if (effect == 4)
            {
                maingame.GetComponent<game>().PushMessage("FIRE CHAKRA !", Color.yellow);
                maingame.GetComponent<game>().currentChakra = "fire";
                maingame.GetComponent<game>().chakra2button.GetComponent<Button>().image.sprite = maingame.GetComponent<game>().chakraTex5;


            }


            maingame.GetComponent<game>().boomParticles.transform.position = targetobj.transform.position;

            maingame.GetComponent<game>().boomParticles.transform.Translate(new Vector3(-3, 0, 0));

            maingame.GetComponent<game>().boomParticles.GetComponent<ParticleSystem>().Play();

            spawner0.GetComponent<spawner>().ReturnToPool(targetobj);


        }

        else if (touchedobject == "lamp2prefab(Clone)")
        {

            //scoreboard.GetComponent<score>().currentscore += 10;

            maingame.GetComponent<game>().level++;

            maingame.GetComponent<game>().PushMessage("WAVE " + maingame.GetComponent<game>().level, Color.white);

            maingame.GetComponent<game>().lamp2ready = false;

            maingame.GetComponent<game>().levelsize += (int)(maingame.GetComponent<game>().levelsize * 0.3f);

            maingame.GetComponent<game>().scoreSwitchAt = scoreboard.GetComponent<score>().currentscore;

            mover.movingspeed = 0.92f; scroll.movingspeed = 0.92f;

            spawner0.GetComponent<spawner>().counter = 15;
            spawner1.GetComponent<spawner>().counter = 15;

            spawner.lamp2spawned = false;

            maingame.GetComponent<game>().lamppickup.SetActive(false);

            maingame.GetComponent<game>().arrowicon.transform.localPosition = new Vector3(12.11969f, 1.25f, 0.8363594f);

            //maingame.GetComponent<game>().PushMessage("Teleport !", Color.white);

            scoreboard.GetComponent<score>().currentscore += 1;

            if (game.sfxON) AudioSource.PlayClipAtPoint(lamp, audiotarget.position, 0.5f);

            if (maingame.GetComponent<game>().backgroundname == "duneday") maingame.GetComponent<game>().setbackgroundDuneNight();
            else if (maingame.GetComponent<game>().backgroundname == "dunenight") maingame.GetComponent<game>().setWaterground();
            else if (maingame.GetComponent<game>().backgroundname == "waterday") maingame.GetComponent<game>().setDarkground();
            else if (maingame.GetComponent<game>().backgroundname == "dark") maingame.GetComponent<game>().setbackgroundDuneDay();

            maingame.GetComponent<game>().starrain.GetComponent<ParticleSystem>().loop = false;
            maingame.GetComponent<game>().starrain.GetComponent<ParticleSystem>().Play();

            maingame.GetComponent<game>().boomParticles.transform.position = targetobj.transform.position;

            maingame.GetComponent<game>().boomParticles.transform.Translate(new Vector3(-3, 0, 0));

            maingame.GetComponent<game>().boomParticles.GetComponent<ParticleSystem>().Play();

            spawner0.GetComponent<spawner>().ReturnToPool(targetobj);

#if UNITY_ANDROID 
            if (Social.localUser.authenticated)
            { 
            if (maingame.GetComponent<game>().level >= 5 && PlayerPrefs.GetInt("achv15") == 0) { Social.ReportProgress(SpiritDash.GPGSIds.achievement_wave_man, 100.0f, (bool success) => { }); PlayerPrefs.SetInt("achv15", 1); PlayerPrefs.Save();  }
            if (maingame.GetComponent<game>().level >= 10 && PlayerPrefs.GetInt("achv16") == 0) { Social.ReportProgress(SpiritDash.GPGSIds.achievement_spirit_runner, 100.0f, (bool success) => { }); PlayerPrefs.SetInt("achv16", 1); PlayerPrefs.Save();  }
            if (maingame.GetComponent<game>().level >= 20 && PlayerPrefs.GetInt("achv17") == 0) { Social.ReportProgress(SpiritDash.GPGSIds.achievement_wave_musician, 100.0f, (bool success) => { }); PlayerPrefs.SetInt("achv17", 1); PlayerPrefs.Save();  }
            if (maingame.GetComponent<game>().level >= 50 && PlayerPrefs.GetInt("achv18") == 0) { Social.ReportProgress(SpiritDash.GPGSIds.achievement_zombie_apocalypse_survivor, 100.0f, (bool success) => { }); PlayerPrefs.SetInt("achv18", 1); PlayerPrefs.Save();  }
            if (maingame.GetComponent<game>().level >= 100 && PlayerPrefs.GetInt("achv19") == 0) { Social.ReportProgress(SpiritDash.GPGSIds.achievement_buffer_overflow, 100.0f, (bool success) => { }); PlayerPrefs.SetInt("achv19", 1); PlayerPrefs.Save();  }
            }
#endif

        }

    }

    public Color32 ToColor(uint HexVal)
    {
        byte R = (byte)((HexVal >> 16) & 0xFF);
        byte G = (byte)((HexVal >> 8) & 0xFF);
        byte B = (byte)((HexVal) & 0xFF);
        return new Color32(R, G, B, 255);
    }

    public void changeChakrabar(float size) {

        maingame.GetComponent<game>().progressbar1.GetComponent<RectTransform>().sizeDelta = new Vector2(size, 3);
            
    }

    public void chakrabar(float time)
    {

        maingame.GetComponent<game>().progressbar1.SetActive(true);
        iTween.ValueTo(maingame.GetComponent<game>().progressbar1, iTween.Hash("from", 300, "to", 0, "time", time, "easetype", iTween.EaseType.linear, "onupdate", "changeChakrabar", "onupdatetarget", this.gameObject));



    }

    public void DarkChakra()
    {


        ResetThings3();

        //RenderSettings.fog = true;
        //RenderSettings.fogMode = FogMode.Exponential;
        //RenderSettings.fogDensity = 0.02f;

        //Time.timeScale = 1.4f;

        //flare.intensity = 4;

        //rotator.GetComponent<landrotate>().play();

        maingame.GetComponent<game>().multiplier = true;
        maingame.GetComponent<game>().multiplierGUI.SetActive(true);

        maingame.GetComponent<game>().coinMat.color = ToColor(0x8ADD95);


        GetComponent<ImageSequenceSingleTexture>().folderName = "Sequence4";
        GetComponent<ImageSequenceSingleTexture>().numberOfFrames = 4;

        GetComponent<Renderer>().material.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);

        //}

        GetComponent<ImageSequenceSingleTexture>().resetFolder();


        blastinto2.Play();

        float chakratime = darkchakratime + maingame.GetComponent<game>().extraCharkatime;

        chakrabar(chakratime);

        StopCoroutine("ResetThings");
        StartCoroutine("ResetThings", chakratime);
        
    }

    public void GhostChakra()
    {

        ResetThings3();


        ghostmode = true;

        GetComponent<Renderer>().material.color = new Color(1.0f, 1.0f, 1.0f, 0.6f);

        GetComponent<ImageSequenceSingleTexture>().folderName = "Sequence3";
        GetComponent<ImageSequenceSingleTexture>().numberOfFrames = 4;

        //GetComponent<Renderer>().material.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);

        GetComponent<ImageSequenceSingleTexture>().resetFolder();


        blastinto2.Play();

        float chakratime = ghostchakratime + maingame.GetComponent<game>().extraCharkatime;

        chakrabar(chakratime);

        StopCoroutine("ResetThings");
        StartCoroutine("ResetThings", chakratime);

        
    }

    public void CrownChakra()
    {

        ResetThings3();

        spawner0.GetComponent<spawner>().goldrushmode = true;

        GetComponent<ImageSequenceSingleTexture>().folderName = "Sequence2";
        GetComponent<ImageSequenceSingleTexture>().numberOfFrames = 6;

        GetComponent<Renderer>().material.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
        
        GetComponent<ImageSequenceSingleTexture>().resetFolder();


        blastinto2.Play();

        float chakratime = crownchakratime + maingame.GetComponent<game>().extraCharkatime;

        chakrabar(chakratime);

        StopCoroutine("ResetThings");
        StartCoroutine("ResetThings", chakratime);


    }

    public void FireChakra()
    {

        ResetThings3();


        GetComponent<ImageSequenceSingleTexture>().folderName = "Sequence5";
        GetComponent<ImageSequenceSingleTexture>().numberOfFrames = 3;

        GetComponent<Renderer>().material.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
        
        GetComponent<ImageSequenceSingleTexture>().resetFolder();


        blastinto2.Play();

        superbeamMode = true;
        StartCoroutine("DelaySuperbeamBox",0.2f);

        float chakratime = firechakratime + maingame.GetComponent<game>().extraCharkatime;

        chakrabar(chakratime);
        StartCoroutine("ResetThings", chakratime);
        

    }

    IEnumerator DelaySuperbeamBox(float delay)
    {
        yield return new WaitForSeconds(delay);
        SuperbeamBox.SetActive(true);
        StopCoroutine("DelaySuperbeamBox");
    }


        IEnumerator ResetThings2(float delay)
    {
        yield return new WaitForSeconds(delay);

        ghostmode = false;

        GetComponent<Renderer>().material.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);

        StopCoroutine("ResetThings2");
    }

        IEnumerator ResetThings(float delay)  
	{  

		//wait for the time defined at the delay parameter  
		//Debug.Log (delay);
		yield return new WaitForSeconds(delay);

        StopCoroutine("ResetThings2");

        ResetThings3();

        StopCoroutine("ResetThings");  
	}

    void ResetThings3()
    {
        StopCoroutine("DelaySuperbeamBox");

        if (Time.timeScale == 0 || game.ended == true) StopCoroutine("ResetThings");

        Time.timeScale = 1.0f;

        flare.intensity = 0.85f;

        ghostmode = false;

        GetComponent<Renderer>().material.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);

        spawner0.GetComponent<spawner>().goldrushmode = false;

        //RenderSettings.fogMode = FogMode.ExponentialSquared;

        //RenderSettings.fogDensity = 0.004f;

        //RenderSettings.fog = false;

        if (GetComponent<ImageSequenceSingleTexture>().folderName != "Sequence1") blastinto2.Play();

        GetComponent<ImageSequenceSingleTexture>().folderName = "Sequence1";
        GetComponent<ImageSequenceSingleTexture>().numberOfFrames = 6;
        GetComponent<ImageSequenceSingleTexture>().resetFolder();

        superbeamMode = false;
        SuperbeamBox.SetActive(false);

        maingame.GetComponent<game>().multiplier = false;
        maingame.GetComponent<game>().multiplierGUI.SetActive(false);

        maingame.GetComponent<game>().coinMat.color = new Color(1, 1, 1);

        maingame.GetComponent<game>().progressbar1.SetActive(false);
        

    }


    IEnumerator switchRain(float delay)
    {
        yield return new WaitForSeconds(delay);

        //maingame.GetComponent<game>().rain.GetComponent<ParticleSystemRenderer>().renderMode = ParticleSystemRenderMode.Billboard;

    }

    
	IEnumerator SlowMo(float delay)  
	{  
		yield return new WaitForSeconds(delay); 

		Time.timeScale = 0 ;
		GetComponent<Renderer>().material.color = new Color(1.0f,1.0f,1.0f,0.0f);
		StopCoroutine("SlowMo"); 


//		float temp = Time.timeScale;
//
//		temp -= 0.10f;
//
//		if (temp <=0 ) {StopCoroutine("SlowMo"); Time.timeScale = 0 ;}
//		else Time.timeScale = temp;


	}

	IEnumerator Fadein(float delay)  
	{  
		yield return new WaitForSeconds(delay); 

		GetComponent<Renderer>().material.color = new Color(1.0f,1.0f,1.0f,1.0f);

		StopCoroutine("Fadein"); 
		
		
	}


    public void resetHorsy()
    {

        state = "running";

        maingame.GetComponent<game>().doublejump = false;

        maingame.GetComponent<game>().triplejump = false;


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
