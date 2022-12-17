package com.example.app;

import static SERVER.SERVER.urlLogin;

import androidx.appcompat.app.AppCompatActivity;

import android.annotation.SuppressLint;
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

public class SignUp extends AppCompatActivity {
    EditText email;
    EditText password;
    EditText confirmpassword;
    EditText name;
    Button sinup;
    TextView textlogin;
    @SuppressLint("MissingInflatedId")
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_sign_up);
        email = findViewById(R.id.edittext_email);
        password = findViewById(R.id.edittext_password);
        confirmpassword = findViewById(R.id.edittext_confirmpassword);
        name = findViewById(R.id.edittext_name);
        sinup = findViewById(R.id.button_dangky);
        textlogin=findViewById(R.id.txtlogin);
       textlogin.setOnClickListener(new View.OnClickListener() {
           @Override
           public void onClick(View view) {
               Intent intent =new Intent(getApplicationContext(), Login.class);
               startActivity(intent);
           }
       });
        sinup.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                if(email.getText().toString().equals("")||password.getText().toString().equals("")||name.getText().toString().equals("")||confirmpassword.getText().toString().equals(""))
                    Toast.makeText(SignUp.this, "Vui lòng nhập đầy đủ thông tin!", Toast.LENGTH_SHORT).show();
                if(!password.getText().toString().equals(confirmpassword.getText().toString()))
                    Toast.makeText(SignUp.this, "Mật khẩu không trùng khớp!", Toast.LENGTH_SHORT).show();
                else
                {
                    String url= SERVER.urlSignup +"fullname="+name.getText().toString()+"&phone="+email.getText().toString()+"&password="+password.getText().toString();
                    RequestQueue queue = Volley.newRequestQueue(getApplicationContext());
                    StringRequest stringRequest = new StringRequest(Request.Method.GET, url.trim(),
                            new Response.Listener<String>() {
                                @Override
                                public void onResponse(String response) {
                                        Toast.makeText(getApplicationContext(),response.toString(),Toast.LENGTH_LONG).show();
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