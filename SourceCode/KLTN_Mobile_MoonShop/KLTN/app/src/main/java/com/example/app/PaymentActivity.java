package com.example.app;

import androidx.appcompat.app.AppCompatActivity;
import androidx.cardview.widget.CardView;
import androidx.recyclerview.widget.GridLayoutManager;
import androidx.recyclerview.widget.RecyclerView;

import android.annotation.SuppressLint;
import android.content.Context;
import android.content.Intent;
import android.content.SharedPreferences;
import android.os.Bundle;
import android.util.Base64;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;
import android.widget.ImageView;
import android.widget.TextView;
import android.widget.Toast;

import com.android.volley.Request;
import com.android.volley.RequestQueue;
import com.android.volley.Response;
import com.android.volley.VolleyError;
import com.android.volley.toolbox.JsonArrayRequest;
import com.android.volley.toolbox.StringRequest;
import com.android.volley.toolbox.Volley;
import com.example.app.Models.CartAdapter;
import com.example.app.Models.CartData;
import com.example.app.Models.ProductAdapter;
import com.example.app.Models.ProductData;
import com.squareup.picasso.Picasso;

import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

import java.util.ArrayList;

import SERVER.SERVER;

public class PaymentActivity extends AppCompatActivity {

    EditText txtpaymentname,txtpaymentphone,txtpaymentaddress;
    CardView cv;
    Button thanhtoan;
    ImageView back;
    RecyclerView rvproduct;
    TextView tongtien,tensp,price,sl;
    RequestQueue requestQueue;
    ImageView img,cong,tru;
    SharedPreferences luutru;
    ProductAdapter productAdapter;
    ArrayList<ProductData> dataproduct=new ArrayList<>();
    ArrayList<CartData> datacart=new ArrayList<>();
    CartAdapter cartAdapter;
    private  Integer gia=0;
    @SuppressLint("MissingInflatedId")
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_payment);
        luutru= getSharedPreferences("savefile", Context.MODE_PRIVATE);
        back=findViewById(R.id.backhome);
        cv=findViewById(R.id.odone);
        //cv.setVisibility(View.INVISIBLE);
        tongtien=findViewById(R.id.txttongtienpay);
        tensp=findViewById(R.id.txtproname);
        img=findViewById(R.id.proimg);
        price=findViewById(R.id.txtproprice);
        sl=findViewById(R.id.txtodsl);
        rvproduct=findViewById(R.id.rvpayment);
        sl.setText("1");
        txtpaymentname=findViewById(R.id.txtpaymentname);
        txtpaymentname.setFocusable(false);
        txtpaymentphone=findViewById(R.id.txtpaymentphone);
        txtpaymentphone.setFocusable(false);
        txtpaymentaddress=findViewById(R.id.txtpaymentaddress);
        thanhtoan=findViewById(R.id.btnpaymentend);
        requestQueue= Volley.newRequestQueue(this);
        back.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                Intent intent1=new Intent(PaymentActivity.this,MainActivity.class);
                startActivity(intent1);
            }
        });
        luutru= getSharedPreferences("savefile", Context.MODE_PRIVATE);
        String cusID= luutru.getString("id","123");
        String customerID=cusID.substring(1,cusID.length()-1);
        Intent intent=getIntent();

        long maID=intent.getLongExtra("proIDDD",0);
        if (maID>0)
        {
            //cv.setVisibility(View.VISIBLE);
            LoadProductDetail(maID);
        }
        else
        {
            cv.setVisibility(View.GONE);
            loadCartt(customerID);
        }

        cong=findViewById(R.id.imodcong);
        cong.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                Integer soluong=Integer.parseInt(sl.getText().toString());
                sl.setText(soluong+1+"");
                tongtien.setText(gia*(soluong+1)+" VND");
            }
        });
        tru=findViewById(R.id.imodtru);
        tru.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                Integer soluong=Integer.parseInt(sl.getText().toString());
               if(soluong>1)
               {
                   sl.setText(soluong-1+"");

                   tongtien.setText(gia*(soluong-1)+"");
               }

            }
        });

        LoadCus(customerID);
        thanhtoan.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                Integer soluong=Integer.parseInt(sl.getText().toString());
                //String url=SERVER.url+ "/orderOne?proid=20221129200137&quantity=2&cusi=20221210184002&address=36/32S2,Phường Tân Sơn Nhì,Quận Tân Phú,Thành phố Hồ Chí Minh";
              String url=SERVER.url;
               if (maID>0)
               {
                    url=url+"/orderOne?proid="+maID+"&quantity="+soluong+"&cusi="+customerID+"&address="+txtpaymentaddress.getText().toString();
                    //Toast.makeText(getApplicationContext(),url, Toast.LENGTH_LONG).show();//display the response on screen

               }
               else
               {
                   url=url+"/ordermulti?cusi="+customerID+"&address="+txtpaymentaddress.getText().toString();
                   //Toast.makeText(getApplicationContext(),url, Toast.LENGTH_LONG).show();//display the response on screen

               }
                StringRequest mStringRequest = new StringRequest(Request.Method.GET, url, new Response.Listener<String>() {
                    @Override
                    public void onResponse(String response) {
                        if(response.equals("true"))
                        {
                            Toast.makeText(getApplicationContext(),"Đặt hàng thành công", Toast.LENGTH_LONG).show();//display the response on screen
                            Intent intent1 = new Intent(PaymentActivity.this,
                                    MainActivity.class);
                            startActivity(intent1);
                        }
                        else
                        {
                            Toast.makeText(getApplicationContext(),"Đặt hàng thất bại", Toast.LENGTH_LONG).show();//display the response on screen
                        }


                    }
                }, new Response.ErrorListener() {
                    @Override
                    public void onErrorResponse(VolleyError error) {
                        Toast.makeText(getApplicationContext(),"Lỗi :" + error.toString(), Toast.LENGTH_LONG).show();//display the response on screen

                    }
                });

                requestQueue.add(mStringRequest);
                //Toast.makeText(getApplicationContext(),"Bạn chọn đặt hàng tên khách hàng:"+txtpaymentaddress.getText(),Toast.LENGTH_SHORT).show();

            }
        });
    }
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
                       ProductData cd=new ProductData(productObject.getLong("productID"),productObject.getString("productName"),productObject.getInt("productQuantity"),productObject.getInt("productPrice"),productObject.getString("productDescribe"),productObject.getString("productImage"),productObject.getInt("productTypeID"),productObject.getInt("isActive"),productObject.getString("Size"),productObject.getString("Color"),productObject.getString("img1"),productObject.getString("img2"),productObject.getString("img3"),productObject.getString("origin"),productObject.getString("brand"),productObject.getString("desciption"));
                       tongtien.setText(cd.productPrice+"");
                        //Toast.makeText(getApplicationContext(),"loi"+cd.productImage,Toast.LENGTH_SHORT).show();
                        tensp.setText(cd.productName);
                        price.setText(cd.productPrice+"");
                        gia=Integer.parseInt(cd.productPrice+"");
                        Picasso.get().load(SERVER.urlhinhanh+cd.productImage).into(img);
                    }
                    catch (JSONException e)
                    {
                        Toast.makeText(getApplicationContext(),"loi"+e.toString(),Toast.LENGTH_SHORT).show();
                    }

                }
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
    public void LoadCus(String ma)
    {
        String url=SERVER.url+"/getinfouser?id="+ma;
        //String url= SERVER.url+"?id="+ma;
        RequestQueue requestQueue= Volley.newRequestQueue(getApplicationContext());
        Response.Listener<JSONArray>thanhcong=new Response.Listener<JSONArray>() {
            @Override
            public void onResponse(JSONArray response) {
                for(int i=0;i<response.length();i++)
                {
                    try {
                        JSONObject u=response.getJSONObject(i);

                        txtpaymentname.setText(u.getString("customerName"));
                        String add=u.getString("customerAdd");
                        if(add.length()>0)
                        {
                            txtpaymentaddress.setText(add);
                        }
                        txtpaymentphone.setText(u.getString("customerPhone"));
                        //Toast.makeText(getApplicationContext(),u.toString(),Toast.LENGTH_SHORT).show();

                    }
                    catch (JSONException e)
                    {
                        Toast.makeText(getApplicationContext(),"loi"+e.toString(),Toast.LENGTH_SHORT).show();
                    }

                }
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
    public void loadCartt(String id)
    {
        String url= SERVER.urlgetcart+id;
        RequestQueue requestQueue= Volley.newRequestQueue(this);
        Response.Listener<JSONArray>thanhcong=new Response.Listener<JSONArray>() {
            @Override
            public void onResponse(JSONArray response) {
                for(int i=0;i<response.length();i++)
                {
                    try {
                        JSONObject c=response.getJSONObject(i);
                        CartData cd=new CartData(c.getLong("cartID"),c.getLong("productID"),c.getString("productName"),c.getString("productImage"),c.getLong("productPrice"),c.getInt("cartQuantity"),c.getLong("cartMoney"));
                        datacart.add(cd);
                    }
                    catch (JSONException e)
                    {
                        Toast.makeText(getApplicationContext(),"loi"+e.toString(),Toast.LENGTH_SHORT).show();
                    }
                }
                int tongg=0;
                for(int i=0;i<datacart.size();i++)
                {
                    tongg=tongg+Integer.parseInt(String.valueOf(datacart.get(i).cartMoney));
                }
                tongtien.setText(tongg+ " VND");
                //cap nhat lai danh sach
                cartAdapter.notifyDataSetChanged();
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
        cartAdapter=new CartAdapter(getApplicationContext(),datacart);
        rvproduct.setHasFixedSize(true);
        rvproduct.setLayoutManager(new GridLayoutManager(getApplicationContext(),RecyclerView.VERTICAL));
        rvproduct.setAdapter(cartAdapter);
    }
}