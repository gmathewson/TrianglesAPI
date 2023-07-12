using Microsoft.AspNetCore.Mvc;
using TrianglesAPI.Models;
using TrianglesAPI.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TrianglesAPI.Controllers
{
    [Route("api/Triangles")]
    [ApiController]
    public class TrianglesController : ControllerBase
    {
        // GET: api/<TrianglesController>
        [HttpGet("GetVertices")]
        public ActionResult<Triangle> GetVertices(string id)
        {
            var triangle = TriangleService.Get(id);

            if (triangle.Vertices.Count == 0)
            {
                return BadRequest(id + " is not a valid Id.  Id is case sensitive and should lie between A1-F12");
            }
            return triangle;
        }

        // GET api/<TriangleController>/5
        [HttpGet("GetId")]
        public ActionResult<Triangle> GetId(string verticesString)
        {

            //split the string of the 3 pairs of coordinates at the place where the brackets join
            string[] vertexList = verticesString.Split(")(", StringSplitOptions.RemoveEmptyEntries);

            //Convert into coordinates
            List<Coordinates> vertices = new List<Coordinates>();

            try
            {
                for (int i = 0; i < 3; i++)
                {
                    //trim brackets off
                    vertexList[i] = vertexList[i].Replace("(", "");
                    vertexList[i] = vertexList[i].Replace(")", "");

                    vertices.Add(new Coordinates
                    {
                        XCoord = int.Parse(vertexList[i].Substring(0, vertexList[i].IndexOf(','))),
                        YCoord = int.Parse(vertexList[i].Substring(vertexList[i].IndexOf(',') + 1))
                    });
                }
            }
            catch (Exception)
            {
                return BadRequest(verticesString + " is not a valid format for entering coordinates \n" +
                        "Please enter coordinates in the format (x1,y1)(x2,y2)(x3,y3)");
            }

            //add to triangle - this will generate the triangleId
            Triangle triangle = TriangleService.Get(vertices);

            if (triangle.Id == "")
            {
                return BadRequest(verticesString + " does not form one of the given triangles");
            }
            return triangle;
        }
    }
}
