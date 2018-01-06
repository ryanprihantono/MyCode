/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */

package appserverv3;

import data.ClientGeneralInformation;
import data.ImageManager;
import data.NetworkTrafficReport;
import data.TaskListReport;
import data.WindowOpenReport;
import java.awt.image.BufferedImage;
import java.io.ByteArrayInputStream;
import java.io.FileInputStream;
import java.io.FileOutputStream;
import java.io.IOException;
import java.io.ObjectInputStream;
import java.io.ObjectOutputStream;
import java.net.Socket;
import java.util.logging.Level;
import java.util.logging.Logger;
import javax.imageio.ImageIO;

/**
 *
 * @author Lockhart
 */
public class ClientHandler extends Thread{

    private Socket client;
    private String clientHostname;
    private String clientIP;
    private String status;

    private ClientGeneralInformation clientGeneralInformation=null;
    private TaskListReport taskListReport = null;
    private NetworkTrafficReport networkTrafficReport = null;
    private WindowOpenReport windowOpenReport = null;

    private ObjectOutputStream out;
    private ObjectInputStream in;

    private ImageManager imageManager;
    private BufferedImage imageToShow;
    private boolean imageToShowAvailable=false;
    private boolean isCapture = false;

    private Object cmd;
    private String clientMsg = "";

    public ClientHandler(Socket client) {
        this.client = client;
        this.status = "Monitoring";
        clientHostname = client.getInetAddress().getHostName();
        clientIP = client.getInetAddress().getHostAddress();
        initImageManager();
    }
    public ClientHandler(Socket client,ClientGeneralInformation clientGeneralInformation){
        this.client = client;
        this.clientGeneralInformation = clientGeneralInformation;
        this.status = "Monitoring";
        this.clientHostname = clientGeneralInformation.getComputerName();
        this.clientIP = clientGeneralInformation.getIpAddress();
        clientGeneralInformation.setGroup("Default");
        initImageManager();
    }
    public ClientHandler(ClientGeneralInformation clientGeneralInformation){
        this.clientGeneralInformation = clientGeneralInformation;
        this.status = "Not Connected";
        this.clientHostname = clientGeneralInformation.getComputerName();
        this.clientIP = clientGeneralInformation.getIpAddress();
        clientGeneralInformation.setGroup("Default");
        initImageManager();
    }
    @Override
    public void run(){

        try {
            out = new ObjectOutputStream(client.getOutputStream());
            in = new ObjectInputStream(client.getInputStream());
            

        } catch (IOException ex) {
            Logger.getLogger(ClientHandler.class.getName()).log(Level.SEVERE, null, ex);
        }
        //System.out.println(clientHostname+":"+clientIP);
        if(clientGeneralInformation == null)
            retrieveClientGeneralInformation();
        
        saveImageManager();
        while (true) {
            //try{client.setSoTimeout(1);}catch(Exception e){}
            try {
                cmd = in.readObject();
                identifyData(cmd);
                //System.out.println(client.isInputShutdown());

            } catch (Exception ex) {
                //Logger.getLogger(ClientHandler.class.getName()).log(Level.SEVERE, null, ex);
                try {
                    client.close();
                    setStatusDisconnect();
                } catch (IOException ex1) {
                    Logger.getLogger(ClientHandler.class.getName()).log(Level.SEVERE, null, ex1);
                }
                //System.out.println("test");

                break;
            }
            //System.out.println(client.isInputShutdown());
        }
    }

