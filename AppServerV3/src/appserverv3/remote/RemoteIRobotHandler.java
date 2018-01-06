/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */

package appserverv3.remote;

import java.io.IOException;
import java.io.ObjectOutputStream;
import java.lang.reflect.InvocationHandler;
import java.lang.reflect.Method;
import java.net.ServerSocket;
import java.net.Socket;
import java.util.logging.Level;
import java.util.logging.Logger;

/**
 *
 * @author Lockhart
 */
public class RemoteIRobotHandler implements InvocationHandler{
    
    private ServerSocket server = null;
    private Socket sock = null;
    private ObjectOutputStream oos = null;
    private boolean connect = false;
    public RemoteIRobotHandler() {
        try {
            server = new ServerSocket(2011);
            sock = server.accept();
            oos = new ObjectOutputStream(sock.getOutputStream());
            connect = true;
        } catch (Exception ex) {
            ex.printStackTrace();
        }
    }

    public Object invoke(Object proxy, Method method, Object[] args) throws Throwable {
        try{
            oos.writeObject(method.getName());
            oos.writeObject(method.getParameterTypes());
            oos.writeObject(args);
        }catch(Exception e){
            //System.out.println("tutup");
            connect= false;
            oos.close();
            sock.close();
            server.close();
        }
        throw new UnsupportedOperationException("Not supported yet.");
    }

    public boolean isConnect() {
        return connect;
    }

    public void closeConnection(){
        try {
            connect = false;
            sock.close();
            server.close();
        } catch (IOException ex) {
            Logger.getLogger(RemoteIRobotHandler.class.getName()).log(Level.SEVERE, null, ex);
        }
    }

}
