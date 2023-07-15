# TrianglesAPI
Load TrianglesAPI in visual studio and run.  This creates a Web API endpoint at https://localhost:[port]/api/Triangles/
When the code is ran from VisualStudio this is delivered by swagger at https://localhost[port]/swagger/index.html

GetVertices accepts Triangle Id between A1 and F12, and returns Json of the triangle object which contains the triangle ID and a list of the 3 pairs of coordinates.  
Triangle Id is not case sensitive, and if an incorrect id is entered an error message will be returned.

GetId accepts vertices in the form (x1,y1)(x2,y2)(x3,y3), and returns Json of the triangle object which contains the triangle ID and a list of the 3 pairs of coordinates
An error message is returned if the coordinates are not in the correct format, or if they do not represent one of the given triangles.

Solution also contains an MS Test project, TrianglesTest which loads testdata from an xml document.  
The first 72 tests are the correct triangles.  
The remaining tests cover a variety of scenarios, to ensure that the code can handle them.

Tests check that:
 - for all correct ids, the correct coordinates are returned.
 - for all correctly inputed vertices, the correct id is returned.
 - no triangle is returned for incorrectly entered information / coordinates that don't form one of the given triangles.

