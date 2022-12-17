package com.example.app.Models;

public class CartData {
    public long cartID ;
    public long productID ;
    public String productName ;
    public String  productImage;
    public long productPrice ;
    public int cartQuantity ;
    public long cartMoney ;

    public CartData() {
    }

    public CartData(String productName, String productImage) {
        this.productName = productName;
        this.productImage = productImage;
    }

    public CartData(long cartID, long productID, String productName, String productImage, long productPrice, int cartQuantity, long cartMoney) {
        this.cartID = cartID;
        this.productID = productID;
        this.productName = productName;
        this.productImage = productImage;
        this.productPrice = productPrice;
        this.cartQuantity = cartQuantity;
        this.cartMoney = cartMoney;
    }

    public long getCartID() {
        return cartID;
    }

    public void setCartID(long cartID) {
        this.cartID = cartID;
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

    public String getProductImage() {
        return productImage;
    }

    public void setProductImage(String productImage) {
        this.productImage = productImage;
    }

    public long getProductPrice() {
        return productPrice;
    }

    public void setProductPrice(long productPrice) {
        this.productPrice = productPrice;
    }

    public int getCartQuantity() {
        return cartQuantity;
    }

    public void setCartQuantity(int cartQuantity) {
        this.cartQuantity = cartQuantity;
    }

    public long getCartMoney() {
        return cartMoney;
    }

    public void setCartMoney(long cartMoney) {
        this.cartMoney = cartMoney;
    }
}
