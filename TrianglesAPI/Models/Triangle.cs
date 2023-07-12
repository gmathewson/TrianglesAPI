namespace TrianglesAPI.Models
{
    public class Triangle
    {
        private string id = "";
        private List<Coordinates> vertices = new();


        public string Id
        {
            get => id;
            set
            {
                id = value;
                CalculateCoordinates();
            }
        }

        public List<Coordinates> Vertices
        {
            get => vertices;
            set
            {
                vertices = value;
                CalculateId();
            }
        }

        private void CalculateCoordinates()
        {
            //given location id calculate coordinates
            //start in bottom left (F1) 
            List<Coordinates> coordinatesbuilder = new()
            {
                new Coordinates() { XCoord = 0, YCoord = 0 },
                new Coordinates() { XCoord = 10, YCoord = 0 },
                new Coordinates() { XCoord = 0, YCoord = 10 }
            };
            //if the column is an even number, set the x and y of the firt pair of coordinates to 10
            try
            {
                if (int.Parse(id.Substring(1)) % 2 == 0)
                {
                    coordinatesbuilder[0] = new Coordinates() { XCoord = 10, YCoord = 10 };
                }

                //switch on column, adding to the x coordinate

                switch (int.Parse(id.Substring(1)))
                {
                    case 1:
                    case 2:
                        break;
                    case 3:
                    case 4:
                        coordinatesbuilder = AddToX(coordinatesbuilder, new List<int> { 10, 10, 10 });
                        break;
                    case 5:
                    case 6:
                        coordinatesbuilder = AddToX(coordinatesbuilder, new List<int> { 20, 20, 20 });
                        break;
                    case 7:
                    case 8:
                        coordinatesbuilder = AddToX(coordinatesbuilder, new List<int> { 30, 30, 30 });
                        break;
                    case 9:
                    case 10:
                        coordinatesbuilder = AddToX(coordinatesbuilder, new List<int> { 40, 40, 40 });
                        break;
                    case 11:
                    case 12:
                        coordinatesbuilder = AddToX(coordinatesbuilder, new List<int> { 50, 50, 50 });
                        break;
                    default:
                        throw new Exception();  //invalid id

                }
                //switch on row, and add to y coordinate
                if (coordinatesbuilder.Count == 3)
                {
                    switch (id.Substring(0, 1))

                    {
                        case "F": break;
                        case "E":
                            coordinatesbuilder = AddToY(coordinatesbuilder, new List<int> { 10, 10, 10 });
                            break;
                        case "D":
                            coordinatesbuilder = AddToY(coordinatesbuilder, new List<int> { 20, 20, 20 });
                            break;
                        case "C":
                            coordinatesbuilder = AddToY(coordinatesbuilder, new List<int> { 30, 30, 30 });
                            break;
                        case "B":
                            coordinatesbuilder = AddToY(coordinatesbuilder, new List<int> { 40, 40, 40 });
                            break;
                        case "A":
                            coordinatesbuilder = AddToY(coordinatesbuilder, new List<int> { 50, 50, 50 });
                            break;
                        default:
                            throw new Exception();

                    }
                }
            }
            catch (Exception)
            {
                coordinatesbuilder.RemoveRange(0, 3);  //if the id is invalid - return an empty set
            }

            vertices = coordinatesbuilder;

        }

        private void CalculateId()
        {
            List<int> x_coords = new();
            List<int> y_coords = new();

            string row;
            string column;
            //given coordinates; calculate ID
            //Get individual lists of the x and y coordinates
            try
            {
                for (int i = 0; i < 3; i++)
                {
                    x_coords.Add(vertices[i].XCoord);
                    y_coords.Add(vertices[i].YCoord);

                }

                //check the y coordinates to get the row;
                if (y_coords.Contains(0) && y_coords.Contains(10)) { row = "F"; }
                else if (y_coords.Contains(10) && y_coords.Contains(20)) { row = "E"; }
                else if (y_coords.Contains(20) && y_coords.Contains(30)) { row = "D"; }
                else if (y_coords.Contains(30) && y_coords.Contains(40)) { row = "C"; }
                else if (y_coords.Contains(40) && y_coords.Contains(50)) { row = "B"; }
                else if (y_coords.Contains(50) && y_coords.Contains(60)) { row = "A"; }
                else throw new Exception();

                //check the x coordinates to get the column
                //the sum of the x coordinates will tell you if its an odd or even column
                if (x_coords.Contains(0) && x_coords.Contains(10) && x_coords.Sum() == 10) { column = "1"; }
                else if (x_coords.Contains(0) && x_coords.Contains(10) && x_coords.Sum() == 20) { column = "2"; }
                else if (x_coords.Contains(10) && x_coords.Contains(20) && x_coords.Sum() == 40) { column = "3"; }
                else if (x_coords.Contains(10) && x_coords.Contains(20) && x_coords.Sum() == 50) { column = "4"; }
                else if (x_coords.Contains(20) && x_coords.Contains(30) && x_coords.Sum() == 70) { column = "5"; }
                else if (x_coords.Contains(20) && x_coords.Contains(30) && x_coords.Sum() == 80) { column = "6"; }
                else if (x_coords.Contains(30) && x_coords.Contains(40) && x_coords.Sum() == 100) { column = "7"; }
                else if (x_coords.Contains(30) && x_coords.Contains(40) && x_coords.Sum() == 110) { column = "8"; }
                else if (x_coords.Contains(40) && x_coords.Contains(50) && x_coords.Sum() == 130) { column = "9"; }
                else if (x_coords.Contains(40) && x_coords.Contains(50) && x_coords.Sum() == 140) { column = "10"; }
                else if (x_coords.Contains(50) && x_coords.Contains(60) && x_coords.Sum() == 160) { column = "11"; }
                else if (x_coords.Contains(50) && x_coords.Contains(60) && x_coords.Sum() == 170) { column = "12"; }
                else throw new Exception();

                // Check that the triangle is sloping in the right direction
                //in triangles with correct orientation the pattern is the same between x and y coordinates
                //ie x:{min, max, max} y:{min, max, max} or x{min, min, max} y:{min, min, max}
                //if incorrect they are different x{min, min, max} y{min, max, max} or x{min max max} y{min min max}
                x_coords.Sort();
                y_coords.Sort();
                if ((x_coords[1] == x_coords.Max() && y_coords[1] == y_coords.Min()) || x_coords[1] == x_coords.Min() && y_coords[1] == y_coords.Max())
                {
                    throw new Exception();
                }
            }
            catch (Exception)
            {
                row = "";
                column = "";
            }

            id = row + column;

        }

        private static List<Coordinates> AddToY(List<Coordinates> coords, List<int> y_add)
        {
            for (int i = 0; i < 3; i++)
            {
                coords[i].YCoord += y_add[i];
            }
            return coords;
        }
        private static List<Coordinates> AddToX(List<Coordinates> coords, List<int> x_add)
        {
            for (int i = 0; i < 3; i++)
            {
                coords[i].XCoord += x_add[i];
            }
            return coords;
        }


    }
}

