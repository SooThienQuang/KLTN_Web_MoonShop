package com.example.app;

import static SERVER.SERVER.urlLogin;

import androidx.appcompat.app.AppCompatActivity;

import android.content.Context;
import android.content.Intent;
import android.content.SharedPreferences;
import android.os.Bundle;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;
import android.widget.TextView;
import android.widget.Toast;

import com.android.volley.Request;
import com.android.volley.RequestQueue;
import com.android.volley.Response;
import com.android.volley.VolleyError;
import com.android.volley.toolbox.StringRequest;
import com.android.volley.toolbox.Volley;

import SERVER.SERVER;

public class Login extends AppCompatActivity {
    EditText email;
    EditText password;
    Button login;
    RequestQueue requestQueue;
    SharedPreferences luutru;
    TextView sigup;
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_login);
        email = findViewById(R.id.edittext_email);
        password = findViewById(R.id.edittext_password);
        login = findViewById(R.id.button_dangnhap);
        luutru=getSharedPreferences("savefile", Context.MODE_PRIVATE);
      // String url = "http://thienquang-001-site1.itempurl.com/user?phone="+email.getText().toString()+"&password="+password.getText().toString();
        sigup=findViewById(R.id.sinuptext);
        sigup.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                Intent intent =new Intent(getApplicationContext(), SignUp.class);
                startActivity(intent);
            }
        });
        login.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                String url= urlLogin+email.getText()+"&password="+password.getText();
               // Toast.makeText(getApplicationContext(),url,Toast.LENGTH_LONG).show();
                if(email.getText().toString().equals("")||password.getText().toString().equals(""))
                {
                    Toast.makeText(getApplicationContext(),"Vui lòng nhập đầy đủ thông tin",Toast.LENGTH_LONG).show();
                }
                else
                {
                    RequestQueue queue = Volley.newRequestQueue(getApplicationContext());
                    StringRequest stringRequest = new StringRequest(Request.Method.GET, url.trim(),
                            new Response.Listener<String>() {
                                @Override
                                public void onResponse(String response) {
                                    if(!response.equals("null"))
                                    {  SharedPreferences.Editor editor=luutru.edit();
                                        editor.putString("id",response.trim());
                                        editor.putString("us",email.getText().toString());
                                        editor.putString("pwd",password.getText().toString());
                                        editor.commit();
                                        Toast.makeText(getApplicationContext(),"Đăng nhập thành công" ,Toast.LENGTH_LONG).show();
                                        Intent intent=new Intent(Login.this,MainActivity.class);
                                        startActivity(intent);
                                    }
                                    else
                                    {
                                        Toast.makeText(getApplicationContext(),"Tên đăng nhập hoặc mật khẩu không chính xác",Toast.LENGTH_LONG).show();
                                    }
                                }
                            }, new Response.ErrorListener() {
                        @Override
                        public void onErrorResponse(VolleyError error) {
                            Toast.makeText(getApplicationContext(),error.toString(),Toast.LENGTH_LONG).show();
                        }
                    });

                    queue.add(stringRequest);
                }

            }
        });
    }
}