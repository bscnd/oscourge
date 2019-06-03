package ouscourge.net.client;

import java.io.IOException;
import java.net.DatagramPacket;
import java.net.DatagramSocket;
import java.net.InetAddress;
import java.net.SocketException;
import java.util.Scanner;

import org.json.JSONObject;

import ouscourge.net.data.MessageUDP;
import ouscourge.util.Constants;

public class ClientUDP {

	private InetAddress address;
	private String name;
	private int port;

	public ClientUDP(InetAddress address, int port, String name) throws SocketException {
		this.address = address;
		this.port = port;
		this.name = name;
	}

	public ClientUDP(InetAddress address, int port) throws SocketException {
		this.address = address;
		this.port = port;
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
							byte[] buff = new byte[Constants.BUFFER_SIZE];
							DatagramPacket pck = new DatagramPacket(buff, buff.length, adress, Constants.PORT);
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

			byte[] buff2 = new byte[Constants.BUFFER_SIZE];

			System.out.println("Hey " + msg + ", you can now send messages");
			while (running) {
				msg = scan.nextLine();

				message.remove("msg");
				message.put("msg", msg);

				buff2 = message.toString().getBytes();
				DatagramPacket dp = new DatagramPacket(buff2, buff2.length, adress, Constants.PORT);
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
