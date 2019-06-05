package ouscourge.ui;

import java.awt.BorderLayout;
import java.awt.CardLayout;
import java.awt.Component;
import java.awt.Dimension;
import java.awt.EventQueue;
import java.awt.FlowLayout;
import java.awt.GridLayout;
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;
import java.io.IOException;

import javax.swing.Box;
import javax.swing.JButton;
import javax.swing.JFrame;
import javax.swing.JLabel;
import javax.swing.JMenu;
import javax.swing.JMenuBar;
import javax.swing.JMenuItem;
import javax.swing.JOptionPane;
import javax.swing.JPanel;
import javax.swing.JScrollPane;
import javax.swing.JTextArea;
import javax.swing.JTextField;
import javax.swing.SwingConstants;
import javax.swing.border.EmptyBorder;

import ouscourge.net.server.ServerUDP;

public class ServerUI extends JFrame {

	/**
	 * 
	 */
	private static final long serialVersionUID = 1L;
	private static final String version = "3.2";

	private JPanel contentPane;
	private JTextField textField;
	private JLabel lblState;
	public Thread serverThread;
	public ServerUDP server;
	public int port;

	/**
	 * Launch the application.
	 */
	public static void main(String[] args) {
		EventQueue.invokeLater(new Runnable() {
			public void run() {
				try {
					ServerUI frame = new ServerUI();
					frame.setLocationRelativeTo(null);
					frame.setVisible(true);
					frame.setTitle("DotS - Server");
				} catch (Exception e) {
					e.printStackTrace();
				}
			}
		});
	}

	/**
	 * Create the frame.
	 */
	public ServerUI() {
		setResizable(false);
		setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);
		setBounds(100, 100, 400, 300);

		JMenuBar menuBar = new JMenuBar();
		setJMenuBar(menuBar);

		JMenu mnOptions = new JMenu("Options");
		menuBar.add(mnOptions);

		JMenuItem mntmStop = new JMenuItem("Stop");

		mnOptions.add(mntmStop);

		JMenuItem mntmRestart = new JMenuItem("Restart");

		mnOptions.add(mntmRestart);

		JMenu mnHelp = new JMenu("Help");
		menuBar.add(mnHelp);

		JMenuItem mntmInfos = new JMenuItem("Infos");
		mntmInfos.addActionListener(new ActionListener() {
			public void actionPerformed(ActionEvent arg0) {
				JOptionPane.showMessageDialog(null,
						"This little server was brought to you by the creators of Oscourge, have fun !");
			}
		});
		mnHelp.add(mntmInfos);
		contentPane = new JPanel();
		contentPane.setBorder(new EmptyBorder(5, 5, 5, 5));
		setContentPane(contentPane);
		contentPane.setLayout(new BorderLayout(0, 0));

		JPanel panel_2 = new JPanel();
		contentPane.add(panel_2);
		CardLayout cl = new CardLayout(0, 0);
		panel_2.setLayout(cl);

		JPanel panel_3 = new JPanel();
		panel_2.add(panel_3, "pan3");
		panel_3.setLayout(new BorderLayout(0, 0));

		JPanel panel = new JPanel();
		panel_3.add(panel, BorderLayout.CENTER);
		panel.setLayout(new GridLayout(0, 1, 0, 0));

		Component verticalStrut_1 = Box.createVerticalStrut(20);
		panel.add(verticalStrut_1);

		JPanel panel_5 = new JPanel();
		FlowLayout flowLayout = (FlowLayout) panel_5.getLayout();
		flowLayout.setVgap(20);
		panel.add(panel_5);

		JLabel lblPort = new JLabel("Port :");
		panel_5.add(lblPort);

		textField = new JTextField();
		panel_5.add(textField);
		textField.setMinimumSize(new Dimension(100, 25));
		textField.setPreferredSize(new Dimension(100, 25));
		textField.setColumns(10);

		JPanel panel_1 = new JPanel();
		panel.add(panel_1);
		panel_1.setLayout(new FlowLayout(FlowLayout.CENTER, 5, 5));

		JButton btnNewButton = new JButton("Start");
		btnNewButton.setPreferredSize(new Dimension(100, 25));
		panel_1.add(btnNewButton);

		Component verticalStrut = Box.createVerticalStrut(20);
		panel.add(verticalStrut);

		Component horizontalStrut = Box.createHorizontalStrut(100);
		panel_3.add(horizontalStrut, BorderLayout.WEST);

		Component horizontalStrut_1 = Box.createHorizontalStrut(100);
		panel_3.add(horizontalStrut_1, BorderLayout.EAST);

		JPanel panel_4 = new JPanel();
		panel_2.add(panel_4, "pan4");
		panel_4.setLayout(new BorderLayout(0, 0));

		JTextArea textArea = new JTextArea();
		textArea.setEditable(false);
		JScrollPane scrollPane = new JScrollPane(textArea);
		panel_4.add(scrollPane, BorderLayout.CENTER);

		lblState = new JLabel(version);
		lblState.setHorizontalAlignment(SwingConstants.RIGHT);
		contentPane.add(lblState, BorderLayout.SOUTH);

		btnNewButton.addActionListener(new ActionListener() {
			public void actionPerformed(ActionEvent arg0) {
				String text = textField.getText();
				try {
					port = Integer.parseInt(text);
					if (port < 1024 || port > 8191)
						throw new NumberFormatException();

					server = new ServerUDP(port, textArea, lblState);
					serverThread = new Thread(server);
					serverThread.start();

					cl.show(panel_2, "pan4");

				} catch (NumberFormatException e) {
					JOptionPane.showMessageDialog(null, "You have to enter an integer between 1024 and 8191", "Error",
							JOptionPane.ERROR_MESSAGE);
				} catch (IOException e) {
					e.printStackTrace();
					JOptionPane.showMessageDialog(null, "Oups, there was an error", "Error", JOptionPane.ERROR_MESSAGE);
				}

			}
		});

		mntmStop.addActionListener(new ActionListener() {
			public void actionPerformed(ActionEvent e) {
				if (serverThread != null && serverThread.isAlive() && server != null)
					server.shutdown();
				cl.show(panel_2, "pan3");
				lblState.setText(version);
			}
		});

		mntmRestart.addActionListener(new ActionListener() {
			public void actionPerformed(ActionEvent e) {
				if (serverThread != null && serverThread.isAlive() && server != null) {
					server.shutdown();
				}
				try {
					server = new ServerUDP(port, textArea, lblState);

					serverThread = new Thread(server);
					serverThread.start();

					cl.show(panel_2, "pan4");
				} catch (IOException e1) {
					e1.printStackTrace();
				}
			}
		});

	}

}
