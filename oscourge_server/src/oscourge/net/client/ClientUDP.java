package oscourge.net.client;

import java.io.IOException;
import java.net.DatagramPacket;
import java.net.DatagramSocket;
import java.net.InetAddress;
import java.net.SocketException;
import java.util.Scanner;

import javax.swing.JTextArea;

import org.json.JSONObject;

import oscourge.net.data.MessageUDP;

public class ClientUDP {

	public static int MAX_CLIENTS = 2;
	public static int BUFFER_SIZE = 8156;
	public static int PORT = 1331;

	private InetAddress address;
	private String name;
	private int port;
	private boolean isConnected;

	private JTextArea console;
	private DatagramSocket clientSocket;
	private boolean sendingData;

	public boolean isConnected() {
		return isConnected;
	}

	public void setConnected(boolean isConnected) {
		this.isConnected = isConnected;
	}

	public ClientUDP(InetAddress address, int port, String name) throws SocketException {
		this.address = address;
		this.port = port;
		this.name = name;
	}

	public ClientUDP(InetAddress address, int port) throws SocketException {
		this.address = address;
		this.port = port;
	}

	public ClientUDP(InetAddress address, int port, JTextArea console) throws SocketException {
		this.address = address;
		this.port = port;
		this.console = console;
	}

	public InetAddress getAddress() {
		return address;
	}

	public String getName() {
		return name;
	}

	public void setName(String name) {
		this.name = name;
	}

	public void setAddress(InetAddress address) {
		this.address = address;
	}

	public int getPort() {
		return port;
	}

	public void setPort(int port) {
		this.port = port;
	}

	private void print(String mess) {
		console.append(mess);
		console.append("\n");
		System.out.println(mess);
	}

	public void connect() {

		try {
			clientSocket = new DatagramSocket();

			boolean listening = true;

			// Receive thread
			Thread t = new Thread(new Runnable() {
				public void run() {
					try {
						while (listening) {
							byte[] buff = new byte[BUFFER_SIZE];
							DatagramPacket pck = new DatagramPacket(buff, buff.length, address, PORT);
							clientSocket.receive(pck);
							print("data received : " + new String(pck.getData()));
						}
					} catch (IOException e) {
						e.printStackTrace();
					}
				}
			});
			t.start();
			
			MessageUDP msg = new MessageUDP();
			msg.setType(MessageUDP.CONNECTION);
			sendMsg(msg);
			
			print("connecting");

		} catch (SocketException e1) {
			e1.printStackTrace();
		}
	}

	public void startData() {
		sendingData = true;
		Thread t = new Thread(new Runnable() {
			public void run() {
				try {
					MessageUDP msg = new MessageUDP();
					msg.setMsg("data");
					msg.setType(MessageUDP.DATA);
					while (sendingData) {
						sendMsg(msg);
						Thread.sleep(2000);
					}
				} catch (InterruptedException e) {
					e.printStackTrace();
				}
			}
		});
		t.start();
	}

	public void stopData() {
		sendingData = false;
	}

	public void sendPause() {
		MessageUDP msg = new MessageUDP();
		msg.setType(MessageUDP.PAUSE);
		sendMsg(msg);
	}

	public void sendResume() {
		MessageUDP msg = new MessageUDP();
		msg.setType(MessageUDP.RESUME);
		sendMsg(msg);
	}

	public void sendHandShake() {
		MessageUDP msg = new MessageUDP();
		msg.setType(MessageUDP.HANDSHAKE);
		sendMsg(msg);
	}

	public void sendConnectionError() {
		MessageUDP msg = new MessageUDP();
		msg.setType(MessageUDP.CONNECTION_ERROR);
		sendMsg(msg);
	}

	public void sendReConnection() {
		MessageUDP msg = new MessageUDP();
		msg.setType(MessageUDP.RECONNECTION);
		sendMsg(msg);
	}

	public void sendMsg(MessageUDP msg) {
		try {
			byte[] buff = new byte[BUFFER_SIZE];
			buff = msg.toJson().toString().getBytes();
			DatagramPacket dp = new DatagramPacket(buff, buff.length, address, PORT);
			clientSocket.send(dp);
		} catch (IOException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
	}

	/**
	 * Old (Talkative)
	 * @param args
	 */
	public static void main(String[] args) {
		try {
			DatagramSocket client = new DatagramSocket();
			InetAddress adress = InetAddress.getByName("127.0.0.1");

			boolean running = true;

			// Receive thread
			Thread t = new Thread(new Runnable() {
				public void run() {
					try {
						while (running) {
							byte[] buff = new byte[BUFFER_SIZE];
							DatagramPacket pck = new DatagramPacket(buff, buff.length, adress, PORT);
							client.receive(pck);
							System.out.println("data received : " + new String(pck.getData()));
							JSONObject message = new JSONObject(new String(pck.getData()));
							System.out.println(message.get("name") + "> " + message.get("msg"));
						}
					} catch (IOException e) {
						// TODO Auto-generated catch block
						e.printStackTrace();
					}
				}
			});
			t.start();

			// Send thread
			Scanner scan = new Scanner(System.in);
			System.out.println("Enter your name :");
			String msg = scan.nextLine();
			JSONObject message = new JSONObject();
			message.put("name", msg);
			message.put("msg", msg);
			message.put("type", MessageUDP.CONNECTION);

			byte[] buff2 = new byte[BUFFER_SIZE];

			System.out.println("Hey " + msg + ", you can now send messages");
			while (running) {
				msg = scan.nextLine();

				message.remove("msg");
				message.put("msg", msg);

				buff2 = message.toString().getBytes();
				DatagramPacket dp = new DatagramPacket(buff2, buff2.length, adress, PORT);
				message.put("type", MessageUDP.DATA);
				client.send(dp);
			}

			client.close();
			scan.close();
		} catch (IOException e) {
			e.printStackTrace();
		}
	}
}
