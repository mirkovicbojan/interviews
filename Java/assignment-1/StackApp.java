import java.util.*;






class Solution{

	//Checks if input string has only brackets or if it has an even number of characters, if not returns false.
	static boolean inputValidation(String input) {
		
		if(input == "") {
			return true;
		}
		
		if (null == input || ((input.length() % 2) != 0)) 
		{
		    return false;
		} 
		else 
		{
		    char[] ch = input.toCharArray();
		    for (char c : ch) {
		        if (!(c == '{' || c == '[' || c == '(' || c == '}' || c == ']' || c == ')')) 
		        {
		            return false;
		        }
		    }
		}
		return true;
	}

	static boolean isBalanced(String input) 
	{
		Stack<Character> stack = new Stack<Character>();
		//Traversing through each character of string
		for(int i = 0; i<input.length(); i++) 
		{
			
			char c = input.charAt(i);
			//Checking if the character is an opening bracket, if it is pushes it to stack and continues to next character
			if(c == '[' || c == '{' || c == '(') 
			{
				stack.push(c);
				continue;
			}
			if(stack.isEmpty()) 
			{
				/*returns false if stack is empty as it does not have any opening brackets
				therefore it isn't balanced*/
				return false;
			}
			
			char check;
			/*
			 	Once it's gone through all of the opening brackets
			 	it checks if the closing brackets are positioned correctly
			 	by seeing if the last character in stack is equal to 
			 	the current character. 
			 */
			switch(c) 
			{
				case ']':
					check = stack.pop();
					if(check == '(' || check == '{') {
						return false;
					}
					break;
				case '}':
					check = stack.pop();
					if(check == '(' || check == '[') {
						return false;
					}
					break;
				case ')':
					check = stack.pop();
					if(check == '[' || check == '{') {
						return false;
					}
					break;
			}		
		}
		return (stack.isEmpty());
	}
	
    public static void main(String []argh)
    {
        Scanner sc = new Scanner(System.in);
        System.out.println("Enter a string.");
        while (sc.hasNext()) {
            String input=sc.next();
            if(inputValidation(input)) {
            	System.out.println(isBalanced(input));
            }
            else {
            	System.out.println("false");
            	
            }
        }

    }
}