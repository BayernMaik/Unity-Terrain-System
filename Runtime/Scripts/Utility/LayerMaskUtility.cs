public static class LayerMaskUtility
{
    public static LayerMaskData[] LayerMasks = new LayerMaskData[]
    {
        new LayerMaskData("Default", (1 << 0), 0), // 0 - Default Layer
        new LayerMaskData("TransparentFX", (1 << 1), 1), // 1 - Default Layer
        new LayerMaskData("Ignore Raycast", (1 << 2), 2), // 2 - Default Layer
        new LayerMaskData("Default", (1 << 3), 3), // 3 - Default Layer
        new LayerMaskData("Default", (1 << 4), 4), // 4 - Default Layer
        new LayerMaskData("Default", (1 << 5), 5), // 5 - Default Layer
        new LayerMaskData("Default", (1 << 6), 6), // 6 - Default Layer
        new LayerMaskData("Default", (1 << 7), 7), // 7 - Default Layer
        new LayerMaskData("Default", (1 << 8), 8), // 8 - Default Layer
        new LayerMaskData("Default", (1 << 9), 9), // 9 - Default Layer
        new LayerMaskData("Default", (1 << 10), 10), // 10 - Default Layer
        new LayerMaskData("Default", (1 << 11), 11), // 11 - Default Layer
        new LayerMaskData("Default", (1 << 12), 12), // 12 - Default Layer
        new LayerMaskData("Default", (1 << 13), 13), // 13 - Default Layer
        new LayerMaskData("Default", (1 << 14), 14), // 14 - Default Layer
        new LayerMaskData("Default", (1 << 15), 15), // 15 - Default Layer
        new LayerMaskData("Default", (1 << 16), 16), // 16 - Default Layer
        new LayerMaskData("Default", (1 << 17), 17), // 17 - Default Layer
        new LayerMaskData("Default", (1 << 18), 18), // 18 - Default Layer
        new LayerMaskData("Default", (1 << 19), 19), // 19 - Default Layer
    };

    public static int TerrainID { get { return 8; } }
    public static int TerrainObjectsID { get { return 9; } }
}
public struct LayerMaskData
{
    private string _layerName;
    private int _layerBitMask;
    private int _layerID;
    public LayerMaskData(string layerName, int layerBitMask, int layerInt)
    {
        this._layerName = layerName;
        this._layerBitMask = layerBitMask;
        this._layerID = layerInt;
    }
    public string name { get { return _layerName; } set { _layerName = value; } }
    public int bitMask { get { return _layerBitMask; } set { _layerBitMask = value; } }
    public int id { get { return _layerID; } set { _layerID = value; } }
}
