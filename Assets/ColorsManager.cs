using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorsManager : SingletonDontDest<ColorsManager> {
    [SerializeField] private Color chaserUiColor;
    [SerializeField] private Color escapeeUiColor;
    [SerializeField] private Color chaserLightColor;
    [SerializeField] private Color escapeeLightColor;

    public Color ChaserUiColor { get { return chaserUiColor; }}
    public Color EscapeeUiColor { get { return escapeeUiColor; }}

    public Color ChaserLightColor { get { return chaserLightColor; }}
    public Color EscapeeLightColor { get { return escapeeLightColor; } }
}
