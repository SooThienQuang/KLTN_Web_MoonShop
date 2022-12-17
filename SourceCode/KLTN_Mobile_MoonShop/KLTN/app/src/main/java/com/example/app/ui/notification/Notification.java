package com.example.app.ui.notification;

import android.content.Context;
import android.content.SharedPreferences;
import android.os.Bundle;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ListView;
import android.widget.TextView;
import android.widget.Toast;

import androidx.annotation.NonNull;
import androidx.annotation.Nullable;
import androidx.fragment.app.Fragment;

import com.android.volley.RequestQueue;
import com.android.volley.Response;
import com.android.volley.VolleyError;
import com.android.volley.toolbox.JsonArrayRequest;
import com.android.volley.toolbox.Volley;
import com.example.app.Models.NotificationAdapter;
import com.example.app.Models.Notification_class;
import com.example.app.Models.ProductData;
import com.example.app.R;

import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

import java.util.ArrayList;

import SERVER.SERVER;

public class Notification extends Fragment {
    @Nullable
    @Override
    public View onCreateView(@NonNull LayoutInflater inflater, @Nullable ViewGroup container, @Nullable Bundle savedInstanceState) {
        View view=inflater.inflate(R.layout.fragment_notification,container,false);
        return view;
    }
    //xử lý bên trong
    ArrayList<Notification_class> data=new ArrayList<>();
    ListView listView;
    NotificationAdapter notificationAdapter;
    SharedPreferences luutru;
    TextView t;
    @Override
    public void onViewCreated(@NonNull View view, @Nullable Bundle savedInstanceState) {
        super.onViewCreated(view, savedInstanceState);
        luutru= getContext().getSharedPreferences("savefile", Context.MODE_PRIVATE);
        listView=view.findViewById(R.id.lstNoti);

        String cusID= luutru.getString("id","123");
        String id=cusID.substring(1,cusID.length()-1);
        String url= SERVER.urlNotify+id;
       // Toast.makeText(getContext(),url,Toast.LENGTH_SHORT).show();
        RequestQueue requestQueue= Volley.newRequestQueue(getContext());
        Response.Listener<JSONArray>thanhcong=new Response.Listener<JSONArray>() {
            @Override
            public void onResponse(JSONArray response) {
                for(int i=0;i<response.length();i++)
                {
                    try {
                        JSONObject productObject=response.getJSONObject(i);
                        //Notification_class cd=new Notification_class(productObject.getLong("notiID"),productObject.getLong("sendUserID"),productObject.getString("sendUserFullName"),productObject.getLong("receiveUserID"),productObject.getString("receiveUserFullName"),productObject.getLong("objectID"),productObject.getLong("objectTypeID"),productObject.getString("title"),productObject.getString("message"),productObject.getString("image"),productObject.getLong("menutype"),productObject.getLong("isRead"));
                        Notification_class cd=new Notification_class(productObject.getLong("notiID"),productObject.getString("title"),productObject.getString("message"),productObject.getString("image"));
                        data.add(cd);
                    }
                    catch (JSONException e)
                    {
                        Toast.makeText(getContext(),"loi"+e.toString(),Toast.LENGTH_SHORT).show();
                    }

                }
                //cap nhat lai danh sach
                notificationAdapter.notifyDataSetChanged();
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
        //data.add(new Notification_class(R.drawable.hot,"Khuyến mãi","Lap top mới ra mắt giảm từ 15%"));
        //oadProduct(cusID.trim());
        notificationAdapter=new NotificationAdapter(getContext(),data);
        listView.setAdapter(notificationAdapter);

    }
    public void LoadProduct(String id)
    {
        String url= SERVER.urlNotify+id;
        RequestQueue requestQueue= Volley.newRequestQueue(getContext());
        Response.Listener<JSONArray>thanhcong=new Response.Listener<JSONArray>() {
            @Override
            public void onResponse(JSONArray response) {
                for(int i=0;i<response.length();i++)
                {
                    try {
                        JSONObject productObject=response.getJSONObject(i);
                           // Notification_class cd=new Notification_class(productObject.getLong("notiID"),productObject.getLong("sendUserID"),productObject.getString("sendUserFullName"),productObject.getLong("receiveUserID"),productObject.getString("receiveUserFullName"),productObject.getLong("objectID"),productObject.getLong("objectTypeID"),productObject.getString("title"),productObject.getString("message"),productObject.getString("image"),productObject.getLong("menutype"),productObject.getLong("isRead"));
                        Notification_class cd=new Notification_class(productObject.getLong("notiID"),productObject.getString("title"),productObject.getString("message"),productObject.getString("image"));
                        data.add(cd);
                    }
                    catch (JSONException e)
                    {
                        Toast.makeText(getContext(),"loi"+e.toString(),Toast.LENGTH_SHORT).show();
                    }

                }
                //cap nhat lai danh sach
                notificationAdapter.notifyDataSetChanged();
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
