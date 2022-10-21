// See https://aka.ms/new-console-template for more information

// bounds = {{вг1, вг2, вг3...}, вг1 - верхняя граница первого измерения
//          {нг1, нг2, нг3...}}  нг1 - аналогично вг1

Console.Write("Введите количество измерений: ");
string input = Console.ReadLine();
int dimensions = Convert.ToInt32(input);
int[,] bounds = new int[2, dimensions];

Console.WriteLine("Введите нижнюю и верхнюю границу для:");
for (int i = 0; i < dimensions; i++)
{
    Console.Write($"{i + 1}-го измерения: ");
    string[] input2 = Console.ReadLine().Split();
    bounds[1, i] = Convert.ToInt32(input2[0]);
    bounds[0, i] = Convert.ToInt32(input2[1]);
}

IntArray[] array = new IntArray[bounds[0, 0] - bounds[1, 0] + 1];
IntArray A = new IntArray(array, 0, 0, 0);
GenerateNextGen(A.nextarray, 1, bounds); // вызывает конструкторы у текущего измерения и вызывает генерацию следующего
Console.WriteLine(A);

void GenerateNextGen(IntArray[] nextgen, int dimension, int[,] bounds)
{
    for (int i = 0; i < nextgen.Length; i++)
    {
        if (dimension - 1 < bounds.Length / 2 - 1)
        {
            IntArray[] nextgen2 = new IntArray[bounds[0, dimension] - bounds[1, dimension] + 1];
            nextgen[i] = new IntArray(nextgen2, dimension, bounds[1, dimension - 1], 0);
            GenerateNextGen(nextgen[i].nextarray, dimension + 1, bounds);
        }
        else
        {
            var rand = new Random();
            IntArray[] nextgen2 = new IntArray[1];
            nextgen[i] = new IntArray(nextgen2, -1, bounds[1, dimension - 1], rand.Next(), true);
        }
    }
}
class IntArray
{
    public IntArray[] nextarray;
    int dimension;
    int value;
    int firstElementIndex;
    bool isLastDimension;
    public IntArray(IntArray[] nextarray, int dimension, int firstElementIndex, int value, bool isLastDimension = false)
    {
        this.nextarray = nextarray;
        this.dimension = dimension;
        this.firstElementIndex = firstElementIndex;
        this.value = value;
        this.isLastDimension = isLastDimension;
    }

    public override string ToString()
    {
        string temp = String.Empty;
        IntArray[] array = this.nextarray;
        for (int i = 0; i < array.Length; i++)
        {
            if (array[i].dimension == 1 && !array[i].isLastDimension)
                temp = temp + $"IntArray[{i + array[i].firstElementIndex}]" + array[i].ToString();
            else if (!array[i].isLastDimension)
                temp = temp + $"[{i + array[i].firstElementIndex}]" + array[i].ToString();
            else
                temp = temp + $"[{i + array[i].firstElementIndex}] = {array[i].value}" + Environment.NewLine;
        }
        return temp;
    }
}

