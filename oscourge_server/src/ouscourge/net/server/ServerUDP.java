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
import ouscourge.net.util.Consts;

public class ServerUDP implements Runnable {

	public static final String TYPE = "type";
	public static final String NAME = "name";
	public static final int P1 = 1;
	public static final int P2 = 2;

	private int gameState;
	public static final int INIT = 1;
	public static final int PLAYING = 2;
	public static final int WAITING = 3;
	public static final int PAUSE = 4;
	public static final int CONNECTION_ERROR = 5;

	private boolean running;
	private ClientUDP player1;
	private int hdShkReceivedP1;
	private ClientUDP player2;
	private int hdShkReceivedP2;

	private HandShake pauseHandShake; 
	
	private JTextArea console;

	private final DatagramSocket serverSocket;

	public ServerUDP(int port, JTextArea console) throws IOException {
		this.serverSocket = new DatagramSocket(port);
		this.console = console;
		gameState = INIT;
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

			switch (gameState) {
			case INIT:
				beforeStart(client, message);
				break;
			case PLAYING:
				duringGame(client, message);
				break;
			case CONNECTION_ERROR:
				break;
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
					print(Consts.newClient + msg.getName());
				} else if (player2 == null) {
					player2 = client;
					print(Consts.newClient + msg.getName());
				} else {
					print("ERROR : this should not happen (game should have started)");
				}

			}

			if (player1 != null && player2 != null) {
				startGame();
			} else {
				sendTo(client,MessageUDP.WAIT);
			}

		} else {
			if (ind != -1) // It's the connected client
				sendTo(client,MessageUDP.WAIT);
			else
				sendErrorMessage(client, Consts.wrongMsgType);
			print(Consts.wrongMsgType);
		}
	}

	private void duringGame(ClientUDP client, MessageUDP msg) throws IOException {
		int ind = isConnected(client);

		if (ind == -1) {
			print(Consts.tooManyClients);
			sendErrorMessage(client, Consts.gameFullMsg);
		} else if (ind != P1 && ind != P2)
			print(Consts.badIndex);

		switch (msg.getType()) {

		case MessageUDP.DATA:
			if (ind == P1)
				sendTo(player2, msg);
			else
				sendTo(player1, msg);

		case MessageUDP.CONNECTION_ERROR:
			gameState =  CONNECTION_ERROR;
			if (ind == P1) {

			} else {

			}
			break;
		case MessageUDP.PAUSE:
			pauseGame();
			break;
		case MessageUDP.HANDSHAKE:
			if(ind==P1) hdShkReceivedP1++; else hdShkReceivedP2++;
		break;
		case MessageUDP.RESUME:
			resumeGame();
			break;
		}
	}

	private void resumeGame() throws IOException {
		gameState = PLAYING;
		if(pauseHandShake != null)
			pauseHandShake.stopHandShake();
		sendTo(player1,MessageUDP.RESUME);
		sendTo(player2,MessageUDP.RESUME);
	}
	
	private void startGame() throws IOException {
		gameState = PLAYING;
		sendRoles();
	}

	private void pauseGame() throws IOException {
		gameState = PAUSE;
		sendTo(player1,MessageUDP.PAUSE);
		sendTo(player2,MessageUDP.PAUSE);
		hdShkReceivedP1 = 0;
		hdShkReceivedP2 = 0;
		pauseHandShake = new HandShake(this);
		(new Thread(pauseHandShake)).start();
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
	
	public void sendTo(ClientUDP client, int type) throws IOException {
		MessageUDP m = new MessageUDP();
		m.setType(type);
		sendTo(client, m);
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
	
	private void sendErrorMessage(ClientUDP client, String errorMsg) throws IOException {
		MessageUDP m = new MessageUDP();
		m.setType(MessageUDP.ERROR);
		m.setMsg(errorMsg);
		sendTo(client, m);
	}

//	private void sendWaitMessage(ClientUDP client) throws IOException {
//		MessageUDP m = new MessageUDP();
//		m.setType(MessageUDP.WAIT);
//		sendTo(client, m);
//	}
//
//	private void sendPauseMessage(ClientUDP client) throws IOException {
//		MessageUDP m = new MessageUDP();
//		m.setType(MessageUDP.PAUSE);
//		sendTo(client, m);
//	}
//	
//	private void sendResumeMessage(ClientUDP client) throws IOException {
//		MessageUDP m = new MessageUDP();
//		m.setType(MessageUDP.RESUME);
//		sendTo(client, m);
//	}
//	
//	private void sendHandShakeMessage(ClientUDP client) throws IOException {
//		MessageUDP m = new MessageUDP();
//		m.setType(MessageUDP.HANDSHAKE);
//		sendTo(client, m);
//	}

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
		private boolean running;

		HandShake(ServerUDP server) {
			this.server = server;
			this.running=true;
		}

		@Override
		public void run() {
			System.out.println("HandShake starts");
			int handShakeSent=0;
			while (running) {
				try {
					if(server.gameState == PAUSE) {
						int ratioP1 = handShakeSent - server.hdShkReceivedP1;
						int ratioP2 = handShakeSent - server.hdShkReceivedP2;
						
						if(ratioP1 > 10 || ratioP2 > 10) {
							// HandShake failed
							print("handshake failed");
						}
					
						server.sendTo(server.player1,MessageUDP.HANDSHAKE);
						server.sendTo(server.player2,MessageUDP.HANDSHAKE);
						handShakeSent++;
					}
					Thread.sleep(500);
				} catch (IOException | InterruptedException e) {
					e.printStackTrace();
				}
			}
		}
		
		public void stopHandShake() {
			this.running=false;
		}

	}

}