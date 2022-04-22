package CannedFoodWarehouse;

import java.io.*;
import java.time.*;
import java.util.*;




public class main {
	private static ArrayList<Bin> allBins = new ArrayList<Bin>();
	
	
public static void LoadCans() {

		File file = new File("data/canned_food.csv");
		try 
		{
			Scanner inputStream = new Scanner(file);
			inputStream.next();
			while (inputStream.hasNext()) 
			{

				String data = inputStream.next();
				String[] canF = data.split(",");
				Can can = new Can();
				can.setId(Integer.parseInt(canF[0]));
				can.setType(canF[1]);
				can.setExpiration_date(canF[2]);
				
				for(int i = 0; i<10; i++) 
				{
					if(allBins.get(i).getId() == Integer.parseInt(canF[3])) 
					{
						allBins.get(i).setType(canF[1]);
						if(allBins.get(i).addToCanList(can)) {
							continue;
						}
						else {
							continue;
						}
					}
				}
					
			}
			inputStream.close();
		}
		catch (FileNotFoundException e) 
		{
			System.out.println("File not found.");
			
		}
	}

public static boolean checkID(int id) {
	ArrayList<Can> allcans = new ArrayList<Can>(); 
	for(int i = 0; i< allBins.size(); i++) 
	{
		allcans.addAll(allBins.get(i).getCanList_array());
	}
	for(int i = 0; i<allcans.size(); i++) {
		if(allcans.get(i).getId() == id) {
			return true;
		}
	}
	return false;
}

public static void listAllBins() {
	System.out.println("Bin No. | Type           | No. Items | Min. Expiry Date ");
	for(int i = 0; i<10; i++) {
		if(allBins.get(i).getCanNumber() == 0) {
			continue;
		}
		else {allBins.get(i).BinToString();}
	}
}

public static void listSingleBin() {
	int id;
	Scanner sc = new Scanner(System.in);
	System.out.println("Enter the id of the bin you'd like to see: \n");
	id = sc.nextInt();
	if(id<=10) {
		for(int i = 0; i<10; i++) {
			if(allBins.get(i).getId() == id) {
				allBins.get(i).getCanList();
				return;
			}
		}
	}
	else 
	{
		System.out.println("Please enter a number from 1-10.");
		return;
	}
}	

public static void addFoodToBin() {
	Scanner sc = new Scanner(System.in);
	System.out.println("Please enter food ID:");
	int id = sc.nextInt();
	if(checkID(id)) {
		System.out.println("Please enter a unique ID for the food item.");
		return;
	}
	System.out.println("Please enter food type inside of quotation marks:");
	String type = sc.next();
	System.out.println("Please enter expiration date:");
	String expiration = sc.next();
	Can can = new Can(id, type, expiration);
	for(int i = 0; i< allBins.size(); i++) {
		if(allBins.get(i).addToCanList(can)) 
		{
			System.out.println("Food stored in existing bin.");
			return;
		}
	}
	for(int i = 0; i< allBins.size(); i++) {
		if(allBins.get(i).getCanNumber() == 0) {
			allBins.get(i).addToEpmtyCanList(can);
			allBins.get(i).setType(type);
			System.out.println("Food stored properly in empty bin.");
			return;
		}
	}
	System.out.println("Food was unable to be stored as there are no empty bins.");

	
	
	return;
	
	
}

public static void removeFoodFromBin() {
	Scanner sc = new Scanner(System.in);
	System.out.println("Please enter food ID");
	int id = sc.nextInt();
	if(!checkID(id)) {
		System.out.println("Please enter an existing ID.");
		return;
	}
	
	for(int i = 0; i< allBins.size(); i++) 
	{
		allBins.get(i).removeFromCanList(id);
	}
}


public static void removeExpiredFood() {
	
	for(int i = 0; i< allBins.size(); i++) 
	{
		allBins.get(i).removeExpiredFood();
	}
	System.out.println("All expired food has been removed. \n");
}

public static void saveChanges() {
	ArrayList<Can> allcans = new ArrayList<Can>();
	try 
	{
		FileWriter fw = new FileWriter("data/canned_food.csv", false);
		BufferedWriter bw = new BufferedWriter(fw);
		PrintWriter pw = new PrintWriter(bw); 
		for(int i = 0; i< allBins.size(); i++) 
		{
			allcans.clear();
			allcans.addAll(allBins.get(i).getCanList_array());
			for(int j = 0; j<allcans.size(); j++) {
					pw.println(allcans.get(j).getId() +","
								+allcans.get(j).getType()
								+","+allcans.get(j).getExpiration_date()
								+","+allBins.get(i).getId());
					pw.flush();
			
			}
		}
		pw.close();
	}
	catch(IOException e) 
	{
		System.out.println("Error occured while reading file.");
	}
}

	public static void main(String[] args) {
		//Creates 10 empty bins and assigns them id's from 1 to 10
		int i = 1;
		while(i<11) {
			Bin bin = new Bin();
			bin.setId(i);
			allBins.add(bin);
			i++;
		}
		//Loads cans into bins.
		LoadCans();
		Scanner sc = new Scanner(System.in);
		int input;
		boolean done = false;
		
		while(!done) {
			System.out.println(
					"\n1.List all bins \n"
					+ "2. List a single bin \n"
					+ "3. Add food to bin \n"
					+ "4. Remove food from bin \n"
					+ "5. Remove all expired foods \n"
					+ "6. Save and Exit \n");
			input = sc.nextInt();
			switch(input) 
			{
				case 1:
					listAllBins();
					break;
				case 2:
					listSingleBin();
					break;
				case 3:
					try 
					{
						addFoodToBin();
						break;
					}
					catch(java.time.DateTimeException e) 
					{
						System.out.println("Please enter the correct date format. yyyy-mm-dd");
						break;
					}
					
				case 4:
					removeFoodFromBin();
					break;
				case 5:
					removeExpiredFood();
					break;
				case 6:
					 saveChanges();
					 System.out.println("Changes saved. Exiting program.");
					return;
			}
		}
		sc.close();
	}

}
