//Joshua Lini 10-23-18 ArrayUtils Project
package utils;

public class ArrayUtils {
	
	public static void printArray(int[] a) throws Exception{
		if(a == null) throw new Exception ("No array specified");
		
		String output = "[";
		for(int ix = 0; ix < a.length; ix++) {
			output += a[ix];
			if(ix != a.length-1) {
				output += ", ";
			}
		}
		output += "]";
		System.out.println(output);
	}
	
	public static void printArray(double[] a) throws Exception{
		if(a == null) throw new Exception ("No array specified");
		
		String output = "[";
		for(int ix = 0; ix < a.length; ix++) {
			output += a[ix];
			if(ix != a.length-1) {
				output += ", ";
			}
		}
		output += "]";
		System.out.println(output);
	}
	
	public static void printArray(String[] a) throws Exception{
		if(a == null) throw new Exception ("No array specified");
		
		String output = "[";
		for(int ix = 0; ix < a.length; ix++) {
			output += a[ix];
			if(ix != a.length-1) {
				output += ", ";
			}
		}
		output += "]";
		System.out.println(output);
	}
	
	public static void reverseArray(int[] a) throws Exception{
		if(a == null) throw new Exception ("No array specified");
		
		int temp;
		for(int i = 0; i < a.length/2; i++) {
			temp = a[i];
			a[i] = a[a.length-1-i];
			a[a.length-1-i] = temp;
		}
	}
	
	public static void reverseArray(double[] a) throws Exception{
		if(a == null) throw new Exception ("No array specified");
		
		double temp;
		for(int i = 0; i < a.length/2; i++) {
			temp = a[i];
			a[i] = a[a.length-1-i];
			a[a.length-1-i] = temp;
		}
	}
	
	public static void reverseArray(String[] a) throws Exception{
		if(a == null) throw new Exception ("No array specified");
		
		String temp;
		for(int i = 0; i < a.length/2; i++) {
			temp = a[i];
			a[i] = a[a.length-1-i];
			a[a.length-1-i] = temp;
		}
	}
	
	public static void swap(int[] a, int loc1, int loc2) throws Exception{
		if(a == null) throw new Exception ("No array specified");
		
		if(a.length <= 0) throw new Exception ("No values to swap");
		
		if(loc1 < 0 || loc2 < 0 || loc1 > a.length-1 || loc2 > a.length-1) throw new Exception ("Index out of bounds");
		
		int temp = a[loc1];
		a[loc1] = a[loc2];
		a[loc2] = temp;
	}
	
	public static void swap(double[] a, int loc1, int loc2) throws Exception{
		if(a == null) throw new Exception ("No array specified");
		
		if(a.length <= 0) throw new Exception ("No values to swap");
		
		if(loc1 < 0 || loc2 < 0 || loc1 > a.length-1 || loc2 > a.length-1) throw new Exception ("Index out of bounds");
		
		double temp = a[loc1];
		a[loc1] = a[loc2];
		a[loc2] = temp;
	}
	
	public static void swap(String[] a, int loc1, int loc2) throws Exception{
		if(a == null) throw new Exception ("No array specified");
		
		if(a.length <= 0) throw new Exception ("No values to swap");
		
		if(loc1 < 0 || loc2 < 0 || loc1 > a.length-1 || loc2 > a.length-1) throw new Exception ("Index out of bounds");
		
		String temp = a[loc1];
		a[loc1] = a[loc2];
		a[loc2] = temp;
	}
	
	public static int findSmallest(int[] a, int begin, int end) throws Exception{
		if(a == null) throw new Exception ("No array specified");
		
		if(a.length <= 0) throw new Exception ("No values in array");
		
		if(begin < 0 || end < 0 || begin > a.length-1 || end > a.length-1) throw new Exception ("Index out of bounds");
		
		if(begin > end) {
			int temp = begin;
			begin = end;
			end = temp;
		}
		
		int smallest = a[begin];
		int smallestIndex = begin;
		
		for(int i = begin+1; i <= end; i++) {
			if(a[i] < smallest) {
				smallest = a[i];
				smallestIndex = i;
			}
		}
		return smallestIndex;
	}
	
	public static int findSmallest(double[] a, int begin, int end) throws Exception{
		if(a == null) throw new Exception ("No array specified");
		
		if(a.length <= 0) throw new Exception ("No values in array");
		
		if(begin < 0 || end < 0 || begin > a.length-1 || end > a.length-1) throw new Exception ("Index out of bounds");
		
		if(begin > end) {
			int temp = begin;
			begin = end;
			end = temp;
		}
		
		double smallest = a[begin];
		int smallestIndex = begin;
		
		for(int i = begin+1; i <= end; i++) {
			if(a[i] < smallest) {
				smallest = a[i];
				smallestIndex = i;
			}
		}
		return smallestIndex;
	}
	
