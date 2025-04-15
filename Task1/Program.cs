// ADS103 Assessment 2 Task 1
// Adam Olesh

using System.Diagnostics;

public class Task1
{
    public static void Main(string[] args)
    {
        
        
        // Read the input from the file
        string currentDir = Directory.GetCurrentDirectory();
        string filePath = Path.Combine(currentDir, "input/a2_task1_input1.txt");
        string fileContent = File.ReadAllText(filePath);
        
        // Split the content into an array of strings
        string[] numbers = fileContent.Split(new[] { ' ', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
        // Remove the first element (the number of elements)
        numbers = numbers.Skip(1).ToArray();
        // Convert the string array to a list of integers
        List<int> input1bubbleSort = new List<int>(Array.ConvertAll(numbers, int.Parse));
        List<int> input1quickSort = new List<int>(input1bubbleSort);
        List<int> input1countingSort = new List<int>(input1bubbleSort);
        
        // Setup the stopwatch
        Stopwatch sw = new Stopwatch();
        
        // Sort the array using bubble sort
        sw.Start();
        bubbleSort(input1bubbleSort);
        sw.Stop();
        Console.WriteLine($"Bubble sort took: {sw.Elapsed.TotalMilliseconds}ms");
        var input1BubbleTime = sw.Elapsed.TotalMilliseconds;
        sw.Reset();
        
        // Sort the array using quick sort
        sw.Start();
        quickSort(input1quickSort, 0, input1quickSort.Count - 1);
        sw.Stop();
        Console.WriteLine($"Quick sort took: {sw.Elapsed.TotalMilliseconds}ms");
        var input1QuickTime = sw.Elapsed.TotalMilliseconds;
        sw.Reset();
        
        // On my PC quick sort is around 97x faster than bubble sort for input1

        if (input1BubbleTime > input1QuickTime)
        {
            int mult = (int)(input1BubbleTime / input1QuickTime);
            Console.WriteLine("Quick sort is faster than bubble sort by around " + mult + "x for input1");
        } else if (input1BubbleTime < input1QuickTime)
        {
            int mult = (int)(input1QuickTime / input1BubbleTime);
            Console.WriteLine("Bubble sort is faster than quick sort by around " + mult + "x for input1");
        }
        else
        {
            Console.WriteLine("Both algorithms took the same amount of time to sort input1");
        }
        
        // Read input2 from file
        string filePath2 = Path.Combine(currentDir, "input/a2_task1_input2.txt");
        string fileContent2 = File.ReadAllText(filePath2);
        // Split the content into an array of strings
        string[] numbers2 = fileContent2.Split(new[] { ' ', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
        // Remove the first element (the number of elements)
        numbers2 = numbers2.Skip(1).ToArray();
        // Convert the string array to a list of integers
        List<int> input2bubbleSort = new List<int>(Array.ConvertAll(numbers2, int.Parse));
        List<int> input2quickSort = new List<int>(input2bubbleSort);
        List<int> input2countingSort = new List<int>(input2bubbleSort);
        
        // Sort the array using bubble sort
        sw.Start();
        bubbleSort(input2bubbleSort);
        sw.Stop();
        Console.WriteLine($"Bubble sort took: {sw.Elapsed.TotalMilliseconds}ms");
        var input2BubbleTime = sw.Elapsed.TotalMilliseconds;
        sw.Reset();
        
        // Sort the array using quick sort
        sw.Start();
        quickSort(input2quickSort, 0, input2quickSort.Count - 1);
        sw.Stop();
        Console.WriteLine($"Quick sort took: {sw.Elapsed.TotalMilliseconds}ms");
        var input2QuickTime = sw.Elapsed.TotalMilliseconds;
        sw.Reset();
        
        // Bubble sort normally would take around the same amount of time for input2 as input1
        // This is due to bubble sort always being O(n^2) time complexity
        // But because of the nature of the input2 data set being mostly sorted and the early exit of the bubble sort
        // due to the swapRequired boolean it can be over 1000x faster than normal (on my PC)
        // Quick sort is nearly 100x slower on input2 compared to input1
        // this is due to the nature of the input2 data set being mostly sorted
        // causing the quick sort to pick pivot points close to the left of the array
        // which is a near worst case scenario for quick sort causing it to take close to O(n^2) time
        if (input2BubbleTime > input2QuickTime)
        {
            int mult = (int)(input2BubbleTime / input2QuickTime);
            Console.WriteLine("Quick sort is faster than bubble sort by around " + mult + "x for input2");
        } else if (input2BubbleTime < input2QuickTime)
        {
            int mult = (int)(input2QuickTime / input2BubbleTime);
            Console.WriteLine("Bubble sort is faster than quick sort by around " + mult + "x for input2");
        }
        else
        {
            Console.WriteLine("Both algorithms took the same amount of time to sort input2");
        }
        
        // I also implemented a counting sort algorithm
        // as the inputs are all positive integers it is a good candidate for this algorithm
        sw.Start();
        countingSort(input1countingSort);
        sw.Stop();
        Console.WriteLine($"Counting sort took: {sw.Elapsed.TotalMilliseconds}ms for input1");
        var input1CountingTime = sw.Elapsed.TotalMilliseconds;
        sw.Reset();
        
        sw.Start();
        countingSort(input2countingSort);
        sw.Stop();
        Console.WriteLine($"Counting sort took: {sw.Elapsed.TotalMilliseconds}ms for input2");
        var input2CountingTime = sw.Elapsed.TotalMilliseconds;
        sw.Reset();
        
        // Counting sort took around 3ms on my PC for input1.
        // This is slower than quick sort since it is a best case of O(n log n) for quick sort.
        // Counting sort took around 0.1ms on my PC for input2.
        // Which was around 30x faster than quick and bubble sort for input2.
        // It had this faster speed due to cache locality on the CPU 
        // since it did not have to jump around the count array nearly as much as input1 since input2 is mostly sorted
        // even though theoretically input1 and input2 should take the same amount of time
        // for counting sort since they are the same size and range
    }

    // Bubble sort implementation
    // Added the swapRequired boolean to optimize the algorithm. This lets us break out of the loop if the array is already sorted.
    public static void bubbleSort(List<int> list)
    {
        bool swapRequired;
        
        for (int i = 0; i < list.Count - 1; i++)
        {
            swapRequired = false;
            
            for (int j = 0; j < list.Count - 1; j++)
            {
                if (list[j] > list[j + 1])
                {
                    (list[j], list[j + 1]) = (list[j + 1], list[j]);
                    swapRequired = true;
                }
            }
            // If no swaps were made, the list is already sorted
            if (!swapRequired)
                break;
        }

    }
    
    // Quick sort implementation. Has average time complexity of O(n log n) and worst case of O(n^2) when the last element is always chosen as the pivot.
    // Selects the last element as the pivot and sorts numbers lower than it to the left and numbers higher than it to the right
    // then splits the array into two parts and sorts them recursively.
    public static void quickSort(List<int> list, int startIndex, int endIndex)
    {
        if (startIndex < endIndex)
        {
            int pivotIndex = partition(list, startIndex, endIndex);
            quickSort(list, startIndex, pivotIndex - 1);
            quickSort(list, pivotIndex + 1, endIndex);
        }
    }

    private static int partition(List<int> list, int startIndex, int endIndex)
    {
        int pivot = list[endIndex];
        int i = startIndex - 1;
        for (int j = startIndex; j < endIndex; j++)
        {
            if (list[j] < pivot)
            {
                i++;
                (list[i], list[j]) = (list[j], list[i]);
            }
        }
        (list[i + 1], list[endIndex]) = (list[endIndex], list[i + 1]);
        return i + 1;
    }
    
    // Counting sort implementation. Has time complexity of O(n+k) where n is the number of elements and k is the range of the input.
    // Counting can have a lost worse space complexity of O(k) where k is the range of the input.
    public static void countingSort(List<int> list)
    {
        int max = list.Max();
        int range = max + 1;
        
        // Create a count array to store the count of each unique element
        int[] count = new int[range];
        
        // Store the count of each element in the count array
        foreach (int num in list)
            count[num]++;
        
        // Sort the original list using the count array
        int index = 0;
        for (int i = 0; i < range; i++)
        {
            while (count[i] > 0)
            {
                list[index++] = i;
                count[i]--;
            }
        }
    }
}
