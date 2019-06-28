using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Scripts.Networking;

public class menu : MonoBehaviour
{
	public GameObject tallFull;
	public GameObject smallFull;
	public GameObject container;
	public Camera cam;
	public GameObject borderLeft,borderBot,borderRight;

	private GameObject spawn;
	int i=0;

    float timeBeforeClear = 30;


    private void Start()
    {

        Cursor.visible = true;

        Time.timeScale = 1.0f;
    }

    void Update()
	{
        timeBeforeClear -= Time.deltaTime;
        if (timeBeforeClear < 0)
        {
            timeBeforeClear = 30;
            Clear();
        }


        var dist = (transform.position - Camera.main.transform.position).z;

		var leftBorder = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, dist)).x;
		var rightBorder = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, dist)).x;
		var botBorder = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, dist)).y;

		Vector3 newL= new Vector3(leftBorder,borderLeft.transform.position.y,borderLeft.transform.position.z);
		borderLeft.transform.position =newL;
		Vector3 newR= new Vector3(rightBorder,borderRight.transform.position.y,borderRight.transform.position.z);
		borderRight.transform.position = newR;
		Vector3 newB= new Vector3(borderBot.transform.position.x,botBorder,borderBot.transform.position.z);
		borderBot.transform.position = newB;




		if(i<100){
			i++;
		}
		else{
			i=0;


			float r=Random.Range(0f,1f);
			if(r<0.5){
				spawn= Instantiate(tallFull, this.transform.position, Quaternion.identity);
				spawn.transform.parent = container.transform;
				spawn.GetComponent<Rigidbody2D>().AddForce(new Vector2 (Random.Range(-700.0f, 700.0f),Random.Range(-1000.0f, 0.0f))); 
			}
			else{
				spawn= Instantiate(smallFull, this.transform.position, Quaternion.identity);
				spawn.transform.parent = container.transform;
				spawn.GetComponent<Rigidbody2D>().AddForce(new Vector2 (Random.Range(-700.0f, 700.0f),Random.Range(-1000.0f, 0.0f))); 
			}
		}
	}



    public void Clear()
    {
        StartCoroutine(ClearRoutine());
    }

    IEnumerator ClearRoutine()
    {
        yield return new WaitForSeconds(0.5f);
        borderBot.GetComponent<BoxCollider2D>().enabled = false;
        yield return new WaitForSeconds(3f);
        borderBot.GetComponent<BoxCollider2D>().enabled = true;



    }

    void OnApplicationQuit()
    {
        if (ClientUDP.Instance.gameState != ClientUDP.OFFLINE)
            ClientUDP.Instance.sendTypedMessage(Message.ENDGAME);
    }

}
