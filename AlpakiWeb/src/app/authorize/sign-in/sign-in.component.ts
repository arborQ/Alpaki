import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl } from '@angular/forms';
import { Validators } from '@angular/forms';
import { SignInService } from '../sign-in.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-sign-in',
  templateUrl: './sign-in.component.html',
  styleUrls: ['./sign-in.component.less']
})
export class SignInComponent implements OnInit {
  loginForm: FormGroup;
  isProcessing = false;
  constructor(private router: Router,  private signInService: SignInService) { }

  ngOnInit(): void {
    this.loginForm = new FormGroup({
      login: new FormControl('admin', Validators.required),
      password: new FormControl('admin', Validators.required),
    });
  }

  onLoginFormSubmitted(): void {
    const login = this.loginForm.get('login').value;
    const password = this.loginForm.get('password').value;
    this.isProcessing = true;
    this.loginForm.disable();

    this.signInService
      .signIn(login, password)
      .toPromise()
      .then(() => {
        this.router.navigateByUrl('/dreams/list');
      })
      .finally(() => {
        this.loginForm.enable();
        this.isProcessing = false;
        this.loginForm.markAsPristine();
      });
  }
}
