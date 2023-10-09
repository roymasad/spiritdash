using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class spawner : MonoBehaviour {


	public GameObject barrel;

	public GameObject trunk;

	public GameObject cactus;

	public GameObject dustball;

	public GameObject pumpkin;

	public GameObject ufo;

	public GameObject gold;

	public GameObject badgold;

	public GameObject diamond;

	public GameObject lamp;

	public GameObject lamp2;

	public GameObject shurikan;

    public GameObject health;

    public GameObject ghost;

    public int counter;

	private bool cancelnextblocker;

	public GameObject floor;

	public GameObject maingame;

	public int level;

	public bool popuplatePool;

	public bool itemCreatedFlag;

	public bool goldrushmode;

    public bool floatingfloors = false;

    public static bool lamp2spawned = false;

    
    private Quaternion resetrot = new Quaternion(0.0f,0.0f,0.0f,0.0f);

	private static ArrayList PoolFree;
	private static ArrayList PoolUsed;

    float lastfloorPosition;


    void  populatePool() {

		if (!popuplatePool) return;

		PoolFree = new ArrayList(); 
		PoolUsed = new ArrayList(); 

		GameObject instance = null;
		//Quaternion resetrot = new Quaternion(0.0f,0.0f,0.0f,0.0f);

		for (int i = 1; i < 20; i++) {

			instance = Instantiate(gold) as GameObject; 
			instance.transform.position = this.transform.position; 
			instance.transform.parent = this.transform;
            instance.transform.rotation = resetrot;

			instance.SetActive(false);
			//instance.GetComponent<mover>().enabled = false;
			//ChangeLayerRecursively(instance, "hidden");

			instance.tag = "gold";
			PoolFree.Add(instance);  

		}

		for (int i = 1; i < 6; i++) {
			
			instance = Instantiate(badgold) as GameObject; 
			instance.transform.position = this.transform.position; 
			instance.transform.parent = this.transform; instance.transform.rotation = resetrot;

			instance.SetActive(false);
			//instance.GetComponent<mover>().enabled = false;
			//ChangeLayerRecursively(instance, "hidden");

			instance.tag = "badgold";
			PoolFree.Add(instance);  
			
		}


		for (int i = 1; i < 6; i++) {
			
			instance = Instantiate(diamond) as GameObject; 
			instance.transform.position = this.transform.position; 
			instance.transform.parent = this.transform; instance.transform.rotation = resetrot;

			instance.SetActive(false);
			//instance.GetComponent<mover>().enabled = false;
			//ChangeLayerRecursively(instance, "hidden");

			instance.tag = "diamond";
			PoolFree.Add(instance);  
			
		}


        instance = Instantiate(health) as GameObject;
        instance.transform.position = this.transform.position;
        instance.transform.parent = this.transform; instance.transform.rotation = resetrot;

        instance.SetActive(false);
        //instance.GetComponent<mover>().enabled = false;
        //ChangeLayerRecursively(instance, "hidden");

        instance.tag = "health";
        PoolFree.Add(instance);


        instance = Instantiate(ghost) as GameObject;
        instance.transform.position = this.transform.position;
        instance.transform.parent = this.transform; instance.transform.rotation = resetrot;

        instance.SetActive(false);
        //instance.GetComponent<mover>().enabled = false;
        //ChangeLayerRecursively(instance, "hidden");

        instance.tag = "ghost";
        PoolFree.Add(instance);


        for (int i = 1; i < 2; i++) {
			
			instance = Instantiate(lamp) as GameObject; 
			instance.transform.position = this.transform.position; 
			instance.transform.parent = this.transform; instance.transform.rotation = resetrot;

			instance.SetActive(false);
			//instance.GetComponent<mover>().enabled = false;
			//ChangeLayerRecursively(instance, "hidden");

			instance.tag = "lamp";
			PoolFree.Add(instance);  
			
		}

		for (int i = 1; i < 2; i++) {
			
			instance = Instantiate(lamp2) as GameObject; 
			instance.transform.position = this.transform.position; 
			instance.transform.parent = this.transform; instance.transform.rotation = resetrot;

			instance.SetActive(false);
			//instance.GetComponent<mover>().enabled = false;
			//ChangeLayerRecursively(instance, "hidden");

			instance.tag = "lamp2";
			PoolFree.Add(instance);  
			
		}

		for (int i = 1; i < 5; i++) {
			
			instance = Instantiate(barrel) as GameObject; 
			instance.transform.position = this.transform.position; 
			instance.transform.parent = this.transform; instance.transform.rotation = resetrot;

			instance.SetActive(false);
			//instance.GetComponent<mover>().enabled = false;
			//ChangeLayerRecursively(instance, "hidden");

			instance.tag = "barrel";
			PoolFree.Add(instance);  
			
		}


		for (int i = 1; i < 5; i++) {
			
			instance = Instantiate(cactus) as GameObject; 
			instance.transform.position = this.transform.position; 
			instance.transform.parent = this.transform; instance.transform.rotation = resetrot;

			instance.SetActive(false);
			//instance.GetComponent<mover>().enabled = false;
			//ChangeLayerRecursively(instance, "hidden");

			instance.tag = "cactus";
			PoolFree.Add(instance);  
			
		}

		for (int i = 1; i < 5; i++) {
			
			instance = Instantiate(trunk) as GameObject; 
			instance.transform.position = this.transform.position; 
			instance.transform.parent = this.transform; instance.transform.rotation = resetrot;

			instance.SetActive(false);
			//instance.GetComponent<mover>().enabled = false;
			//ChangeLayerRecursively(instance, "hidden");

			instance.tag = "trunk";
			PoolFree.Add(instance);  
			
		}

		for (int i = 1; i < 10; i++) {
			
			instance = Instantiate(floor) as GameObject; 
			instance.transform.position = this.transform.position; 
			instance.transform.parent = this.transform; instance.transform.rotation = resetrot;

			instance.SetActive(false);
			//instance.GetComponent<mover>().enabled = false;
			//ChangeLayerRecursively(instance, "hidden");

			instance.tag = "floor";
			PoolFree.Add(instance);  
			
		}

		for (int i = 1; i < 2; i++) {
			
			instance = Instantiate(ufo) as GameObject; 
			instance.transform.position = this.transform.position; 
			instance.transform.parent = this.transform; instance.transform.rotation = resetrot;

			instance.SetActive(false);
			//instance.GetComponent<mover>().enabled = false;
			//ChangeLayerRecursively(instance, "hidden");

			instance.tag = "ufo";
			PoolFree.Add(instance);  
			
		}

		for (int i = 1; i < 2; i++) {
			
            
			instance = Instantiate(pumpkin) as GameObject; 
			instance.transform.position = this.transform.position; 
			instance.transform.parent = this.transform; instance.transform.rotation = resetrot;

			instance.SetActive(false);
			//instance.GetComponent<mover>().enabled = false;
			//ChangeLayerRecursively(instance, "hidden");

			instance.tag = "pumpkin";
			PoolFree.Add(instance);  
            
			
		}

		for (int i = 1; i < 2; i++) {

            /*
            instance = Instantiate(dustball) as GameObject; 
			instance.transform.position = this.transform.position; 
			instance.transform.parent = this.transform; instance.transform.rotation = resetrot;

			instance.SetActive(false);
			//instance.GetComponent<mover>().enabled = false;
			//ChangeLayerRecursively(instance, "hidden");

			instance.tag = "dustball";
			PoolFree.Add(instance);  
            */
			
		}

		for (int i = 1; i < 3; i++) {
			
			instance = Instantiate(shurikan) as GameObject; 
			instance.transform.position = this.transform.position; 
			instance.transform.parent = this.transform; instance.transform.rotation = resetrot;

			instance.SetActive(false);
			//instance.GetComponent<mover>().enabled = false;
			//ChangeLayerRecursively(instance, "hidden");

			instance.tag = "shurikan";
			PoolFree.Add(instance);  
			
		}

	}

	public void GetFromPool(string type) {

		if (type == "empty") { itemCreatedFlag = true; return; }

        //goldrushmode = true;

        //Quaternion resetrot = new Quaternion(0.0f,0.0f,0.0f,0.0f);
        
        Vector3 temp;

        
		ArrayList PoolTemp = new ArrayList();

        //Debug.Log(type);


        GameObject refFloor = this.gameObject;





        floatingfloors = true;
        if (floatingfloors && level == 1)
        {

            if (type == "lamp2" && lamp2spawned == true) return;

            foreach (GameObject instance in PoolFree)
            {

                if (instance.tag == "floor")
                {
                    instance.transform.position = this.transform.position;
                    instance.transform.parent = this.transform;
                    instance.transform.rotation = resetrot;

                    instance.SetActive(true);

                    temp = instance.transform.position;

                    if (maingame.GetComponent<game>().lamp2ready && maingame.GetComponent<game>().level < 4) { temp.y = lastfloorPosition - 1f; if (temp.y > 9f) temp.y = 9f; if (temp.y < 7.3f) temp.y = 7.3f; }

                    else temp.y += Random.Range(0, 5);

                    instance.transform.position = temp;

                    //Debug.Log(instance.transform.position);

                    lastfloorPosition = temp.y;
                    
                    refFloor = instance;

                    float size = Random.Range(0.4f, 0.6f);

                    refFloor.transform.localScale = new Vector3(size, 1f, 1f);


                    PoolFree.Remove(refFloor);
                    PoolUsed.Add(refFloor);

                    break;

                }

            }

           

        }





        if (type == "gold" && goldrushmode == false)
		{

			int iterations = Random.Range(1, 4);

			float alignment = ((iterations-1) * 3) / 2;

			for (int i = 0 ; i < iterations; i++)
			{
				foreach (GameObject instance in PoolFree)
				{
				
					if (instance.tag == type && !PoolTemp.Contains(instance) )
					{
						instance.transform.position = refFloor.transform.position; 
						instance.transform.parent = refFloor.transform;
                        instance.transform.rotation = resetrot;

                        if (level == 0) instance.GetComponent<mover>().enabled = true;
                        else instance.GetComponent<mover>().enabled = false;

                        instance.SetActive(true);
						//instance.GetComponent<mover>().enabled = true;
						//ChangeLayerRecursively(instance, "items");

						PoolTemp.Add(instance);

						itemCreatedFlag = true;

						temp = instance.transform.position;

						temp.x += (3 * i) - alignment;

						instance.transform.position = temp;
						
						break;
						
					}
					
				}
			}

			foreach (GameObject instance in PoolTemp)
			{
				PoolFree.Remove(instance); 
				PoolUsed.Add(instance); 

			}

		}




		else 
		{
			foreach (GameObject instance in PoolFree)
			{


				if (instance.tag == type)
				{
					instance.transform.position = refFloor.transform.position; 
					instance.transform.parent = refFloor.transform;
                    instance.transform.rotation = resetrot;

                    if (level == 0) instance.GetComponent<mover>().enabled = true;
                    else instance.GetComponent<mover>().enabled = false;

                    instance.SetActive(true);
					//instance.GetComponent<mover>().enabled = true;
					//ChangeLayerRecursively(instance, "items");

					PoolFree.Remove(instance); 
					PoolUsed.Add(instance); 
					itemCreatedFlag = true;
                    

                    if (goldrushmode && type == "gold") {

                        instance.transform.position = this.transform.position;
                        instance.transform.parent = this.transform;

                        instance.GetComponent<mover>().homeinmode = true; 
						instance.GetComponent<mover>().target = maingame.GetComponent<game>().horse; 

						temp = instance.transform.position;

						temp.y += Random.Range(0, 10);
						temp.x += Random.Range(-5, 5);

						instance.transform.position = temp;

                        instance.GetComponent<mover>().enabled = true;



                    }

                    if (type == "lamp2" && level == 1)
                    {

                        lamp2spawned = true;
                        //Debug.Log("lamp2spawned = " + lamp2spawned);
                    }

                    if (type == "shurikan") {

                        if (game.sfxON) instance.GetComponent<AudioSource>().Play();


                        maingame.GetComponent<game>().PushMessage("DANGER !!", new Color(0.9f, 0.3f, 0.3f));
                        instance.GetComponent<mover>().enabled = true;
                        instance.transform.position = this.transform.position;
                        instance.transform.parent = this.transform;

                    }


                    if (type == "ghost")
                    {

                        if (game.sfxON) instance.GetComponent<AudioSource>().Play();

                        instance.GetComponent<mover>().enabled = true;
                        instance.transform.position = this.transform.position;
                        instance.transform.parent = this.transform;
                    }

                    if (type == "ufo" || type ==  "pumpkin")
                    {
                        instance.GetComponent<mover>().enabled = true;
                        instance.transform.position = this.transform.position;
                        instance.transform.parent = this.transform;

                    }


                    return;

				}

			}
		}


	}


   

    public void ReturnToPool(GameObject instance)
    {
        instance.SetActive(false);
        //instance.GetComponent<mover>().enabled = false;
        //ChangeLayerRecursively(instance, "hidden");

        //Quaternion resetrot = new Quaternion(0.0f,0.0f,0.0f,0.0f);
        instance.transform.position = this.transform.position;
        instance.transform.parent = this.transform;
        instance.transform.rotation = resetrot;



        //instance.rigidbody.velocity = Vector3.zero;
        //instance.rigidbody.angularVelocity = Vector3.zero;

        instance.GetComponent<mover>().homeinmode = false;

        if (instance.tag == "lamp2")
        {

            lamp2spawned = false;
            //Debug.Log("lamp2spawned = " + lamp2spawned);
        }


        PoolFree.Add(instance);
        PoolUsed.Remove(instance);
    }

        // Use this for initialization
        void Start () {
	

		counter = 1;

		cancelnextblocker = false;

		populatePool();

		goldrushmode = false;

        lamp2spawned = false;

    }
	
	// Update is called once per frame
	void Update () {
        //Debug.Log(counter);
	}
//	void ChangeLayerRecursively(GameObject obj , string name)
//	{
//
//		int lay = 0 ;
//
//		if (name == "items") lay = 11;
//		else if (name == "hidden") lay = 14;
//
//		obj.layer = lay;
//
//		if(obj.transform.GetChildCount() > 0)
//
//			foreach(Transform i in obj.transform)
//
//				ChangeLayerRecursively(i.gameObject, name);
//	}


	public void Spawn()  
	{  

		if (game.paused == true) return;

		itemCreatedFlag = false;

		//Debug.Log("Spawn");

		int upperlimit;

		int item;

		int group;

		int skip = 0;

		if (level == 1) skip = Random.Range(0,3);

		if (counter > 100)  upperlimit = 2;
		else if (counter > 50)  upperlimit = 3;
		else upperlimit = 4;

	

		group = Random.Range(1,upperlimit);

		while (cancelnextblocker && group == 1)
		{
			group = Random.Range(1,3);
		}



		if (goldrushmode) {


			for (int i = 1; i <7; i++) {

				GetFromPool("gold");
			}

			int sel = Random.Range(1,3); 
			if (sel == 1) GetFromPool("badgold");

			//Debug.Log ("gold rush");

		}


		else if (group == 1 && skip == 0 )  {

			item = Random.Range(1,5);

			if (item  < 4) {

				if (item ==1 && counter > 1) GetFromPool("barrel");
				if (item ==2 && counter > 40) GetFromPool("trunk");
				if (item ==3 && counter > 80) GetFromPool("cactus");
			
			
			}

			else if (item ==4) {

				if (maingame.GetComponent<game>().backgroundname == "dark")
                { GetFromPool("ufo"); }

                else 
				{
                    int seed1 = Random.Range(1, 30);
                    if (seed1 == 11 && maingame.GetComponent<game>().level > 4) GetFromPool("pumpkin");
                }

				
			
			}

			cancelnextblocker = true;

		}



		else {

				item = Random.Range(1,21);
                int seed = Random.Range(1, 3);

                

                if (item == 11 && counter > 10) { GetFromPool("badgold"); }
                
                else if (item == 14 && counter > 50 && level == 0) { GetFromPool("shurikan"); }
                else if (item == 15 && counter > 75 && level == 1) { GetFromPool("shurikan"); }
                else if (item == 20 && level == 1 && counter > 100 && seed == 1) { GetFromPool("ghost"); }

                else if (item == 19) { GetFromPool("empty"); }
                else if ((item > 0 && item < 21) && level == 1 && maingame.GetComponent<game>().lamp2ready) { GetFromPool("lamp2"); }
                else if ((item > 0 && item < 21) && level == 0 && maingame.GetComponent<game>().lamp2ready) { }

                else if (item == 12 && counter > 25 && seed == 1 && maingame.GetComponent<game>().level > 2) { GetFromPool("lamp"); }
                else if (item == 10 && counter > 20  && maingame.GetComponent<game>().level > 1 && level == 1) { GetFromPool("diamond"); }

                else if (item == 1) { GetFromPool("gold"); }
                else if (item == 2) { GetFromPool("gold"); }
                else if (item == 3 && level == 0) { GetFromPool("gold"); }
                else if (item ==4 && level == 0) {GetFromPool("gold"); }
				else if (item ==5 && level == 0) {GetFromPool("gold"); }
				else if (item ==6 && level == 0) {GetFromPool("gold"); }
				else if (item ==7 && level == 0) {GetFromPool("gold"); }
				else if (item ==8 && level == 1) {GetFromPool("gold"); }
				else if (item ==9) { GetFromPool("gold"); }

                else if (item == 16  && counter > 30 && seed == 1 && maingame.GetComponent<game>().lamp2ready == false && maingame.GetComponent<game>().level > 3) { GetFromPool("health"); }

                else if (item == 17) {  GetFromPool("gold");  }
                else if (item == 18) { GetFromPool("gold"); }



            cancelnextblocker = false;

		}


		//if ( level == 1 && itemCreatedFlag == true) {GetFromPool("floor");}

		counter++;


	}  


}
