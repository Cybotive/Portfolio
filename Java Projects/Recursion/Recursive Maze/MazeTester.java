//Joshua Lini 2-22-19

public class MazeTester {
	
	private static int[][] grid = { {1,1,1,0,1,1,0,0,0,1,1,1,1},
									{1,0,1,1,1,0,1,1,1,1,0,0,1},
									{0,0,0,0,1,0,1,0,1,0,1,0,0},
									{1,1,1,0,1,1,1,0,1,0,1,1,1},
									{1,0,1,0,0,0,0,1,1,1,0,0,1},
									{1,0,1,1,1,1,1,1,0,1,1,1,1},
									{1,0,0,0,0,0,0,0,0,0,0,0,0},
									{1,1,1,1,1,1,1,1,1,1,1,1,1}};
	
	public static void main(String[] args) {
		
		System.out.println("Unsolved Maze:");
		printMaze(grid);
		System.out.println();//newline
		
		if(traverse(0, 0) == false) {
			System.out.println("Maze Unsolvable. Here is My Attempt:");
			printMaze(grid);
		}else {
			System.out.println("Solved Maze:");
			printMaze(grid);
			System.out.println();//newline
			printMazeSolutionPretty(grid);
		}
		
	}
	
	private static boolean traverse(int x, int y) {
		
		if(!isValidCoords(x, y)) { //This happens here so we don't get any index exceptions and such
			return false;
		}
		
		if(grid[y][x] == 0) { //Current square is a wall
			return false;
		}
		
		grid[y][x] = 7;//So far successful
		
		if(x == grid[0].length-1 && y == grid.length-1) { //Base Case - Reached goal
			return true;
		}
		
		if(isValidCoords(x, y-1)) { //Not sure if checking this is still necessary with the check above, but not worth getting rid of it
			if(traverse(x, y-1)) //Up
				return true;//We got what we need here, no need to try other directions currently
		}
		
		if(isValidCoords(x+1, y)) {
			if(traverse(x+1, y)) //Right
				return true;
		}
		
		if(isValidCoords(x, y+1)) {
			if(traverse(x, y+1)) //Down
				return true;
		}
		
		if(isValidCoords(x-1, y)) {
			if(traverse(x-1, y)) //Left
				return true;
		}
		
		grid[y][x] = 3;//well, we tried
		return false;//nowhere to go from here, so backtrack
	}
	
	private static boolean isValidCoords(int x, int y) {
		
		if(x < 0 || x > grid[0].length-1 || y < 0 || y > grid.length-1) { //Coordinates out of bounds
			return false;
		}else if(grid[y][x] > 1){ //Have we already been there?
			return false;
		}else { //All good
			return true;
		}
		
	}
	
	private static void printMaze(int[][] maze) { //Custom method to make it prettier than [x, x1, x2]
		for(int y = 0; y < maze.length; y++) { //Traverse the rows
			for(int x = 0; x < maze[y].length; x++) { //Traverse the columns
				System.out.print(maze[y][x] + " ");
			}
			System.out.println();//newline
		}
	}
	
	private static void printMazeSolutionPretty(int[][] maze) { //Way easier to follow the solution
		for(int y = 0; y < maze.length; y++) { //Traverse the rows
			for(int x = 0; x < maze[y].length; x++) { //Traverse the columns
				if(maze[y][x] != 7) {
					System.out.print("# ");
				}else {
					System.out.print("- ");
				}
			}
			System.out.println();//newline
		}
	}
	
}
