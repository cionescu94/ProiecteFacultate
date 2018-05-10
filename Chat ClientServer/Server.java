import java.io.*;
import java.net.*;
import java.text.SimpleDateFormat;
import java.util.*;

public class Server {
	private static int uniqueId;
	private ArrayList<ClientThread> al;
	private ServerGUI sg;
	private SimpleDateFormat sdf;
	private int port;
	private boolean keepGoing;
	

	public Server(int port) {
		this(port, null);
	}
	
	public Server(int port, ServerGUI sg) {
		
		this.sg = sg;
		this.port = port;
		sdf = new SimpleDateFormat("HH:mm:ss");
		al = new ArrayList<ClientThread>();
	}
	
	public void start() {
		keepGoing = true;
		try 
		{
			ServerSocket serverSocket = new ServerSocket(port);
			while(keepGoing) 
			{
				display("Serverul asteapta clienti la  portul  " + port + ".");
				Socket socket = serverSocket.accept();  	
				if(!keepGoing)
					break;
				ClientThread t = new ClientThread(socket);  
				al.add(t);									// save it in the ArrayList
				t.start();
			}
			try {
				serverSocket.close();
				for(int i = 0; i < al.size(); ++i) {
					ClientThread tc = al.get(i);
					try {
					tc.sInput.close();
					tc.sOutput.close();
					tc.socket.close();
					}
					catch(IOException ioE) {
					}
				}
			}
			catch(Exception e) {
				display("Exceptie la inchiderea serverului si clientului: " + e);
			}
		}
		catch (IOException e) {
            String msg = sdf.format(new Date()) + " Exceptie pe un nou ServerSocket: " + e + "\n";
			display(msg);
		}
	}		
	protected void stop() {
		keepGoing = false;
		try {
			new Socket("localhost", port);
		}
		catch(Exception e) {
		}
	}
	
	private void display(String msg) {
		String time = sdf.format(new Date()) + " " + msg;
		if(sg == null)
			System.out.println(time);
		else
			sg.appendEvent(time + "\n");
	}
	
	private synchronized void broadcast(String message) {
		String time = sdf.format(new Date());
		String messageLf = time + " " + message + "\n";
		if(sg == null)
			System.out.print(messageLf);
		else
			sg.appendRoom(messageLf);     
		
		for(int i = al.size(); --i >= 0;) {
			ClientThread ct = al.get(i);
			if(!ct.writeMsg(messageLf)) {
				al.remove(i);
				display("Client deconectat " + ct.username + " si scos din lista.");
			}
		}
	}

	synchronized void remove(int id) {
		for(int i = 0; i < al.size(); ++i) {
			ClientThread ct = al.get(i);
			if(ct.id == id) {
				al.remove(i);
				return;
			}
		}
	}
	
	
	public static void main(String[] args) {
		int portNumber = 1000;
		switch(args.length) {
			case 1:
				try {
					portNumber = Integer.parseInt(args[0]);
				}
				catch(Exception e) {
					System.out.println("Port invalid.");
					System.out.println("Usage is: > java Server [portNumber]");
					return;
				}
			case 0:
				break;
			default:
				System.out.println("Usage is: > java Server [portNumber]");
				return;
				
		}
		Server server = new Server(portNumber);
		server.start();
	}

	class ClientThread extends Thread {
		Socket socket;
		ObjectInputStream sInput;
		ObjectOutputStream sOutput;
		int id;
		String username;
		ChatMessage cm;
		String date;

		ClientThread(Socket socket) {
			id = ++uniqueId;
			this.socket = socket;
			System.out.println("Threadul incearca sa creeze Object Input/Output Streams");
			try
			{
				sOutput = new ObjectOutputStream(socket.getOutputStream());
				sInput  = new ObjectInputStream(socket.getInputStream());
				username = (String) sInput.readObject();
				display(username + " s-a conectat.");
			}
			catch (IOException e) {
				display("Exceptie la crearea de noi Input/output Streams: " + e);
				return;
			}
			catch (ClassNotFoundException e) {
			}
            date = new Date().toString() + "\n";
		}

		
		public void run() {
			
			boolean keepGoing = true;
			while(keepGoing) {
				
				try {
					cm = (ChatMessage) sInput.readObject();
				}
				catch (IOException e) {
					display(username + " Exceptie la citirea de  Streams: " + e);
					break;				
				}
				catch(ClassNotFoundException e2) {
					break;
				}
				String message = cm.getMessage();

				switch(cm.getType()) {

				case ChatMessage.MESSAGE:
					broadcast(username + ": " + message);
					break;
				case ChatMessage.LOGOUT:
					display(username + " deconectat cu Logout.");
					keepGoing = false;
					break;
				case ChatMessage.ONLINE:
					writeMsg("Lista de useri conectati la " + sdf.format(new Date()) + "\n");
					for(int i = 0; i < al.size(); ++i) {
						ClientThread ct = al.get(i);
						writeMsg((i+1) + ") " + ct.username + " since " + ct.date);
					}
					break;
				}
			}
			
			remove(id);
			close();
		}
		
		
		private void close() {
		
			try {
				if(sOutput != null) sOutput.close();
			}
			catch(Exception e) {}
			try {
				if(sInput != null) sInput.close();
			}
			catch(Exception e) {};
			try {
				if(socket != null) socket.close();
			}
			catch (Exception e) {}
		}

		
		private boolean writeMsg(String msg) {
			if(!socket.isConnected()) {
				close();
				return false;
			}
			
			try {
				sOutput.writeObject(msg);
			}
			
			catch(IOException e) {
				display("Eroare trimitere mesaj catre  " + username);
				display(e.toString());
			}
			return true;
		}
	}
}

