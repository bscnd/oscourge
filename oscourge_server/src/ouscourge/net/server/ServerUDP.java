package ouscourge.net.server;

import java.io.IOException;
import java.net.DatagramPacket;
import java.net.DatagramSocket;
import java.net.SocketException;
import java.net.SocketTimeoutException;
import java.util.Arrays;

import javax.swing.JLabel;
import javax.swing.JTextArea;

import org.json.JSONObject;

import ouscourge.net.client.ClientUDP;
import ouscourge.net.data.MessageUDP;
import ouscourge.net.util.Consts;

public class ServerUDP implements Runnable {

	// Player 1
	private ClientUDP player1;
	private int hdShkReceivedP1;
	public static final int P1 = 1;

	// Player 2
	private ClientUDP player2;
	private int hdShkReceivedP2;
	public static final int P2 = 2;

	// All states for the protocol
	private int gameState;
	public static final int INIT = 1;
	public static final int PLAYING = 2;
	public static final int PAUSE = 3;
	public static final int CONNECTION_ERROR = 4;

	// Threads
	private boolean running; // boolean for the main thread
	private HandShake pauseHandShake;

	// Graphical interaction
	private JTextArea console;
	private JLabel lblState;

	// Server socket
	private final DatagramSocket serverSocket;

	public ServerUDP(int port, JTextArea console, JLabel lblState) throws IOException {
		this.serverSocket = new DatagramSocket(port);
		this.console = console;
		this.lblState = lblState;
		changeState(INIT);
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
				if (gameState == CONNECTION_ERROR)
					serverSocket.setSoTimeout(60000);
				else {
					serverSocket.setSoTimeout(0);
				}
				if (!serverSocket.isClosed())
					serverSocket.receive(packet);

				handlePacketReceived(packet);

			} catch (SocketTimeoutException e) {
				print(Consts.timeout);
				try {
					endGame();
				} catch (IOException e1) {
					// TODO Auto-generated catch block
					e1.printStackTrace();
				}
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
		print(new String(dataReceived));
		JSONObject data = new JSONObject(new String(dataReceived));

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
			case PAUSE:
				pausedGame(client, message);
				break;
			case CONNECTION_ERROR:
				interruptedGame(client, message);
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
					player1.setConnected(true);
					print(Consts.newClient + msg.getName());
				} else if (player2 == null) {
					player2 = client;
					player2.setConnected(true);
					print(Consts.newClient + msg.getName());
				} else {
					print("ERROR : this should not happen (game should have started)");
				}

			}

			if (player1 != null && player2 != null) {
				startGame();
			} else {
				sendTo(client, MessageUDP.WAIT);
			}

		} else {
			if (ind != -1) // It's the connected client
				sendTo(client, MessageUDP.WAIT);
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
		} else if (ind != P1 && ind != P2) {
			print(Consts.badIndex);
		} else {

			switch (msg.getType()) {

			case MessageUDP.DATA:
				if (ind == P1)
					sendTo(player2, msg);
				else
					sendTo(player1, msg);
				break;
			case MessageUDP.CONNECTION_ERROR:
				connectionError();
				break;
			case MessageUDP.PAUSE:
				pauseGame();
				break;

			default:
				print(Consts.wrongMsgType);
				break;
			}
		}
	}

	private void restartGame(ClientUDP client, MessageUDP msg) throws IOException {
		int ind = isConnected(client);
		if (ind == -1) {
			print(Consts.tooManyClients);
			sendErrorMessage(client, Consts.gameFullMsg);
		} else if (ind != P1 && ind != P2) {
			print(Consts.badIndex);
		} else {
			if (ind == P1)
				sendTo(player2, MessageUDP.RESTART);
			else
				sendTo(player1, MessageUDP.RESTART);
		}
	}

	private void pausedGame(ClientUDP client, MessageUDP msg) throws IOException {
		int ind = isConnected(client);

		if (ind == -1) {
			print(Consts.tooManyClients);
			sendErrorMessage(client, Consts.gameFullMsg);
		} else if (ind != P1 && ind != P2) {
			print(Consts.badIndex);
		} else {

			switch (msg.getType()) {
			case MessageUDP.HANDSHAKE:
				if (ind == P1)
					hdShkReceivedP1++;
				else
					hdShkReceivedP2++;
				break;
			case MessageUDP.RESUME:
				resumeGame();
				break;
			case MessageUDP.RESTART:
				restartGame(client, msg);
				try {
					Thread.sleep(200);
				} catch (InterruptedException e) {
					e.printStackTrace();
				}
				resumeGame();
				break;
			default:
				print(Consts.wrongMsgType);
				break;

			}

		}
	}

	private void interruptedGame(ClientUDP client, MessageUDP msg) throws IOException {
		int ind = isConnected(client);

		if (ind == -1) {
			print(Consts.tooManyClients);
			sendErrorMessage(client, Consts.gameFullMsg);
		} else if (ind != P1 && ind != P2) {
			print(Consts.badIndex);
		} else {
			if (msg.getType() == MessageUDP.RECONNECTION) {
				if (ind == P1)
					player1.setConnected(true);
				else
					player2.setConnected(true);

				if (player1.isConnected() && player2.isConnected()) {
					resumeGame();
				}

			} else {
				print(Consts.wrongMsgType);
			}
		}

	}

	private void endGame() throws IOException {
		changeState(INIT);
		if (player1 != null) {
			sendTo(player1, MessageUDP.ENDGAME);
		}
		if (player2 != null) {
			sendTo(player2, MessageUDP.ENDGAME);
		}
		player1 = null;
		player2 = null;
		print(Consts.endGame);
	}

	private void connectionError() throws IOException {
		changeState(CONNECTION_ERROR);
		player1.setConnected(false);
		player2.setConnected(false);
		sendTo(player1, MessageUDP.CONNECTION_ERROR);
		sendTo(player2, MessageUDP.CONNECTION_ERROR);
	}

	private void resumeGame() throws IOException {
		changeState(PLAYING);
		if (pauseHandShake != null)
			pauseHandShake.stopHandShake();
		sendTo(player1, MessageUDP.RESUME);
		sendTo(player2, MessageUDP.RESUME);
	}

	private void startGame() throws IOException {
		changeState(PLAYING);
		System.out.println("changed to playing");
		sendRoles();
	}

	private void pauseGame() throws IOException {
		changeState(PAUSE);
		sendTo(player1, MessageUDP.PAUSE);
		sendTo(player2, MessageUDP.PAUSE);
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
		if (console != null) {
			console.append(mess);
			console.append("\n");
		}
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
		print("message sent type : " + message.getType());
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

	/**
	 * Changes the state of the game and updates the graphical indication
	 * 
	 * @param state
	 */
	private void changeState(int state) {
		if (lblState != null) {
			switch (state) {
			case INIT:
				lblState.setText("INIT");
				break;
			case PLAYING:
				lblState.setText("PLAYING");
				break;
			case PAUSE:
				lblState.setText("PAUSE");
				break;
			case CONNECTION_ERROR:
				lblState.setText("CONNECTION_ERROR");
				break;
			default:
				print("wrong state");
				break;
			}
		}
		gameState = state;
	}

	/**
	 * Shuts the server down
	 */
	public void shutdown() {
		running = false;
		if (pauseHandShake != null)
			pauseHandShake.stopHandShake();
		serverSocket.close();
		if (console != null)
			console.setText("");
	}

	/**
	 * 
	 * @author Nils_Richard
	 *
	 */
	class HandShake implements Runnable {
		private final ServerUDP server;
		private boolean running;

		HandShake(ServerUDP server) {
			this.server = server;
			this.running = true;
		}

		@Override
		public void run() {
			System.out.println("HandShake starts");
			int handShakeSent = 0;
			while (running) {
				try {
					if (server.gameState == PAUSE) {
						int ratioP1 = handShakeSent - server.hdShkReceivedP1;
						int ratioP2 = handShakeSent - server.hdShkReceivedP2;

						if (ratioP1 > 10 || ratioP2 > 10) {
							connectionError();
							print("handshake failed");
							stopHandShake();
						}

						server.sendTo(server.player1, MessageUDP.HANDSHAKE);
						server.sendTo(server.player2, MessageUDP.HANDSHAKE);
						handShakeSent++;
					}
					Thread.sleep(4000);
				} catch (IOException | InterruptedException e) {
					e.printStackTrace();
				}
			}
		}

		public void stopHandShake() {
			this.running = false;
		}

	}

}