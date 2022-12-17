package com.example.app.Models;

import java.io.Serializable;

public class Notification_class implements Serializable {
    public long notiID ;
    public  long sendUserID ;
    public String sendUserFullName ;
    public long receiveUserID ;
    public String receiveUserFullName ;
    public long objectID ;
    public long objectTypeID ;
    public String title ;
    public String message ;
    public String image ;
    public long menutype ;
    public long isRead ;

    public Notification_class() {
    }

    public Notification_class(long notiID, String title, String message,String image) {
        this.notiID = notiID;
        this.title = title;
        this.message = message;
        this.image=image;
    }

    public Notification_class(long notiID, long sendUserID, String sendUserFullName, long receiveUserID, String receiveUserFullName, long objectID, long objectTypeID, String title, String message, String image, long menutype, long isRead) {
        this.notiID = notiID;
        this.sendUserID = sendUserID;
        this.sendUserFullName = sendUserFullName;
        this.receiveUserID = receiveUserID;
        this.receiveUserFullName = receiveUserFullName;
        this.objectID = objectID;
        this.objectTypeID = objectTypeID;
        this.title = title;
        this.message = message;
        this.image = image;
        this.menutype = menutype;
        this.isRead = isRead;
    }

    public long getNotiID() {
        return notiID;
    }

    public void setNotiID(long notiID) {
        this.notiID = notiID;
    }

    public long getSendUserID() {
        return sendUserID;
    }

    public void setSendUserID(long sendUserID) {
        this.sendUserID = sendUserID;
    }

    public String getSendUserFullName() {
        return sendUserFullName;
    }

    public void setSendUserFullName(String sendUserFullName) {
        this.sendUserFullName = sendUserFullName;
    }

    public long getReceiveUserID() {
        return receiveUserID;
    }

    public void setReceiveUserID(long receiveUserID) {
        this.receiveUserID = receiveUserID;
    }

    public String getReceiveUserFullName() {
        return receiveUserFullName;
    }

    public void setReceiveUserFullName(String receiveUserFullName) {
        this.receiveUserFullName = receiveUserFullName;
    }

    public long getObjectID() {
        return objectID;
    }

    public void setObjectID(long objectID) {
        this.objectID = objectID;
    }

    public long getObjectTypeID() {
        return objectTypeID;
    }

    public void setObjectTypeID(long objectTypeID) {
        this.objectTypeID = objectTypeID;
    }

    public String getTitle() {
        return title;
    }

    public void setTitle(String title) {
        this.title = title;
    }

    public String getMessage() {
        return message;
    }

    public void setMessage(String message) {
        this.message = message;
    }

    public String getImage() {
        return image;
    }

    public void setImage(String image) {
        this.image = image;
    }

    public long getMenutype() {
        return menutype;
    }

    public void setMenutype(long menutype) {
        this.menutype = menutype;
    }

    public long getIsRead() {
        return isRead;
    }

    public void setIsRead(long isRead) {
        this.isRead = isRead;
    }
}
