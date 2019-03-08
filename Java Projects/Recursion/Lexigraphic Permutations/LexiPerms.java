import java.util.ArrayList;
import java.util.Collections;

//Joshua Lini 2-20-19
public class LexiPerms {

	public static void main(String[] args) {
		ArrayList<String> strAL = new ArrayList<String>();
		permute("AAAALLLLWW", 0, 9, strAL);
		Collections.sort(strAL);
		//System.out.println(strAL);
		/*for(String yep : strAL) {
			System.out.println(yep);
		}*/
		
		for(int count = 0; count < strAL.size(); count++) {
			System.out.println(count+1 + ". " + strAL.get(count));
		}
		
	}
	
	private static void permute(String str, int l, int r, ArrayList<String> strALIn) 
    { 
        if (l == r) {
        	if(!strALIn.contains(str)) {
        		strALIn.add(str);
        	}
        }
        else
        { 
            for (int i = l; i <= r; i++) 
            { 
                str = swap(str,l,i); 
                permute(str, l+1, r, strALIn); 
                str = swap(str,l,i); 
            } 
        } 
    }
	
	private static String swap(String a, int i, int j) 
    { 
        char temp; 
        char[] charArray = a.toCharArray(); 
        temp = charArray[i] ; 
        charArray[i] = charArray[j]; 
        charArray[j] = temp; 
        return String.valueOf(charArray); 
    }

}
