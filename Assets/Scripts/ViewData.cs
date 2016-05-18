using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ViewDataMgr {
    private static ViewDataMgr _instance;
    private ViewDataMgr() { }
    public static ViewDataMgr Instance {
        get {
            if (_instance == null)
                _instance = new ViewDataMgr();
            return _instance;
        }
    }

    public List<ViewData> GetViewDatas() {
        List<ViewData> datas = new List<ViewData>();
        for (int i = 0; i < 10; i++) {
            TagType tag = RandomTagType(i);
            datas.Add(new ViewData(string.Format("I am Tag{0:D2}", i), RandomTagType(i)));
        }
        return datas;
    }

    private TagType RandomTagType(int index) {
        TagType tag = TagType.Angry;
        switch (index % 4) { 
            case 0:
                tag = TagType.Angry;
                break;
            case 1:
                tag = TagType.Bad;
                break;
            case 2:
                tag = TagType.Go;
                break;
            case 3:
                tag = TagType.Laugh;
                break;
            default:
                break;
        }
        return tag;
    }
}

public class ViewData  {
    private string title;
    private string spriteName;
    private TagType tag;

    public string Title
    {
        get { return title; }
    }

    public string SpriteName
    {
        get { return spriteName; }
    }

    public TagType Tag
    {
        get { return tag; }
    }

    public ViewData(string titleStr, TagType tagType) {
        title = titleStr;
        tag = tagType;
        ChooseSprite();
    }

    private void ChooseSprite() {
        switch (tag) { 
            case TagType.Angry:
                spriteName = "Emoticon - Angry";
                break;
            case TagType.Bad:
                spriteName = "Emoticon - Smirk";
                break;
            case TagType.Go:
                spriteName = "Emoticon - Rambo";
                break;
            case TagType.Laugh:
                spriteName = "Emoticon - Laugh";
                break;
            default:
                break;
        }
    }
}

public enum TagType { 
    Laugh,
    Angry,
    Go,
    Bad,
}
