package ouscourge.net.server;

import java.io.IOException;
import java.net.DatagramPacket;
import java.net.DatagramSocket;
import java.net.SocketException;
import java.util.Arrays;

import javax.swing.JTextArea;

import org.json.JSONObject;

import ouscourge.net.client.ClientUDP;
import ouscourge.net.data.MessageUDP;

public class ServerUDP implements Runnable {

	public static final String TYPE = "type";
	public static final String NAME = "name";
	public static final int P1 = 1;
	public static final int P2 = 2;

	private boolean gameStarted;
	private boolean running;
	private ClientUDP player1;
	private ClientUDP player2;

	private JTextArea console;

	private final DatagramSocket serverSocket;

	public ServerUDP(int port, JTextArea console) throws IOException {
		this.serverSocket = new DatagramSocket(port);
		this.console = console;
		gameStarted = false;
		running = false;
	}

	
	@Override
	public void run() {
		running = true;
		print("Server started on port: " + serverSocket.getLocalPort());
		byte[] data = new byte[1024];
		while (running) {
			try {

				DatagramPacket packet = new DatagramPacket(data, data.length);
				if (!serverSocket.isClosed())
					serverSocket.receive(packet);

				handlePacketReceived(packet);
				
				
			} catch (SocketException e) {
				if (e.getMessage().equals("socket closed"))
					System.out.println("socket closed");
				else
					e.printStackTrace();
			} catch (IOException e) {
				e.printStackTrace();
			}
		}
		shutdown();
	}

	private void handlePacketReceived(DatagramPacket clientPacket) throws IOException {
		byte[] dataReceived = Arrays.copyOf(clientPacket.getData(), clientPacket.getLength());
		JSONObject data = new JSONObject(new String(dataReceived));
		print(new String(dataReceived));

		MessageUDP message = MessageUDP.valueOf(data);

		try {
			
			ClientUDP client = new ClientUDP(clientPacket.getAddress(), clientPacket.getPort());

			if (!gameStarted) {
				beforeStart(client, message);
			} else {
				duringGame(client, message);
			}

		} catch (SocketException e) {
			e.printStackTrace();
		}

	}

	private void beforeStart(ClientUDP client, MessageUDP msg) throws IOException {
		int ind = isConnected(client);
		if (msg.getType() == MessageUDP.CONNECTION) {

			if (ind == -1) { // client not already connected

				if (player1 == null) {
					player1 = client;
					print("New client connected : " + msg.getName());
				} else if (player2 == null) {
					player2 = client;
					print("New client connected : " + msg.getName());
				} else {
					print("ERROR : this should not happen (game should have started)");
				}

			}

			if (player1 != null && player2 != null) {
				startGame();
			} else {
				sendWaitMessage(client);
			}

		} else {
			if (ind != -1)
				sendWaitMessage(client);
			else
				sendErrorMessage(client, "wrong type of message");
			print("Received a wrong type of message");
		}
	}

	private void duringGame(ClientUDP client, MessageUDP msg) throws IOException {
		int ind = isConnected(client);

		if (ind == -1) {
			print("too many clients");
			sendErrorMessage(client, "this game is full");
		}

		if (msg.getType() == MessageUDP.DATA) {

			switch (ind) {
			case P1:
				sendTo(player2, msg);
				break;
			case P2:
				sendTo(player1, msg);
				break;
			default:
				print("ERROR bad index");
			}
		}
	}
	
	private void startGame() throws IOException {
		gameStarted = true;
		sendRoles();
//		(new Thread(new HandShake(this))).start();
	}

	private void sendErrorMessage(ClientUDP client, String errorMsg) throws IOException {
		MessageUDP m = new MessageUDP();
		m.setType(MessageUDP.ERROR);
		m.setMsg(errorMsg);
		sendTo(client, m);
	}

	private void sendWaitMessage(ClientUDP client) throws IOException {
		MessageUDP m = new MessageUDP();
		m.setType(MessageUDP.WAIT);
		sendTo(client, m);
	}

	private void sendRoles() throws IOException {
		MessageUDP m = new MessageUDP();
		m.setType(MessageUDP.ROLE);
		m.setMsg(P1 + "");
		sendTo(player1, m);
		m.setMsg(P2 + "");
		sendTo(player2, m);
	}

	private void print(String mess) {
		console.append(mess);
		console.append("\n");
		System.out.println(mess);
	}

	public void sendTo(ClientUDP client, byte[] data) throws IOException {
		DatagramPacket p = new DatagramPacket(data, data.length, client.getAddress(), client.getPort());
		serverSocket.send(p);
	}

	public void sendTo(ClientUDP client, MessageUDP message) throws IOException {
		byte[] data = new byte[8156];
		data = message.toJson().toString().getBytes();
		DatagramPacket p = new DatagramPacket(data, data.length, client.getAddress(), client.getPort());
		serverSocket.send(p);
	}

	private int isConnected(ClientUDP client) {
		if (player1 != null && player1.getAddress().equals(client.getAddress())
				&& player1.getPort() == client.getPort())
			return 1;
		if (player2 != null && player2.getAddress().equals(client.getAddress())
				&& player2.getPort() == client.getPort())
			return 2;
		return -1;
	}

	/**
	 * Shuts down the server
	 */
	public void shutdown() {
		running = false;
		serverSocket.close();
		console.setText("");
	}
	
	class HandShake implements Runnable {
		private final ServerUDP server;

		HandShake(ServerUDP server) {
			this.server = server;
		}

		@Override
		public void run() {
			System.out.println("HandShake starts");
			MessageUDP message;
			while(server.running) {
				message = new MessageUDP();
				try {
					server.sendTo(server.player1, message);
				} catch (IOException e) {
					e.printStackTrace();
				}
			}
		}

	}

}