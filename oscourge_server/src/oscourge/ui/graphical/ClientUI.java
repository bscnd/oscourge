package oscourge.ui.graphical;

import java.awt.BorderLayout;
import java.awt.EventQueue;

import javax.swing.JFrame;
import javax.swing.JPanel;
import javax.swing.border.EmptyBorder;

import oscourge.net.client.ClientUDP;

import javax.swing.JTextArea;
import java.awt.GridLayout;
import javax.swing.JButton;
import javax.swing.JLabel;
import java.awt.event.ActionListener;
import java.net.InetAddress;
import java.awt.event.ActionEvent;
import javax.swing.JScrollPane;

public class ClientUI extends JFrame {

	/**
	 * 
	 */
	private static final long serialVersionUID = -4906440519081926058L;

	private JPanel contentPane;

	private static ClientUDP client;

	public static JTextArea console;
	
	/**
	 * Launch the application.
	 */
	public static void main(String[] args) {
		EventQueue.invokeLater(new Runnable() {
			public void run() {
				try {
					ClientUI frame = new ClientUI();
					frame.setVisible(true);

					client = new ClientUDP(InetAddress.getByName("127.0.0.1"), 1331, console);

				} catch (Exception e) {
					e.printStackTrace();
				}
			}
		});
	}

	/**
	 * Create the frame.
	 */
	public ClientUI() {
		setTitle("ClientUI");
		setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);
		setBounds(100, 100, 450, 500);
		contentPane = new JPanel();
		contentPane.setBorder(new EmptyBorder(5, 5, 5, 5));
		contentPane.setLayout(new BorderLayout(0, 0));
		setContentPane(contentPane);

		JPanel mainPanel = new JPanel();
		contentPane.add(mainPanel, BorderLayout.CENTER);
		mainPanel.setLayout(new BorderLayout(0, 0));

		JPanel panel = new JPanel();
		mainPanel.add(panel, BorderLayout.CENTER);
		panel.setLayout(new BorderLayout(0, 0));

		console = new JTextArea();
		JScrollPane scrollPane = new JScrollPane(console);
		panel.add(scrollPane);

		JPanel panel_1 = new JPanel();
		mainPanel.add(panel_1, BorderLayout.NORTH);
		panel_1.setLayout(new GridLayout(3, 3, 0, 0));

		JPanel panel_2 = new JPanel();
		panel_1.add(panel_2);

		JButton btnConnect = new JButton("Connect");
		btnConnect.addActionListener(new ActionListener() {
			public void actionPerformed(ActionEvent e) {
				client.connect();
			}
		});
		panel_2.add(btnConnect);

		JPanel panel_7 = new JPanel();
		panel_1.add(panel_7);

		JButton btnStartdata = new JButton("StartData");
		btnStartdata.addActionListener(new ActionListener() {
			public void actionPerformed(ActionEvent e) {
				client.startData();
			}
		});
		panel_7.add(btnStartdata);

		JPanel panel_9 = new JPanel();
		panel_1.add(panel_9);

		JButton btnStopdata = new JButton("StopData");
		btnStopdata.addActionListener(new ActionListener() {
			public void actionPerformed(ActionEvent e) {
				client.stopData();
			}
		});
		panel_9.add(btnStopdata);

		JPanel panel_8 = new JPanel();
		panel_1.add(panel_8);

		JButton btnPause = new JButton("Pause");
		btnPause.addActionListener(new ActionListener() {
			public void actionPerformed(ActionEvent e) {
				client.sendPause();
			}
		});
		panel_8.add(btnPause);

		JPanel panel_5 = new JPanel();
		panel_1.add(panel_5);

		JButton btnResume = new JButton("Resume");
		btnResume.addActionListener(new ActionListener() {
			public void actionPerformed(ActionEvent e) {
				client.sendResume();
			}
		});
		panel_5.add(btnResume);

		JLabel label_1 = new JLabel("");
		panel_1.add(label_1);

		JPanel panel_4 = new JPanel();
		panel_1.add(panel_4);

		JButton btnConnectionerror = new JButton("ConnectionError");
		btnConnectionerror.addActionListener(new ActionListener() {
			public void actionPerformed(ActionEvent e) {
				client.sendConnectionError();
			}
		});
		panel_4.add(btnConnectionerror);

		JPanel panel_6 = new JPanel();
		panel_1.add(panel_6);

		JButton btnHandshake = new JButton("HandShake");
		btnHandshake.addActionListener(new ActionListener() {
			public void actionPerformed(ActionEvent e) {
				client.sendHandShake();
			}
		});
		panel_6.add(btnHandshake);

		JPanel panel_3 = new JPanel();
		panel_1.add(panel_3);

		JButton btnReconnection = new JButton("Reconnection");
		btnReconnection.addActionListener(new ActionListener() {
			public void actionPerformed(ActionEvent e) {
				client.sendReConnection();
			}
		});
		panel_3.add(btnReconnection);
		
		JButton btnClear = new JButton("CLEAR");
		btnClear.addActionListener(new ActionListener() {
			public void actionPerformed(ActionEvent arg0) {
				console.setText("");
			}
		});
		mainPanel.add(btnClear, BorderLayout.SOUTH);
	}

}
