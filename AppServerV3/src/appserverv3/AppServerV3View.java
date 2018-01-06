/*
 * AppServerV3View.java
 */

package appserverv3;

import javax.swing.event.ChangeEvent;
import javax.swing.event.TreeSelectionEvent;
import org.jdesktop.application.Action;
import org.jdesktop.application.ResourceMap;
import org.jdesktop.application.SingleFrameApplication;
import org.jdesktop.application.FrameView;
import org.jdesktop.application.TaskMonitor;
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;
import java.awt.event.MouseAdapter;
import java.awt.event.MouseEvent;
import java.awt.event.WindowAdapter;
import java.awt.event.WindowEvent;
import java.awt.image.BufferedImage;
import java.util.List;
import java.util.Vector;
import javax.swing.BorderFactory;
import javax.swing.Timer;
import javax.swing.Icon;
import javax.swing.ImageIcon;
import javax.swing.JButton;
import javax.swing.JDialog;
import javax.swing.JFrame;
import javax.swing.JMenuItem;
import javax.swing.JPopupMenu;
import javax.swing.JScrollPane;
import javax.swing.JSeparator;
import javax.swing.JTabbedPane;
import javax.swing.JToggleButton;
import javax.swing.JTree;
import javax.swing.SwingConstants;
import javax.swing.event.ChangeListener;
import javax.swing.event.TreeSelectionListener;
import javax.swing.table.DefaultTableModel;
import javax.swing.tree.DefaultMutableTreeNode;

/**
 * The application's main frame.
 */
public class AppServerV3View extends FrameView implements Runnable{

    public AppServerV3View(SingleFrameApplication app) {
        super(app);

        initComponents();
        jToolBar1.setFloatable(false);
        server = new Server();
        Thread tr = new Thread(server);
        tr.start();
        Thread tr1 = new Thread(this);
        tr1.start();
        

        jTabbedPane1.addChangeListener(new ChangeListener() {
            // This method is called whenever the selected tab changes
            public void stateChanged(ChangeEvent evt) {
                JTabbedPane jTabbedPane = (JTabbedPane)evt.getSource();

                // Get current tab
                int sel = jTabbedPane.getSelectedIndex();
                
                if(sel==1){
                    initThumbnails();
                }
                else if(sel==2){
                    initHistoryDataPanel();
                    initTreeView();
                }
            }
        });
        

        // status bar initialization - message timeout, idle icon and busy animation, etc
        ResourceMap resourceMap = getResourceMap();
        int messageTimeout = resourceMap.getInteger("StatusBar.messageTimeout");
        messageTimer = new Timer(messageTimeout, new ActionListener() {
            public void actionPerformed(ActionEvent e) {
                statusMessageLabel.setText("");
            }
        });
        messageTimer.setRepeats(false);
        int busyAnimationRate = resourceMap.getInteger("StatusBar.busyAnimationRate");
        for (int i = 0; i < busyIcons.length; i++) {
            busyIcons[i] = resourceMap.getIcon("StatusBar.busyIcons[" + i + "]");
        }
        busyIconTimer = new Timer(busyAnimationRate, new ActionListener() {
            public void actionPerformed(ActionEvent e) {
                busyIconIndex = (busyIconIndex + 1) % busyIcons.length;
                statusAnimationLabel.setIcon(busyIcons[busyIconIndex]);
            }
        });
        idleIcon = resourceMap.getIcon("StatusBar.idleIcon");
        statusAnimationLabel.setIcon(idleIcon);
        

        // connecting action tasks to status bar via TaskMonitor
        TaskMonitor taskMonitor = new TaskMonitor(getApplication().getContext());
        taskMonitor.addPropertyChangeListener(new java.beans.PropertyChangeListener() {
            public void propertyChange(java.beans.PropertyChangeEvent evt) {
                String propertyName = evt.getPropertyName();
                if ("started".equals(propertyName)) {
                    if (!busyIconTimer.isRunning()) {
                        statusAnimationLabel.setIcon(busyIcons[0]);
                        busyIconIndex = 0;
                        busyIconTimer.start();
                    }
                    
                } else if ("done".equals(propertyName)) {
                    busyIconTimer.stop();
                    statusAnimationLabel.setIcon(idleIcon);
                    
                } else if ("message".equals(propertyName)) {
                    String text = (String)(evt.getNewValue());
                    statusMessageLabel.setText((text == null) ? "" : text);
                    messageTimer.restart();
                } else if ("progress".equals(propertyName)) {
                    int value = (Integer)(evt.getNewValue());
                    
                }
            }
        });
    }

    @Action
    public void showAboutBox() {
        if (aboutBox == null) {
            JFrame mainFrame = AppServerV3App.getApplication().getMainFrame();
            aboutBox = new AppServerV3AboutBox(mainFrame);
            aboutBox.setLocationRelativeTo(mainFrame);
        }
        AppServerV3App.getApplication().show(aboutBox);
    }

