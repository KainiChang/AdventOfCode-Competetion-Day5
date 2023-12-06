namespace code;

public class InputHandler
{

    // store lines of strings in a 2d array
    public static int[,] ReadInput2DArray(string input)
    {
        // Split the input into rows
        string[] rows = input.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

        int numRows = rows.Length;
        int numCols = rows[0].Length;
        var heightMap = new int[numRows, numCols];

        for (int i = 0; i < numRows; i++)
        {
            for (int j = 0; j < numCols; j++)
            {
                heightMap[i, j] = rows[i][j] - '0'; // Convert char to int
            }
        }
        return heightMap;
    }
    public static string[] ReadInputLines(string input)
    {
        // Split the input into rows
        string[] rows = input.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
        return rows;
    }

        
}