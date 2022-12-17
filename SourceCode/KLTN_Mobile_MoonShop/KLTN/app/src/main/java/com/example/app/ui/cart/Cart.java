package com.example.app.ui.cart;

import android.content.Context;
import android.content.DialogInterface;
import android.content.Intent;
import android.content.SharedPreferences;
import android.os.Bundle;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.Button;
import android.widget.TextView;
import android.widget.Toast;

import androidx.annotation.NonNull;
import androidx.annotation.Nullable;
import androidx.appcompat.app.AlertDialog;
import androidx.fragment.app.Fragment;
import androidx.recyclerview.widget.GridLayoutManager;
import androidx.recyclerview.widget.LinearLayoutManager;
import androidx.recyclerview.widget.RecyclerView;

import com.android.volley.RequestQueue;
import com.android.volley.Response;
import com.android.volley.VolleyError;
import com.android.volley.toolbox.JsonArrayRequest;
import com.android.volley.toolbox.Volley;
import com.example.app.MainActivity;
import com.example.app.Models.CartAdapter;
import com.example.app.Models.CartData;
import com.example.app.PaymentActivity;
import com.example.app.R;

import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

import java.util.ArrayList;

import SERVER.SERVER;

public class Cart extends Fragment {
    ArrayList<CartData> datacart=new ArrayList<>();
    //khai bao adapter
    CartAdapter cartAdapter;
    RecyclerView rvcart;
    SharedPreferences luutru;
    TextView tongtien;
    Button thanhtoan;
    RequestQueue requestQueue;
    @Nullable
    @Override
    public View onCreateView(@NonNull LayoutInflater inflater, @Nullable ViewGroup container, @Nullable Bundle savedInstanceState) {
        View view=inflater.inflate(R.layout.fragment_cart,container,false);
        return view;
    }
    @Override
    public void onViewCreated(@NonNull View view, @Nullable Bundle savedInstanceState) {
        super.onViewCreated(view, savedInstanceState);
        luutru= getContext().getSharedPreferences("savefile", Context.MODE_PRIVATE);
        String cusID= luutru.getString("id","123");
        String id=cusID.substring(1,cusID.length()-1);
        rvcart=view.findViewById(R.id.rvcart);
        thanhtoan=view.findViewById(R.id.btnthanhtoan);
        tongtien=view.findViewById(R.id.carttongtien);
       // String url="http://thienquang-001-site1.itempurl.com/cart?id=";
         String url= SERVER.urlgetcart+id;
        RequestQueue requestQueue= Volley.newRequestQueue(getContext());
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
                        Toast.makeText(getContext(),"loi"+e.toString(),Toast.LENGTH_SHORT).show();
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
                Toast.makeText(getContext(),"that bai"+error.toString(),Toast.LENGTH_SHORT).show();
            }
        };
        JsonArrayRequest jsonArrayRequest=new JsonArrayRequest(url,thanhcong,thatbai);
        requestQueue.add(jsonArrayRequest);
        cartAdapter=new CartAdapter(getContext(),datacart);
        rvcart.setHasFixedSize(true);
        rvcart.setLayoutManager(new GridLayoutManager(getContext(),RecyclerView.VERTICAL));
        rvcart.setAdapter(cartAdapter);
       // LoadCart(id);


        thanhtoan.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                if(datacart.isEmpty())
                {
                    Toast.makeText(getContext(),"Bạn chưa có sản phẩm ",Toast.LENGTH_SHORT).show();
                }
                else
                {
                    Intent intent=new Intent(getContext(), PaymentActivity.class);
                    startActivity(intent);
                }

                //Toast.makeText(getContext(),"Bạn chọn thanh toán cusID:"+id,Toast.LENGTH_SHORT).show();

            }
        });

    }
}
