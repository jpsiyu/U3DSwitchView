using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MyView : MonoBehaviour {

    private UIButton left;
    private UIButton right;

    private GameObject tagLaugh;
    private GameObject tagAngry;
    private GameObject tagBad;
    private GameObject tagGo;
    private UITable table;

    private GameObject iconPrefab;
    private List<GameObject> iconCacheList;

    private int currentIndex;
    private IView curView;

    void Awake() {
        left = transform.FindChild("offset/Left").GetComponent<UIButton>();
        right = transform.FindChild("offset/Right").GetComponent<UIButton>();
        tagLaugh = transform.FindChild("offset/TagLaugh").gameObject;
        tagAngry = transform.FindChild("offset/TagAngry").gameObject;
        tagGo = transform.FindChild("offset/TagGo").gameObject;
        tagBad = transform.FindChild("offset/TagBad").gameObject;
        table = transform.FindChild("offset/TagIcons").GetComponent<UITable>();

        string path = "Assets/Prefabs/Icon.prefab";
        iconPrefab = Resources.LoadAssetAtPath(path, typeof(GameObject)) as GameObject;
    }

    private GameObject AddPrefab(Transform parent, GameObject prefab)
    {
        GameObject go = Instantiate(prefab, parent.position, Quaternion.identity) as GameObject;
        go.transform.SetParent(parent);
        return go;
    }

    private GameObject GetIconFromCache(int index) {
        if (index >= iconCacheList.Count) {
            GameObject go = AddPrefab(table.transform, iconPrefab);
            iconCacheList.Add(go);
            return go;
        }else{
            return iconCacheList[index];
        }
    }

	// Use this for initialization
	void Start () {
        EventDelegate.Add(left.onClick, delegate { Left(); });
        EventDelegate.Add(right.onClick, delegate { Right(); });

        iconCacheList = new List<GameObject>();

        UpdateView();
	}

    private void Left() {
        if (currentIndex <= 0) return;
        currentIndex--;
        UpdateView();
    }

    private void Right() {
        if (currentIndex >= ViewDataMgr.Instance.GetViewDatas().Count-1) return;
        currentIndex++;
        UpdateView();
    }

    private void UpdateView() { 
        List<ViewData> datas = ViewDataMgr.Instance.GetViewDatas();

        if (curView != null) curView.Hide();

        curView = ChooseView(datas[currentIndex]);
        curView.UpdateData(datas[currentIndex]);
        curView.UpdateView();

        UpdateIcons();
    }

    private void UpdateIcons() {
        List<ViewData> datas = ViewDataMgr.Instance.GetViewDatas();
        for(int i = 0; i < datas.Count; i++){
            GameObject go = GetIconFromCache(i);
            go.transform.localScale = new Vector3(1f, 1f, 1f);
            if(currentIndex == i)
                go.GetComponent<UISprite>().spriteName = "Button X";
            else 
                go.GetComponent<UISprite>().spriteName = "Button A";
        }
        table.Reposition();
    }

    private IView ChooseView(ViewData data)
    {
        IView view = null;
        switch (data.Tag) { 
            case TagType.Laugh:
                view = new LaughView(tagLaugh);
                break;
            case TagType.Angry:
                view = new AngryView(tagAngry);
                break;
            case TagType.Bad:
                view = new BadView(tagBad);
                break;
            case TagType.Go:
                view = new GoView(tagGo);
                break;
            default:
                break;
        }
        return view;
    }


}

#region views

public interface IView {
    void UpdateView();
    void UpdateData(ViewData data);
    void Hide();
}

public class AngryView:IView { 
    private GameObject go;
    private ViewData data;

    private UILabel label;
    private UISprite sprite;

    public AngryView(GameObject go) {
        this.go = go;
    }

    public void UpdateData(ViewData data) {
        this.data = data;
        BindElemens();

    }

    private void BindElemens()
    {
        label = go.transform.FindChild("Label").GetComponent<UILabel>();
        sprite = go.transform.FindChild("Sprite").GetComponent<UISprite>();
    }

    public void UpdateView() {
        go.SetActive(true);
        label.text = data.Title;
        sprite.spriteName = data.SpriteName;
    }

    public void Hide() {
        go.SetActive(false);
    }
}

public class LaughView : IView
{
    private GameObject go;
    private ViewData data;

    private UILabel label;
    private UISprite sprite;

    public LaughView(GameObject go)
    {
        this.go = go;
        BindElemens();

    }

    public void UpdateData(ViewData data) {
        this.data = data;
    }

    private void BindElemens()
    {
        label = go.transform.FindChild("Label").GetComponent<UILabel>();
        sprite = go.transform.FindChild("Sprite").GetComponent<UISprite>();
    }

    public void UpdateView()
    {
        go.SetActive(true);
        label.text = data.Title;
        sprite.spriteName = data.SpriteName;
    }

    public void Hide()
    {
        go.SetActive(false);
    }
}

public class GoView : IView
{
    private GameObject go;
    private ViewData data;

    private UILabel label;
    private UISprite sprite;

    public GoView(GameObject go)
    {
        this.go = go;
        BindElemens();

    }

    public void UpdateData(ViewData data) {
        this.data = data;
    }

    private void BindElemens()
    {
        label = go.transform.FindChild("Label").GetComponent<UILabel>();
        sprite = go.transform.FindChild("Sprite").GetComponent<UISprite>();
    }

    public void UpdateView()
    {
        go.SetActive(true);
        label.text = data.Title;
        sprite.spriteName = data.SpriteName;
    }

    public void Hide()
    {
        go.SetActive(false);
    }
}

public class BadView : IView
{
    private GameObject go;
    private ViewData data;

    private UILabel label;
    private UISprite sprite;

    public BadView(GameObject go)
    {
        this.go = go;
        BindElemens();
    }

    public void UpdateData(ViewData data) {
        this.data = data;
    }

    private void BindElemens()
    {
        label = go.transform.FindChild("Label").GetComponent<UILabel>();
        sprite = go.transform.FindChild("Sprite").GetComponent<UISprite>();
    }

    public void UpdateView()
    {
        go.SetActive(true);
        label.text = data.Title;
        sprite.spriteName = data.SpriteName;
    }

    public void Hide()
    {
        go.SetActive(false);
    }
}
#endregion