using UnityEngine;

public class MusicMat : MonoBehaviour {
    public bool flashOnBeat = false;

    private Color defaultColor;
    private Material material_left;
    private Material material_right;
    private Transform childTransform_left;
    private Transform childTransform_right;
    private Vector3 defaultPosition_left;
    private Vector3 defaultPosition_right;
    private float beatDisplacement = .1f;
    private Clock clock;


    /*
        Made a quick work-around so that there are two different backgrounds that pulse and change color instead of one.
        This is a poor implementation, and should be fixed in the future!
    */
    //TODO: Fix the poor code for the two backgrounds!
    void Awake() {
        childTransform_left = transform.GetChild(0).transform;
        childTransform_right = transform.GetChild(1).transform;
        defaultPosition_left = childTransform_left.localPosition;
        defaultPosition_right = childTransform_right.localPosition;
        material_left = childTransform_left.GetComponent<Renderer>().material;
        material_right = childTransform_right.GetComponent<Renderer>().material;
        defaultColor = material_left.color;
        clock = FindObjectOfType<Clock>();
    }

    void Update() {
        var time = Time.deltaTime * (1 / clock.HalfInterval);
        material_left.color = Color.Lerp(material_left.color, defaultColor, time);
        material_right.color = Color.Lerp(material_right.color, defaultColor, time);
        childTransform_left.localPosition = Vector3.Lerp(childTransform_left.localPosition, defaultPosition_left, time);
        childTransform_right.localPosition = Vector3.Lerp(childTransform_right.localPosition, defaultPosition_right, time);
    }

    void OnEnable() {
        var constants = FindObjectOfType<Constants>();
        Clock.OnBeat += delegate { Flash(constants.onbeatColor); };
        Clock.DownBeat += delegate { Flash(constants.downbeatColor); };
    }

    private void Flash(Color color) {
        material_left.color = color;
        material_right.color = color;
        childTransform_left.localPosition = defaultPosition_left + new Vector3(0, 0, 1f) * beatDisplacement;
        childTransform_right.localPosition = defaultPosition_right + new Vector3(0, 0, 1f) * beatDisplacement;
    }
}
