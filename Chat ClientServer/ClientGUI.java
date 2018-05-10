import javax.swing.*;
import java.awt.*;
import java.awt.event.*;



public class ClientGUI extends JFrame implements ActionListener {

	private static final long serialVersionUID = 1L;
	private JLabel label;
	private JTextField tf;
	private JTextField tfServer, tfPort;
	private JButton login, logout, Online;
	private JTextArea ta;
	private boolean connected;
	private Client client;
	private int defaultPort;
	private String defaultHost;

	ClientGUI(String host, int port) {

		super("Chat Client");
		defaultPort = port;
		defaultHost = host;
		
		JPanel centerPanel = new JPanel(new GridLayout(3,1));
		JPanel serverAndPort = new JPanel(new GridLayout(1,5, 1, 3));
		tfServer = new JTextField(host);
		tfPort = new JTextField("" + port);
		tfPort.setHorizontalAlignment(SwingConstants.RIGHT);

		serverAndPort.add(new JLabel("Server Address:  "));
		serverAndPort.add(tfServer);
		serverAndPort.add(new JLabel("Port Number:  "));
		serverAndPort.add(tfPort);
		serverAndPort.add(new JLabel(""));
		centerPanel.add(serverAndPort);

	
		label = new JLabel("Introduceti un username", SwingConstants.CENTER);
		centerPanel.add(label);
		tf = new JTextField("Anonim");
		tf.setBackground(Color.WHITE);
		centerPanel.add(tf);
		add(centerPanel, BorderLayout.NORTH);

	
		ta = new JTextArea("Bine ati venit\n", 40, 40);
		JPanel northPanel = new JPanel(new GridLayout(1,1));
		northPanel.add(new JScrollPane(ta));
		ta.setEditable(false);
		add(northPanel, BorderLayout.CENTER);

	
		login = new JButton("Login");
		login.addActionListener(this);
		logout = new JButton("Logout");
		logout.addActionListener(this);
		logout.setEnabled(false);		
		Online = new JButton("Cine mai e online");
		Online.addActionListener(this);
		Online.setEnabled(false);		

		JPanel southPanel = new JPanel();
		southPanel.add(login);
		southPanel.add(logout);
		southPanel.add(Online);
		add(southPanel, BorderLayout.SOUTH);

		setDefaultCloseOperation(EXIT_ON_CLOSE);
		setSize(500, 500);
		setVisible(true);
		tf.requestFocus();

	}

	
	void append(String str) {
		ta.append(str);
		ta.setCaretPosition(ta.getText().length() - 1);
	}
	
	void connectionFailed() {
		login.setEnabled(true);
		logout.setEnabled(false);
		Online.setEnabled(false);
		label.setText("Introduceti un username");
		tf.setText("Anonim");
		tfPort.setText("" + defaultPort);
		tfServer.setText(defaultHost);
		tfServer.setEditable(false);
		tfPort.setEditable(false);
		tf.removeActionListener(this);
		connected = false;
	}
		
	
	public void actionPerformed(ActionEvent e) {
		Object o = e.getSource();
		
		if(o == logout) {
			client.sendMessage(new ChatMessage(ChatMessage.LOGOUT, ""));
			return;
		}
		
		if(o == Online) {
			client.sendMessage(new ChatMessage(ChatMessage.ONLINE, ""));				
			return;
		}

		
		if(connected) {
			client.sendMessage(new ChatMessage(ChatMessage.MESSAGE, tf.getText()));				
			tf.setText("");
			return;
		}
		

		if(o == login) {
			String username = tf.getText().trim();
			if(username.length() == 0)
				return;
			String server = tfServer.getText().trim();
			if(server.length() == 0)
				return;
			String portNumber = tfPort.getText().trim();
			if(portNumber.length() == 0)
				return;
			int port = 0;
			try {
				port = Integer.parseInt(portNumber);
			}
			catch(Exception en) {
				return;  
			}

			
			client = new Client(server, port, username, this);
			if(!client.start()) 
				return;
			tf.setText("");
			label.setText("Scrieti un mesaj");
			connected = true;
			
			
			login.setEnabled(false);
			logout.setEnabled(true);
			Online.setEnabled(true);
			tfServer.setEditable(false);
			tfPort.setEditable(false);
			tf.addActionListener(this);
		}

	}

	public static void main(String[] args) {
		new ClientGUI("localhost", 1000);
	}

}
