import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { FormGroup, FormControl, Validators, FormArray } from '@angular/forms';
import { ICategory, ICategoryStep } from '../categories.service';
import { TranslationWidth } from '@angular/common';

@Component({
  selector: 'app-category-form',
  templateUrl: './category-form.component.html',
  styleUrls: ['./category-form.component.less']
})
export class CategoryFormComponent implements OnInit {
  categoryForm: FormGroup;

  constructor() {

  }

  private createStepFormGroup(step?: ICategoryStep): FormGroup {
    return new FormGroup({
      stepDescription: new FormControl(step?.stepDescription || '', Validators.required),
      isSponsorRelated: new FormControl(step?.isSponsorRelated || false),
      dreamCategoryDefaultStepId: new FormControl(step?.dreamCategoryDefaultStepId || 0, Validators.required)
    }, [Validators.required, Validators.minLength(1)])
  }

  ngOnInit(): void {
    this.categoryForm = new FormGroup({
      categoryName: new FormControl(this.category?.categoryName || '', Validators.required),
      defaultSteps: new FormArray(
        (this.category?.defaultSteps || []).map(s => this.createStepFormGroup(s))
        , [Validators.required, Validators.minLength(1)]),
    });
  }

  @Input() category: ICategory;
  @Output() saveForm: EventEmitter<any> = new EventEmitter();
  get stepsFormData(): FormArray { return <FormArray>this.categoryForm.controls.defaultSteps; }

  removeStep(index: number) {
    this.stepsFormData.removeAt(index);
  }
  addStep(): void {
    this.stepsFormData.push(this.createStepFormGroup());
  }

  onSubmit() {
    console.log(this.categoryForm.getRawValue())
    this.saveForm.emit();
  }
}
