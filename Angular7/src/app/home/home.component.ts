import { UserService } from './../shared/user.service';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Usuarios} from './../shared/Usuarios';
import { Articulos} from './../shared/Articulos';
import { NgForm } from '@angular/forms';
import { LoginComponent} from './../user/login/login.component';
import { ImageUploadModule} from 'angular2-image-upload';
import { ToastrService } from 'ngx-toastr';
@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styles: []
})
export class HomeComponent implements OnInit {
  userDetails;
  artic:Articulos[];
  constructor(private router: Router, private service: UserService) { }

  ngOnInit(form?: LoginComponent) {
    this.service.getUserProfile(form.formModel.UserName).subscribe(
      res => {
        this.userDetails = res;
        alert(res);
      },
      err => {
        console.log(err);
      },
    );
     this.service.refreshList();
  }

  populateForm(emp: Articulos) {
    this.service.formData = Object.assign({}, emp);
    this.service.refreshList();
  }
  
  resetForm(form?: NgForm) {
    if (form != null)
      form.resetForm();
    this.service.formData = {
      Nombre: '',
      PreIni: 0,
      PreOfre: 0,
      Descripcion:'',
      Imagen:null,
      Estado:'',
      IdUsuario:0
    }
  }


  onLogout() {
    localStorage.removeItem('token');
    this.router.navigate(['/user/login']);
  }

  onUploadFinish(event) {
    console.log(event);
   }

  insertRecord(form: NgForm) {
    alert(form);
    this.service.postArticulos(form.value).subscribe(res => {
      this.resetForm(form);
      this.service.refreshList();
    });
  }

  onSubmit(form: NgForm) {
    if (form.value.EmployeeID == null)
      this.insertRecord(form);
    else
      alert('Error de insercion'); 
     }
}
