import { Component, OnInit, EventEmitter } from '@angular/core';
import { of } from 'rxjs';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.less']
})
export class RegisterComponent implements OnInit {
  registerForm: FormGroup;
  isUrlCode = false;
  constructor(private activeRoute: ActivatedRoute) { }
  options = of(['Główny Wrocław', 'Druga Warszawa', 'Gdański przemieście']);

  ngOnInit(): void {
    this.registerForm = new FormGroup({
      firstName: new FormControl('', Validators.required),
      lastName: new FormControl('', Validators.required),
      brand: new FormControl('', Validators.required),
      phoneNumber: new FormControl('', [Validators.required]),
      email: new FormControl('', [Validators.required, Validators.email]),
      password: new FormControl('', [Validators.required]),
      code: new FormControl('', [Validators.required]),
    });

    this.activeRoute.queryParams.subscribe(qyeryParams => {
      const { code } = qyeryParams;
      if (code) {
        this.registerForm.controls.code.setValue(code);
        this.registerForm.controls.code.disable();
      } else {
        this.registerForm.controls.code.enable();
      }
    });
  }

  onFormSubmitted(): void {
  }
}
