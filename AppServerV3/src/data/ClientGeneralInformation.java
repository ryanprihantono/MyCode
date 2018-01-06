/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */

package data;

import java.io.Serializable;

/**
 *
 * @author Lockhart
 */
public class ClientGeneralInformation implements Serializable{
    private static final long serialVersionUID = 1L;
    private String computerName;
    private String ipAddress;
    private String macAddress;
    private String windowsAccount;
    private String group;
    private String linkTime;
    private String status;

    public ClientGeneralInformation() {
    }

    public ClientGeneralInformation(String computerName, String ipAddress, String macAddress, String windowsAccount, String group, String linkTime, String status) {
        this.computerName = computerName;
        this.ipAddress = ipAddress;
        this.macAddress = macAddress;
        this.windowsAccount = windowsAccount;
        this.group = group;
        this.linkTime = linkTime;
        this.status = status;
    }
    public void setGroup(String group){
        this.group = group;
    }
    public String getComputerName() {
        return computerName;
    }

    public String getGroup() {
        return group;
    }

    public String getIpAddress() {
        return ipAddress;
    }

    public String getLinkTime() {
        return linkTime;
    }

    public String getMacAddress() {
        return macAddress;
    }

    public String getStatus() {
        return status;
    }

    public String getWindowsAccount() {
        return windowsAccount;
    }
    
}
