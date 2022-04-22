package CannedFoodWarehouse;
import java.time.LocalDate;
import java.util.ArrayList;
import java.util.concurrent.atomic.AtomicInteger;

public class Bin {
	private ArrayList<Can> canList;
	private int id = 0;
	private String type;

	public Bin() {
		super();
		this.canList = new ArrayList<Can>();
		this.type = "empty";
	}

	public Bin(ArrayList<Can> canList, String type, int id) {
		this.canList = canList;
		this.type = type;
		this.id = id;
	}
	
	public void removeFromCanList(int id) {
		for(int i = 0; i< canList.size(); i++) {
			if(id == canList.get(i).getId()) {
				canList.remove(i);
			}
			else 
			{
				continue;
			}
		}
	}

	public void removeExpiredFood() {
		for(int i = 0; i < canList.size(); i++) 
		{
			if(canList.get(i).getExpiration_date().isBefore(LocalDate.now())) {
				canList.remove(i);
			}
			else 
			{
				continue;
			}
		}
	}
	
	public void getCanList() {
		
		int i = 0;
		System.out.println("Canned Food ID  | Type               | Expiry Date ");
		while(this.canList.size() > i) 
		{
			System.out.println(canList.get(i).getId()+" | "+ canList.get(i).getType() +"  |"
								+canList.get(i).getExpiration_date());
			i++;
		}
	}
	
	public int getCanNumber() {
		if(this.canList.isEmpty()) {
			return 0;
		}
		else {return this.canList.size();}
	}
	
	public boolean addToCanList(Can can) {
		if(this.type.equals(can.getType()) && this.getCanNumber() < 10) {
			this.canList.add(can);
			return true;
		}
		else if(this.type.equals(can.getType()) && this.getCanNumber() >= 10) {
			System.out.println("The bin for this food type is full, food wasn't stored properly.");
			return false;
		}
		return false;
	}
	
	public void addToEpmtyCanList(Can can) {
		this.canList.add(can);
	}
	
	public void setCanList(ArrayList<Can> canList) {
		this.canList = canList;
	}
	
	public ArrayList<Can> getCanList_array(){
		return canList;
	}

	public int getId() {
		return id;
	}

	public void setId(int id) {
		this.id = id;
		}

	public String getType() {
		return type;
	}

	public void setType(String type) {
		this.type = type;
	}
	
	public void BinToString() {
		
		LocalDate min_exp = canList.get(0).getExpiration_date();
		for(int i = 0; i < canList.size(); i++) {
			if(canList.get(i).getExpiration_date().isBefore(min_exp)) {
				min_exp = canList.get(i).getExpiration_date();
			}
		}
		
		System.out.println( this.id +"      | "+this.type+"   | "+this.getCanNumber()+"       | "+ min_exp);
	}
	

}
