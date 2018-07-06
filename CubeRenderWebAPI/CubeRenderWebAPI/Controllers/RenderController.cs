using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RenderCube;

namespace CubeRenderWebAPI.Controllers
{
    [Route("api/[controller]")]
    public class RenderController : Controller
    {
        // GET api/render/8/7/6
        [HttpGet("{x}/{y}/{z}")]
        public IActionResult Get(int x, int y, int z)
        {
            return Render(x, y, z);
        }

        // POST api/Render
        [HttpPost]
        public IActionResult Post([FromBody] RenderRequest renderRequest)
        {
            var x = renderRequest.x;
            var y = renderRequest.y;
            var z = renderRequest.z;

            return Render(x, y, z);
        }

        private IActionResult Render(int x, int y, int z)
        {
            Cube cube = new Cube(Point.Zero, x, y, z);

            Canvas canvas = new Canvas(x + z * 2, y + z * 2);
            CubeRenderer.Render(canvas, cube);

            RenderResult renderResult = new RenderResult(canvas.Render(), canvas.Width, canvas.Height);

            return Ok(renderResult);
        }
    }
}
