//Joshua Lini 11-30-18 MarchMadness Project
import java.util.Scanner;

public class Team implements Comparable<Team>{
	
	//I declared all the variables that are listed in the API but then needed more that the API forgot about
    private String school;
    private int totalGames;
    private int wins;
    private int losses;
    private double winLossPercentage;
    private double simpleRatingSystem;
    private double strengthOfSchedule;
    private int conferenceWins;
    private int conferenceLosses;
    private int homeWins;
    private int homeLosses;
    private int awayWins;
    private int awayLosses;
    public int totalPointsScored; //public according to given API, but no reason for it being public
    private int opponentPointsScored;
    private int minutesPlayed;
    private int fieldGoalsMade;
    private int fieldGoalsAttemped; //Typo in API
    private double fieldGoalPercentage;
    private int threePointersMade;
    private int threePointersAttempted;
    private double threePointerPercentage;
    private int freeThrowsMade; //Start of variables needed but not listed in API
    private int freeThrowsAttempted;
    private double freeThrowPercentage;
    private int offensiveRebounds; //End of above
    private int totalRebounds;
    private int assists;
    private int steals;
    private int blocks;
    private int turnovers;
    private int personalFouls;
    private String input; //This variable is never used, but it is needed according to API
	
	public Team() {
	}
	
	public Team(String sc) throws Exception{
		Scanner parseFin = new Scanner(sc);
		parseFin.useDelimiter(",");
		
		/*I know there is a better way to do the below.
		I thought of using a switch statement or "if/else if" after pulling everything in as String for versatility*/
		parseFin.next(); //Unused value
		this.setSchool(parseFin.next());
		this.setTotalGames(parseFin.nextInt());
		this.setWins(parseFin.nextInt());
		this.setLosses(Integer.parseInt(parseFin.next()));
		this.setWinLossPercentage(parseFin.nextDouble());
		this.setSimpleRatingSystem(parseFin.nextDouble());
		this.setStrengthOfSchedule(parseFin.nextDouble());
		this.setConferenceWins(parseFin.nextInt());
		this.setConferenceLosses(parseFin.nextInt());
		this.setHomeWins(parseFin.nextInt());
		this.setHomeLosses(parseFin.nextInt());
		this.setAwayWins(parseFin.nextInt());
		this.setAwayLosses(parseFin.nextInt());
		this.setTotalPointsScored(parseFin.nextInt());
		this.setOpponentPointsScored(parseFin.nextInt());
		parseFin.next(); //Blank value
		this.setMinutesPlayed(parseFin.nextInt());
		this.setFieldGoalsMade(parseFin.nextInt());
		this.setFieldGoalsAttemped(parseFin.nextInt());
		this.setFieldGoalPercentage(parseFin.nextDouble());
		this.setThreePointersMade(parseFin.nextInt());
		this.setThreePointersAttempted(parseFin.nextInt());
		this.setThreePointerPercentage(parseFin.nextDouble());
		this.setFreeThrowsMade(parseFin.nextInt());
		this.setFreeThrowsAttempted(parseFin.nextInt());
		this.setFreeThrowPercentage(parseFin.nextDouble());
		this.setFreeThrowsMade(parseFin.nextInt());
		this.setTotalRebounds(parseFin.nextInt());
		this.setAssists(parseFin.nextInt());
		this.setSteals(parseFin.nextInt());
		this.setBlocks(parseFin.nextInt());
		this.setTurnovers(parseFin.nextInt());
		this.setPersonalFouls(parseFin.nextInt());
		
		parseFin.close();
	}
	
	public int compareTo(Team that) {
		return this.getSchool().compareTo(that.getSchool());
	}

	@Override
	public String toString() {
		return school + "-" + this.wins + "/" + this.losses + " Overall Efficiency:" + this.getOverallEfficiency();
	}
	
	public double getOffensiveEfficiency() {
		return (double)this.totalPointsScored / (this.fieldGoalsAttemped / this.freeThrowsAttempted);
	}

	public double getDefensiveEfficiency() {
		return (double)this.minutesPlayed / this.opponentPointsScored; 
	}
	
	public double getOverallEfficiency() {
		return ((((double)this.getOffensiveEfficiency() * 2) + ((double)this.getOffensiveEfficiency() * 3)) / 5.0) * (double)this.winLossPercentage;
	}
	
	//I added a getter and setter for totalPointsScored even though its public for uniformity
	public int getTotalPointsScored() {
		return totalPointsScored;
	}

	public void setTotalPointsScored(int totalPointsScored) {
		this.totalPointsScored = totalPointsScored;
	}
	
	//Here and below is just getters and setters for class variables
	public String getSchool() {
		return school;
	}

	public void setSchool(String school) {
		this.school = school;
	}

	public int getTotalGames() {
		return totalGames;
	}

	public void setTotalGames(int totalGames) {
		this.totalGames = totalGames;
	}

	public int getWins() {
		return wins;
	}

	public void setWins(int wins) {
		this.wins = wins;
	}

	public int getLosses() {
		return losses;
	}

	public void setLosses(int losses) {
		this.losses = losses;
	}

