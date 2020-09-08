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

  constructor() {

  }
  get stepsFormData(): FormArray { return this.categoryForm.controls.defaultSteps as FormArray; }
  categoryForm: FormGroup;

  @Input() category: ICategory;
  @Output() saveForm: EventEmitter<any> = new EventEmitter();

  private createStepFormGroup(step?: ICategoryStep): FormGroup {
    return new FormGroup({
      stepDescription: new FormControl(step?.stepDescription || '', Validators.required),
      isSponsorRelated: new FormControl(step?.isSponsorRelated || false),
      dreamCategoryDefaultStepId: new FormControl(step?.dreamCategoryDefaultStepId || 0, Validators.required)
    }, [Validators.required, Validators.minLength(1)]);
  }

  ngOnInit(): void {
    this.categoryForm = new FormGroup({
      categoryId: new FormControl(this.category?.dreamCategoryId || 0),
      categoryName: new FormControl(this.category?.categoryName || '', Validators.required),
      defaultSteps: new FormArray(
        (this.category?.defaultSteps || []).map(s => this.createStepFormGroup(s))
        , [Validators.required, Validators.minLength(1)]),
    });
  }

  removeStep(index: number) {
    this.stepsFormData.removeAt(index);
  }
  addStep(): void {
    this.stepsFormData.push(this.createStepFormGroup());
  }

  onSubmit() {
    var category = this.categoryForm.getRawValue();
    this.saveForm.emit({ category });
  }
}
