import { Injectable } from '@angular/core';
import { FormBuilder, Validators, FormGroup } from '@angular/forms';
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Usuarios } from './Usuarios';
import { Articulos } from './Articulos';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  formData  : Articulos;
  list : Articulos[];
  constructor(private fb: FormBuilder, private http: HttpClient) { }
  readonly BaseURI = 'http://localhost:61777/api';

  formModel = this.fb.group({
    Nombre: ['', Validators.required],
    Email: ['', Validators.email],
    Apellidos: [''],
    Contrase: ['', [Validators.required, Validators.minLength(4)]]
    // Passwords: this.fb.group({
    //   Password: ['', [Validators.required, Validators.minLength(4)]],
    //   ConfirmPassword: ['', Validators.required]
    // }, { validator: this.comparePasswords })

  });

  // comparePasswords(fb: FormGroup) {
  //   let confirmPswrdCtrl = fb.get('ConfirmPassword');
  //   //passwordMismatch
  //   //confirmPswrdCtrl.errors={passwordMismatch:true}
  //   if (confirmPswrdCtrl.errors == null || 'passwordMismatch' in confirmPswrdCtrl.errors) {
  //     if (fb.get('Password').value != confirmPswrdCtrl.value)
  //       confirmPswrdCtrl.setErrors({ passwordMismatch: true });
  //     else
  //       confirmPswrdCtrl.setErrors(null);
  //   }
  // }

  register() {
    var body = {
      Nombre: this.formModel.value.Nombre,
      Email: this.formModel.value.Email,
      Apellidos: this.formModel.value.Apellidos,
      Contrase: this.formModel.value.Contrase
    };
    return this.http.post(this.BaseURI + '/Usuarios', body);
  }

  postArticulos(formData : Articulos){
    return this.http.post(this.BaseURI+'/Articulos',formData);
     
   }

  login(formData:Usuarios) {
    return this.http.get(this.BaseURI + '/Usuarios1?Nombre='+formData.Nombre);
  }

  refreshList(){
    this.http.get(this.BaseURI+'/Articulos')
    .toPromise().then(res => this.list = res as Articulos[]);
  }
  getUserProfile(form:string) {
    return this.http.get(this.BaseURI + '/Usuarios?Nombre='+form);
  }



}
