//Joshua Lini 11-1-18 FileUtils Project - This class uses ArrayUtils class from last assignment

//This class is according to spec of original assignment, the MarchMadness API has an out of date API for FIleUtils
package utils;

import java.util.Scanner;
import java.io.File;
import java.io.PrintStream;

public class FileUtils {
	
	//File has to be in same folder as \src if using Eclipse IDE
	
	public static Scanner openInputFile(File file) throws Exception{
		if(file == null || (!file.exists()) || (!file.canRead())) throw new IllegalArgumentException("File not valid");
		
		return new Scanner(file);
	}
	
	public static PrintStream openOutputFile(File createFile) throws Exception{
		if(createFile == null || (!createFile.exists()) || (!createFile.canRead())) throw new Exception("File not valid");
		
		PrintStream printOut = new PrintStream(createFile);
		
		return printOut;
	}
	
	public static Scanner scannerFactoryUserInput() throws Exception{
		Scanner userIn = new Scanner(System.in);
		Scanner retScan = null;
		boolean keepAsking = true;
		
		while(keepAsking) {
			System.out.print("Input file name to be opened for reading: ");
			String fname = userIn.nextLine();
			File readFile = new File(fname);
			
			
			if(readFile.exists() && readFile.canRead()) {
				retScan = openInputFile(readFile);
				keepAsking = false;
			} else {
				System.out.println("Invalid entry, try again.");
			}
		}
		
		userIn.close();
		return retScan;
	}
	
	public static String[] fileToArray(String fname) throws Exception{
		File file = new File(fname);
		Scanner input = openInputFile(file);
		String[] retArray = new String[0];
		
		while(input.hasNext()) {
			retArray = ArrayUtils.push(retArray, input.next());
		}
		
		return retArray;
	}
	
	public static int countRecords(Scanner scan, int linesPerRec) throws Exception{
		if(linesPerRec < 1) throw new Exception ("Invalid record length");
		if(scan == null) throw new Exception ("Invalid scanner");
		
		int records = 0;
		
		while(scan.hasNext()) {
			for(int a = 0; a < linesPerRec; a++) {
				if(!scan.hasNext()) { //this is triggered when we've hit the end of the file and the last record is smaller than specified, so it doesn't count it - not Exception-worthy
					records--;
					break;
				}
				scan.nextLine();
			}
			records++;
		}
		
		return records;
	}
	
	public static int countRecords(Scanner scan) throws Exception{
		return countRecords(scan, 1);
	}
	
	public static void arrayToFile(int[] a, String fname) throws Exception{
		if(a == null) throw new Exception ("Array unspecified");
		if(fname == null) throw new Exception ("Filename cannot be null");
		
		File file = new File(fname);
		PrintStream output = openOutputFile(file);
		
		for(int i = 0; i < a.length; i++) {
			output.println(a[i]);
		}
	}
	
	public static void arrayToFile(double[] a, String fname) throws Exception{
		if(a == null) throw new Exception ("Array unspecified");
		if(fname == null) throw new Exception ("Filename cannot be null");
		
		File file = new File(fname);
		PrintStream output = openOutputFile(file);
		
		for(int i = 0; i < a.length; i++) {
			output.println(a[i]);
		}
	}
	
	public static void arrayToFile(String[] a, String fname) throws Exception{
		if(a == null) throw new Exception ("Array unspecified");
		if(fname == null) throw new Exception ("Filename cannot be null");
		
		File file = new File(fname);
		PrintStream output = openOutputFile(file);
		
		for(int i = 0; i < a.length; i++) {
			output.println(a[i]);
		}
	}
}