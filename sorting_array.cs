using System;

public class ArrayEquality {
    public static bool AreArraysEqual(int[] array1, int[] array2) {
        int length1 = array1.Length;
        int length2 = array2.Length;

        if (length1 != length2) {
            return false;
        }

        // Sort both arrays
        Array.Sort(array1);
        Array.Sort(array2);

        // Linearly compare elements
        for (int i = 0; i < length1; i++) {
            if (array1[i] != array2[i]) {
                return false;
            }
        }

        // If all elements are the same
        return true;
    }

    public static void Main(string[] args) {
        int[] array1 = {3, 5, 2, 5, 2};
        int[] array2 = {2, 3, 5, 5, 2};

        if (AreArraysEqual(array1, array2)) {
            Console.WriteLine("The arrays are equal");
        } else {
            Console.WriteLine("The arrays are not equal");
        }
    }
}
