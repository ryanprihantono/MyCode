/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */

package data;

import java.awt.Graphics2D;
import java.awt.image.BufferedImage;
import java.io.File;
import java.io.IOException;
import java.io.Serializable;
import java.util.Vector;
import java.util.logging.Level;
import java.util.logging.Logger;
import javax.imageio.ImageIO;

/**
 *
 * @author Lockhart
 */
public class ImageManager implements Serializable{
    private Vector imageFileNames;
    private String hostname;

    public ImageManager(String hostname) {
        imageFileNames = new Vector();
        this.hostname = hostname;
    }
    public void saveImage(BufferedImage bufferedImage){
        try {
            String fileName = getNextFileName();
            File file = new File("screenshots\\" + fileName);
            ImageIO.write(bufferedImage, "jpeg", file);
            imageFileNames.add(fileName);
        } catch (IOException ex) {
            Logger.getLogger(ImageManager.class.getName()).log(Level.SEVERE, null, ex);
        }
    }
    public BufferedImage getThumbnail(){
        String fileName = null;
        if(imageFileNames.size()==0)
            fileName = "screenshots\\nopic.jpg";
        else{
            fileName = "screenshots\\nopic.jpg";//"screenshots\\"+(String) imageFileNames.lastElement();
            
        }
        BufferedImage bi = null;
        //System.out.println(fileName);
        try {
            bi = ImageIO.read(new File(fileName));
            int type = bi.getType() == 0 ? BufferedImage.TYPE_INT_ARGB : bi.getType();
            BufferedImage resizedImage = new BufferedImage(100, 100, type);
            Graphics2D g = resizedImage.createGraphics();
            g.drawImage(bi, 0, 0, 100, 100, null);
            g.dispose();

        } catch (IOException ex) {
            Logger.getLogger(ImageManager.class.getName()).log(Level.SEVERE, null, ex);
        }
        return bi;
    }
    public String getNextFileName(){
        if(imageFileNames.size()!=0){
            String temp = (String) imageFileNames.lastElement();
            temp = temp.substring(hostname.length());
            temp = temp.substring(0,temp.length()-4);
            int nextIdx = Integer.parseInt(temp);
            nextIdx++;
            return hostname+nextIdx+".jpg";
        }
        return hostname+1+".jpg";
    }
}