    public void identifyData(Object cmd){
        if((Integer)cmd == ServerCommand.GENERAL_INFORMATION){
            Object obj = receiveData();
            if(obj instanceof ClientGeneralInformation){
                //System.out.println("test");
                clientGeneralInformation = (ClientGeneralInformation) obj;
                clientGeneralInformation.setGroup("Default");
                //System.out.println(clientGeneralInformation.getComputerName()+" : "+clientGeneralInformation.getMacAddress());
            }
        }
        else if((Integer)cmd == ServerCommand.GET_WINDOWOPEN_REPORT){
            Object obj = receiveData();
            if(obj instanceof WindowOpenReport){
                windowOpenReport = (WindowOpenReport) obj;
                //System.out.println("asdf");
            }
        }
        else if((Integer)cmd == ServerCommand.GET_TASKLIST_REPORT){
            Object obj = receiveData();
            if(obj instanceof TaskListReport){
                taskListReport = (TaskListReport) obj;
            }
        }
        else if((Integer)cmd == ServerCommand.GET_NETWORK_REPORT){
            Object obj = receiveData();
            if(obj instanceof NetworkTrafficReport){
                networkTrafficReport = (NetworkTrafficReport) obj;
            }
        }
        else if((Integer)cmd == ServerCommand.SCREENSHOT_VIEW_START){
            isCapture = true;
        }
        else if((Integer)cmd == ServerCommand.SCREENSHOT_VIEW_STOP){
            isCapture = false;
            imageToShowAvailable = false;
            //saveImageManager();
        }
        else if((Integer)cmd == ServerCommand.SCREENSHOT_VIEW_ONPROGRESS){
            //System.out.println("asdf");
            Object obj = receiveData();
            if(obj instanceof BufferedImage){
                imageToShow = (BufferedImage)obj;
                //imageManager.saveImage(imageToShow);
                imageToShowAvailable = true;
            }
        }
        else if((Integer)cmd == ServerCommand.SEND_MESSAGE){
            try {
                clientMsg = (String) in.readObject();
            } catch (Exception ex) {
                ex.printStackTrace();
            }
        }
    }
    public Object receiveData(){
        Object objReceived = null;
        byte[] objBuffer = null;
        byte[] buffer=null;
        int offset=0;
        int bytesRead=0;
        int objectSize=0;
        Object obj;
        try {
            //1 size
            obj = in.readObject();
            if(obj instanceof Integer){
                objectSize = (Integer)obj;
                objBuffer = new byte[objectSize];
            }
            do{
                
                obj = in.readObject();
                if(obj instanceof Integer){
                    bytesRead = (Integer)obj;
                }
                obj = in.readObject();
                if(obj instanceof byte[]){
                    buffer = (byte[]) obj;
                    System.arraycopy(buffer, 0, objBuffer, offset, bytesRead);
                    offset+=bytesRead;
                }
            }while(offset<objectSize);
        } catch (Exception ex) {
            ex.printStackTrace();
        }
        ByteArrayInputStream bais = new ByteArrayInputStream(objBuffer);
        ObjectInputStream ois=null;

        if(!isCapture){
            try {
                ois = new ObjectInputStream(bais);
                objReceived = ois.readObject();
                ois.close();
                bais.close();
            } catch (Exception e) {
                e.printStackTrace();
            }
        }
        else{
        BufferedImage buffImg;
            try {
                buffImg = ImageIO.read(bais);
                objReceived = buffImg;
                bais.close();
            } catch (IOException ex) {

                //Logger.getLogger(ClientHandler.class.getName()).log(Level.SEVERE, null, ex);
            }
        }
        return objReceived;
    }
    public void sendCommand(int command){
        try {
            //System.out.println(command);
            out.writeObject(command);
            
        } catch (Exception ex) {
            ex.printStackTrace();
        }
    }
    public void sendMessage(String msg){
        try {
            out.writeObject(ServerCommand.SEND_MESSAGE);
            out.writeObject(msg);
        } catch (IOException ex) {
            Logger.getLogger(ClientHandler.class.getName()).log(Level.SEVERE, null, ex);
        } 
    }
    public String getMessage(){
        return clientMsg;
    }
    public void clearMessage(){
        clientMsg = "";
    }
    public void setClientMacAddress(){

    }
    public String getClientHostName(){
        return clientHostname;
    }
    public ClientGeneralInformation getClientGeneralInformation(){
        return clientGeneralInformation;
    }
    public void setClientGeneralInformation(ClientGeneralInformation clientGeneralInformation){
        this.clientGeneralInformation = clientGeneralInformation;
    }
    public void retrieveClientGeneralInformation(){
        sendCommand(ServerCommand.GENERAL_INFORMATION);
    }
    public String getStatus(){
        return status;
    }
    public void setStatusConnect(){
        this.status = "Monitoring";
    }
    public void setStatusDisconnect(){
        this.status = "Not Connected";
    }
    public boolean isImageToShowAvailable(){
        return imageToShowAvailable;
    }
    public BufferedImage getImageToShow(){
        imageToShowAvailable = false;
        return imageToShow;
    }
    public void saveImageManager(){
        try{
            FileOutputStream fos = new FileOutputStream("imageManagers\\"+clientHostname+".dat");
            ObjectOutputStream oos = new ObjectOutputStream(fos);
            oos.writeObject(imageManager);
            oos.close();
        }catch(Exception e){
            e.printStackTrace();
        }
    }
    public ImageManager loadImageManager(){
        ImageManager tempImageManager=null;
        try{
            FileInputStream fis = new FileInputStream("imageManagers\\"+clientHostname+".dat");
            ObjectInputStream ois = new ObjectInputStream(fis);
            tempImageManager = (ImageManager)ois.readObject();
            ois.close();
        }catch(Exception e){
            //e.printStackTrace();
        }
        return tempImageManager;
    }
    public void initImageManager(){
        imageManager = loadImageManager();
        if(imageManager==null){
            imageManager = new ImageManager(clientHostname);
        }
    }
    public ImageManager getImageManager(){
        return imageManager;
    }

    public TaskListReport getTaskListReport() {
        return taskListReport;
    }
    public WindowOpenReport getWindowOpenReport() {
        return windowOpenReport;
    }


    public NetworkTrafficReport getNetworkTrafficReport() {
        return networkTrafficReport;
    }

}
