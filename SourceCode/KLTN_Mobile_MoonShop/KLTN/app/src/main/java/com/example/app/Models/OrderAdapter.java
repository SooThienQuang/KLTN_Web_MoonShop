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
import androidx.recyclerview.widget.RecyclerView;

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

import SERVER.SERVER;

public class OrderAdapter extends RecyclerView.Adapter<KHUNGNHIN> {
    Context context;
    ArrayList<OrderData> data=new ArrayList<>();
    RequestQueue requestQueue;
    public OrderAdapter(Context context, ArrayList<OrderData> data) {
        this.context = context;
        this.data = data;
    }

    @NonNull
    @Override
    public KHUNGNHIN onCreateViewHolder(@NonNull ViewGroup parent, int viewType) {
        View v= LayoutInflater.from(context).inflate(R.layout.order_layout_item,null);
        return new KHUNGNHIN(v);
    }

    @Override
    public void onBindViewHolder(@NonNull KHUNGNHIN holder, int position) {
        OrderData cd = data.get(position);
        requestQueue= Volley.newRequestQueue(context.getApplicationContext());
        Picasso.get().load(SERVER.urlhinhanh+cd.productImage).into(holder.image);
        holder.gia.setText((int) cd.productPrice+" VND");
        holder.name.setText(cd.productName);
        holder.sl.setText(cd.orderQuantity+"");
        holder.stt.setText(cd.status);
        holder.money.setText(cd.orderMoney +" VND");
        holder.image.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                Intent intent=new Intent(context, DetailsProductActivity.class);
                intent.putExtra("proID",cd.productID);
                context.startActivity(intent);
            }
        });
    }

    @Override
    public int getItemCount() {
        return data.size();
    }
}


class KHUNGNHIN extends RecyclerView.ViewHolder {
    ImageView image;
    TextView name, gia,stt,money;
    TextView sl;

    public KHUNGNHIN(@NonNull View itemView) {
        super(itemView);
        image = itemView.findViewById(R.id.imageod);
        name = itemView.findViewById(R.id.txtodname);
        gia = itemView.findViewById(R.id.txtodprice);
        sl=itemView.findViewById(R.id.txtslod);
        stt=itemView.findViewById(R.id.statusod);
        money=itemView.findViewById(R.id.txtodmoney);
    }

}
