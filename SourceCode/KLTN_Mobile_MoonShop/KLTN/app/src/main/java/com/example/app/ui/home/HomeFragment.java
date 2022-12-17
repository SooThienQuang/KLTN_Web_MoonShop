package com.example.app.ui.home;

import android.os.Bundle;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.Button;
import android.widget.ImageView;
import android.widget.TextView;
import android.widget.Toast;
import android.widget.ViewFlipper;

import androidx.annotation.NonNull;
import androidx.appcompat.widget.Toolbar;
import androidx.drawerlayout.widget.DrawerLayout;
import androidx.fragment.app.Fragment;
import androidx.lifecycle.ViewModelProvider;
import androidx.recyclerview.widget.GridLayoutManager;
import androidx.recyclerview.widget.LinearLayoutManager;
import androidx.recyclerview.widget.RecyclerView;

import com.android.volley.Request;
import com.android.volley.RequestQueue;
import com.android.volley.Response;
import com.android.volley.VolleyError;
import com.android.volley.toolbox.JsonArrayRequest;
import com.android.volley.toolbox.StringRequest;
import com.android.volley.toolbox.Volley;
import com.example.app.Models.ProductAdapter;
import com.example.app.Models.ProductData;
import com.example.app.databinding.FragmentHomeBinding;
import com.squareup.picasso.Picasso;

import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

import java.util.ArrayList;

import SERVER.SERVER;

public class HomeFragment extends Fragment {
    Toolbar toolbar;
    DrawerLayout drawerLayout;
    //khaio bao mang
    //ArrayList<ProducerType> data=new ArrayList<>();
    ArrayList<ProductData> dataproduct=new ArrayList<>();
    //khai bao adapter
    //ProducerTypeAdapter chuDeAdapter;
    ProductAdapter productAdapter;
    //anh xa
    RecyclerView rvproducer,rvhot;
    RecyclerView rvproduct;
    ViewFlipper viewFlipper;
    private FragmentHomeBinding binding;

    public View onCreateView(@NonNull LayoutInflater inflater,
                             ViewGroup container, Bundle savedInstanceState) {
        HomeViewModel homeViewModel =
                new ViewModelProvider(this).get(HomeViewModel.class);

        binding = FragmentHomeBinding.inflate(inflater, container, false);
        View root = binding.getRoot();
       // final TextView textView = binding.textHome;
        rvhot=binding.rvsphot;

        rvproducer=binding.rvproducer;
        rvproduct=binding.rvproduct;
        viewFlipper=binding.viewfliperslide;
        //add du lieu
       // chuDeAdapter=new ProducerTypeAdapter(getContext(),data);
       // rvproducer.setAdapter(chuDeAdapter);
       // rvproducer.setLayoutManager(new LinearLayoutManager(getContext(),RecyclerView.HORIZONTAL,false));


        rvproduct.setHasFixedSize(true);
        productAdapter=new ProductAdapter(getContext(),dataproduct);
        rvproduct.setLayoutManager(new GridLayoutManager(getContext(),2));
        rvproduct.setAdapter(productAdapter);

//        rvhot.setLayoutManager(new LinearLayoutManager(getContext(),RecyclerView.HORIZONTAL,false));
//        rvhot.setAdapter(productAdapter);
        //LoadChuDe();
        LoadProduct();
         loadSlide();
        return root;
    }

    @Override
    public void onDestroyView() {
        super.onDestroyView();
        binding = null;
    }
    public void LoadProduct()
    {
        String url= SERVER.urlsanpham;
        RequestQueue requestQueue= Volley.newRequestQueue(getContext());
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
                        Toast.makeText(getContext(),"loi"+e.toString(),Toast.LENGTH_SHORT).show();
                    }

                }
                //cap nhat lai danh sach
                productAdapter.notifyDataSetChanged();
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
    public  void loadSlide()
    {

//    viet lay hinh tu server ve
        ArrayList<String>manquangcao=new ArrayList<>();
        manquangcao.add(SERVER.urlSlide+"6d5228ff244c89beb121fca21d553275.png");
        manquangcao.add(SERVER.urlSlide+"4327ee1f7bca83cf039c03ff9e97c50b.png");
        manquangcao.add(SERVER.urlSlide+"ad336ade20ae3b327cf9a32b93b9ec17.png");
        for(int i=0;i< manquangcao.size();i++)
        {
            ImageView hinh=new ImageView(getContext());
            Picasso.get().load(manquangcao.get(i)).into(hinh);
            hinh.setScaleType(ImageView.ScaleType.FIT_XY);
            viewFlipper.addView(hinh);
        }
        viewFlipper.setAutoStart(true);
        viewFlipper.setFlipInterval(5000);
    }


}