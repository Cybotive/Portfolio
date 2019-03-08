//Joshua Lini StatsUtils project 11-4-18 - Uses ArrayUtils class from project 1
package utils;

public class StatsUtils {
	public static void main(String[] args) throws Exception{
		int[] iArray = {1, 3, 1, 2, 2, 5, 4, 4};
		System.out.println(mean(iArray));
		double[] dArray = {1, 3, 5, 6, 6, 2};
		System.out.println(mean(dArray));
		
		System.out.println(median(iArray));
		System.out.println(median(dArray));
		
		System.out.println(midpoint(iArray));
		System.out.println(midpoint(dArray));
		
		ArrayUtils.printArray(modes(iArray));
		ArrayUtils.printArray(modes(dArray));
		
		double[] dArray1 = {1, 3, 1, 2, 2, 5, 4, 4};
		System.out.println(standardDeviation(iArray));
		System.out.println(standardDeviation(dArray1));
	}
	
	public static double mean(int[] array) throws Exception{
		if(array == null) throw new Exception("No array specified");
		if(array.length < 1) throw new Exception("Empty array");
		
		double total = 0;
		for(int i = 0; i < array.length; i++) {
			total += array[i];
		}
		
		return total/array.length;
	}
	
	public static double mean(double[] array) throws Exception{
		if(array == null) throw new Exception("No array specified");
		if(array.length < 1) throw new Exception("Empty array");
		
		double total = 0;
		for(int i = 0; i < array.length; i++) {
			total += array[i];
		}
		
		return total/array.length;
	}
	
	public static double median(int[] array) throws Exception{
		if(array == null) throw new Exception("No array specified");
		if(array.length < 1) throw new Exception("Empty array");
		
		int[] sorted = array;
		
		if(!ArrayUtils.isSorted(array)) {
			ArrayUtils.selectionSort(sorted);
		}
		
		if(sorted.length % 2 == 0) { //even length
			return (sorted[(sorted.length/2)-1] + sorted[sorted.length/2]) / 2;
		} else { //odd length
			return sorted[sorted.length/2];
		}
	}
	
	public static double median(double[] array) throws Exception{
		if(array == null) throw new Exception("No array specified");
		if(array.length < 1) throw new Exception("Empty array");
		
		double[] sorted = array;
		
		if(!ArrayUtils.isSorted(array)) {
			ArrayUtils.selectionSort(sorted);
		}
		
		if(sorted.length % 2 == 0) { //even length
			return (sorted[(sorted.length/2)-1] + sorted[sorted.length/2]) / 2;
		} else { //odd length
			return sorted[sorted.length/2];
		}
	}
	
	public static double midpoint(int[] array) throws Exception{
		if(array == null) throw new Exception("No array specified");
		if(array.length < 1) throw new Exception("Empty array");
		
		int[] sorted = array;
		
		if(!ArrayUtils.isSorted(array)) {
			ArrayUtils.selectionSort(sorted);
		}
		
		return (sorted[0] + sorted[sorted.length-1]) / 2;
	}
	
	public static double midpoint(double[] array) throws Exception{
		if(array == null) throw new Exception("No array specified");
		if(array.length < 1) throw new Exception("Empty array");
		
		double[] sorted = array;
		
		if(!ArrayUtils.isSorted(array)) {
			ArrayUtils.selectionSort(sorted);
		}
		
		return (sorted[0] + sorted[sorted.length-1]) / 2;
	}
	
	public static int[] modes(int[] array) throws Exception{
		if(array == null) throw new Exception("No array specified");
		if(array.length < 1) throw new Exception("Empty array");
		
		int[] valuesNoDup = array;
		ArrayUtils.selectionSort(valuesNoDup);
		
		//ArrayUtils.printArray(valuesNoDup);
		
		for(int i = 1; i < valuesNoDup.length; i++) {
			if(valuesNoDup[i] == valuesNoDup[i-1]) {
				valuesNoDup = ArrayUtils.removeElement(valuesNoDup, i-1);
				i -= 1; //shift the index to edited array
			}
		}
		
		int[] mode = valuesNoDup;
		int[] count = new int[mode.length];
		
		for(int item: array) {
			count[ArrayUtils.binarySearch(mode, item)]++; //increment the index that matches where the item is located in mode[]
		}
		
		int largest = 0;
		for(int i = 0; i < count.length; i++) {
			if(count[i] > largest) {
				largest = count[i];
			}
		}
		
		int[] modez = new int[0];
		for(int i = 0; i < count.length; i++) {
			if(count[i] == largest) {
				modez = ArrayUtils.addElement(modez, mode[i]);
			}
		}
		
		return modez;
	}
	
	public static double[] modes(double[] array) throws Exception{
		if(array == null) throw new Exception("No array specified");
		if(array.length < 1) throw new Exception("Empty array");
		
		double[] valuesNoDup = array;
		ArrayUtils.selectionSort(valuesNoDup);
		
		//ArrayUtils.printArray(valuesNoDup);
		
		for(int i = 1; i < valuesNoDup.length; i++) {
			if(valuesNoDup[i] == valuesNoDup[i-1]) {
				valuesNoDup = ArrayUtils.removeElement(valuesNoDup, i-1);
				i -= 1; //shift the index to edited array
			}
		}
		
		double[] mode = valuesNoDup;
		int[] count = new int[mode.length];
		
		for(double item: array) {
			count[ArrayUtils.binarySearch(mode, item)]++; //increment the index that matches where the item is located in mode[]
		}
		
		int largest = 0;
		for(int i = 0; i < count.length; i++) {
			if(count[i] > largest) {
				largest = count[i];
			}
		}
		
		double[] modez = new double[0];
		for(int i = 0; i < count.length; i++) {
			if(count[i] == largest) {
				modez = ArrayUtils.addElement(modez, mode[i]);
			}
		}
		
		return modez;
	}
	
	public static double standardDeviation(int[] array) throws Exception{
		if(array == null) throw new Exception("No array specified");
		if(array.length <= 1) throw new Exception("Invalid array length");
		
		double total = 0;
		for(int item: array) {
			total += item;
		}
		double mean = total/array.length;
		//System.out.println(mean);
		
		double[] deviations = new double[array.length];
		for(int i = 0; i < array.length; i++) {
			deviations[i] = array[i] - mean;
			deviations[i] = Math.pow(deviations[i], 2);
		}
		//ArrayUtils.printArray(deviations);
		
		double totalDev = 0;
		for(double item: deviations) {
			totalDev += item;
		}
		mean = totalDev/array.length-1;
		
		return Math.pow(mean, 2);
	}
	
	public static double standardDeviation(double[] array) throws Exception{
		if(array == null) throw new Exception("No array specified");
		if(array.length <= 1) throw new Exception("Invalid array length");
		
		double total = 0;
		for(double item: array) {
			total += item;
		}
		double mean = total/array.length;
		//System.out.println(mean);
		
		double[] deviations = new double[array.length];
		for(int i = 0; i < array.length; i++) {
			deviations[i] = array[i] - mean;
			deviations[i] = Math.pow(deviations[i], 2);
		}
		//ArrayUtils.printArray(deviations);
		
		double totalDev = 0;
		for(double item: deviations) {
			totalDev += item;
		}
		mean = totalDev/array.length-1;
		
		return Math.pow(mean, 2);
	}
}
