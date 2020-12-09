using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FigmaToCanvas : MonoBehaviour
{
    public Canvas canvas;
    public ImportFigmaJSON figma;
    public Renderer mRenderer;
    public Button buttonPrefab;
    public string imgRoot;

    // Store width and height of figma and Unity canvas dimensions
    private float cWidth;
    private float cHeight;

    private List<FrameData> frames;


    // Start is called before the first frame update
    void Start()
    {
        // Store dimensions of Unity canvas
        cWidth = canvas.GetComponent<RectTransform>().rect.width;
        cHeight = canvas.GetComponent<RectTransform>().rect.height;

        ResetButtonsAndTransitions();

        frames = figma.GetFrames();

        foreach (var frame in frames)
        {
            if (frame.name == figma.GetStartingFrame())
            {
                RenderFrame(frame.id);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RenderFrame(string id)
    {

        // test - get frame texture on there and button as well
        FrameData frame = GetFrame(id);

        if (frame == null)
        {
            Debug.Log("Frame " + id + " not found!");
            return;
        }

        // set texture to Assets/figma_pictures/Test/[framename].png
        string path = imgRoot + frame.name + ".png";
        byte[] data = System.IO.File.ReadAllBytes(path);
        Texture2D tex = new Texture2D(2, 2);
        tex.LoadImage(data);

        mRenderer.material.SetTexture("_MainTex", tex);

        ResetButtonsAndTransitions();

        // loop through hotspots and populate canvas with invButtons
        foreach (var hotspot in frame.hotspots)
        {
            AddButtonToFrame(hotspot, frame, hotspot.visible);
        }
    }

    /* Adds a button with its transition either to a figma UI button or physical button based on its visibility */
    private void AddButtonToFrame(HotspotData hotspot, FrameData frame, bool visible)
    {
        // Add button to frame if visible
        if (visible)
        {
            float xFrac = hotspot.x / (float)frame.width;
            float yFrac = hotspot.y / (float)frame.height;
            float wFrac = hotspot.w / (float)frame.width;
            float hFrac = hotspot.h / (float)frame.height;

            float bX = xFrac * cWidth;
            float bY = yFrac * cHeight;
            float bW = wFrac * cWidth;
            float bH = hFrac * cHeight;

            Button button = Instantiate(buttonPrefab);
            button.gameObject.name = hotspot.name;
            RectTransform rect = button.GetComponent<RectTransform>();

            // Set button (and collider) size to be scaled to button size in figma screen
            rect.SetParent(canvas.transform, false);
            rect.anchoredPosition = new Vector3(bX, -bY, 0);
            rect.sizeDelta = new Vector2(bW, bH);

            BoxCollider button_collider = button.GetComponent<BoxCollider>();
            button_collider.size = new Vector3(bW, bH, 1f);
            button_collider.center = new Vector3(bW / 2f, -bH / 2f, 0);

            string transitionID = hotspot.transition.parameters[0];
            

            // For each invButton, set onclick to renderframe of id of frame transition goes to
            button.onClick.AddListener(delegate { RenderFrame(transitionID); });

            // Also set transitionID in Figmainteractable so that if VRPointer collides with the button, we also transition
            button.GetComponent<FigmaInteractable>().transitionID = transitionID;


        } else // set the right transition for the physical object
        {
            GameObject intObj = GameObject.Find(hotspot.name);
            if (intObj != null)
            {
                string transitionID = hotspot.transition.parameters[0];
                FigmaInteractable interactable = intObj.GetComponent<FigmaInteractable>();
                interactable.transitionID = transitionID;
            }
        }
    }

    private FrameData GetFrame(string id)
    {
        foreach(FrameData frame in this.frames)
        {
            if (frame.id == id)
            {
                return frame;
            }
        }
        return null;
    }

    private void ResetButtonsAndTransitions()
    {
        // delete all current children buttons of the canvas
        foreach (Transform child in canvas.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
        // Reset all transitions of physical buttons
        foreach (FigmaInteractable interactable in FindObjectsOfType<FigmaInteractable>())
        {
            interactable.transitionID = "-1";
        }
    }
    public void printButtonPressed(string button)
    {
        Debug.Log(button + " pressed!");
    }
}
