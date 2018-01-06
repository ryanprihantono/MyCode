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
 * @author Tetchanz
 */
public class TaskListReport implements Serializable{
    private static final long serialVersionUID = 1L;

    private Vector windowTitle = null;
    private Vector memoryUsage = null;
    private Vector timeStamp = null;
    public TaskListReport() {
        windowTitle = new Vector();
        timeStamp = new Vector();
        memoryUsage = new Vector();
        createReport();
    }

    public void createReport() {
        try {
            String line = null;
            String[] attribs = null;
            Process p = Runtime.getRuntime().exec("tasklist /fo csv /v /nh");
            BufferedReader input = new BufferedReader(new InputStreamReader(p.getInputStream()));
            while((line = input.readLine()) != null){
                attribs = line.split("\",\"");
                if(!attribs[8].equals("N/A\"")){
                    attribs[8]=attribs[8].substring(0,attribs[8].length()-1);
                    windowTitle.add(attribs[8]);
                    timeStamp.add(attribs[7]);
                    memoryUsage.add(attribs[4]);
                }
            }
            input.close();
        } catch (Exception ex) {
            ex.printStackTrace();
        }
    }

    public Vector getMemoryUsage() {
        return memoryUsage;
    }

    public Vector getTimeStamp() {
        return timeStamp;
    }

    public Vector getWindowTitle() {
        return windowTitle;
    }
    
}
