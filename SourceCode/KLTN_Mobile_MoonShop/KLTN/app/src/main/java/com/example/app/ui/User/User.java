package com.example.app.ui.User;

import android.content.Context;
import android.content.Intent;
import android.content.SharedPreferences;
import android.os.Bundle;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.Button;
import android.widget.ImageView;
import android.widget.TextView;
import android.widget.Toast;

import androidx.annotation.NonNull;
import androidx.annotation.Nullable;
import androidx.fragment.app.Fragment;
import androidx.recyclerview.widget.GridLayoutManager;
import androidx.recyclerview.widget.RecyclerView;

import com.android.volley.RequestQueue;
import com.android.volley.Response;
import com.android.volley.VolleyError;
import com.android.volley.toolbox.JsonArrayRequest;
import com.android.volley.toolbox.Volley;
import com.example.app.DetailsProductActivity;
import com.example.app.Login;
import com.example.app.MainActivity;
import com.example.app.Models.CartAdapter;
import com.example.app.Models.CartData;
import com.example.app.Models.OrderAdapter;
import com.example.app.Models.OrderData;
import com.example.app.R;
import com.example.app.SignUp;
import com.example.app.UserActivity;
import com.squareup.picasso.Picasso;

import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

import java.util.ArrayList;

import SERVER.SERVER;

public class User extends Fragment {
    @Nullable
    @Override
    public View onCreateView(@NonNull LayoutInflater inflater, @Nullable ViewGroup container, @Nullable Bundle savedInstanceState) {
        View view=inflater.inflate(R.layout.fragment_user,container,false);
        return view;
    }
    //xử lý bên trong
    RequestQueue requestQueue;
    SharedPreferences luutru;
    Button signup,fl;
    ImageView img;
    RecyclerView rvod;
    ArrayList<OrderData> datacart=new ArrayList<>();
    OrderAdapter cartAdapter;
    TextView fullname,sdt,diachi;
    //khai bao adapter
    @Override
    public void onViewCreated(@NonNull View view, @Nullable Bundle savedInstanceState) {
        super.onViewCreated(view, savedInstanceState);
        requestQueue= Volley.newRequestQueue(getContext());
        luutru= getContext().getSharedPreferences("savefile", Context.MODE_PRIVATE);
        String cusID= luutru.getString("id","123");
        String customerID=cusID.substring(1,cusID.length()-1);
        img=view.findViewById(R.id.imguser);
        signup=view.findViewById(R.id.btnlgout);
        fullname=view.findViewById(R.id.txtfullname);
        rvod=view.findViewById(R.id.rvod);
        sdt=view.findViewById(R.id.txtsdt);
        diachi=view.findViewById(R.id.txtdiachi);
        signup.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                Intent intent=new Intent(getContext(), Login.class);
                startActivity(intent);
            }
        });
        LoadCus(customerID);
//        public long orderID ;
//        public long productID ;
//        public String productName ;
//        public String  productImage;
//        public long productPrice ;
//        public int orderQuantity ;
//        public long orderMoney ;
        String url= SERVER.url+"/orrder?id="+customerID;
        RequestQueue requestQueue= Volley.newRequestQueue(getContext());
        Response.Listener<JSONArray>thanhcong=new Response.Listener<JSONArray>() {
            @Override
            public void onResponse(JSONArray response) {
                for(int i=0;i<response.length();i++)
                {
                    try {
                        JSONObject c=response.getJSONObject(i);
                        OrderData cd=new OrderData(c.getLong("cartID"),c.getLong("productID"),c.getString("productName"),c.getString("productImage"),c.getLong("productPrice"),c.getInt("cartQuantity"),c.getLong("cartMoney"),c.getString("status"));
                        datacart.add(cd);
                    }
                    catch (JSONException e)
                    {
                        Toast.makeText(getContext(),"loi"+e.toString(),Toast.LENGTH_SHORT).show();
                    }
                }
                //cap nhat lai danh sach
                cartAdapter.notifyDataSetChanged();
            }
        };

        Response.ErrorListener thatbai=new Response.ErrorListener() {
            @Override
            public void onErrorResponse(VolleyError error) {
                Toast.makeText(getContext(),"that bai"+error.toString(),Toast.LENGTH_SHORT).show();
            }
        };
        JsonArrayRequest jsonArrayRequest=new JsonArrayRequest(url,thanhcong,thatbai);
        requestQueue.add(jsonArrayRequest);
        cartAdapter=new OrderAdapter(getContext(),datacart);
        rvod.setHasFixedSize(true);
        rvod.setLayoutManager(new GridLayoutManager(getContext(),RecyclerView.VERTICAL));
        rvod.setAdapter(cartAdapter);
    }
    public void LoadCus(String ma)
    {
        String url= SERVER.url+"/getinfouser?id="+ma;
        //String url= SERVER.url+"?id="+ma;
        RequestQueue requestQueue= Volley.newRequestQueue(getContext());
        Response.Listener<JSONArray>thanhcong=new Response.Listener<JSONArray>() {
            @Override
            public void onResponse(JSONArray response) {
                for(int i=0;i<response.length();i++)
                {
                    try {
                        JSONObject u=response.getJSONObject(i);
                        Picasso.get().load(SERVER.urlhinhanh+u.get("photo")).into(img);
                        fullname.setText("Họ và tên: "+u.getString("customerName"));
                        diachi.setText("Địa chỉ: "+u.getString("customerAdd"));
                        sdt.setText("Số điện thoại: "+u.getString("customerPhone"));
                    }
                    catch (JSONException e)
                    {
                        Toast.makeText(getContext(),"loi"+e.toString(),Toast.LENGTH_SHORT).show();
                    }

                }
            }
        };
        Response.ErrorListener thatbai=new Response.ErrorListener() {
            @Override
            public void onErrorResponse(VolleyError error) {
                Toast.makeText(getContext(),"that bai"+error.toString(),Toast.LENGTH_SHORT).show();
            }
        };
        JsonArrayRequest jsonArrayRequest=new JsonArrayRequest(url,thanhcong,thatbai);
        requestQueue.add(jsonArrayRequest);
    }
}
