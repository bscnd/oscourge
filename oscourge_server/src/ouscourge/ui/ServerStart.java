package ouscourge.ui;

import java.io.IOException;

import ouscourge.net.server.ServerUDP;

public class ServerStart {

	public static void main(String[] args) {
		int port = 0;
		try {
			port = Integer.parseInt(args[0]);
			if (port < 1024 || port > 8191)
				throw new NumberFormatException();
			
			ServerUDP server = new ServerUDP(port, null, null);
			Thread serverThread = new Thread(server);
			serverThread.start();

		} catch (NumberFormatException e) {
			System.out.println("You have to enter an integer between 1024 and 8191");
		} catch (IOException e) {
			e.printStackTrace();
		} catch (ArrayIndexOutOfBoundsException e) {
			System.out.println("You have to pass the port for the server in first argument");
		}

	}

}
