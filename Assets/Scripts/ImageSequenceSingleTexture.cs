using UnityEngine;  
using System.Collections;  

public class ImageSequenceSingleTexture : MonoBehaviour  
{  
	//A texture object that will output the animation  
	private Texture texture;  
	//With this Material object, a reference to the game object Material can be stored  
	private Material goMaterial;  
	//An integer to advance frames  
	private int frameCounter = 0;  
	
	//A string that holds the name of the folder which contains the image sequence  
	public string folderName;  
	//The name of the image sequence  
	public string imageSequenceName;  
	//The number of frames the animation has  
	public int numberOfFrames;  
	
	//The base name of the files of the sequence  
	private string baseName;  

	public bool jumping;

	private Texture[] avatartextures ;

	void loadTextures() {

		avatartextures = new Texture[numberOfFrames];

		for (int i = 0; i< numberOfFrames; i++){


			avatartextures[i] = (Texture)Resources.Load(baseName + i.ToString("D5"), typeof(Texture)); 


		}



	}
	
	void Awake()  
	{  
		//Get a reference to the Material of the game object this script is attached to  
		this.goMaterial = this.GetComponent<Renderer>().material;  
		//With the folder name and the sequence name, get the full path of the images (without the numbers)  
		this.baseName = this.folderName + "/" + this.imageSequenceName;  
	}

	public void resetFolder() {

		this.baseName = this.folderName + "/" + this.imageSequenceName;  

		loadTextures();

	}
	
	void Start ()  
	{  
		//set the initial frame as the first texture. Load it from the first image on the folder  
		//texture = (Texture)Resources.Load(baseName + "00000", typeof(Texture));  
		loadTextures();

		jumping = false;

	}  
	
	void FixedUpdate ()  
	{  

		//Debug.Log(paused);
		if (jumping == true) StartCoroutine("PlayLoop", 0.13f);  
		else StartCoroutine("PlayLoop", 0.0167f); 


		//Set the material's texture to the current value of the frameCounter variable  
		goMaterial.mainTexture = this.texture;  
	}  
	
	//The following methods return a IEnumerator so they can be yielded:  
	//A method to play the animation in a loop  
	IEnumerator PlayLoop(float delay)  
	{  
		//wait for the time defined at the delay parameter  
		yield return new WaitForSeconds(delay);    
		
		//advance one frame  
		frameCounter = (++frameCounter)%numberOfFrames;  
		
		//load the current frame  
		//this.texture = (Texture)Resources.Load(baseName + frameCounter.ToString("D5"), typeof(Texture));  
		this.texture = avatartextures[frameCounter];

		
		//Stop this coroutine  
		StopCoroutine("PlayLoop");  
	}  
	
	//A method to play the animation just once  
	IEnumerator Play(float delay)  
	{  
		//wait for the time defined at the delay parameter  
		yield return new WaitForSeconds(delay);    
		
		//if it isn't the last frame  
		if(frameCounter < numberOfFrames-1)  
		{  
			//Advance one frame  
			++frameCounter;  
			
			//load the current frame  
			this.texture = (Texture)Resources.Load(baseName + frameCounter.ToString("D5"), typeof(Texture));  
		}  
		
		//Stop this coroutine  
		StopCoroutine("Play");  
	}  
}  