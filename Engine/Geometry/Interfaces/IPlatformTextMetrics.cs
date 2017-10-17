namespace Engine
{
    /// <summary>
    /// https://stackoverflow.com/a/6708492/7004229
    /// </summary>
    public interface IPlatformTextMetrics
    {
        Size2D MeasureString(string text, RenderFont font, TextFormat format, int width);

        Size2D MeasureString(string text, RenderFont font, int width);

        Size2D MeasureStringClose(string text, RenderFont font, int width);
    }
}
