package ouscourge.net.data;

import org.json.JSONObject;

public class MessageUDP {

	public final static int DATA = 1;
	public final static int WAIT = 2;
	public final static int ROLE = 3;
	public final static int ERROR = 4;
	public final static int CONNECTION = 5;
	public final static int CONNECTION_ERROR = 6;
	public final static int PAUSE = 7;
	public final static int HANDSHAKE = 8;
	public final static int RESUME = 9;
	public final static int RECONNECTION = 10;
	public final static int ENDGAME = 11;
	
	public final static String MSG = "msg";
	public final static String TYPE = "type";
	public final static String NAME = "name";
	public final static String POSITION = "position";
	public final static String INPUTVALUES = "inputValues";

	public static MessageUDP valueOf(JSONObject obj) {
		MessageUDP newObj = new MessageUDP();
		if (obj.has(MSG) && !obj.isNull(MSG))
			newObj.setMsg(obj.getString(MSG));
		if (obj.has(NAME) && !obj.isNull(NAME))
			newObj.setName(obj.getString(NAME));
		if (obj.has(TYPE) && !obj.isNull(TYPE))
			newObj.setType(obj.getInt(TYPE));
		if (obj.has(INPUTVALUES) && !obj.isNull(INPUTVALUES))
			newObj.setInputValues(InputValues.valueOf(obj.getJSONObject(INPUTVALUES)));
		if (obj.has(POSITION) && !obj.isNull(POSITION))
			newObj.setPosition(Vector3.valueOf(obj.getJSONObject(POSITION)));
		return newObj;
	}

	private int type;

	private String msg;

	private String name;

	private Vector3 position;

	private InputValues inputValues;

	public InputValues getInputValues() {
		return inputValues;
	}

	public String getMsg() {
		return msg;
	}

	public String getName() {
		return name;
	}

	public Vector3 getPosition() {
		return position;
	}

	public int getType() {
		return type;
	}

	public void setInputValues(InputValues inputValues) {
		this.inputValues = inputValues;
	}

	public void setMsg(String msg) {
		this.msg = msg;
	}

	public void setName(String name) {
		this.name = name;
	}

	public void setPosition(Vector3 position) {
		this.position = position;
	}

	public void setType(int type) {
		this.type = type;
	}

	public JSONObject toJson() {
		JSONObject json = new JSONObject();
		json.put(NAME, this.name);
		json.put(MSG, this.msg);
		json.put(TYPE, this.type + "");
		if (this.position != null)
			json.put(POSITION, this.position.toJson());
		if (this.inputValues != null)
			json.put(INPUTVALUES, this.inputValues.toJson());
		return json;
	}
}
