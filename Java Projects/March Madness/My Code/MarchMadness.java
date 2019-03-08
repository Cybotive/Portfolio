//Joshua Lini 11-30-18 MarchMadness Project
import utils.FileUtils;
import java.io.File;
import java.io.PrintStream;
import java.util.Arrays;
import java.util.Scanner;

public class MarchMadness {

	public static void main(String[] args) throws Exception{
		//Since the main focus of the program is building reports, the filepath variables are set at the top for versatility
		String report1Loc = "src\\Report1.txt";
		String report2Loc = "src\\Report2.txt";
		String report3Loc = "src\\Report3.txt";
		String report4Loc = "src\\Report4.txt";
		
		//NOTE there are 34 categories in the CSV file, but not all are utilized
		
		String[] categories = new String[34]; //The categories functionality was eventually nixxed, but alteration is needed to remove it
		
		File csvIn = new File("src\\ncaa_bb_2016_2017.csv"); //Hardcode required
		Scanner fin = FileUtils.openInputFile(csvIn);
		fin.useDelimiter(",|\r\n"); //Values are separated by commas, but lines are separated by "\r\n" in the CSV
		
		for(int i = 0; i < 34; i++) {
			categories[i] = fin.next();
		}
		
		Team[] teamArray = new Team[351]; //There are 351 teams in the CSV file
		
		fin.useDelimiter("\r\n");
		
		for(int i = 0; i < 351; i++) {
			teamArray[i] = new Team(fin.next());
		}
		
		//Report #1: List top 20 teams by offensive efficiency
		Arrays.sort(teamArray, new OffensiveEfficiencySort());
		
		File report1 = new File(report1Loc);
		report1.createNewFile();
		
		PrintStream reportStream = FileUtils.openOutputFile(report1);
		reportStream.println("Report #1: Top 20 Division 1 NCAA Teams of 2016-17 Listed by Ascending Offensive Efficiency\n");
		for(int i = 0; i < 20; i++) {
			reportStream.println(teamArray[i]);
		}
		
		//Report #2: List top 20 teams by defensive efficiency
		Arrays.sort(teamArray, new DefensiveEfficiencySort());
		
		File report2 = new File(report2Loc);
		report2.createNewFile();
		
		reportStream = FileUtils.openOutputFile(report2);
		reportStream.println("Report #2: Top 20 Division 1 NCAA Teams of 2016-17 Listed by Ascending Defensive Efficiency\n");
		for(int i = 0; i < 20; i++) {
			reportStream.println(teamArray[i]);
		}
		
		//Report #3: List top 64 teams by Overall efficiency
		Arrays.sort(teamArray, new OverallEfficiencySort());
		
		File report3 = new File(report3Loc);
		report3.createNewFile();
		
		reportStream = FileUtils.openOutputFile(report3);
		reportStream.println("Report #3: Top 64 Division 1 NCAA Teams of 2016-17 Listed by Ascending Overall Efficiency\n");
		for(int i = 0; i < 64; i++) {
			reportStream.println(teamArray[i]);
		}
		
		//Report #4: List all teams alphabetically and display each teams overall efficiency.
		Arrays.sort(teamArray); //Natural order is based on team name
		
		File report4 = new File(report4Loc);
		report4.createNewFile();
		
		reportStream = FileUtils.openOutputFile(report4);
		reportStream.println("Report #4: All Division 1 NCAA Teams of 2016-17 Listed by Team Name Alphabetically\n");
		for(Team teamIn : teamArray) {
			reportStream.println(teamIn);
		}
		
	}

}