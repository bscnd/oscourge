package ouscourge.net.data;

import org.json.JSONObject;

public class InputValues {
	private boolean jump;
	private float horizontal;
	
	public final static String JUMP = "jump";
	public final static String HORIZONTAL = "horizontal";
	
	public static InputValues valueOf(JSONObject obj) {
		InputValues newObj = new InputValues();
		newObj.setHorizontal(obj.getFloat(HORIZONTAL));
		newObj.setJump(obj.getBoolean(JUMP));
		return newObj;
	}
	
	public JSONObject toJson() {
		JSONObject json = new JSONObject();
		json.put(HORIZONTAL, this.horizontal);
		json.put(JUMP, this.jump);
		return json;
	}

	public float getHorizontal() {
		return horizontal;
	}

	public void setHorizontal(float horizontal) {
		this.horizontal = horizontal;
	}

	public boolean isJump() {
		return jump;
	}

	public void setJump(boolean jump) {
		this.jump = jump;
	}

}
