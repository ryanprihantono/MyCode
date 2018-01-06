/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */

package appserverv3;

import data.ClientGeneralInformation;
import java.io.FileInputStream;
import java.io.FileOutputStream;
import java.io.IOException;
import java.io.ObjectInputStream;
import java.io.ObjectOutputStream;
import java.net.ServerSocket;
import java.net.Socket;
import java.util.Vector;
import java.util.logging.Level;
import java.util.logging.Logger;

/**
 *
 * @author Lockhart
 */
public class Server implements Runnable{

    private ServerSocket serverSocket;
    private Vector clientHandlers;
    Vector clientGeneralInformations;
    Vector imageManagers;
    //private Vector clientThreads;
    //private int clientCounter=0;

    public Server() {
        try {

            serverSocket = new ServerSocket(2009);
            clientHandlers = new Vector();
            clientGeneralInformations = loadClientGeneralInformations();
            if(clientGeneralInformations == null)
                clientGeneralInformations = new Vector();
            
            setClientHandlers();
            //ClientHandler ch = (ClientHandler) clientHandlers.get(0);
            //System.out.println(ch.getClientGeneralInformation().getComputerName());
            if(clientGeneralInformations.size()!=0){
                ClientGeneralInformation cgi = (ClientGeneralInformation) clientGeneralInformations.get(0);
            //System.out.println(cgi.getComputerName());
            }
            //System.out.println("CGI size = "+clientGeneralInformations.size());
            //System.out.println("clientHandlers size = "+clientHandlers.size());
            //clientThreads = new Vector();
        } catch (IOException ex) {
            Logger.getLogger(Server.class.getName()).log(Level.SEVERE, null, ex);
        }
    }

    public void run() {
        while(true){
            //System.out.println(clientCounter);
            try {
                Socket clientSocket = serverSocket.accept();

                ClientHandler tempClient = new ClientHandler(clientSocket);
                tempClient.start();
                Thread.sleep(1000);
                //System.out.println(tempClient.getClientGeneralInformation().getComputerName());
                int idxCGI = clientDataSync(tempClient);
                if(idxCGI != -1){
                    tempClient.setStatusConnect();
                    clientHandlers.set(idxCGI, tempClient);
                    clientGeneralInformations.set(idxCGI, tempClient.getClientGeneralInformation());

                }
                else{
                    clientHandlers.add(tempClient);
                    clientGeneralInformations.add(tempClient.getClientGeneralInformation());
                    System.out.println(tempClient.getClientGeneralInformation().getComputerName());
                }
                
                saveClientGeneralInformations();
                    

                //Thread tr = new Thread((ClientHandler)clientHandlers.get(clientCounter));
                //clientThreads.add(tr);
                
                //clientCounter++;
            } catch (Exception ex) {
                ex.printStackTrace();
            }
        }
    }
    public void removeExpiredClient(int index){
        clientHandlers.removeElementAt(index);
        //clientThreads.removeElementAt(index);
    }
    public Vector getClientHandlers(){
        return clientHandlers;
    }
    public void saveClientGeneralInformations(){
        try{
            FileOutputStream fos = new FileOutputStream("clients.dat");
            ObjectOutputStream oos = new ObjectOutputStream(fos);
            oos.writeObject(clientGeneralInformations);
            oos.close();
        }catch(Exception e){
            e.printStackTrace();
        }
    }
    public Vector loadClientGeneralInformations(){
        Vector tempClients=null;
        try{
            FileInputStream fis = new FileInputStream("clients.dat");
            ObjectInputStream ois = new ObjectInputStream(fis);
            tempClients = (Vector)ois.readObject();
            ois.close();
        }catch(Exception e){
            //e.printStackTrace();
        }
        return tempClients;
    }
    
    public void setClientHandlers(){
        for(int i=0;i<clientGeneralInformations.size();i++){
            clientHandlers.add(new ClientHandler((ClientGeneralInformation)clientGeneralInformations.get(i)));
        }
    }
    public int clientDataSync(ClientHandler tempClient){
        
        for(int i=0;i<clientGeneralInformations.size();i++){
            ClientGeneralInformation temp = (ClientGeneralInformation) clientGeneralInformations.get(i);
            if(tempClient.getClientGeneralInformation().getComputerName().equals(temp.getComputerName())){
                return i;
            }
        }
        return -1;
    }
    /*public Vector getClientThreads(){
        return clientThreads;
    }*/
}
