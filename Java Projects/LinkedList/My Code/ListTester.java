//Joshua Lini 3-7-19 - Written from scratch
import java.util.Scanner;
import java.util.InputMismatchException;
import java.util.Random;

public class ListTester {
	
	private static Scanner kin = new Scanner(System.in);
	private static LinkedList LL = new LinkedList();

	public static void main(String[] args) {
		menu();
	}
	
	private static void menu() {
		int input;
		
		try {
			System.out.println("Welcome to ListTester - Coded by Joshua Lini\n"
					+ "Please Choose From the Following Options:\n"
					+ "\n"
					+ "1. Create a New List\n"
					+ "2. Sort the List\n"
					+ "3. Print the List\n"
					+ "4. Print the List in Reverse\n"
					+ "5. Even Number Sub-List\n"
					+ "6. Print Nth Nodes\n"
					+ "7. Remove Node by Value\n"
					+ "8. Clear List\n"
					+ "9. Quit\n");
			
			System.out.print("Choice: ");
			input = kin.nextInt();
		}
		catch(InputMismatchException e) {
			System.out.println("Invalid Input...\n");
			
			kin.next();//flush buffer
			
			menu();
			return;
		}
		
		switch(input) {
		case 1:
			opt1();
			break;
		case 2:
			opt2();
			break;
		case 3:
			opt3();
			break;
		case 4:
			opt4();
			break;
		case 5:
			opt5();
			break;
		case 6:
			opt6();
			break;
		case 7:
			opt7();
			break;
		case 8:
			opt8();
			break;
		case 9:
			return;
		}
		
	}
	
	private static void opt1() { //"Create a New List"
		System.out.print("You have chosen to create a new list, please input size (input negative for main menu): ");
		
		int tempSize = userIntInput();
		if(tempSize < 0) { //check for user negative input, or failed input
			System.out.println("Invalid Input, Main Menu...\n");
			menu();
			return;
		}
		//input is assumed valid from here on out
		
		System.out.println("Generating Integer List of Size " + tempSize + "...");
		if(newList(tempSize) == true) {
			System.out.println("List Successfully Generated, Main Menu...\n");
		}
		else {
			System.out.println("List Generation Failed, Main Menu...\n");
		}
		
		menu();
		return;
	}
	
	private static void opt2() { //"Sort the List"
		System.out.println("Sorting List...");
		LL = LL.sorted();
		System.out.println();
		menu();
	}
	
	private static void opt3() { //"Print the List"
		System.out.println("Printing List...");
		LL.printList();
		System.out.println();
		menu();
	}
	
	private static void opt4() { //"Print the List in Reverse"
		System.out.println("Printing List in Reverse Order...");
		LL.printListReversed();
		System.out.println();
		menu();
	}
	
	private static void opt5() { //"Even Number Sub-List"
		System.out.println("Generating Sub-List of Even Integer Values...");
		LinkedList evens = subListEvenInts();
		evens.printList();
		
		System.out.println();
		menu();
	}
	
	private static void opt6() { //"Print Nth Nodes"
		printNthNodes();
		
		System.out.println();
		menu();
	}
	
	private static void opt7() { //"Remove Node by Value"
		System.out.print("Enter Value to Remove From List: ");
		int val = userIntInput();
		int count = 0;
		
		while(LL.remove(val) == true) {
			count++;
		}
		
		System.out.println(count + " Node(s) Removed");
		System.out.println();
		menu();
	}
	
	private static void opt8() { //"Clear List"
		LL = new LinkedList();
		menu();
	}
	
	private static int userIntInput() { //checked positive integer input
		try{
			return kin.nextInt();
		}
		catch(InputMismatchException e) {
			kin.nextLine();//flush buffer
			return -1;//failed
		}
	}
	
	private static int randInt() {
		Random rgen = new Random();
		return rgen.nextInt();
	}
	
	private static boolean newList(int size) {
		LL = new LinkedList();
		
		if(size < 0) { //negative size means 0 to me
			return false;
		}
		
		for(int i = 0; i < size; i++) {
			LL.add(randInt());
		}
		
		return true;
	}
	
	private static LinkedList subListEvenInts() {
		LinkedList evens = new LinkedList();
		
		for(int i = 0; i < LL.getSize(); i++) {
			int temp = LL.valueAtIndex(i);
			
			if(temp % 2 == 0) {
				evens.add(temp);
			}
		}
		
		return evens;
	}
	
	/*Print the contents of every "nth" node in the list. Obtain the "n" from the user, ensure it is
greater than 0.*/
	private static void printNthNodes() {
		System.out.print("Specify Value n Nodes to Print: ");
		int n = userIntInput();
		System.out.println("Printing Every Nth Node...");
		LL.printListNth(n);
	}
	
}
