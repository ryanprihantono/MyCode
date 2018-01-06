/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */

package appserverv3;

import appserverv3.remote.DeskEventsHandler;
import java.awt.BorderLayout;
import java.awt.Dimension;
import java.awt.FlowLayout;
import java.awt.Toolkit;
import java.awt.event.KeyEvent;
import java.awt.event.KeyListener;
import java.awt.image.BufferedImage;
import javax.swing.ImageIcon;
import javax.swing.JFrame;
import javax.swing.JLabel;
import javax.swing.JOptionPane;
import javax.swing.JPanel;

/**
 *
 * @author Lockhart
 */
public class ScreenShotFrame extends JFrame implements Runnable{
    private ClientHandler clientHandler;
    private boolean isRemote = false;
    private JLabel label1 = null;
    private DeskEventsHandler dec = null;
    private boolean isMonitoring = true;
    private boolean isCapture = false;

    public ScreenShotFrame(ClientHandler clientHandler) {
        this.clientHandler = clientHandler;
        init();
    }

    public void init(){
        setLayout(new FlowLayout());
        //screenShotFrame.setTitle("Screenshot");
        //screenShotFrame.setSize(500, 400);

        setUndecorated(true);
        Dimension screenSize = Toolkit.getDefaultToolkit().getScreenSize();
        setBounds(0,0,screenSize.width, screenSize.height);
        
    }
    public void run(){
        JPanel jPanel = new JPanel(new BorderLayout());
        label1 = new JLabel();
        
        jPanel.add(label1,"Center");
        
        addKeyListener(new KeyListener() {

            public void keyTyped(KeyEvent e) {
            }
            public void keyPressed(KeyEvent e) {
                int id = e.getID ();
                String keyString = "";
                int keyCode = e.getKeyCode ();
                keyString = KeyEvent.getKeyText (keyCode);
                if(keyString.equals("F4")){
                    //JOptionPane.showMessageDialog(screenShotFrame, trDec.isAlive());
                    isMonitoring = false;
                    if(isRemote){
                        label1.removeMouseListener(dec);
                        label1.removeMouseWheelListener(dec);
                        label1.removeMouseMotionListener(dec);
                        removeKeyListener(dec);
                        //JOptionPane.showMessageDialog(screenShotFrame, trDec.isAlive());
                        isRemote = false;
                        dec.closeConnection();
                        clientHandler.sendCommand(ServerCommand.STOP_SENDING_EVENT);
                    }
                    dispose();
                }
                else if(keyString.equals("F2")){
                    if(!isRemote){
                        isRemote = true;
                        dec = new DeskEventsHandler();
                        Thread trDec = new Thread(dec);
                        trDec.start();
                        label1.addMouseListener(dec);
                        label1.addMouseMotionListener(dec);
                        label1.addMouseWheelListener(dec);
                        addKeyListener(dec);
                        clientHandler.sendCommand(ServerCommand.START_SENDING_EVENT);
                    }
                    else{
                        label1.removeMouseListener(dec);
                        label1.removeMouseWheelListener(dec);
                        label1.removeMouseMotionListener(dec);
                        removeKeyListener(dec);
                        //JOptionPane.showMessageDialog(screenShotFrame, trDec.isAlive());
                        isRemote = false;
                        dec.closeConnection();
                        clientHandler.sendCommand(ServerCommand.STOP_SENDING_EVENT);
                    }
                }
                else if(keyString.equals("F3")){
                    if(!isCapture)
                        isCapture = true;
                    else
                        isCapture = false;
                }
                else if(keyString.equals("F1")){
                    showHelp();
                }
            }
            public void keyReleased(KeyEvent e) {
            }
        });

        add(jPanel);
        setVisible(true);
        showHelp();
        clientHandler.sendCommand(ServerCommand.SCREENSHOT_VIEW_START);
        boolean flag=false;
        
        while(isMonitoring){
            if(!flag){
                clientHandler.sendCommand(ServerCommand.SCREENSHOT_VIEW_ONPROGRESS);
                flag=true;
            }
            if(clientHandler.isImageToShowAvailable()){
                BufferedImage bi = clientHandler.getImageToShow();
                ImageIcon icon = new ImageIcon(bi);
                label1.setIcon(icon);
                flag=false;
                if(isCapture)
                    clientHandler.getImageManager().saveImage(bi);
            }
            
            clientHandler.saveImageManager();
        }
        clientHandler.sendCommand(ServerCommand.SCREENSHOT_VIEW_STOP);
    }
    public void showHelp(){
        Thread tr = new Thread(new Runnable() {
            public void run() {
                JOptionPane.showMessageDialog(null, "Press F1 to show Help\nPress F2 to Remote\nPress F3 to save Screenshot\nPress F4 to Exit Screenshot","Screenshot Help",JOptionPane.QUESTION_MESSAGE);
            }
        });
        tr.start();
    }
}