	public double getWinLossPercentage() {
		return winLossPercentage;
	}

	public void setWinLossPercentage(double winLossPercentage) {
		this.winLossPercentage = winLossPercentage;
	}

	public double getSimpleRatingSystem() {
		return simpleRatingSystem;
	}

	public void setSimpleRatingSystem(double simpleRatingSystem) {
		this.simpleRatingSystem = simpleRatingSystem;
	}

	public double getStrengthOfSchedule() {
		return strengthOfSchedule;
	}

	public void setStrengthOfSchedule(double strengthOfSchedule) {
		this.strengthOfSchedule = strengthOfSchedule;
	}

	public int getConferenceWins() {
		return conferenceWins;
	}

	public void setConferenceWins(int conferenceWins) {
		this.conferenceWins = conferenceWins;
	}

	public int getConferenceLosses() {
		return conferenceLosses;
	}

	public void setConferenceLosses(int conferenceLosses) {
		this.conferenceLosses = conferenceLosses;
	}

	public int getHomeWins() {
		return homeWins;
	}

	public void setHomeWins(int homeWins) {
		this.homeWins = homeWins;
	}

	public int getHomeLosses() {
		return homeLosses;
	}

	public void setHomeLosses(int homeLosses) {
		this.homeLosses = homeLosses;
	}

	public int getAwayWins() {
		return awayWins;
	}

	public void setAwayWins(int awayWins) {
		this.awayWins = awayWins;
	}

	public int getAwayLosses() {
		return awayLosses;
	}

	public void setAwayLosses(int awayLosses) {
		this.awayLosses = awayLosses;
	}

	public int getOpponentPointsScored() {
		return opponentPointsScored;
	}

	public void setOpponentPointsScored(int opponentPointsScored) {
		this.opponentPointsScored = opponentPointsScored;
	}

	public int getMinutesPlayed() {
		return minutesPlayed;
	}

	public void setMinutesPlayed(int minutesPlayed) {
		this.minutesPlayed = minutesPlayed;
	}

	public int getFieldGoalsMade() {
		return fieldGoalsMade;
	}

	public void setFieldGoalsMade(int fieldGoalsMade) {
		this.fieldGoalsMade = fieldGoalsMade;
	}

	public int getFieldGoalsAttemped() {
		return fieldGoalsAttemped;
	}

	public void setFieldGoalsAttemped(int fieldGoalsAttemped) {
		this.fieldGoalsAttemped = fieldGoalsAttemped;
	}

	public double getFieldGoalPercentage() {
		return fieldGoalPercentage;
	}

	public void setFieldGoalPercentage(double fieldGoalPercentage) {
		this.fieldGoalPercentage = fieldGoalPercentage;
	}

	public int getThreePointersMade() {
		return threePointersMade;
	}

	public void setThreePointersMade(int threePointersMade) {
		this.threePointersMade = threePointersMade;
	}

	public int getThreePointersAttempted() {
		return threePointersAttempted;
	}

	public void setThreePointersAttempted(int threePointersAttempted) {
		this.threePointersAttempted = threePointersAttempted;
	}

	public double getThreePointerPercentage() {
		return threePointerPercentage;
	}

	public void setThreePointerPercentage(double threePointerPercentage) {
		this.threePointerPercentage = threePointerPercentage;
	}

	public int getTotalRebounds() {
		return totalRebounds;
	}

	public void setTotalRebounds(int totalRebounds) {
		this.totalRebounds = totalRebounds;
	}

	public int getAssists() {
		return assists;
	}

	public void setAssists(int assists) {
		this.assists = assists;
	}

	public int getSteals() {
		return steals;
	}

	public void setSteals(int steals) {
		this.steals = steals;
	}

	public int getBlocks() {
		return blocks;
	}

	public void setBlocks(int blocks) {
		this.blocks = blocks;
	}

	public int getTurnovers() {
		return turnovers;
	}

	public void setTurnovers(int turnovers) {
		this.turnovers = turnovers;
	}

	public int getPersonalFouls() {
		return personalFouls;
	}

	public void setPersonalFouls(int personalFouls) {
		this.personalFouls = personalFouls;
	}

	public String getInput() {
		return input;
	}

	public void setInput(String input) {
		this.input = input;
	}

	public int getFreeThrowsMade() {
		return freeThrowsMade;
	}

	public void setFreeThrowsMade(int freeThrowsMade) {
		this.freeThrowsMade = freeThrowsMade;
	}

	public int getFreeThrowsAttempted() {
		return freeThrowsAttempted;
	}

	public void setFreeThrowsAttempted(int freeThrowsAttempted) {
		this.freeThrowsAttempted = freeThrowsAttempted;
	}

	public double getFreeThrowPercentage() {
		return freeThrowPercentage;
	}

	public void setFreeThrowPercentage(double freeThrowPercentage) {
		this.freeThrowPercentage = freeThrowPercentage;
	}

	public int getOffensiveRebounds() {
		return offensiveRebounds;
	}

	public void setOffensiveRebounds(int offensiveRebounds) {
		this.offensiveRebounds = offensiveRebounds;
	}
	
}