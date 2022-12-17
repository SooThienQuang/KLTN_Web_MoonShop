package com.example.app;

import androidx.appcompat.app.AppCompatActivity;
import androidx.appcompat.widget.SearchView;
import androidx.recyclerview.widget.GridLayoutManager;
import androidx.recyclerview.widget.RecyclerView;

import android.annotation.SuppressLint;
import android.content.Intent;
import android.os.Bundle;
import android.view.View;
import android.widget.Button;
import android.widget.TextView;
import android.widget.Toast;

import com.android.volley.RequestQueue;
import com.android.volley.Response;
import com.android.volley.VolleyError;
import com.android.volley.toolbox.JsonArrayRequest;
import com.android.volley.toolbox.Volley;
import com.example.app.Models.ProductAdapter;
import com.example.app.Models.ProductData;

import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

import java.util.ArrayList;

import SERVER.SERVER;

public class Search extends AppCompatActivity {

    private Button back;
    SearchView searchView;
    private RecyclerView rvproduct1;
    TextView kqsearch;
    ArrayList<ProductData> dataproduct=new ArrayList<>();
    ProductAdapter productAdapter;
    @SuppressLint("MissingInflatedId")
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_search);
        Intent intent=getIntent();
        String keyword=intent.getStringExtra("txtsearch");
        back=findViewById(R.id.btnbacksearch);
        rvproduct1=findViewById(R.id.rvsearchtext);
        kqsearch=findViewById(R.id.ketquasearch);
        kqsearch.setText("Kết quả tìm kiếm cho từ khóa :"+keyword);
        back.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                Intent intent =new Intent(Search.this,MainActivity.class);
                startActivity(intent);
            }
        });
        searchView=findViewById(R.id.searchviewtext);
        searchView.setOnQueryTextListener(new SearchView.OnQueryTextListener() {
            @Override
            public boolean onQueryTextSubmit(String s) {
                Intent intent=new Intent(getApplicationContext(),Search.class);
                intent.putExtra("txtsearch",s);
                startActivity(intent);
                return false;
            }

            @Override
            public boolean onQueryTextChange(String s) {
                return false;
            }
        });
        rvproduct1.setHasFixedSize(true);
        productAdapter=new ProductAdapter(this,dataproduct);
        rvproduct1.setLayoutManager(new GridLayoutManager(this,3));
        rvproduct1.setAdapter(productAdapter);
        LoadProduct(keyword);
    }
    public void LoadProduct(String keyword)
    {
        String url= SERVER.urlsearchtext+keyword;
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