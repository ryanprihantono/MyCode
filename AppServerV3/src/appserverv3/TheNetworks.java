/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */

package appserverv3;

import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStreamReader;
import java.io.PrintWriter;
import java.net.InetAddress;
import java.net.NetworkInterface;
import java.net.SocketException;
import java.net.UnknownHostException;
import java.util.ArrayList;
import java.util.List;
import java.util.logging.Level;
import java.util.logging.Logger;

/**
 *
 * @author Lockhart
 */
public class TheNetworks implements Runnable{
    private List<String> clientHostName;
    private List<String> networksIP;
    private List<String> networksMACAddress;

    
    private String line=null;

    public TheNetworks() {
        clientHostName = new ArrayList<String>();
        networksIP = new ArrayList<String>();
        networksMACAddress = new ArrayList<String>();
    }

    public void run(){
        try {
            Process process = Runtime.getRuntime().exec("cmd");
            BufferedReader br = new BufferedReader(new InputStreamReader(process.getInputStream()));
            PrintWriter writer = new PrintWriter(process.getOutputStream());
            writer.println("net view");
            writer.println("exit");
            writer.close();

            try {
                String temp = "";
                line = br.readLine();
                if(line.contains("\\\\")){
                    temp = line.substring(2);
                    
                    clientHostName.add(temp.substring(0, temp.indexOf("  ")));
                }
                while (line!=null) {
                    if(line.contains("\\\\")){
                        temp = line.substring(2);
                        clientHostName.add(temp.substring(0, temp.indexOf("  ")));
                    }
                    line = br.readLine();
                }
                process.waitFor();
                
                process.destroy();
            } catch (InterruptedException ex) {
                Logger.getLogger(TheNetworks.class.getName()).log(Level.SEVERE, null, ex);
            }

        } catch (IOException ex) {
            Logger.getLogger(TheNetworks.class.getName()).log(Level.SEVERE, null, ex);
        }
        //for(int i=0;i<clientHostName.size();i++)
            //System.out.println(clientHostName.get(i)+" : ");
        gettingNetworksIP();
        //System.out.println("Selesai");
        
    }
    public List<String> getClientHostname(){
        return clientHostName;
    }
    public void gettingNetworksIP(){
        InetAddress address;
        for(int i=0;i<clientHostName.size();i++){
        //System.out.println(clientHostName.get(0)+":"+clientHostName.get(0).length());
            try {
                address = InetAddress.getByName(clientHostName.get(i));
                networksIP.add(address.getHostAddress());
                //gettingMacAddress(address.getHostAddress());
                //System.out.println(address.getHostAddress());
            } catch (UnknownHostException ex) {
                //Logger.getLogger(TheNetworks.class.getName()).log(Level.SEVERE, null, ex);
                //System.out.println("Address not accessible");
                networksIP.add("Address not accessible");
                //networksMACAddress.add("Address not accessible");
            }
        }
    }
    public void gettingMacAddress(String ip){
        try {
            NetworkInterface ni = NetworkInterface.getByInetAddress(InetAddress.getByName(ip));
            if(ni!=null){
                byte[] mac = ni.getHardwareAddress();

                if(mac!=null){
                    StringBuilder sb = new StringBuilder();
                    for(int j=0;j<mac.length;j++)
                        sb.append(String.format(String.format("%02X%s", mac[j], (j < mac.length - 1) ? "-" : "")));
                    networksMACAddress.add(sb.toString());
                    System.out.println(sb.toString());
                }
                else
                    networksMACAddress.add("Address not accessible");
            }
            else
                networksMACAddress.add("Address not accessible");
        } catch (Exception ex) {
            ex.printStackTrace();
        }
    }
    public List<String> getNetworksIP(){
        return networksIP;
    }
    public List<String> getNetworksMACAddress(){
        return networksMACAddress;
    }
}
