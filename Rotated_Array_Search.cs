using System;

public class RotatedArraySearch {
    public static int SearchInRotatedArray(int[] nums, int target) {
        int n = nums.Length;

        // Logic
        int start = 0;
        int end = n - 1;

        while (start <= end) {
            int mid = (start + end) / 2;

            if (nums[mid] == target) {
                return mid;
            }

            // Two cases
            if (nums[start] <= nums[mid]) {
                // Left
                if (target >= nums[start] && target <= nums[mid]) {
                    end = mid - 1;
                } else {
                    start = mid + 1;
                }
            } else {
                // Right
                if (target >= nums[mid] && target <= nums[end]) {
                    start = mid + 1;
                } else {
                    end = mid - 1;
                }
            }
        }

        return -1;
    }

    public static void Main(string[] args) {
        int[] nums = {4, 5, 6, 7, 0, 1, 2, 3};
        int target = 0;

        int result = SearchInRotatedArray(nums, target);
        Console.WriteLine(result);
    }
}