	public static int findSmallest(String[] a, int begin, int end) throws Exception{
		if(a == null) throw new Exception ("No array specified");
		
		if(a.length <= 0) throw new Exception ("No values in array");
		
		if(begin < 0 || end < 0 || begin > a.length-1 || end > a.length-1) throw new Exception ("Index out of bounds");
		
		if(begin > end) {
			int temp = begin;
			begin = end;
			end = temp;
		}
		
		String smallest = a[begin];
		int smallestIndex = begin;
		
		for(int i = begin+1; i <= end; i++) {
			if(a[i].compareTo(smallest) < 0) {
				smallest = a[i];
				smallestIndex = i;
			}
		}
		return smallestIndex;
	}
	
	public static boolean compare(int[] a1, int[] a2) throws Exception{
		if(a1 == null || a2 == null) throw new Exception ("One or more arrays unspecified");
		
		if(a1.length == 0 && a2.length == 0) return true;
		if(a1.length == 0 || a2.length == 0) return false;
		if(a1.length != a2.length) return false;
		
		for(int i = 0; i < a1.length; i++) {
			if(a1[i] != a2[i]) return false;
		}
		return true;
	}
	
	public static boolean compare(double[] a1, double[] a2) throws Exception{
		if(a1 == null || a2 == null) throw new Exception ("One or more arrays unspecified");
		
		if(a1.length == 0 && a2.length == 0) return true;
		if(a1.length == 0 || a2.length == 0) return false;
		if(a1.length != a2.length) return false;
		
		for(int i = 0; i < a1.length; i++) {
			if(a1[i] != a2[i]) return false;
		}
		return true;
	}

	public static boolean compare(String[] a1, String[] a2) throws Exception{
		if(a1 == null || a2 == null) throw new Exception ("One or more arrays unspecified");
		
		if(a1.length == 0 && a2.length == 0) return true;
		if(a1.length == 0 || a2.length == 0) return false;
		if(a1.length != a2.length) return false;
		
		for(int i = 0; i < a1.length; i++) {
			if(a1[i].compareTo(a2[i]) != 0) return false;
		}
		return true;
	}
	
	public static boolean isSorted(int[] a) throws Exception{
		if(a == null) throw new Exception ("No array specified");
		
		if(a.length <= 0) throw new Exception ("No values in array");
		
		for(int i = 0; i < a.length-1; i++) {
			if(!(a[i] <= a[i+1])) {
				return false;
			}
		}
		return true;
	}
	
	public static boolean isSorted(double[] a) throws Exception{
		if(a == null) throw new Exception ("No array specified");
		
		if(a.length <= 0) throw new Exception ("No values in array");
		
		for(int i = 0; i < a.length-1; i++) {
			if(!(a[i] <= a[i+1])) {
				return false;
			}
		}
		return true;
	}
	
	public static boolean isSorted(String[] a) throws Exception{
		if(a == null) throw new Exception ("No array specified");
		
		if(a.length <= 0) throw new Exception ("No values in array");
		
		for(int i = 0; i < a.length-1; i++) {
			if(!(a[i].compareTo(a[i+1]) < 0)) {
				return false;
			}
		}
		return true;
	}
	
	public static int[] cloneArray(int[] a) throws Exception{
		if(a == null) throw new Exception ("No array specified");
		
		int[] cloneA = new int[a.length];
		
		for(int i = 0; i < a.length; i++) {
			cloneA[i] = a[i];
		}
		return cloneA;
	}
	
	public static double[] cloneArray(double[] a) throws Exception{
		if(a == null) throw new Exception ("No array specified");
		
		double[] cloneA = new double[a.length];
		
		for(int i = 0; i < a.length; i++) {
			cloneA[i] = a[i];
		}
		return cloneA;
	}
	
	public static String[] cloneArray(String[] a) throws Exception{
		if(a == null) throw new Exception ("No array specified");
		
		String[] cloneA = new String[a.length];
		
		for(int i = 0; i < a.length; i++) {
			cloneA[i] = a[i];
		}
		return cloneA;
	}
	
	public static int linearSearch(int[] a, int target) throws Exception{
		if(a == null) throw new Exception ("No array specified");
		
		for(int i = 0; i < a.length; i++) {
			if(a[i] == target) return i;
		}
		return -1;
	}
	
	public static int linearSearch(double[] a, double target) throws Exception{
		if(a == null) throw new Exception ("No array specified");
		
		for(int i = 0; i < a.length; i++) {
			if(a[i] == target) return i;
		}
		return -1;
	}
	
