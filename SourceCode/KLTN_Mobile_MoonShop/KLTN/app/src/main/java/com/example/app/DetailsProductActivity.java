package com.example.app;

import androidx.annotation.NonNull;
import androidx.annotation.Nullable;
import androidx.appcompat.app.AppCompatActivity;
import androidx.recyclerview.widget.GridLayoutManager;
import androidx.recyclerview.widget.RecyclerView;

import android.content.Context;
import android.content.Intent;
import android.content.SharedPreferences;
import android.os.Bundle;
import android.util.Base64;
import android.view.MenuItem;
import android.view.View;
import android.webkit.WebView;
import android.widget.Button;
import android.widget.ImageView;
import android.widget.TextView;
import android.widget.Toast;
import android.widget.ViewFlipper;

import com.android.volley.AuthFailureError;
import com.android.volley.Request;
import com.android.volley.RequestQueue;
import com.android.volley.Response;
import com.android.volley.VolleyError;
import com.android.volley.toolbox.JsonArrayRequest;
import com.android.volley.toolbox.JsonRequest;
import com.android.volley.toolbox.StringRequest;
import com.android.volley.toolbox.Volley;
import com.example.app.Models.ProductAdapter;
import com.example.app.Models.ProductData;
import com.google.android.material.bottomnavigation.BottomNavigationView;
import com.google.android.material.navigation.NavigationBarView;
import com.squareup.picasso.Picasso;

import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;
import org.json.JSONStringer;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.Map;

import SERVER.SERVER;

public class DetailsProductActivity extends AppCompatActivity {
    ArrayList<ProductData> dataproduct=new ArrayList<>();

    ProductAdapter productAdapter;
    RecyclerView rvproduct;
    ViewFlipper viewFlipper;
    BottomNavigationView bottomNavigationView;
    TextView ten,gia,tt;
    Button b1;
    RequestQueue requestQueue;
    SharedPreferences luutru;
    WebView webView;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_details_product);
