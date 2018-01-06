/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */

package appserverv3.remote;

/**
 *
 * @author Lockhart
 */
public interface IRobot {
    //public void mouseMove(MouseEvent e);
    public void mouseMove(int arg0,int arg1);


    public void mouseClick(int arg0);


    public void mousePress(int arg0);


    public void mouseRelease(int arg0);


    public void mouseWheel(int arg0);


    public void keyPress(int arg0);


    public void keyRelease(int arg0);


    public void keyTyped(int arg0);
    

}
