namespace CubeRenderWebAPI.Controllers
{
    public class RenderResult
    {
        public string rendered;
        public int width;
        public int height;

        public RenderResult(string rendered, int width, int height)
        {
            this.rendered = rendered;
            this.width = width;
            this.height = height;
        }
    }
}