	public static int linearSearch(String[] a, String target) throws Exception{
		if(a == null) throw new Exception ("No array specified");
		
		for(int i = 0; i < a.length; i++) {
			if(a[i].compareTo(target) == 0) return i;
		}
		return -1;
	}
	
	public static int[] push(int[] a, int element) throws Exception{
		if(a == null) throw new Exception ("No array specified");
		
		int[] retArray = new int[a.length+1];
		
		for(int i = 0; i < a.length; i++) {
			retArray[i] = a[i];
		}
		retArray[retArray.length-1] = element;
		
		return retArray;
	}
	
	public static double[] push(double[] a, double element) throws Exception{
		if(a == null) throw new Exception ("No array specified");
		
		double[] retArray = new double[a.length+1];
		
		for(int i = 0; i < a.length; i++) {
			retArray[i] = a[i];
		}
		retArray[retArray.length-1] = element;
		
		return retArray;
	}
	
	public static String[] push(String[] a, String element) throws Exception{
		if(a == null) throw new Exception ("No array specified");
		
		String[] retArray = new String[a.length+1];
		
		for(int i = 0; i < a.length; i++) {
			retArray[i] = a[i];
		}
		retArray[retArray.length-1] = element;
		
		return retArray;
	}
	
	public static int[] addElement(int[] a, int element) throws Exception{
		return push(a, element);
	}
	
	public static double[] addElement(double[] a, double element) throws Exception{
		return push(a, element);
	}
	
	public static String[] addElement(String[] a, String element) throws Exception{
		return push(a, element);
	}
	
	public static int[] pop(int[] a) throws Exception{
		if(a == null) throw new Exception ("No array specified");
		
		if(a.length <= 0) throw new Exception ("No values in array");
		
		int[] retArray = new int[a.length-1];
		
		for(int i = 0; i < a.length-1; i++) {
			retArray[i] = a[i];
		}
		
		return retArray;
	}
	
	public static double[] pop(double[] a) throws Exception{
		if(a == null) throw new Exception ("No array specified");
		
		if(a.length <= 0) throw new Exception ("No values in array");
		
		double[] retArray = new double[a.length-1];
		
		for(int i = 0; i < a.length-1; i++) {
			retArray[i] = a[i];
		}
		
		return retArray;
	}
	
	public static String[] pop(String[] a) throws Exception{
		if(a == null) throw new Exception ("No array specified");
		
		if(a.length <= 0) throw new Exception ("No values in array");
		
		String[] retArray = new String[a.length-1];
		
		for(int i = 0; i < a.length-1; i++) {
			retArray[i] = a[i];
		}
		
		return retArray;
	}
	
	public static int[] insert(int[] a, int item, int loc) throws Exception{
		if(a == null) throw new Exception ("No array specified");
		
		if(loc < 0 || loc > a.length) throw new Exception ("Index out of bounds");
		
		int[] retArray = new int[a.length+1];
		
		for(int i = 0; i < loc; i++) {
			retArray[i] = a[i];
		}
		
		retArray[loc] = item;
		
		for(int i = loc+1; i < retArray.length; i++) {
			retArray[i] = a[i-1];
		}
		
		return retArray;
	}
	
	public static double[] insert(double[] a, double item, int loc) throws Exception{
		if(a == null) throw new Exception ("No array specified");
		
		if(loc < 0 || loc > a.length) throw new Exception ("Index out of bounds");
		
		double[] retArray = new double[a.length+1];
		
		for(int i = 0; i < loc; i++) {
			retArray[i] = a[i];
		}
		
		retArray[loc] = item;
		
		for(int i = loc+1; i < retArray.length; i++) {
			retArray[i] = a[i-1];
		}
		
		return retArray;
	}
	
	public static String[] insert(String[] a, String item, int loc) throws Exception{
		if(a == null) throw new Exception ("No array specified");
		
		if(loc < 0 || loc > a.length) throw new Exception ("Index out of bounds");
		
		String[] retArray = new String[a.length+1];
		
		for(int i = 0; i < loc; i++) {
			retArray[i] = a[i];
		}
		
		retArray[loc] = item;
		
		for(int i = loc+1; i < retArray.length; i++) {
			retArray[i] = a[i-1];
		}
		
		return retArray;
	}
	
	public static int[] removeElement(int[] a, int index) throws Exception{
		if(a == null) throw new Exception ("No array specified");
		
		if(index < 0 || index > a.length-1) throw new IllegalArgumentException("Bad Parameter");
		
		int[] retArray = new int[a.length-1];
		
		for(int i = 0; i < index; i++) {
			retArray[i] = a[i];
		}
		for(int i = index; i < retArray.length; i++) {
			retArray[i] = a[i+1];
		}
		
		return retArray;
	}
	
