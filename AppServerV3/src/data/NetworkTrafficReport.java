/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */

package data;

import java.io.BufferedReader;
import java.io.InputStreamReader;
import java.io.Serializable;
import java.util.Vector;

/**
 *
 * @author Lockhart
 */
public class NetworkTrafficReport implements Serializable{
    private static final long serialVersionUID = 1L;

    private Vector protocol = null;
    private Vector localAddress = null;
    private Vector foreignAddress = null;
    private Vector state = null;
    public NetworkTrafficReport() {
        protocol = new Vector();
        localAddress = new Vector();
        foreignAddress = new Vector();
        state = new Vector();
        createReport();
        
    }

    public void createReport() {
		try {
			String line = null;
			String[] attribs = null;
			Process p = Runtime.getRuntime().exec("netstat -f");
			BufferedReader input = new BufferedReader(new InputStreamReader(p.getInputStream()));
			int i=0;
			while((line = input.readLine()) != null){
				attribs = line.split("\\s* \\s*");
				if(i>3){
					protocol.add(attribs[1]);
					localAddress.add(attribs[2]);
					foreignAddress.add(attribs[3]);
					state.add(attribs[4]);
					//System.out.println(attribs[0]+"#"+attribs[1]+"#"+attribs[2]+"#"+attribs[3]);
				}
				i++;
			}
			input.close();
		} catch (Exception ex) {
			ex.printStackTrace();
		}
	}

    public Vector getForeignAddress() {
        return foreignAddress;
    }

    public Vector getLocalAddress() {
        return localAddress;
    }

    public Vector getProtocol() {
        return protocol;
    }

    public Vector getState() {
        return state;
    }
    
}
