package CannedFoodWarehouse;
import java.time.LocalDate;
import java.time.format.DateTimeFormatter;

public class Can {
	private int id;
	private String type;
	private LocalDate expiration_date;

	public Can() {
		super();
	}

	public Can(int id, String type, String expiration_date) {
		setExpiration_date(expiration_date);
		this.id = id;
		this.type = type;
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

	public LocalDate getExpiration_date() {
		return expiration_date;
	}

	public void setExpiration_date(String expiration_dateS) {
		
			LocalDate expiration_date = LocalDate.parse(expiration_dateS);
			this.expiration_date = expiration_date;
			return;
		
		
	}

}
