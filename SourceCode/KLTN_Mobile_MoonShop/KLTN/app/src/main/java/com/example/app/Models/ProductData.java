package com.example.app.Models;

public class ProductData {
    public long productID ;
 public String productName;
   public int productQuantity ;
    public int   productPrice ;
    public String  productDescribe ;
  public String productImage ;
    public int  productTypeID ;
  public int  isActive;
    public String Size ;
    public String Color ;
    public String img1 ;
    public String img2 ;
    public String img3 ;
    public String origin;
    public String brand ;
    public String desciption ;

    public ProductData() {
    }

    public ProductData(long productID, String productName, int productQuantity, int productPrice, String productDescribe, String productImage, int productTypeID, int isActive, String size, String color, String img1, String img2, String img3, String origin, String brand, String desciption) {
        this.productID = productID;
        this.productName = productName;
        this.productQuantity = productQuantity;
        this.productPrice = productPrice;
        this.productDescribe = productDescribe;
        this.productImage = productImage;
        this.productTypeID = productTypeID;
        this.isActive = isActive;
        Size = size;
        Color = color;
        this.img1 = img1;
        this.img2 = img2;
        this.img3 = img3;
        this.origin = origin;
        this.brand = brand;
        this.desciption = desciption;
    }

    public long getProductID() {
        return productID;
    }

    public void setProductID(long productID) {
        this.productID = productID;
    }

    public String getProductName() {
        return productName;
    }

    public void setProductName(String productName) {
        this.productName = productName;
    }

    public int getProductQuantity() {
        return productQuantity;
    }

    public void setProductQuantity(int productQuantity) {
        this.productQuantity = productQuantity;
    }

    public int getProductPrice() {
        return productPrice;
    }

    public void setProductPrice(int productPrice) {
        this.productPrice = productPrice;
    }

    public String getProductDescribe() {
        return productDescribe;
    }

    public void setProductDescribe(String productDescribe) {
        this.productDescribe = productDescribe;
    }

    public String getProductImage() {
        return productImage;
    }

    public void setProductImage(String productImage) {
        this.productImage = productImage;
    }

    public int getProductTypeID() {
        return productTypeID;
    }

    public void setProductTypeID(int productTypeID) {
        this.productTypeID = productTypeID;
    }

    public int getIsActive() {
        return isActive;
    }

    public void setIsActive(int isActive) {
        this.isActive = isActive;
    }

    public String getSize() {
        return Size;
    }

    public void setSize(String size) {
        Size = size;
    }

    public String getColor() {
        return Color;
    }

    public void setColor(String color) {
        Color = color;
    }

    public String getImg1() {
        return img1;
    }

    public void setImg1(String img1) {
        this.img1 = img1;
    }

    public String getImg2() {
        return img2;
    }

    public void setImg2(String img2) {
        this.img2 = img2;
    }

    public String getImg3() {
        return img3;
    }

    public void setImg3(String img3) {
        this.img3 = img3;
    }

    public String getOrigin() {
        return origin;
    }

    public void setOrigin(String origin) {
        this.origin = origin;
    }

    public String getBrand() {
        return brand;
    }

    public void setBrand(String brand) {
        this.brand = brand;
    }

    public String getDesciption() {
        return desciption;
    }

    public void setDesciption(String desciption) {
        this.desciption = desciption;
    }
}
