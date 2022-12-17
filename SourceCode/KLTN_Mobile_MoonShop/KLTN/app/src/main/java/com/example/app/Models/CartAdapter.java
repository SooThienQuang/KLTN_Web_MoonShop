package com.example.app.Models;

import android.content.Context;
import android.content.Intent;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.EditText;
import android.widget.ImageView;
import android.widget.TextView;
import android.widget.Toast;

import androidx.annotation.NonNull;
import androidx.annotation.Nullable;
import androidx.recyclerview.widget.RecyclerView;

import com.android.volley.AuthFailureError;
import com.android.volley.Request;
import com.android.volley.RequestQueue;
import com.android.volley.Response;
import com.android.volley.VolleyError;
import com.android.volley.toolbox.StringRequest;
import com.android.volley.toolbox.Volley;
import com.example.app.DetailsProductActivity;
import com.example.app.R;
import com.squareup.picasso.Picasso;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.Map;

import SERVER.SERVER;

public class CartAdapter extends RecyclerView.Adapter<KHUNG>{
    Context context;
    ArrayList<CartData> data=new ArrayList<>();
    RequestQueue requestQueue;
    public CartAdapter(Context context, ArrayList<CartData> data) {
        this.context = context;
        this.data = data;
    }

    @NonNull
    @Override
    public KHUNG onCreateViewHolder(@NonNull ViewGroup parent, int viewType) {
        View v= LayoutInflater.from(context).inflate(R.layout.cart_layout_item,null);
        return new KHUNG(v);
    }

    @Override
    public void onBindViewHolder(@NonNull KHUNG holder, int position) {
        CartData cd = data.get(position);
        requestQueue= Volley.newRequestQueue(context.getApplicationContext());
        Picasso.get().load(SERVER.urlhinhanh+cd.productImage).into(holder.image);
        holder.gia.setText((int) cd.productPrice+"");
        holder.name.setText(cd.productName);
        holder.sl.setText(cd.cartQuantity+"");
        holder.image.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                Intent intent=new Intent(context, DetailsProductActivity.class);
                intent.putExtra("proID",cd.productID);
                context.startActivity(intent);
            }
        });
        holder.cong.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                int sll=Integer.parseInt(holder.sl.getText().toString())+1;
                holder.sl.setText(sll+"");

            }
        });
        holder.tru.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                if(holder.sl.getText().toString().equals("1")) {
                    Toast.makeText(context.getApplicationContext(), "Lỗi số lượng", Toast.LENGTH_SHORT).show();
                }
                    else
                {
                    int sll=Integer.parseInt(holder.sl.getText().toString())-1;
                    holder.sl.setText(sll+"");
                }

            }
        });
        holder.xoa.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
               // Toast.makeText(context.getApplicationContext(), "xoa cartID:"+cd.cartID+" proid:"+cd.productID, Toast.LENGTH_SHORT).show();
                String url=SERVER.url+ "/deletecart?cartID="+cd.cartID+"&productID="+cd.productID;
                StringRequest  mStringRequest = new StringRequest(Request.Method.GET, url, new Response.Listener<String>() {
                    @Override
                    public void onResponse(String response) {
                        if(response.equals("true"))
                        {
                            Toast.makeText(context.getApplicationContext(), "Xóa giỏ hàng thành công", Toast.LENGTH_LONG).show();//display the response on screen
                        }
                        else
                        {
                            Toast.makeText(context.getApplicationContext(), "Xóa giỏ hàng thất bại", Toast.LENGTH_LONG).show();//display the response on screen
                        }


                    }
                }, new Response.ErrorListener() {
                    @Override
                    public void onErrorResponse(VolleyError error) {
                        Toast.makeText(context.getApplicationContext(), "Lỗi :" + error.toString(), Toast.LENGTH_LONG).show();//display the response on screen

                    }
                });

                requestQueue.add(mStringRequest);
            }

        });

        holder.luu.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                //Toast.makeText(context.getApplicationContext(), "Luu sl:"+holder.sl.getText(), Toast.LENGTH_SHORT).show();
                String url=SERVER.url+ "/upcart?cartID="+cd.cartID+"&productID="+cd.productID+"&quantity="+holder.sl.getText().toString();
                StringRequest  mStringRequest = new StringRequest(Request.Method.GET, url, new Response.Listener<String>() {
                    @Override
                    public void onResponse(String response) {
                        if(response.equals("true"))
                        {
                            Toast.makeText(context.getApplicationContext(), "Cập nhật giỏ hàng thành công", Toast.LENGTH_LONG).show();//display the response on screen
                        }
                        else
                        {
                            Toast.makeText(context.getApplicationContext(), "Cập nhật giỏ hàng thất bại", Toast.LENGTH_LONG).show();//display the response on screen
                        }


                    }
                }, new Response.ErrorListener() {
                    @Override
                    public void onErrorResponse(VolleyError error) {
                        Toast.makeText(context.getApplicationContext(), "Lỗi :" + error.toString(), Toast.LENGTH_LONG).show();//display the response on screen

                    }
                });

                requestQueue.add(mStringRequest);
            }
        });
    }

    @Override
    public int getItemCount() {
        return data.size();
    }
}


class KHUNG extends RecyclerView.ViewHolder {
    ImageView image,cong ,tru,xoa,luu;
    TextView name, gia;
    EditText sl;

    public KHUNG(@NonNull View itemView) {
        super(itemView);
        image = itemView.findViewById(R.id.imagecart);
        name = itemView.findViewById(R.id.txtcartname);
        gia = itemView.findViewById(R.id.txtcartprice);
        sl=itemView.findViewById(R.id.txtcartsl);
        cong=itemView.findViewById(R.id.imcartcong);
        tru=itemView.findViewById(R.id.imcarttru);
        xoa=itemView.findViewById(R.id.btncartdelete);
        luu=itemView.findViewById(R.id.btnsave);
    }

}
