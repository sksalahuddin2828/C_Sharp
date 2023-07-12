using System;

class ReverseArray {
    static void Main() {
        Console.Write("Please enter the total number you want to enter: ");
        int number = int.Parse(Console.ReadLine());

        int[] array = new int[number];
        for (int i = 0; i < number; i++) {
            Console.Write($"Enter the element {i + 1}: ");
            array[i] = int.Parse(Console.ReadLine());
        }

        for (int i = 0; i < number / 2; i++) {
            int temp = array[i];
            array[i] = array[number - 1 - i];
            array[number - 1 - i] = temp;
        }

        Console.WriteLine("\nReverse all elements of the array:");
        foreach (int element in array) {
            Console.WriteLine(element);
        }
    }
}
