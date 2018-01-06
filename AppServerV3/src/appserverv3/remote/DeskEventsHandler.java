/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */

package appserverv3.remote;

import java.awt.event.KeyEvent;
import java.awt.event.KeyListener;
import java.awt.event.MouseEvent;
import java.awt.event.MouseListener;
import java.awt.event.MouseMotionListener;
import java.awt.event.MouseWheelEvent;
import java.awt.event.MouseWheelListener;
import java.lang.reflect.Proxy;

/**
 *
 * @author Lockhart
 */
public class DeskEventsHandler implements Runnable,MouseListener,MouseMotionListener,MouseWheelListener,KeyListener{
    private IRobot iRobot = null;
    private RemoteIRobotHandler remoteIRobotHandler = null;
    public void run() {
        remoteIRobotHandler = new RemoteIRobotHandler();
        iRobot = (IRobot) Proxy.newProxyInstance(this.getClass().getClassLoader(), new Class[]{IRobot.class}, remoteIRobotHandler);
    }

    public IRobot getIRobot() {
        return iRobot;
    }

    public void closeConnection(){
        if(remoteIRobotHandler != null)
            remoteIRobotHandler.closeConnection();
    }

    public void mouseClicked(MouseEvent e) {
        iRobot.mouseClick(e.getButton());
    }

    public void mousePressed(MouseEvent e) {
        iRobot.mousePress(e.getButton());
    }

    public void mouseReleased(MouseEvent e) {
        iRobot.mouseRelease(e.getButton());
    }

    public void mouseEntered(MouseEvent e) {

    }

    public void mouseExited(MouseEvent e) {

    }

    public void mouseDragged(MouseEvent e) {
        mouseMoved(e);
    }

    /*public void mouseMoved(MouseEvent e) {
        iRobot.mouseMove(e);
    }*/
    public void mouseMoved(MouseEvent e) {
        iRobot.mouseMove(e.getX(),e.getY());
    }

    public void mouseWheelMoved(MouseWheelEvent e) {
        iRobot.mouseWheel(e.getWheelRotation());
    }

    public void keyTyped(KeyEvent e) {
        
    }

    public void keyPressed(KeyEvent e) {
        //System.out.println(e.getKeyCode());
        iRobot.keyPress(e.getKeyCode());
    }

    public void keyReleased(KeyEvent e) {
        iRobot.keyRelease(e.getKeyCode());
    }

}