//        WebView myWebView = (WebView) findViewById(R.id.webview);
//        myWebView.loadUrl("http://www.example.com");
        rvproduct=findViewById(R.id.rvproduct2);
        luutru= getSharedPreferences("savefile", Context.MODE_PRIVATE);
        String cusID= luutru.getString("id","123");
        String customerID=cusID.substring(1,cusID.length()-1);
        viewFlipper=findViewById(R.id.viewfliperslideproductdetails);
        requestQueue= Volley.newRequestQueue(this);
        ten=findViewById(R.id.txttensanpham);
        gia=findViewById(R.id.txtgiasanpham);
        tt=findViewById(R.id.txtthongso);
        b1=findViewById(R.id.btnback);
        webView = (WebView) findViewById(R.id.webview);

        rvproduct.setHasFixedSize(true);
        productAdapter=new ProductAdapter(getApplicationContext(),dataproduct);
        rvproduct.setLayoutManager(new GridLayoutManager(getApplicationContext(),2));
        rvproduct.setAdapter(productAdapter);
        b1.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                Intent intent =new Intent(DetailsProductActivity.this,MainActivity.class);
                startActivity(intent);

            }
        });
        LoadProduct1();
        Intent intent;
        intent=getIntent();
        long maID=intent.getLongExtra("proID",0);
       // Toast.makeText(this,""+maID,Toast.LENGTH_SHORT).show();
        LoadProductDetail(maID);
        bottomNavigationView=findViewById(R.id.bottomnavigationdetails);
        bottomNavigationView.setOnItemSelectedListener(new NavigationBarView.OnItemSelectedListener() {
            @Override
            public boolean onNavigationItemSelected(@NonNull MenuItem item) {
                int id=item.getItemId();
                switch (id)
                {
                    case R.id.menuback:
                        Intent intent =new Intent(DetailsProductActivity.this,MainActivity.class);
                        startActivity(intent);
                        break;
                    case  R.id.menupaynow:
                        Intent intent1 = new Intent(DetailsProductActivity.this,
                                PaymentActivity.class);
                        intent1.putExtra("proIDDD",
                                cd.productID);
                        startActivity(intent1);
                        break;
                    case R.id.menuaddtocart:
                        //http://http://thienquang-001-site1.itempurl.com/addcart?userid=20221203020142&productID=20221129194205&quantity=2

                        String url= SERVER.urladdcart+customerID+"&productID="+maID+"&quantity=1";
                        StringRequest  mStringRequest = new StringRequest(Request.Method.GET, url, new Response.Listener<String>() {
                            @Override
                            public void onResponse(String response) {
                                if(response.equals("true"))
                                {
                                    Toast.makeText(getApplicationContext(),"Thêm giỏ hàng thành công", Toast.LENGTH_LONG).show();//display the response on screen
                                }
                                else
                                {
                                    Toast.makeText(getApplicationContext(),"Thêm giỏ hàng thất bại", Toast.LENGTH_LONG).show();//display the response on screen
                                }


                            }
                        }, new Response.ErrorListener() {
                            @Override
                            public void onErrorResponse(VolleyError error) {
                                Toast.makeText(getApplicationContext(),"Lỗi :" + error.toString(), Toast.LENGTH_LONG).show();//display the response on screen

                            }
                        });

                        requestQueue.add(mStringRequest);
                        break;
                }
                return true;
            }
        });
    }
    ProductData cd;
    public void LoadProductDetail(long ma)
    {

        String url= SERVER.urlgetdetalpro+"?id="+ma;
        RequestQueue requestQueue= Volley.newRequestQueue(getApplicationContext());
        Response.Listener<JSONArray>thanhcong=new Response.Listener<JSONArray>() {
            @Override
            public void onResponse(JSONArray response) {
                for(int i=0;i<response.length();i++)
                {
                    try {
                        JSONObject productObject=response.getJSONObject(i);
                         cd=new ProductData(productObject.getLong("productID"),productObject.getString("productName"),productObject.getInt("productQuantity"),productObject.getInt("productPrice"),productObject.getString("productDescribe"),productObject.getString("productImage"),productObject.getInt("productTypeID"),productObject.getInt("isActive"),productObject.getString("Size"),productObject.getString("Color"),productObject.getString("img1"),productObject.getString("img2"),productObject.getString("img3"),productObject.getString("origin"),productObject.getString("brand"),productObject.getString("desciption"));
                        ten.setText(cd.productName);
                        gia.setText("Giá sản phẩm :"+cd.productPrice+""+"VND");
                        tt.setText(cd.productDescribe);
                        String unencodedHtml =cd.desciption;
                        String encodedHtml = Base64.encodeToString(unencodedHtml.getBytes(),
                                Base64.NO_PADDING);
                        webView.loadData(encodedHtml, "text/html", "base64");

                    }
                    catch (JSONException e)
                    {
                        Toast.makeText(getApplicationContext(),"loi"+e.toString(),Toast.LENGTH_SHORT).show();
                    }

                }

                ArrayList<String> manquangcao=new ArrayList<>();
                manquangcao.add(SERVER.urlhinhanh+cd.img3);
                manquangcao.add(SERVER.urlhinhanh+cd.img2);
                manquangcao.add(SERVER.urlhinhanh+cd.img1);
                manquangcao.add(SERVER.urlhinhanh+cd.productImage);
                for(int i=0;i< manquangcao.size();i++)
                {
                    ImageView hinh=new ImageView(getApplicationContext());
                    Picasso.get().load(manquangcao.get(i)).into(hinh);
                    hinh.setScaleType(ImageView.ScaleType.FIT_XY);
                    viewFlipper.addView(hinh);
                }
            }
        };
        viewFlipper.setAutoStart(true);
        viewFlipper.setFlipInterval(5000);
        Response.ErrorListener thatbai=new Response.ErrorListener() {
            @Override
            public void onErrorResponse(VolleyError error) {
                Toast.makeText(getApplicationContext(),"that bai"+error.toString(),Toast.LENGTH_SHORT).show();
            }
        };
        JsonArrayRequest jsonArrayRequest=new JsonArrayRequest(url,thanhcong,thatbai);
        requestQueue.add(jsonArrayRequest);
    }
    public void LoadProduct1()
    {
        String url= SERVER.urlsanpham;
        RequestQueue requestQueue= Volley.newRequestQueue(getApplicationContext());
        Response.Listener<JSONArray>thanhcong=new Response.Listener<JSONArray>() {
            @Override
            public void onResponse(JSONArray response) {
                for(int i=0;i<response.length();i++)
                {
                    try {
                        JSONObject productObject=response.getJSONObject(i);
                        ProductData cd=new ProductData(productObject.getLong("productID"),productObject.getString("productName"),productObject.getInt("productQuantity"),productObject.getInt("productPrice"),productObject.getString("productDescribe"),productObject.getString("productImage"),productObject.getInt("productTypeID"),productObject.getInt("isActive"),productObject.getString("Size"),productObject.getString("Color"),productObject.getString("img1"),productObject.getString("img2"),productObject.getString("img3"),productObject.getString("origin"),productObject.getString("brand"),productObject.getString("desciption"));
                        dataproduct.add(cd);
                    }
                    catch (JSONException e)
                    {
                        Toast.makeText(getApplicationContext(),"loi"+e.toString(),Toast.LENGTH_SHORT).show();
                    }

                }
                //cap nhat lai danh sach
                productAdapter.notifyDataSetChanged();
            }
        };

        Response.ErrorListener thatbai=new Response.ErrorListener() {
            @Override
            public void onErrorResponse(VolleyError error) {
                Toast.makeText(getApplicationContext(),"that bai"+error.toString(),Toast.LENGTH_SHORT).show();
            }
        };
        JsonArrayRequest jsonArrayRequest=new JsonArrayRequest(url,thanhcong,thatbai);
        requestQueue.add(jsonArrayRequest);
    }
}