	public static double[] removeElement(double[] a, int index) throws Exception{
		if(a == null) throw new Exception ("No array specified");
		
		if(index < 0 || index > a.length-1) throw new IllegalArgumentException("Bad Parameter");
		
		double[] retArray = new double[a.length-1];
		
		for(int i = 0; i < index; i++) {
			retArray[i] = a[i];
		}
		for(int i = index; i < retArray.length; i++) {
			retArray[i] = a[i+1];
		}
		
		return retArray;
	}
	
	public static String[] removeElement(String[] a, int index) throws Exception{
		if(a == null) throw new Exception ("No array specified");
		
		if(index < 0 || index > a.length-1) throw new IllegalArgumentException("Bad Parameter");
		
		String[] retArray = new String[a.length-1];
		
		for(int i = 0; i < index; i++) {
			retArray[i] = a[i];
		}
		for(int i = index; i < retArray.length; i++) {
			retArray[i] = a[i+1];
		}
		
		return retArray;
	}
	
	public static int[] removeElement(int[] a) throws Exception{
		return pop(a);
	}
	
	public static double[] removeElement(double[] a) throws Exception{
		return pop(a);
	}
	
	public static String[] removeElement(String[] a) throws Exception{
		return pop(a);
	}
	
	public static int[] removeByValue(int[] a, int value) throws Exception{
		int loc = linearSearch(a, value);
		
		if(loc < 0) throw new Exception ("Non-existent value");
		
		return removeElement(a, loc);
	}
	
	public static double[] removeByValue(double[] a, double value) throws Exception{
		int loc = linearSearch(a, value);
		
		if(loc < 0) throw new Exception ("Non-existent value");
		
		return removeElement(a, loc);
	}
	
	public static String[] removeByValue(String[] a, String value) throws Exception{
		int loc = linearSearch(a, value);
		
		if(loc < 0) throw new Exception ("Non-existent value");
		
		return removeElement(a, loc);
	}
	
	public static int binarySearch(int[] a, int target) throws Exception{
		if(!isSorted(a)) throw new Exception ("Unsorted Array");
		
		int start = 0;
		int end = a.length-1;
		int pointer = (start + end)/2;
		while(pointer != -1) {
			if(target < a[pointer]) {
				end = pointer;
			}
			if(target > a[pointer]) {
				start = pointer+1;
			}
			
			pointer = (start+end)/2;
			//System.out.println(start + " " + end + " " + pointer);
			
			if(a[pointer] == target) return pointer;
			
			if(start == end) pointer = -1;
		}
		
		return pointer;
	}
	
	public static int binarySearch(double[] a, double target) throws Exception{
		if(!isSorted(a)) throw new Exception ("Unsorted Array");
		
		int start = 0;
		int end = a.length-1;
		int pointer = (start + end)/2;
		while(pointer != -1) {
			if(target < a[pointer]) {
				end = pointer;
			}
			if(target > a[pointer]) {
				start = pointer+1;
			}
			
			pointer = (start+end)/2;
			//System.out.println(start + " " + end + " " + pointer);
			
			if(a[pointer] == target) return pointer;
			
			if(start == end) pointer = -1;
		}
		
		return pointer;
	}
	
	public static int binarySearch(String[] a, String target) throws Exception{
		if(!isSorted(a)) throw new Exception ("Unsorted Array");
		
		int start = 0;
		int end = a.length-1;
		int pointer = (start + end)/2;
		while(pointer != -1) {
			if(target.compareTo(a[pointer]) < 0) {
				end = pointer;
			}
			if(target.compareTo(a[pointer]) > 0) {
				start = pointer+1;
			}
			
			pointer = (start+end)/2;
			//System.out.println(start + " " + end + " " + pointer);
			
			if(a[pointer].compareTo(target) == 0) return pointer;
			
			if(start == end) pointer = -1;
		}
		
		return pointer;
	}
	
	public static void selectionSort(int[] a) throws Exception{
		if(a == null) throw new Exception ("Array unspecified");
		
		if(a.length < 1) throw new Exception ("No values to sort");
		
		for(int i = 0; i < a.length-1; i++) {
			swap(a, i, findSmallest(a, i, a.length-1));
		}
	}
	
	public static void selectionSort(double[] a) throws Exception{
		if(a == null) throw new Exception ("Array unspecified");
		
		if(a.length < 1) throw new Exception ("No values to sort");
		
		for(int i = 0; i < a.length-1; i++) {
			swap(a, i, findSmallest(a, i, a.length-1));
		}
	}
	
	public static void selectionSort(String[] a) throws Exception{
		if(a == null) throw new Exception ("Array unspecified");
		
		if(a.length < 1) throw new Exception ("No values to sort");
		
		for(int i = 0; i < a.length-1; i++) {
			swap(a, i, findSmallest(a, i, a.length-1));
		}
	}
}
