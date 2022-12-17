package com.example.app.Models;

import android.content.Context;
import android.content.Intent;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.view.animation.AnimationUtils;
import android.widget.Filter;
import android.widget.Filterable;
import android.widget.ImageView;
import android.widget.TextView;

import androidx.annotation.NonNull;
import androidx.recyclerview.widget.RecyclerView;

import com.example.app.DetailsProductActivity;
import com.example.app.Login;
import com.example.app.R;
import com.squareup.picasso.Picasso;

import java.util.ArrayList;

import SERVER.SERVER;

public class ProductAdapter extends RecyclerView.Adapter<ProductAdapter.KHUNGNHINPRODUCT> implements Filterable {
    Context context;
    ArrayList<ProductData> data;
    ArrayList<ProductData> fulllist;
    public ProductAdapter(Context context, ArrayList<ProductData> data) {
        this.context = context;
        this.data = data;
        this.fulllist = new ArrayList<>(data);
    }

    @NonNull
    @Override
    public KHUNGNHINPRODUCT onCreateViewHolder(@NonNull ViewGroup parent, int viewType) {
        View v= LayoutInflater.from(context).inflate(R.layout.product_layout,null);
        return new KHUNGNHINPRODUCT(v);
    }

    @Override
    public void onBindViewHolder(@NonNull KHUNGNHINPRODUCT holder, int position) {
        ProductData cd = data.get(position);
        //holder.itemView.startAnimation(AnimationUtils.loadAnimation(context.getApplicationContext(), R.anim.move));

        holder.tensanpham.setText(cd.productName);
        holder.giasanpham.setText("Giá sản phẩm :"+cd.productPrice+"  VND");
        Picasso.get().load(SERVER.urlhinhanh+cd.productImage).into(holder.hinhsanpham);
        holder.hinhsanpham.setOnClickListener(new View.OnClickListener() {
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




    class KHUNGNHINPRODUCT extends RecyclerView.ViewHolder {
        TextView tensanpham;
        ImageView hinhsanpham;
        TextView giasanpham;

        public KHUNGNHINPRODUCT(@NonNull View itemView) {
            super(itemView);
            tensanpham=itemView.findViewById(R.id.tvnameproduct);
            hinhsanpham=itemView.findViewById(R.id.imgProduct);
            giasanpham=itemView.findViewById(R.id.tvPriceProduct);

        }

    }
    @Override
    public Filter getFilter() {
        return sechrchFilter;
    }
    Filter sechrchFilter = new Filter() {
        @Override
        protected FilterResults performFiltering(CharSequence charSequence) {
            ArrayList<ProductData> listfilter = new ArrayList<>();
            if (charSequence==null||charSequence.length()==0)
                listfilter.addAll(fulllist);
            else{
                String chuoiloc = charSequence.toString().toLowerCase().trim();
                for (ProductData item : fulllist){
                    listfilter.add(item);
                }
            }
            FilterResults filterResults = new FilterResults();
            filterResults.values=listfilter;
            return filterResults;
        }

        @Override
        protected void publishResults(CharSequence charSequence, FilterResults filterResults) {
            data.clear();
            data.addAll((ArrayList)filterResults.values);
            notifyDataSetChanged();
        }
    };
}
