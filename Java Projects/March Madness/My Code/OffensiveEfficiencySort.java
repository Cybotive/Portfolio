//Joshua Lini 11-30-18 MarchMadness Project
import java.util.Comparator;

public class OffensiveEfficiencySort implements Comparator<Team> {

	@Override
	public int compare(Team team1, Team team2) {
		if(team1.getOffensiveEfficiency() > team2.getOffensiveEfficiency()) {
			return 1;
		} else if(team1.getOffensiveEfficiency() < team2.getOffensiveEfficiency()) {
			return -1;
		}
		return 0;
	}

}
