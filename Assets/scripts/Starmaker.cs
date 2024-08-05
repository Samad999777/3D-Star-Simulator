using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Starmaker : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject yelllowstar;
    public GameObject redstar;
    public GameObject bluestar;
    Vector3 start_position ;
    Vector3 scale; 
    ObjectNote note;
    ObjectNoteInGame note_game;


    void Start()
    {
        start_position = new Vector3(-13f, -0.97f, 0f);
        
        Csvreader reader = new Csvreader();
        Dictionary<string, Csvreader.Star> stars = reader.ReadCSV("stars");
        Vector3 spwan_pos = new Vector3(0f, 0f, 0f);
        int loop_index = 1;
        foreach (var star in stars)
        {
            scale = this.sizescaler(star.Value.radius);

            if (loop_index == 1)
            {
                spwan_pos = start_position;
            }
            else {
                spwan_pos.x = spwan_pos.x+scale.x + +2f;
            
            }


            CreatePrefabInstance(scale, star.Value.color,spwan_pos,star.Value.star_id,star.Value.temperature.ToString(),star.Value.radius.ToString());
            loop_index++;
        }

        //CreatePrefabInstance(2.5f, "yellow");
    }

    public void CreatePrefabInstance(Vector3 star_size, string star_color,Vector3 pos,string name,string temp,string radi)
    {
        
        GameObject prefab=this.yelllowstar;
       
       
        

        if(star_color =="yellow"||star_color == "yellow-white")
        {
            prefab=this.yelllowstar;
            
        }
        else if(star_color =="blue" )
        {
            prefab = this.bluestar;
        }
        else if (star_color == "red")
        {
            prefab = this.redstar;
        }
        
        
            GameObject instance = Instantiate(prefab, pos, Quaternion.identity);
            note = instance.GetComponent<ObjectNote>();
            note_game=instance.GetComponent<ObjectNoteInGame>();
        if (note != null)
        {
            Debug.Log("not null");

        }
        else if (note == null) {
            Debug.Log("null");
        }
            note_game.Text = "Star id: " + name + "\nStar Radius: " + radi+"R" + "\nStar Temp: " + temp+"K";
            instance.transform.localScale = star_size;
            
        
        
    }

    Vector3 sizescaler(float star_size)
    {
        float scaled_size = 2f;
        // Taking 1 Solar mass as scale =2f
        if (star_size >= 1f)
        {
            scaled_size = star_size-1f;

          scaled_size = star_size + 2f;
        }
        else if(star_size < 1f)
        {
            scaled_size = star_size-1f;
            scaled_size = 2f+star_size;
        }
            
        Vector3 size = new Vector3(scaled_size, scaled_size, scaled_size);
        return size;
       

    }

    public void playMusic()
    {
        Debug.Log("Music Player");
    }




}
