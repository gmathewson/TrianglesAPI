using TrianglesAPI.Models;
using System.Xml.Linq;



namespace TrianglesTest
{
    [TestClass]
    public class TriangleTests
    {

        [TestMethod]
        public void SetCoordinatesTest()
        {
            //This test iterates through all correct triangles in the dataset, and ensures that the correct triangle Id is returned given its coordinates
            List<List<string>> dataArray = LoadTestData();
            for (int i = 0; i < 72; i++)
            {
                //Load coordinates from testData 
                List<Coordinates> vertices = new List<Coordinates>
                {
                    new Coordinates(){XCoord = int.Parse(dataArray[i][2]), YCoord=int.Parse(dataArray[i][3])},
                    new Coordinates(){XCoord = int.Parse(dataArray[i][4]), YCoord=int.Parse(dataArray[i][5])},
                    new Coordinates(){XCoord = int.Parse(dataArray[i][6]), YCoord=int.Parse(dataArray[i][7])}
                };

                //Create Triangle
                Triangle testTriangle = new Triangle
                {
                    Vertices = vertices
                };
                //Test value of Triange ID vs expected value from dataset
                Assert.AreEqual(testTriangle.Id, dataArray[i][0] + dataArray[i][1]);
            }
        }

        [TestMethod]
        public void SetCoordinatesOutOfRangeTest()
        {
            //This test iterates through a range of incorrect triangles in the database, and ensures that the errors are handled.
            List<List<string>> dataArray = LoadTestData();
            for (int i = 72; i < dataArray.Count; i++)
            {
                List<Coordinates> vertices = new List<Coordinates>();
                try
                {
                    //Load coordinates from testData 
                    vertices.Add(new Coordinates { XCoord = int.Parse(dataArray[i][2]), YCoord = int.Parse(dataArray[i][3]) });
                    vertices.Add(new Coordinates { XCoord = int.Parse(dataArray[i][4]), YCoord = int.Parse(dataArray[i][5]) });
                    vertices.Add(new Coordinates { XCoord = int.Parse(dataArray[i][6]), YCoord = int.Parse(dataArray[i][7]) });
                }
                catch
                {
                    //do nothing.  This will result in an empty list of coordinates - which emulates the controller
                }
                //Create Triangle
                Triangle testTriangle = new Triangle
                {
                    Vertices = vertices
                };

                //Check that the Triangle Id is empty
                Assert.AreEqual(testTriangle.Id, "");

            }
        }

        [TestMethod]
        public void SetIdTest()
        {
            //This test iterates through all correct triangles in the dataset, and ensures that the correct triangle coordinates are returned given its id
            List<List<string>> dataArray = LoadTestData();
            for (int i = 0; i < 72; i++)
            {
                //Create Triangle
                Triangle testTriangle = new Triangle
                {
                    Id = dataArray[i][0] + dataArray[i][1]
                };
                //Create coordinates list from testData 
                List<Coordinates> vertices = new List<Coordinates>
                {
                    new Coordinates(){XCoord = int.Parse(dataArray[i][2]), YCoord = int.Parse(dataArray[i][3])},
                    new Coordinates(){XCoord = int.Parse(dataArray[i][4]), YCoord = int.Parse(dataArray[i][5])},
                    new Coordinates(){XCoord = int.Parse(dataArray[i][6]), YCoord = int.Parse(dataArray[i][7])}
                };

                //Test value of Triange vertices coordinates vs expected value from dataset
                Assert.IsTrue(vertices.Except(testTriangle.Vertices).Any());

            }
        }

        private static List<List<string>> LoadTestData()
        {
            //Load test data array from xml
            //<Row><Column><x1><y1><x2><y2><x3><y3
            List<List<string>> dataArray = new List<List<string>>();
            XElement[] testData = XDocument.Load(@"TrianglesTest\\TestData.xml").Descendants("TRIANGLE").ToArray();

            for (int i = 0; i < testData.Length; i++)
            {
                List<string> testItem = new List<string>
                {
                    testData[i].Descendants("Row").Single().Value.ToString(),
                    testData[i].Descendants("Column").Single().Value.ToString(),
                    testData[i].Descendants("x1").Single().Value.ToString(),
                    testData[i].Descendants("y1").Single().Value.ToString(),
                    testData[i].Descendants("x2").Single().Value.ToString(),
                    testData[i].Descendants("y2").Single().Value.ToString(),
                    testData[i].Descendants("x3").Single().Value.ToString(),
                    testData[i].Descendants("y3").Single().Value.ToString()
                };
                dataArray.Add(testItem);
                Assert.IsTrue(dataArray.Count > 0);
            }
            return dataArray;
        }
    }
}