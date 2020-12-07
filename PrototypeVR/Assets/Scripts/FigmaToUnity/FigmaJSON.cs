using System;
using System.Collections.Generic;

[Serializable]
public class FigmaData
{
    public string startingFrame;
    public List<FrameData> frames;
}

[Serializable]
public class FrameData
{
    public string id;
    public string name;
    public int width;
    public int height;
    public List<HotspotData> hotspots;
}

[Serializable]
public class HotspotData
{
    public string name;
    public int x;
    public int y;
    public int w;
    public int h;
    public bool visible;
    public TransitionData transition;
}

[Serializable]
public class TransitionData
{
    public string eventType;
    public string actionType;
    public List<string> parameters;
}
