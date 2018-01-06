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
public class WindowOpenReport implements Serializable{
    private static final long serialVersionUID = 1L;

    private Vector windowOpen = null;
    public WindowOpenReport() {
        windowOpen = new Vector();
        createReport();
    }
    public void createReport(){
        try {
            String line = null;
            //String[] attribs = null;
            Process p = Runtime.getRuntime().exec("bin\\WindowOpen.exe");
            BufferedReader input = new BufferedReader(new InputStreamReader(p.getInputStream()));
            int i = 0;
            while ((line = input.readLine()) != null) {
                //attribs = line.split("\\s* \\s*");

                    windowOpen.add(line);
                    
                    System.out.println(line);
 
                i++;
            }
        } catch (Exception ex) {
            ex.printStackTrace();
        }
    }

    public Vector getWindowOpen() {
        return windowOpen;
    }
    
}
