//Joshua Lini 2-14-19
import java.util.Scanner;

public class SubstringTester {

	public static void main(String[] args) {
		
		Scanner kb = new Scanner(System.in);
		
		//recurSubStrings("Sluggo");
		
		System.out.println("This program outputs all the possible substrings from what you input.");
		System.out.print("Please input a string to run through the program: ");
		String input = kb.nextLine();
		
		recurSubStrings(input);
		
		System.out.print("If you would like to quit, enter the letter 'Q', otherwise enter any other input: ");
		input = kb.next();
		
		if(input.equals("Q") || input.equals("q")) {
			System.out.println("Quitting...");
			return;
		}
		
		System.out.println("Restarting...");
		main(null);
		
	}
	
	private static void recurSubStrings(String str) {
		
		if(str == null || str.length() < 1) {
			return;
		}
		
		recurSubStrings(str, true, 0, str.length()-1);
		
	}
	
	private static void recurSubStrings(String str, boolean mode, int start, int end) {//true is truncate right character
		
		if(str == null || str.length() < 1 || start > str.length()-1 || end < 0) {
			return;
		}
		
		for(int i = start; i <= end; i++) {
			System.out.print(str.charAt(i));
		}
		System.out.println();
		
		if(end <= start) {
			end = str.length()-1;
			start++;
			mode = false;
		}
		
		if(mode == true) {
			recurSubStrings(str, true, start, end-1);
		}else {
			recurSubStrings(str, true, start, end);
			
			if(end <= start) {
				start++;
				recurSubStrings(str, true, start, str.length()-1);
			}
		}
		
	}

}
