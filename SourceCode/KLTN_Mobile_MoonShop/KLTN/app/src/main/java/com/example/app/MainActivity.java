package com.example.app;

import android.content.Context;
import android.content.Intent;
import android.content.SharedPreferences;
import android.os.Bundle;
import android.view.MenuItem;
import android.view.View;
import android.view.Menu;

import com.example.app.ui.User.User;
import com.example.app.ui.cart.Cart;
import com.example.app.ui.home.HomeFragment;
import com.example.app.ui.notification.Notification;
import com.google.android.material.bottomnavigation.BottomNavigationView;
import com.google.android.material.navigation.NavigationBarView;

import androidx.annotation.NonNull;
import androidx.appcompat.widget.SearchView;
import androidx.appcompat.widget.Toolbar;
import androidx.fragment.app.Fragment;
import androidx.fragment.app.FragmentTransaction;
import androidx.drawerlayout.widget.DrawerLayout;
import androidx.appcompat.app.AppCompatActivity;

import com.example.app.databinding.ActivityMainBinding;

public class MainActivity extends AppCompatActivity {
    private ActivityMainBinding binding;
    Fragment fragment;
    Toolbar toolbar;
    BottomNavigationView bottomNavigationView;
    SharedPreferences luutru;
    SearchView searchView;
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        luutru=getSharedPreferences("savefile", Context.MODE_PRIVATE);
        binding = ActivityMainBinding.inflate(getLayoutInflater());
        setContentView(binding.getRoot());

        DrawerLayout drawer = binding.drawerlayoutmain;
        //NavigationView navigationView = binding.navView;

        searchView=findViewById(R.id.searchview);
        searchView.setOnQueryTextListener(new SearchView.OnQueryTextListener() {
            @Override
            public boolean onQueryTextSubmit(String s) {
                Intent intent=new Intent(MainActivity.this,Search.class);
                intent.putExtra("txtsearch",s);
                startActivity(intent);
                return false;
            }

            @Override
            public boolean onQueryTextChange(String s) {
                return false;
            }
        });

        loadFragment(new HomeFragment());
        //hien thi thanh menu nam duoi
        bottomNavigationView=findViewById(R.id.bottomnavigation1);
        bottomNavigationView.setOnItemSelectedListener(new NavigationBarView.OnItemSelectedListener() {
            @Override
            public boolean onNavigationItemSelected(@NonNull MenuItem item) {
                int id=item.getItemId();
                switch (id)
                {
                    case R.id.home12:
                        fragment=new HomeFragment();
                        break;
                    case R.id.notification:
                        fragment=new Notification();
                        break;
                    case R.id.user:
                        fragment=new User();

                        break;
                    case R.id.cartb:
                        fragment=new Cart();
                        break;
                }
                loadFragment(fragment);
                return true;
            }
        });
    }
    public void loadFragment(Fragment f)
    {
        FragmentTransaction transaction= getSupportFragmentManager().beginTransaction();
        transaction.replace(R.id.framelayout_main,f);
        transaction.commit();
    }
    @Override
    public boolean onCreateOptionsMenu(Menu menu) {
        // Inflate the menu; this adds items to the action bar if it is present.
        getMenuInflater().inflate(R.menu.main, menu);
        return true;
    }
}