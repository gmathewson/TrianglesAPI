using TrianglesAPI.Models;

namespace TrianglesAPI.Services
{
    public static class TriangleService
    {
        public static Triangle Get(string id) => new()
        {
            Id = id
        };

        
        public static Triangle Get(List<Coordinates> vertices) => new()
        {
            Vertices = vertices
        };


    }
}
