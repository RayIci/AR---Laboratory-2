using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Windows;

public class ToggleTextures : MonoBehaviour
{

    private int textureIndex = 0;
    private List<string> textures = new List<string>();

    private void SetTexture(string texturePath)
    {
        if (System.IO.File.Exists(texturePath))
        {
            byte[] bytes = System.IO.File.ReadAllBytes(texturePath);
            Texture2D texture = new Texture2D(100, 100);
            texture.filterMode = FilterMode.Trilinear;
            texture.LoadImage(bytes);
            MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
            meshRenderer.material.SetTexture("_MainTex", texture);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        var TextureListInDIrectory = System.IO.Directory.GetFiles("./Assets/Textures/", "*", SearchOption.AllDirectories);
        foreach (var Texture in TextureListInDIrectory) 
        {
            if (Texture.EndsWith(".jpg"))
                textures.Add(Texture);
        }

        if (textures.Count > 0)
        {
            SetTexture(textures[textureIndex]);
            textureIndex++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (UnityEngine.Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(UnityEngine.Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                //Select stage    
                if (hit.transform.name == "Texture Cube")
                {
                    textureIndex = textureIndex % textures.Count;
                    SetTexture(textures[textureIndex]);
                    textureIndex++;
                }
            }
        }
    }
}