    /** This method is called from within the constructor to
     * initialize the form.
     * WARNING: Do NOT modify this code. The content of this method is
     * always regenerated by the Form Editor.
     */
    @SuppressWarnings("unchecked")
    // <editor-fold defaultstate="collapsed" desc="Generated Code">//GEN-BEGIN:initComponents
    private void initComponents() {

        mainPanel = new javax.swing.JPanel();
        statusMessageLabel = new javax.swing.JLabel();
        statusAnimationLabel = new javax.swing.JLabel();
        jToolBar1 = new javax.swing.JToolBar();
        jButton1 = new javax.swing.JButton();
        jButton2 = new javax.swing.JButton();
        jButton3 = new javax.swing.JButton();
        jSeparator1 = new javax.swing.JToolBar.Separator();
        jButton4 = new javax.swing.JButton();
        jButton5 = new javax.swing.JButton();
        jSeparator2 = new javax.swing.JToolBar.Separator();
        jButton6 = new javax.swing.JButton();
        jButton7 = new javax.swing.JButton();
        jButton8 = new javax.swing.JButton();
        jButton9 = new javax.swing.JButton();
        jButton10 = new javax.swing.JButton();
        jButton11 = new javax.swing.JButton();
        jButton12 = new javax.swing.JButton();
        jButton13 = new javax.swing.JButton();
        jSeparator3 = new javax.swing.JToolBar.Separator();
        jButton14 = new javax.swing.JButton();
        jSplitPane1 = new javax.swing.JSplitPane();
        jTabbedPane1 = new javax.swing.JTabbedPane();
        jScrollPane1 = new javax.swing.JScrollPane();
        jTable1 = new javax.swing.JTable();
        jScrollPane3 = new javax.swing.JScrollPane();
        jPanel1 = new javax.swing.JPanel();
        jSplitPane2 = new javax.swing.JSplitPane();
        historyDataPanel = new javax.swing.JPanel();
        jToolBar2 = new javax.swing.JToolBar();
        jToggleButton2 = new javax.swing.JToggleButton();
        jSeparator4 = new javax.swing.JToolBar.Separator();
        jToggleButton3 = new javax.swing.JToggleButton();
        jSeparator5 = new javax.swing.JToolBar.Separator();
        jToggleButton4 = new javax.swing.JToggleButton();
        jSeparator6 = new javax.swing.JToolBar.Separator();
        jToggleButton5 = new javax.swing.JToggleButton();
        jSeparator8 = new javax.swing.JToolBar.Separator();
        jToggleButton6 = new javax.swing.JToggleButton();
        jSeparator7 = new javax.swing.JToolBar.Separator();
        jToggleButton7 = new javax.swing.JToggleButton();
        jSeparator9 = new javax.swing.JToolBar.Separator();
        jToggleButton8 = new javax.swing.JToggleButton();
        jSeparator10 = new javax.swing.JToolBar.Separator();
        jToggleButton9 = new javax.swing.JToggleButton();
        jSeparator11 = new javax.swing.JToolBar.Separator();
        jToggleButton10 = new javax.swing.JToggleButton();
        jSeparator12 = new javax.swing.JToolBar.Separator();
        jToggleButton11 = new javax.swing.JToggleButton();
        jSeparator13 = new javax.swing.JToolBar.Separator();
        jToggleButton12 = new javax.swing.JToggleButton();
        jToolBar3 = new javax.swing.JToolBar();
        jScrollPane4 = new javax.swing.JScrollPane();
        jTable3 = new javax.swing.JTable();
        jScrollPane5 = new javax.swing.JScrollPane();
        jTable2 = new javax.swing.JTable();
        jToggleButton1 = new javax.swing.JToggleButton();
        jScrollPane2 = new javax.swing.JScrollPane();
        jPopupMenu1 = new javax.swing.JPopupMenu();

        mainPanel.setName("mainPanel"); // NOI18N

        statusMessageLabel.setName("statusMessageLabel"); // NOI18N

        statusAnimationLabel.setHorizontalAlignment(javax.swing.SwingConstants.LEFT);
        statusAnimationLabel.setName("statusAnimationLabel"); // NOI18N

        jToolBar1.setRollover(true);
        jToolBar1.setName("jToolBar1"); // NOI18N

        org.jdesktop.application.ResourceMap resourceMap = org.jdesktop.application.Application.getInstance(appserverv3.AppServerV3App.class).getContext().getResourceMap(AppServerV3View.class);
        jButton1.setIcon(resourceMap.getIcon("jButton1.icon")); // NOI18N
        jButton1.setText(resourceMap.getString("jButton1.text")); // NOI18N
        jButton1.setToolTipText(resourceMap.getString("jButton1.toolTipText")); // NOI18N
        jButton1.setFocusable(false);
        jButton1.setHorizontalTextPosition(javax.swing.SwingConstants.CENTER);
        jButton1.setName("jButton1"); // NOI18N
        jButton1.setVerticalTextPosition(javax.swing.SwingConstants.BOTTOM);
        jToolBar1.add(jButton1);

        jButton2.setIcon(resourceMap.getIcon("jButton2.icon")); // NOI18N
        jButton2.setText(resourceMap.getString("jButton2.text")); // NOI18N
        jButton2.setToolTipText(resourceMap.getString("jButton2.toolTipText")); // NOI18N
        jButton2.setFocusable(false);
        jButton2.setHorizontalTextPosition(javax.swing.SwingConstants.CENTER);
        jButton2.setName("jButton2"); // NOI18N
        jButton2.setVerticalTextPosition(javax.swing.SwingConstants.BOTTOM);
        jToolBar1.add(jButton2);

        jButton3.setIcon(resourceMap.getIcon("jButton3.icon")); // NOI18N
        jButton3.setToolTipText(resourceMap.getString("jButton3.toolTipText")); // NOI18N
        jButton3.setFocusable(false);
        jButton3.setHorizontalTextPosition(javax.swing.SwingConstants.CENTER);
        jButton3.setName("jButton3"); // NOI18N
        jButton3.setVerticalTextPosition(javax.swing.SwingConstants.BOTTOM);
        jToolBar1.add(jButton3);

        jSeparator1.setName("jSeparator1"); // NOI18N
        jToolBar1.add(jSeparator1);

        jButton4.setIcon(resourceMap.getIcon("jButton4.icon")); // NOI18N
        jButton4.setToolTipText(resourceMap.getString("jButton4.toolTipText")); // NOI18N
        jButton4.setFocusable(false);
        jButton4.setHorizontalTextPosition(javax.swing.SwingConstants.CENTER);
        jButton4.setName("jButton4"); // NOI18N
        jButton4.setVerticalTextPosition(javax.swing.SwingConstants.BOTTOM);
        jToolBar1.add(jButton4);

        jButton5.setIcon(resourceMap.getIcon("jButton5.icon")); // NOI18N
        jButton5.setToolTipText(resourceMap.getString("jButton5.toolTipText")); // NOI18N
        jButton5.setFocusable(false);
        jButton5.setHorizontalTextPosition(javax.swing.SwingConstants.CENTER);
        jButton5.setName("jButton5"); // NOI18N
        jButton5.setVerticalTextPosition(javax.swing.SwingConstants.BOTTOM);
        jToolBar1.add(jButton5);

        jSeparator2.setName("jSeparator2"); // NOI18N
        jToolBar1.add(jSeparator2);

        jButton6.setIcon(resourceMap.getIcon("jButton6.icon")); // NOI18N
        jButton6.setToolTipText(resourceMap.getString("jButton6.toolTipText")); // NOI18N
        jButton6.setFocusable(false);
        jButton6.setHorizontalTextPosition(javax.swing.SwingConstants.CENTER);
        jButton6.setName("jButton6"); // NOI18N
        jButton6.setVerticalTextPosition(javax.swing.SwingConstants.BOTTOM);
        jToolBar1.add(jButton6);

        jButton7.setIcon(resourceMap.getIcon("jButton7.icon")); // NOI18N
        jButton7.setToolTipText(resourceMap.getString("jButton7.toolTipText")); // NOI18N
        jButton7.setFocusable(false);
        jButton7.setHorizontalTextPosition(javax.swing.SwingConstants.CENTER);
        jButton7.setName("jButton7"); // NOI18N
        jButton7.setVerticalTextPosition(javax.swing.SwingConstants.BOTTOM);
        jToolBar1.add(jButton7);

        jButton8.setIcon(resourceMap.getIcon("jButton8.icon")); // NOI18N
        jButton8.setToolTipText(resourceMap.getString("jButton8.toolTipText")); // NOI18N
        jButton8.setFocusable(false);
        jButton8.setHorizontalTextPosition(javax.swing.SwingConstants.CENTER);
        jButton8.setName("jButton8"); // NOI18N
        jButton8.setVerticalTextPosition(javax.swing.SwingConstants.BOTTOM);
        jToolBar1.add(jButton8);

        jButton9.setIcon(resourceMap.getIcon("jButton9.icon")); // NOI18N
        jButton9.setToolTipText(resourceMap.getString("jButton9.toolTipText")); // NOI18N
        jButton9.setFocusable(false);
        jButton9.setHorizontalTextPosition(javax.swing.SwingConstants.CENTER);
        jButton9.setName("jButton9"); // NOI18N
        jButton9.setVerticalTextPosition(javax.swing.SwingConstants.BOTTOM);
        jToolBar1.add(jButton9);

        jButton10.setIcon(resourceMap.getIcon("jButton10.icon")); // NOI18N
        jButton10.setToolTipText(resourceMap.getString("jButton10.toolTipText")); // NOI18N
        jButton10.setFocusable(false);
        jButton10.setHorizontalTextPosition(javax.swing.SwingConstants.CENTER);
        jButton10.setName("jButton10"); // NOI18N
        jButton10.setVerticalTextPosition(javax.swing.SwingConstants.BOTTOM);
        jToolBar1.add(jButton10);

        jButton11.setIcon(resourceMap.getIcon("jButton11.icon")); // NOI18N
        jButton11.setToolTipText(resourceMap.getString("jButton11.toolTipText")); // NOI18N
        jButton11.setFocusable(false);
        jButton11.setHorizontalTextPosition(javax.swing.SwingConstants.CENTER);
        jButton11.setName("jButton11"); // NOI18N
        jButton11.setVerticalTextPosition(javax.swing.SwingConstants.BOTTOM);
        jToolBar1.add(jButton11);

        jButton12.setIcon(resourceMap.getIcon("jButton12.icon")); // NOI18N
        jButton12.setToolTipText(resourceMap.getString("jButton12.toolTipText")); // NOI18N
        jButton12.setFocusable(false);
        jButton12.setHorizontalTextPosition(javax.swing.SwingConstants.CENTER);
        jButton12.setName("jButton12"); // NOI18N
        jButton12.setVerticalTextPosition(javax.swing.SwingConstants.BOTTOM);
        jToolBar1.add(jButton12);

        jButton13.setIcon(resourceMap.getIcon("jButton13.icon")); // NOI18N
        jButton13.setToolTipText(resourceMap.getString("jButton13.toolTipText")); // NOI18N
        jButton13.setFocusable(false);
        jButton13.setHorizontalTextPosition(javax.swing.SwingConstants.CENTER);
        jButton13.setName("jButton13"); // NOI18N
        jButton13.setVerticalTextPosition(javax.swing.SwingConstants.BOTTOM);
        jToolBar1.add(jButton13);

        jSeparator3.setName("jSeparator3"); // NOI18N
        jToolBar1.add(jSeparator3);

        jButton14.setIcon(resourceMap.getIcon("jButton14.icon")); // NOI18N
        jButton14.setToolTipText(resourceMap.getString("jButton14.toolTipText")); // NOI18N
        jButton14.setFocusable(false);
        jButton14.setHorizontalTextPosition(javax.swing.SwingConstants.CENTER);
        jButton14.setName("jButton14"); // NOI18N
        jButton14.setVerticalTextPosition(javax.swing.SwingConstants.BOTTOM);
        jToolBar1.add(jButton14);

        jSplitPane1.setOrientation(javax.swing.JSplitPane.VERTICAL_SPLIT);
        jSplitPane1.setName("jSplitPane1"); // NOI18N

        jTabbedPane1.setName("jTabbedPane1"); // NOI18N

        jScrollPane1.setName("jScrollPane1"); // NOI18N

        jTable1.setModel(new javax.swing.table.DefaultTableModel(
            new Object [][] {
                {"", "", null, null, null, null, null, null, null, null, null},
                {null, null, null, null, null, null, null, null, null, null, null},
                {null, null, null, null, null, null, null, null, null, null, null},
                {null, null, null, null, null, null, null, null, null, null, null},
                {null, null, null, null, null, null, null, null, null, null, null},
                {null, null, null, null, null, null, null, null, null, null, null},
                {null, null, null, null, null, null, null, null, null, null, null},
                {null, null, null, null, null, null, null, null, null, null, null},
                {null, null, null, null, null, null, null, null, null, null, null},
                {null, null, null, null, null, null, null, null, null, null, null},
                {null, null, null, null, null, null, null, null, null, null, null},
                {null, null, null, null, null, null, null, null, null, null, null},
                {null, null, null, null, null, null, null, null, null, null, null},
                {null, null, null, null, null, null, null, null, null, null, null},
                {null, null, null, null, null, null, null, null, null, null, null},
                {null, null, null, null, null, null, null, null, null, null, null},
                {null, null, null, null, null, null, null, null, null, null, null},
                {null, null, null, null, null, null, null, null, null, null, null},
                {null, null, null, null, null, null, null, null, null, null, null},
                {null, null, null, null, null, null, null, null, null, null, null}
            },
            new String [] {
                "Computer Name", "IP Address", "MAC Address", "Remark", "Windows Account", "Group", "Link time", "Status", "RXD Speed", "TXD Speed", "Events"
            }
        ));
        jTable1.setName("jTable1"); // NOI18N
        jScrollPane1.setViewportView(jTable1);
        jTable1.getColumnModel().getColumn(0).setHeaderValue(resourceMap.getString("jTable1.columnModel.title0")); // NOI18N
        jTable1.getColumnModel().getColumn(1).setHeaderValue(resourceMap.getString("jTable1.columnModel.title1")); // NOI18N
        jTable1.getColumnModel().getColumn(2).setHeaderValue(resourceMap.getString("jTable1.columnModel.title2")); // NOI18N
        jTable1.getColumnModel().getColumn(3).setHeaderValue(resourceMap.getString("jTable1.columnModel.title3")); // NOI18N
        jTable1.getColumnModel().getColumn(4).setHeaderValue(resourceMap.getString("jTable1.columnModel.title4")); // NOI18N
        jTable1.getColumnModel().getColumn(5).setHeaderValue(resourceMap.getString("jTable1.columnModel.title5")); // NOI18N
        jTable1.getColumnModel().getColumn(6).setHeaderValue(resourceMap.getString("jTable1.columnModel.title6")); // NOI18N
        jTable1.getColumnModel().getColumn(7).setHeaderValue(resourceMap.getString("jTable1.columnModel.title7")); // NOI18N
        jTable1.getColumnModel().getColumn(8).setHeaderValue(resourceMap.getString("jTable1.columnModel.title8")); // NOI18N
        jTable1.getColumnModel().getColumn(9).setHeaderValue(resourceMap.getString("jTable1.columnModel.title9")); // NOI18N
        jTable1.getColumnModel().getColumn(10).setHeaderValue(resourceMap.getString("jTable1.columnModel.title10")); // NOI18N

        jTabbedPane1.addTab(resourceMap.getString("jScrollPane1.TabConstraints.tabTitle"), jScrollPane1); // NOI18N

        jScrollPane3.setName("jScrollPane3"); // NOI18N

        jPanel1.setName("jPanel1"); // NOI18N
        jPanel1.setLayout(new java.awt.FlowLayout(java.awt.FlowLayout.LEFT, 20, 20));
        jScrollPane3.setViewportView(jPanel1);

        jTabbedPane1.addTab(resourceMap.getString("jScrollPane3.TabConstraints.tabTitle"), jScrollPane3); // NOI18N

        jSplitPane2.setName("jSplitPane2"); // NOI18N

        historyDataPanel.setName("historyDataPanel"); // NOI18N

        jToolBar2.setRollover(true);
        jToolBar2.setName("jToolBar2"); // NOI18N

        jToggleButton2.setText(resourceMap.getString("jToggleButton2.text")); // NOI18N
        jToggleButton2.setFocusable(false);
        jToggleButton2.setHorizontalTextPosition(javax.swing.SwingConstants.CENTER);
        jToggleButton2.setName("jToggleButton2"); // NOI18N
        jToggleButton2.setVerticalTextPosition(javax.swing.SwingConstants.BOTTOM);
        jToolBar2.add(jToggleButton2);

        jSeparator4.setName("jSeparator4"); // NOI18N
        jToolBar2.add(jSeparator4);

        jToggleButton3.setText(resourceMap.getString("jToggleButton3.text")); // NOI18N
        jToggleButton3.setFocusable(false);
        jToggleButton3.setHorizontalTextPosition(javax.swing.SwingConstants.CENTER);
        jToggleButton3.setName("jToggleButton3"); // NOI18N
        jToggleButton3.setVerticalTextPosition(javax.swing.SwingConstants.BOTTOM);
        jToolBar2.add(jToggleButton3);

        jSeparator5.setName("jSeparator5"); // NOI18N
        jToolBar2.add(jSeparator5);

        jToggleButton4.setText(resourceMap.getString("jToggleButton4.text")); // NOI18N
        jToggleButton4.setFocusable(false);
        jToggleButton4.setHorizontalTextPosition(javax.swing.SwingConstants.CENTER);
        jToggleButton4.setName("jToggleButton4"); // NOI18N
        jToggleButton4.setVerticalTextPosition(javax.swing.SwingConstants.BOTTOM);
        jToolBar2.add(jToggleButton4);

        jSeparator6.setName("jSeparator6"); // NOI18N
        jToolBar2.add(jSeparator6);

        jToggleButton5.setText(resourceMap.getString("jToggleButton5.text")); // NOI18N
        jToggleButton5.setFocusable(false);
        jToggleButton5.setHorizontalTextPosition(javax.swing.SwingConstants.CENTER);
        jToggleButton5.setName("jToggleButton5"); // NOI18N
        jToggleButton5.setVerticalTextPosition(javax.swing.SwingConstants.BOTTOM);
        jToolBar2.add(jToggleButton5);

        jSeparator8.setName("jSeparator8"); // NOI18N
        jToolBar2.add(jSeparator8);

        jToggleButton6.setText(resourceMap.getString("jToggleButton6.text")); // NOI18N
        jToggleButton6.setFocusable(false);
        jToggleButton6.setHorizontalTextPosition(javax.swing.SwingConstants.CENTER);
        jToggleButton6.setName("jToggleButton6"); // NOI18N
        jToggleButton6.setVerticalTextPosition(javax.swing.SwingConstants.BOTTOM);
        jToolBar2.add(jToggleButton6);

        jSeparator7.setName("jSeparator7"); // NOI18N
        jToolBar2.add(jSeparator7);

        jToggleButton7.setText(resourceMap.getString("jToggleButton7.text")); // NOI18N
        jToggleButton7.setFocusable(false);
        jToggleButton7.setHorizontalTextPosition(javax.swing.SwingConstants.CENTER);
        jToggleButton7.setName("jToggleButton7"); // NOI18N
        jToggleButton7.setVerticalTextPosition(javax.swing.SwingConstants.BOTTOM);
        jToolBar2.add(jToggleButton7);

        jSeparator9.setName("jSeparator9"); // NOI18N
        jToolBar2.add(jSeparator9);

        jToggleButton8.setText(resourceMap.getString("jToggleButton8.text")); // NOI18N
        jToggleButton8.setFocusable(false);
        jToggleButton8.setHorizontalTextPosition(javax.swing.SwingConstants.CENTER);
        jToggleButton8.setName("jToggleButton8"); // NOI18N
        jToggleButton8.setVerticalTextPosition(javax.swing.SwingConstants.BOTTOM);
        jToolBar2.add(jToggleButton8);

        jSeparator10.setName("jSeparator10"); // NOI18N
        jToolBar2.add(jSeparator10);

        jToggleButton9.setText(resourceMap.getString("jToggleButton9.text")); // NOI18N
        jToggleButton9.setFocusable(false);
        jToggleButton9.setHorizontalTextPosition(javax.swing.SwingConstants.CENTER);
        jToggleButton9.setName("jToggleButton9"); // NOI18N
        jToggleButton9.setVerticalTextPosition(javax.swing.SwingConstants.BOTTOM);
        jToolBar2.add(jToggleButton9);

        jSeparator11.setName("jSeparator11"); // NOI18N
        jToolBar2.add(jSeparator11);

        jToggleButton10.setText(resourceMap.getString("jToggleButton10.text")); // NOI18N
        jToggleButton10.setFocusable(false);
        jToggleButton10.setHorizontalTextPosition(javax.swing.SwingConstants.CENTER);
        jToggleButton10.setName("jToggleButton10"); // NOI18N
        jToggleButton10.setVerticalTextPosition(javax.swing.SwingConstants.BOTTOM);
        jToolBar2.add(jToggleButton10);

        jSeparator12.setName("jSeparator12"); // NOI18N
        jToolBar2.add(jSeparator12);

        jToggleButton11.setText(resourceMap.getString("jToggleButton11.text")); // NOI18N
        jToggleButton11.setFocusable(false);
        jToggleButton11.setHorizontalTextPosition(javax.swing.SwingConstants.CENTER);
        jToggleButton11.setName("jToggleButton11"); // NOI18N
        jToggleButton11.setVerticalTextPosition(javax.swing.SwingConstants.BOTTOM);
        jToolBar2.add(jToggleButton11);

        jSeparator13.setName("jSeparator13"); // NOI18N
        jToolBar2.add(jSeparator13);

        jToggleButton12.setText(resourceMap.getString("jToggleButton12.text")); // NOI18N
        jToggleButton12.setFocusable(false);
        jToggleButton12.setHorizontalTextPosition(javax.swing.SwingConstants.CENTER);
        jToggleButton12.setName("jToggleButton12"); // NOI18N
        jToggleButton12.setVerticalTextPosition(javax.swing.SwingConstants.BOTTOM);
        jToolBar2.add(jToggleButton12);

        jToolBar3.setRollover(true);
        jToolBar3.setName("jToolBar3"); // NOI18N

        jScrollPane4.setName("jScrollPane4"); // NOI18N

        jTable3.setModel(new javax.swing.table.DefaultTableModel(
            new Object [][] {
                {null, null, null, null, null},
                {null, null, null, null, null},
                {null, null, null, null, null},
                {null, null, null, null, null},
                {null, null, null, null, null},
                {null, null, null, null, null},
                {null, null, null, null, null},
                {null, null, null, null, null},
                {null, null, null, null, null},
                {null, null, null, null, null},
                {null, null, null, null, null},
                {null, null, null, null, null},
                {null, null, null, null, null},
                {null, null, null, null, null},
                {null, null, null, null, null},
                {null, null, null, null, null},
                {null, null, null, null, null},
                {null, null, null, null, null},
                {null, null, null, null, null},
                {null, null, null, null, null},
                {null, null, null, null, null}
            },
            new String [] {
                "Computer Remark", "IP Address", "Windows Account", "Time Stamp", "Events"
            }
        ));
        jTable3.setName("jTable3"); // NOI18N
        jScrollPane4.setViewportView(jTable3);
        jTable3.getColumnModel().getColumn(0).setHeaderValue(resourceMap.getString("jTable3.columnModel.title0")); // NOI18N
        jTable3.getColumnModel().getColumn(1).setHeaderValue(resourceMap.getString("jTable3.columnModel.title1")); // NOI18N
        jTable3.getColumnModel().getColumn(2).setHeaderValue(resourceMap.getString("jTable3.columnModel.title2")); // NOI18N
        jTable3.getColumnModel().getColumn(3).setHeaderValue(resourceMap.getString("jTable3.columnModel.title3")); // NOI18N
        jTable3.getColumnModel().getColumn(4).setHeaderValue(resourceMap.getString("jTable3.columnModel.title4")); // NOI18N

        javax.swing.GroupLayout historyDataPanelLayout = new javax.swing.GroupLayout(historyDataPanel);
        historyDataPanel.setLayout(historyDataPanelLayout);
        historyDataPanelLayout.setHorizontalGroup(
            historyDataPanelLayout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
            .addComponent(jToolBar2, javax.swing.GroupLayout.DEFAULT_SIZE, 872, Short.MAX_VALUE)
            .addComponent(jToolBar3, javax.swing.GroupLayout.DEFAULT_SIZE, 872, Short.MAX_VALUE)
            .addComponent(jScrollPane4, javax.swing.GroupLayout.DEFAULT_SIZE, 872, Short.MAX_VALUE)
        );
        historyDataPanelLayout.setVerticalGroup(
            historyDataPanelLayout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
            .addGroup(historyDataPanelLayout.createSequentialGroup()
                .addComponent(jToolBar2, javax.swing.GroupLayout.PREFERRED_SIZE, 25, javax.swing.GroupLayout.PREFERRED_SIZE)
                .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED)
                .addComponent(jToolBar3, javax.swing.GroupLayout.PREFERRED_SIZE, 25, javax.swing.GroupLayout.PREFERRED_SIZE)
                .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED)
                .addComponent(jScrollPane4, javax.swing.GroupLayout.DEFAULT_SIZE, 375, Short.MAX_VALUE))
        );

        jSplitPane2.setRightComponent(historyDataPanel);

        jTabbedPane1.addTab(resourceMap.getString("jSplitPane2.TabConstraints.tabTitle"), jSplitPane2); // NOI18N

        jSplitPane1.setTopComponent(jTabbedPane1);

        jScrollPane5.setName("jScrollPane5"); // NOI18N
        jScrollPane5.setPreferredSize(new java.awt.Dimension(452, 200));

        jTable2.setModel(new javax.swing.table.DefaultTableModel(
            new Object [][] {
                {null, null, null, null},
                {null, null, null, null},
                {null, null, null, null},
                {null, null, null, null},
                {null, null, null, null},
                {null, null, null, null},
                {null, null, null, null},
                {null, null, null, null}
            },
            new String [] {
                "Hostname", "IP Address", "Remark", ""
            }
        ));
        jTable2.setName("jTable2"); // NOI18N
        jScrollPane5.setViewportView(jTable2);
        jTable2.getColumnModel().getColumn(0).setHeaderValue(resourceMap.getString("jTable2.columnModel.title0")); // NOI18N
        jTable2.getColumnModel().getColumn(1).setHeaderValue(resourceMap.getString("jTable2.columnModel.title1")); // NOI18N
        jTable2.getColumnModel().getColumn(2).setHeaderValue(resourceMap.getString("jTable2.columnModel.title2")); // NOI18N
        jTable2.getColumnModel().getColumn(3).setHeaderValue(resourceMap.getString("jTable2.columnModel.title3")); // NOI18N

        jSplitPane1.setRightComponent(jScrollPane5);

        javax.swing.GroupLayout mainPanelLayout = new javax.swing.GroupLayout(mainPanel);
        mainPanel.setLayout(mainPanelLayout);
        mainPanelLayout.setHorizontalGroup(
            mainPanelLayout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
            .addGroup(mainPanelLayout.createSequentialGroup()
                .addComponent(jToolBar1, javax.swing.GroupLayout.PREFERRED_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.PREFERRED_SIZE)
                .addContainerGap(504, Short.MAX_VALUE))
            .addComponent(jSplitPane1, javax.swing.GroupLayout.DEFAULT_SIZE, 969, Short.MAX_VALUE)
            .addGroup(mainPanelLayout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
                .addGroup(mainPanelLayout.createSequentialGroup()
                    .addGap(10, 10, 10)
                    .addComponent(statusMessageLabel)
                    .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED, 959, Short.MAX_VALUE)
                    .addComponent(statusAnimationLabel)
                    .addGap(0, 0, 0)))
        );
        mainPanelLayout.setVerticalGroup(
            mainPanelLayout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
            .addGroup(mainPanelLayout.createSequentialGroup()
                .addComponent(jToolBar1, javax.swing.GroupLayout.PREFERRED_SIZE, 25, javax.swing.GroupLayout.PREFERRED_SIZE)
                .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED)
                .addComponent(jSplitPane1, javax.swing.GroupLayout.DEFAULT_SIZE, 715, Short.MAX_VALUE))
            .addGroup(mainPanelLayout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
                .addGroup(javax.swing.GroupLayout.Alignment.TRAILING, mainPanelLayout.createSequentialGroup()
                    .addContainerGap(644, Short.MAX_VALUE)
                    .addGroup(mainPanelLayout.createParallelGroup(javax.swing.GroupLayout.Alignment.BASELINE)
                        .addComponent(statusMessageLabel)
                        .addComponent(statusAnimationLabel))
                    .addGap(102, 102, 102)))
        );

        jToggleButton1.setText(resourceMap.getString("jToggleButton1.text")); // NOI18N
        jToggleButton1.setName("jToggleButton1"); // NOI18N

        jScrollPane2.setName("jScrollPane2"); // NOI18N

        jPopupMenu1.setName("jPopupMenu1"); // NOI18N

        setComponent(mainPanel);
    }// </editor-fold>//GEN-END:initComponents

    // Variables declaration - do not modify//GEN-BEGIN:variables
    private javax.swing.JPanel historyDataPanel;
    private javax.swing.JButton jButton1;
    private javax.swing.JButton jButton10;
    private javax.swing.JButton jButton11;
    private javax.swing.JButton jButton12;
    private javax.swing.JButton jButton13;
    private javax.swing.JButton jButton14;
    private javax.swing.JButton jButton2;
    private javax.swing.JButton jButton3;
    private javax.swing.JButton jButton4;
    private javax.swing.JButton jButton5;
    private javax.swing.JButton jButton6;
    private javax.swing.JButton jButton7;
    private javax.swing.JButton jButton8;
    private javax.swing.JButton jButton9;
    private javax.swing.JPanel jPanel1;
    private javax.swing.JPopupMenu jPopupMenu1;
    private javax.swing.JScrollPane jScrollPane1;
    private javax.swing.JScrollPane jScrollPane2;
    private javax.swing.JScrollPane jScrollPane3;
    private javax.swing.JScrollPane jScrollPane4;
    private javax.swing.JScrollPane jScrollPane5;
    private javax.swing.JToolBar.Separator jSeparator1;
    private javax.swing.JToolBar.Separator jSeparator10;
    private javax.swing.JToolBar.Separator jSeparator11;
    private javax.swing.JToolBar.Separator jSeparator12;
    private javax.swing.JToolBar.Separator jSeparator13;
    private javax.swing.JToolBar.Separator jSeparator2;
    private javax.swing.JToolBar.Separator jSeparator3;
    private javax.swing.JToolBar.Separator jSeparator4;
    private javax.swing.JToolBar.Separator jSeparator5;
    private javax.swing.JToolBar.Separator jSeparator6;
    private javax.swing.JToolBar.Separator jSeparator7;
    private javax.swing.JToolBar.Separator jSeparator8;
    private javax.swing.JToolBar.Separator jSeparator9;
    private javax.swing.JSplitPane jSplitPane1;
    private javax.swing.JSplitPane jSplitPane2;
    private javax.swing.JTabbedPane jTabbedPane1;
    private javax.swing.JTable jTable1;
    private javax.swing.JTable jTable2;
    private javax.swing.JTable jTable3;
    private javax.swing.JToggleButton jToggleButton1;
    private javax.swing.JToggleButton jToggleButton10;
    private javax.swing.JToggleButton jToggleButton11;
    private javax.swing.JToggleButton jToggleButton12;
    private javax.swing.JToggleButton jToggleButton2;
    private javax.swing.JToggleButton jToggleButton3;
    private javax.swing.JToggleButton jToggleButton4;
    private javax.swing.JToggleButton jToggleButton5;
    private javax.swing.JToggleButton jToggleButton6;
    private javax.swing.JToggleButton jToggleButton7;
    private javax.swing.JToggleButton jToggleButton8;
    private javax.swing.JToggleButton jToggleButton9;
    private javax.swing.JToolBar jToolBar1;
    private javax.swing.JToolBar jToolBar2;
    private javax.swing.JToolBar jToolBar3;
    private javax.swing.JPanel mainPanel;
    private javax.swing.JLabel statusAnimationLabel;
    private javax.swing.JLabel statusMessageLabel;
    // End of variables declaration//GEN-END:variables

    private final Timer messageTimer;
    private final Timer busyIconTimer;
    private final Icon idleIcon;
    private final Icon[] busyIcons = new Icon[15];
    private int busyIconIndex = 0;

    private Vector thumbnailButtons;

    private int currentClient;

    private Server server;

    private int threadFlag=0;

    private boolean isMonitoring = false;

    private JDialog aboutBox;


    public void setNetworkList(){
        TheNetworks tn = new TheNetworks();
        Thread tr = new Thread(tn);
        tr.start();

        List<String> clientHostName = tn.getClientHostname();
        List<String> networksIP = tn.getNetworksIP();
        //List<String> macAddress = tn.getNetworksMACAddress();

        DefaultTableModel tm;
        Object[] headers = {"Hostname","IP Address","Remarks",""};

        boolean isAlive = tr.isAlive();
        while(isAlive){
            //System.out.println(tr.isAlive()+"");
            if(!tr.isAlive()){
                Object[][] obj = new Object[clientHostName.size()][4];
                
                for(int i=0;i<clientHostName.size();i++){
                    //System.out.println(clientHostName.get(i)+" - "+networksIP.get(i)+"\n");
                    obj[i][0] = clientHostName.get(i);
                    obj[i][1] = networksIP.get(i);
                    obj[i][2] = null;
                    obj[i][3] = null;
                }
                isAlive = false;
                tm = new DefaultTableModel(obj, headers);
                jTable2.setModel(tm);
            }
        }
    }

    public void run() {
        if(threadFlag==0){
            threadFlag=1;
            Thread tr2 = new Thread(this);
            tr2.start();
            while(true){
                try {
                    refreshClientList();
                    Thread.sleep(1000*10);
                    
                } catch (Exception ex) {
                    ex.printStackTrace();
                }
            }
        }
        else if(threadFlag==1){
            //System.out.println("zxcv");
            setNetworkList();
        }
    }
   
    public void refreshClientList(){
        Vector clientHandlers = server.getClientHandlers();
        if(clientHandlers.size()!=0){
            DefaultTableModel tm;
            Object[] headers = {"Computer Name","IP Address","MAC Address","Remark","Windows Account","Group","Link Time","Status","RXD Speed","TXD Speed","Events"};

            int limit=0;
            
            if(clientHandlers.size()<20)
                limit = 20-clientHandlers.size();

            Object[][] obj = new Object[clientHandlers.size()+limit][11];
            for(int i=0;i<clientHandlers.size()+limit;i++){
                if(i<clientHandlers.size()){
                    ClientHandler clientHandler = (ClientHandler)clientHandlers.get(i);
                    //System.out.println(clientHandler.getClientGeneralInformation().getComputerName()+" - "+clientHandler.getClientGeneralInformation().getMacAddress());
                    obj[i][0] = clientHandler.getClientGeneralInformation().getComputerName();
                    obj[i][1] = clientHandler.getClientGeneralInformation().getIpAddress();
                    obj[i][2] = clientHandler.getClientGeneralInformation().getMacAddress();
                    obj[i][3] = null;
                    obj[i][4] = clientHandler.getClientGeneralInformation().getWindowsAccount();
                    obj[i][5] = clientHandler.getClientGeneralInformation().getGroup();
                    obj[i][6] = null;
                    obj[i][7] = clientHandler.getStatus();
                    obj[i][8] = null;
                    obj[i][9] = null;
                    obj[i][10] = null;
                }
                else{
                    obj[i][0] = null;
                    obj[i][1] = null;
                    obj[i][2] = null;
                    obj[i][3] = null;
                    obj[i][4] = null;
                    obj[i][5] = null;
                    obj[i][6] = null;
                    obj[i][7] = null;
                    obj[i][8] = null;
                    obj[i][9] = null;
                    obj[i][10] = null;
                }
            }
            tm = new DefaultTableModel(obj, headers);
            jTable1.setModel(tm);
        }
    }
    public void initThumbnails(){
        jPanel1.removeAll();
        thumbnailButtons = new Vector();
        Vector clientHandlers = server.getClientHandlers();
        for(int i=0;i<clientHandlers.size();i++){
            ClientHandler clientHandler = (ClientHandler) clientHandlers.get(i);
            setThumbnail(clientHandler.getImageManager().getThumbnail(), clientHandler.getClientGeneralInformation().getComputerName(),clientHandler.getStatus());
        }
    }
    public void setThumbnail(BufferedImage bufferedImage,String hostname,String status){
        //JLabel label1 = new JLabel();
        JButton butt = new JButton();
        if(status.equals("Monitoring")){
            butt.setText("View "+hostname);
            butt.setEnabled(true);
        }
        else{
            butt.setText(hostname+" "+status);
            butt.setEnabled(false);
        }

        ImageIcon icon = new ImageIcon(bufferedImage);
        butt.setIcon(icon);

        butt.setHorizontalTextPosition(SwingConstants.CENTER);
        butt.setVerticalTextPosition(SwingConstants.BOTTOM);
        /*label1.setIcon(icon);
        label1.setText(hostname);
        label1.setHorizontalTextPosition(SwingConstants.CENTER);
        label1.setVerticalTextPosition(SwingConstants.BOTTOM);*/
        
        thumbnailButtons.add(butt);
        //System.out.println(isMonitoring);
        butt.addActionListener(new ActionListener() {
            public void actionPerformed(ActionEvent e) {
                isMonitoring = true;
                for(int i=0;i<thumbnailButtons.size();i++){
                    if(e.getSource().equals(thumbnailButtons.get(i))){
                        currentClient = i;
                        //System.out.println(currentClient);
                    }
                }
                //screenshotThread();
                Vector clientHandlers = server.getClientHandlers();
                ClientHandler temp = (ClientHandler) clientHandlers.get(currentClient);
                ScreenShotFrame screenShotFrame = new ScreenShotFrame(temp);
                screenShotFrame.setVisible(true);
                Thread tr = new Thread(screenShotFrame);
                tr.start();
            }
        });
        
        //JLabel label2 = new JLabel();
        //label2.setLayout(new BorderLayout());
        //label2.add(label1);
        //label2.add(butt);

        //jPanel1.add(label1);
        jPanel1.add(butt);

    }
    
    private ClientHandler tempClient=null;
    private Vector jToggleButtons;
    private JToggleButton tTemp=null;
    public void initHistoryDataPanel(){
        
        jToggleButtons = new Vector();
        //jToggleButtons.add(jToggleButton2);//blockReport
        jToggleButton2.setVisible(false);
        jSeparator4.setVisible(false);
        jToggleButtons.add(jToggleButton3);//applicationsReport
        //jToggleButtons.add(jToggleButton4);//urlVisited
        jToggleButton4.setVisible(false);
        jSeparator6.setVisible(false);
        jToggleButtons.add(jToggleButton5);//fileReport
        jToggleButtons.add(jToggleButton6);//lanFlowReport
        jToggleButtons.add(jToggleButton7);//screenShot
        jToggleButtons.add(jToggleButton8);//hardwareChangedReport
        //jToggleButtons.add(jToggleButton9);//emailReport
        jToggleButton9.setVisible(false);
        jSeparator11.setVisible(false);
        //jToggleButtons.add(jToggleButton10);//chatReport
        jToggleButton10.setVisible(false);
        jSeparator12.setVisible(false);
        //jToggleButtons.add(jToggleButton11);//successBackup
        jToggleButton11.setVisible(false);
        jSeparator13.setVisible(false);
        jToggleButtons.add(jToggleButton12);//systemReport
        historyDataPanel.setBorder(BorderFactory.createTitledBorder("Default Group Data View"));
        jToolBar2.setFloatable(false);
        jToolBar3.setFloatable(false);
        for (int i = 0; i < jToggleButtons.size(); i++) {
            tTemp = (JToggleButton)jToggleButtons.get(i);
            tTemp.addActionListener(new ActionListener() {
                public void actionPerformed(ActionEvent e) {
                    setSubToolbar( jToggleButtons.indexOf(e.getSource()));
                    toggleSelected(jToggleButtons.indexOf(e.getSource()));
                }
            });

        }
    }
    private String[] blockReport = {"Program Blocked","URL Blocked","Bandwith out of range","Web Pages Filtered","Online Movie Blocked","Failure IP-MAC Address Binding"};
    private String[] applicationsReport = {"Windows Open","Programs Run","Analyze Application Usage"};
    private String[] urlVisited = {"URL Visited","Analyze Websites Usage"};
    private String[] fileReport = {"Files/folders founded or coppied","Files/folder deleted","Files/folders modified","Plug/remove Flash memory disk","Share folder operated"};
    private String[] lanFlowReport = {"Network card flow"};
    private String[] screenShot = {"Screenshot","Screenshot(animation)"};
    private String[] hardwareChangedReport = {"Hardware changed report"};
    private String[] emailReport = {"Outlook Express or Outlook"};
    private String[] chatReport = {"AIM conversation","ICQ conversation","Skype conversation","MSN conversation","Yahoo Messenger conversation","Google Talk"};
    private String[] successBackup = {"Success Backup","No need Backup","Failure Backup"};
    private String[] systemReport = {"Monitor Start","Computer Captured","Capture Interrupted"};
    private Vector togglesTitle;
    private Vector subToggles;
    private int currIdx;
    public void setSubToolbar(int idx){
        togglesTitle = new Vector();
        //togglesTitle.add(blockReport);
        togglesTitle.add(applicationsReport);
        //togglesTitle.add(urlVisited);
        togglesTitle.add(fileReport);
        togglesTitle.add(lanFlowReport);
        togglesTitle.add(screenShot);
        togglesTitle.add(hardwareChangedReport);
        //togglesTitle.add(emailReport);
        //togglesTitle.add(chatReport);
        //togglesTitle.add(successBackup);
        togglesTitle.add(systemReport);
        jToolBar3.removeAll();
        String []temp = (String[])togglesTitle.get(idx);
        currIdx = idx;
        subToggles = new Vector();
        for(int i=0;i<temp.length;i++){
            
            JToggleButton tempToggle = new JToggleButton(temp[i]);
            subToggles.add(tempToggle);
            tempToggle.addActionListener(new ActionListener() {

                public void actionPerformed(ActionEvent e) {
                    subToggleSelected(subToggles.indexOf(e.getSource()));
                    if(tempClient != null){
                        initTableContent(currIdx, subToggles.indexOf(e.getSource()));
                    }
                }
            });
            jToolBar3.add(tempToggle);
            JSeparator js = new JSeparator(SwingConstants.VERTICAL);
            jToolBar3.add(js);
        }
    }
    public void initTableContent(int toggleIdx,int subToggleIdx){
        //System.out.println(toggleIdx+":"+subToggleIdx);
        switch(toggleIdx){
            case 0:
                switch(subToggleIdx){
                    case 0:
                        //window open
                        tempClient.sendCommand(ServerCommand.GET_WINDOWOPEN_REPORT);
                        initWindowOpenTable();
                        break;
                    case 1:
                        //program run
                        tempClient.sendCommand(ServerCommand.GET_TASKLIST_REPORT);
                        //System.out.println("test");
                        initTasklistTable();
                        break;
                    case 2:
                        //analize application usage
                        break;
                }
                break;
            case 1:
                switch(subToggleIdx){
                    case 0:
                        //files/folders founded or coppied
                        break;
                    case 1:
                        //files/folders deleted
                        break;
                    case 2:
                        //files/folders modified
                        break;
                    case 3:
                        //plug/remove flash memory disk
                        break;
                    case 4:
                        //share folder opperated
                        break;
                }
                break;
            case 2:
                //network card flow
                tempClient.sendCommand(ServerCommand.GET_NETWORK_REPORT);
                //System.out.println("test");
                initNetworkTrafficTable();
                break;
            case 3:
                switch(subToggleIdx){
                    case 0:
                        //screenshot
                        break;
                    case 1:
                        //screenshot animation
                        break;
                }
                break;
            case 4:
                //hardware changed report
                break;
            case 5:
                switch(subToggleIdx){
                    case 0:
                        //monitor start
                        break;
                    case 1:
                        //computer captured
                        break;
                    case 2:
                        //monitor interrupted
                        break;
                }
                break;
        }
    }
    public void initTasklistTable(){
        //System.out.println("test");
        while(tempClient.getTaskListReport() == null){}
        DefaultTableModel tm;
        Object[] headers = {"Computer Name","IP Address","Windows Account","CPU Time","Memory Usage","Events"};

        Vector windowTitles = tempClient.getTaskListReport().getWindowTitle();
        Vector cpuTimes = tempClient.getTaskListReport().getTimeStamp();
        Vector memoryUsages = tempClient.getTaskListReport().getMemoryUsage();
        int limit=0;

        if(windowTitles.size()<20)
            limit = 20-windowTitles.size();

        Object[][] obj = new Object[windowTitles.size()+limit][6];
        for(int i=0;i<windowTitles.size()+limit;i++){
            if(i<windowTitles.size()){
                String windowTitle = (String)windowTitles.get(i);
                String cpuTime = (String)cpuTimes.get(i);
                String memoryUsage = (String)memoryUsages.get(i);
                //System.out.println(clientHandler.getClientGeneralInformation().getComputerName()+" - "+clientHandler.getClientGeneralInformation().getMacAddress());
                obj[i][0] = tempClient.getClientGeneralInformation().getComputerName();
                obj[i][1] = tempClient.getClientGeneralInformation().getIpAddress();
                obj[i][2] = tempClient.getClientGeneralInformation().getWindowsAccount();
                obj[i][3] = cpuTime;
                obj[i][4] = memoryUsage;
                obj[i][5] = windowTitle;


            }
            else{
                obj[i][0] = null;
                obj[i][1] = null;
                obj[i][2] = null;
                obj[i][3] = null;
                obj[i][4] = null;
                obj[i][5] = null;
            }
        }
        tm = new DefaultTableModel(obj, headers);
        jTable3.setModel(tm);
    }
    public void initWindowOpenTable(){
        //System.out.println("test");
        while(tempClient.getWindowOpenReport() == null){}
        DefaultTableModel tm;
        Object[] headers = {"Computer Name","IP Address","Windows Account","Events"};

        Vector windowOpens = tempClient.getWindowOpenReport().getWindowOpen();

        int limit=0;

        if(windowOpens.size()<20)
            limit = 20-windowOpens.size();

        Object[][] obj = new Object[windowOpens.size()+limit][4];
        for(int i=0;i<windowOpens.size()+limit;i++){
            if(i<windowOpens.size()){
                String windowOpen = (String)windowOpens.get(i);
                //System.out.println(clientHandler.getClientGeneralInformation().getComputerName()+" - "+clientHandler.getClientGeneralInformation().getMacAddress());
                obj[i][0] = tempClient.getClientGeneralInformation().getComputerName();
                obj[i][1] = tempClient.getClientGeneralInformation().getIpAddress();
                obj[i][2] = tempClient.getClientGeneralInformation().getWindowsAccount();
                obj[i][3] = windowOpen;
            }
            else{
                obj[i][0] = null;
                obj[i][1] = null;
                obj[i][2] = null;
                obj[i][3] = null;
            }
        }
        tm = new DefaultTableModel(obj, headers);
        jTable3.setModel(tm);
    }
    public void initNetworkTrafficTable(){
        //System.out.println("test");
        while(tempClient.getNetworkTrafficReport() == null){}
        DefaultTableModel tm;
        Object[] headers = {"Computer Name","IP Address","Windows Account","Protocol","Local Address","Foreign Address","State"};

        Vector protocols = tempClient.getNetworkTrafficReport().getProtocol();
        Vector localAddresses = tempClient.getNetworkTrafficReport().getLocalAddress();
        Vector foreignAddresses = tempClient.getNetworkTrafficReport().getForeignAddress();
        Vector states = tempClient.getNetworkTrafficReport().getState();
        int limit=0;

        if(protocols.size()<20)
            limit = 20-protocols.size();

        Object[][] obj = new Object[protocols.size()+limit][7];
        for(int i=0;i<protocols.size()+limit;i++){
            if(i<protocols.size()){
                String protocol = (String)protocols.get(i);
                String localAddress = (String)localAddresses.get(i);
                String foreignAddress = (String)foreignAddresses.get(i);
                String state = (String)states.get(i);
                //System.out.println(protocol+"#"+localAddress+"#"+foreignAddress+"#"+state);
                obj[i][0] = tempClient.getClientGeneralInformation().getComputerName();
                obj[i][1] = tempClient.getClientGeneralInformation().getIpAddress();
                obj[i][2] = tempClient.getClientGeneralInformation().getWindowsAccount();
                obj[i][3] = protocol;
                obj[i][4] = localAddress;
                obj[i][5] = foreignAddress;
                obj[i][6] = state;


            }
            else{
                obj[i][0] = null;
                obj[i][1] = null;
                obj[i][2] = null;
                obj[i][3] = null;
                obj[i][4] = null;
                obj[i][5] = null;
                obj[i][6] = null;
            }
        }
        tm = new DefaultTableModel(obj, headers);
        jTable3.setModel(tm);
    }
    public void subToggleSelected(int idx){
        for(int i=0;i<subToggles.size();i++){
            JToggleButton temp=(JToggleButton)subToggles.get(i);
            if(i==idx){
                temp.setSelected(true);
            }
            else{
                temp.setSelected(false);
            }
        }
    }
    public void toggleSelected(int idx){
        for(int i=0;i<jToggleButtons.size();i++){
            JToggleButton temp=(JToggleButton)jToggleButtons.get(i);
            if(i==idx){
                temp.setSelected(true);
            }
            else{
                temp.setSelected(false);
            }
        }
    }
    boolean popUpFlag = false;
    public void initTreeView(){
        Vector clientHandlers = server.getClientHandlers();
        DefaultMutableTreeNode root = new DefaultMutableTreeNode("Groups");
        DefaultMutableTreeNode defaultGroup = new DefaultMutableTreeNode("Default");
        Vector groupsString = new Vector();
        Vector children = new Vector();
        children.add(defaultGroup);
        groupsString.add("Default");
        boolean nodeFlag = true;
        for (int i = 0; i < clientHandlers.size(); i++) {
            ClientHandler clientHandler = (ClientHandler) clientHandlers.get(i);
            for (int j = 0; j < children.size(); j++) {
                nodeFlag = true;
                String tempString = (String) groupsString.get(j);
                DefaultMutableTreeNode tempChild = (DefaultMutableTreeNode) children.get(j);
                if(clientHandler.getClientGeneralInformation().getGroup().equals(tempString)){
                    DefaultMutableTreeNode tempGrandChild = new DefaultMutableTreeNode(clientHandler.getClientGeneralInformation().getComputerName());
                    tempChild.add(tempGrandChild);
                    nodeFlag = false;
                    break;
                }
            }
            if(nodeFlag){
                DefaultMutableTreeNode tempGrandChild = new DefaultMutableTreeNode(clientHandler.getClientGeneralInformation().getComputerName());
                DefaultMutableTreeNode tempChild = new DefaultMutableTreeNode(clientHandler.getClientGeneralInformation().getGroup());
                groupsString.add(clientHandler.getClientGeneralInformation().getGroup());
                tempChild.add(tempGrandChild);
                children.add(tempChild);
            }
        }
        for (int i = 0; i < children.size(); i++) {
            DefaultMutableTreeNode tempChild = (DefaultMutableTreeNode) children.get(i);
            //System.out.println(tempGroup.getChildAt(0));
            root.add(tempChild);
        }
        //jScrollPane6.removeAll();

        final JTree jTree = new JTree(root);
        jTree.addTreeSelectionListener(new TreeSelectionListener() {
            public void valueChanged(TreeSelectionEvent e) {
                //System.out.println(e.getPath().getPathCount());
                if(e.getPath().getPathCount()==3){
                    tempClient = getCurrentClient(e.getPath().getLastPathComponent().toString());
                    //System.out.println(e.getPath().getLastPathComponent());
                    popUpFlag = true;
                }
                else
                    popUpFlag = false;

                //JOptionPane.showMessageDialog(mainPanel, e.getSource());
            }
        });
        jTree.addMouseListener(new MouseAdapter() {
            
            @Override
            public void mousePressed(MouseEvent e ){
                 checkForTriggerEvent( e );
             }
            @Override
             public void mouseReleased( MouseEvent e ){
                 checkForTriggerEvent( e );
             }
             private void checkForTriggerEvent( MouseEvent e ){
                 if ( e.isPopupTrigger() ){
                     if(popUpFlag)
                        setPopupMenu(e);
                 }
             }
        });
        jSplitPane2.setLeftComponent(new JScrollPane(jTree));
        //jTree.setName("jTree"); // NOI18N
        //jScrollPane6.setViewportView(jTree);
        //jScrollPane6.add(jTree);
        /*jTree1.setVisible(false);
        jTree1 = jTree;
        jTree1.setVisible(true);*/
    }
    public ClientHandler getCurrentClient(String computerName){
        Vector clientHandlers = server.getClientHandlers();
        ClientHandler clientHandler = null;
        for (int i = 0; i < clientHandlers.size(); i++) {
            clientHandler = (ClientHandler) clientHandlers.get(i);
            if(clientHandler.getClientGeneralInformation().getComputerName().equals(computerName)){
                return clientHandler;
            }
        }
        return clientHandler;
    }
    Vector menus = new Vector();
    public void setPopupMenu(MouseEvent e){
        menus.clear();
        JPopupMenu jPopupMenu = new JPopupMenu();
        String []arg = {"Chat with "+tempClient.getClientHostName()};
        //Vector menus = new Vector();
        for (int i = 0; i < arg.length; i++) {
            JMenuItem menu = new JMenuItem(arg[i]);
            menus.add(menu);

            jPopupMenu.add(menu);
            menu.addActionListener(new ActionListener() {
                public void actionPerformed(ActionEvent e) {
                    for (int j = 0; j < menus.size(); j++) {
                        JMenuItem obj = (JMenuItem) menus.get(j);
                        if(e.getSource() == obj){
                            if(j==0){
                                showPopupChatBox(tempClient);
                            }
                        }
                    }
                }
            });
        }
        jPopupMenu.show(e.getComponent(), e.getX(), e.getY());
    }
    private Vector tempCh = new Vector();
    ChatBox cb = null;
    public void showPopupChatBox(ClientHandler ch){
        boolean flag = true;
        for (int i = 0; i < tempCh.size(); i++) {
            if(ch == (ClientHandler)tempCh.get(i)){
                flag = false;
            }
        }
        tempCh.add(ch);
        if(flag){
            cb = new ChatBox(ch);
            
            cb.setVisible(true);

            Thread tr = new Thread(new Runnable() {
                public void run() {
                    while(cb.isShowing()){
                        if(!cb.getClientHandler().getMessage().equals("")){
                            cb.setMessage(cb.getClientHandler().getMessage());
                            cb.getClientHandler().clearMessage();
                        }
                    }
                }
            });
            tr.start();
            cb.addWindowListener(new WindowAdapter() {
                @Override
                public void windowClosed(WindowEvent e){
                    ChatBox temp = (ChatBox) e.getSource();
                    
                    for (int i = 0; i < tempCh.size(); i++) {
                        ClientHandler chTemp = (ClientHandler) tempCh.get(i);
                        //System.out.println(temp.getTitle()+":"+chTemp.getClientHostName());
                        if(chTemp.getClientHostName().equals(temp.getTitle())){
                            chTemp.sendCommand(ServerCommand.CLOSE_CHATBOX);
                            tempCh.removeElementAt(i);
                        }
                    }
                }
            });
        }
    }
}
