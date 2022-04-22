import java.util.HashSet;


public class Sudoku {

    public static void main(String[] args) {
        char[][] board1 = new char[][]{
                {'5', '3', '.', '.', '7', '.', '.', '.', '.'},
                {'6', '.', '.', '1', '9', '5', '.', '.', '.'},
                {'.', '9', '8', '.', '.', '.', '.', '6', '.'},
                {'8', '.', '.', '.', '6', '.', '.', '.', '3'},
                {'4', '.', '.', '8', '.', '3', '.', '.', '1'},
                {'7', '.', '.', '.', '2', '.', '.', '.', '6'},
                {'.', '6', '.', '.', '.', '.', '2', '8', '.'},
                {'.', '.', '.', '4', '1', '9', '.', '.', '5'},
                {'.', '.', '.', '.', '8', '.', '.', '7', '9'}
        };

        System.out.println(isValidSudoku(board1)); // Output: TRUE

        char[][] board2 = {{'8', '3', '.', '.', '7', '.', '.', '.', '.'}
                , {'6', '.', '.', '1', '9', '5', '.', '.', '.'}
                , {'.', '9', '8', '.', '.', '.', '.', '6', '.'}
                , {'8', '.', '.', '.', '6', '.', '.', '.', '3'}
                , {'4', '.', '.', '8', '.', '3', '.', '.', '1'}
                , {'7', '.', '.', '.', '2', '.', '.', '.', '6'}
                , {'.', '6', '.', '.', '.', '.', '2', '8', '.'}
                , {'.', '.', '.', '4', '1', '9', '.', '.', '5'}
                , {'.', '.', '.', '.', '8', '.', '.', '7', '9'}};

        System.out.println(isValidSudoku(board2)); // Output: FALSE


    }

    public static boolean isValidSudoku(char[][] board) 
    {
    	/*Use of hash sets is ideal in this type of search operation
    	 * Hash sets story unique values therefore two of the same number cannot 
    	 * exist in one hash set. We can do the entire search in one Hash Set by adding string
    	 * seperators for rows, columns and boxes. 
    	 * */
    	
    	HashSet<String> exists = new HashSet<String>();
    	for(int i = 0; i < 9; i++) 
    	{
    		for(int j = 0; j < 9; j++) 
    		{
    			char current_number = board[i][j];
    			if(current_number != '.') 
    			{
    				/* We add values that aren't empty (".") into the hash set
    				 * and since .add() has a return value of true or false
    				 * we can check if in any of these rows, columns or boxes 
    				 * that given value already exists and if it does .add will return false
    				 * therefore out function will as well as duplicates cannot exist in
    				 * rows, columns or boxes by sudoku rules. 
    				 * */
    				if(!exists.add(current_number + "found in row" + i) ||
    				   !exists.add(current_number + "found in column" + j) ||
    				   !exists.add(current_number + "found in box" + i/3 + "-" + j/3)){ return false; }
    			}
    		}
    	}
    	//If there were no duplicates (no .add() function returned false) we return true because the board is valid.
    	return true;
    }
 }


