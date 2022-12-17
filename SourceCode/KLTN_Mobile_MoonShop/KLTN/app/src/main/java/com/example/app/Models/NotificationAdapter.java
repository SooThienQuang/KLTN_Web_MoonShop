package com.example.app.Models;

import android.content.Context;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.BaseAdapter;
import android.widget.ImageView;
import android.widget.TextView;

import com.example.app.R;
import com.squareup.picasso.Picasso;

import java.util.ArrayList;

import SERVER.SERVER;

public class NotificationAdapter extends BaseAdapter {
Context context;
ArrayList<Notification_class>data;

    public NotificationAdapter(Context context, ArrayList<Notification_class> data) {
        this.context = context;
        this.data = data;
    }

    @Override
    public int getCount() {
        return data.size();
    }

    @Override
    public Object getItem(int i) {
        return data.get(i);
    }

    @Override
    public long getItemId(int i) {
        return 0;
    }

    @Override
    public View getView(int i, View view, ViewGroup viewGroup) {
        if(view==null)
        {
            Notification_class quocgia=data.get(i);
            view= LayoutInflater.from(context).inflate(R.layout.notification_layout_item,null);
            ImageView imageView=view.findViewById(R.id.imgnoti);
            TextView textView=view.findViewById(R.id.txtnamenoti);
            TextView textView1=view.findViewById(R.id.txtnotidetail);
            Picasso.get().load(SERVER.urlhinhanh+quocgia.image).into(imageView);
            textView.setText(quocgia.title);
            textView1.setText(quocgia.message);
        }
        return view;
    }
}
