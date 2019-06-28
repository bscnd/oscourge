package oscourge.net.data;

import org.json.JSONObject;

public class Vector3 {
	private float x;
	public static final String X = "x";
	public static final String Y = "y";
	public static final String Z = "z";
	private float y;
	private float z;

	public static Vector3 valueOf(JSONObject obj) {
		Vector3 newObj = new Vector3();
		newObj.setX(obj.getFloat(X));
		newObj.setY(obj.getFloat(Y));
		newObj.setZ(obj.getFloat(Z));
		return newObj;
	}
	
	public JSONObject toJson() {
		JSONObject newObj = new JSONObject();
		newObj.put(X, this.x);
		newObj.put(Y, this.y);
		newObj.put(Z, this.z);
		return newObj;
	}

	public float getX() {
		return x;
	}

	public void setX(float x) {
		this.x = x;
	}

	public float getY() {
		return y;
	}

	public void setY(float y) {
		this.y = y;
	}

	public float getZ() {
		return z;
	}

	public void setZ(float z) {
		this.z = z;
	}

}
